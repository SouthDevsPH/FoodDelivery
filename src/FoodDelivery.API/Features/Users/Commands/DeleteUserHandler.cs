using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Users.Commands;

public record DeleteUserCommand(int UserId) : IRequest<bool>;

public class DeleteUserHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteUserCommand, bool>
{
	public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Users.FindAsync([request.UserId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.Users.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
