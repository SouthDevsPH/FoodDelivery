using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Orders.Commands;

public record UpdateOrderCommand(int OrderId, string? Status, decimal? TotalAmount, string? Address, DateTime? DeliveryTime) : IRequest<bool>;

public class UpdateOrderHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateOrderCommand, bool>
{
	public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Orders.FindAsync([request.OrderId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.Status is not null)
		{
			entity.Status = request.Status;
		}

		if (request.TotalAmount.HasValue)
		{
			entity.TotalAmount = request.TotalAmount.Value;
		}

		if (request.Address is not null)
		{
			entity.Address = request.Address;
		}

		if (request.DeliveryTime.HasValue)
		{
			entity.DeliveryTime = request.DeliveryTime.Value;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
