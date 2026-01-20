using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.OrderItems.Commands;

public record DeleteOrderItemCommand(int OrderItemId) : IRequest<bool>;

public class DeleteOrderItemHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteOrderItemCommand, bool>
{
	public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.OrderItems.FindAsync([request.OrderItemId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.OrderItems.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
