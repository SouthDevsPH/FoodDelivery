using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Users.Commands;

public record CreateUserCommand(string Username, string PasswordHash, string? Email, string Role) : IRequest<int>;

public class CreateUserHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateUserCommand, int>
{
	public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var entity = new User
		{
			Username = request.Username,
			PasswordHash = request.PasswordHash,
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
