using FoodDelivery.API.Features.Orders.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Orders.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto?>;

public class GetOrderByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
	public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		var o = await db.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

		if (o is null)
		{
			return null;
		}

		return new OrderDto(o.OrderId, o.UserId, o.MerchantId, o.OrderDate, (OrderStatusEnum)o.OrderStatusId, o.TotalAmount, o.Address, o.DeliveryTime);
	}
}

