using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Users.Commands;

public record CreateUserCommand(string Username, string Password, string? Email, string Role) : IRequest<int>;

public class CreateUserHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateUserCommand, int>
{
	public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		// Hash the password using BCrypt
		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 11);

		var entity = new User
		{
			Username = request.Username,
			PasswordHash = passwordHash,
			Email = request.Email,
			Role = request.Role,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};

		db.Users.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.UserId;
	}
}

