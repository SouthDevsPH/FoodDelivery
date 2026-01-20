using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Orders.Commands;

public record DeleteOrderCommand(int OrderId) : IRequest<bool>;

public class DeleteOrderHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteOrderCommand, bool>
{
	public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Orders.FindAsync([request.OrderId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.Orders.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
