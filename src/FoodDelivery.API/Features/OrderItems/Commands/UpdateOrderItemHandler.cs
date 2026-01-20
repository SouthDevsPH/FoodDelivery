using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.OrderItems.Commands;

public record UpdateOrderItemCommand(int OrderItemId, int Quantity, decimal Price) : IRequest<bool>;

public class UpdateOrderItemHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateOrderItemCommand, bool>
{
	public async Task<bool> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.OrderItems.FindAsync([request.OrderItemId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		entity.Quantity = request.Quantity;
		entity.Price = request.Price;

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
