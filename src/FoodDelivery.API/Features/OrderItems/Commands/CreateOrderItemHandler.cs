using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.OrderItems.Commands;

public record CreateOrderItemCommand(int OrderId, int ProductId, int Quantity, decimal Price) : IRequest<int>;

public class CreateOrderItemHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateOrderItemCommand, int>
{
	public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
	{
		var entity = new OrderItem
		{
			OrderId = request.OrderId,
			ProductId = request.ProductId,
			Quantity = request.Quantity,
			Price = request.Price
		};

		db.OrderItems.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.OrderItemId;
	}
}
