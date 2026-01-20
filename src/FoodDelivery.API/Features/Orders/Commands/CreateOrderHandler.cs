using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using MediatR;

namespace FoodDelivery.API.Features.Orders.Commands;

public record CreateOrderCommand(int UserId, int MerchantId, decimal TotalAmount, string Address) : IRequest<int>;

public class CreateOrderHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateOrderCommand, int>
{
	public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = new Order
		{
			UserId = request.UserId,
			MerchantId = request.MerchantId,
			OrderDate = DateTime.Now,
			OrderStatusId = (int)OrderStatusEnum.Pending,
			TotalAmount = request.TotalAmount,
			Address = request.Address
		};

		db.Orders.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.OrderId;
	}
}

