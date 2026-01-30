using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Users.Commands;

public record UpdateUserCommand(int UserId, string Username, string? Password, string? Email, string Role) : IRequest<bool>;

public class UpdateUserHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateUserCommand, bool>
{
	public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Users.FindAsync([request.UserId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		entity.Username = request.Username;
		entity.Email = request.Email;
		entity.Role = request.Role;

		// Hash the password if provided
		if (!string.IsNullOrWhiteSpace(request.Password))
		{
			entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 11);
		}

		entity.UpdatedAt = DateTime.Now;

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
