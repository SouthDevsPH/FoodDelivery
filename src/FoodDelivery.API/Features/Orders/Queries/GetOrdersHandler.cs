using FoodDelivery.API.Features.Orders.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Orders.Queries;

public record GetOrdersQuery : IRequest<List<OrderDto>>;

public class GetOrdersHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
	public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		return await db.Orders.AsNoTracking()
			.Select(o => new OrderDto(o.OrderId, o.UserId, o.MerchantId, o.OrderDate, (OrderStatusEnum)o.OrderStatusId, o.TotalAmount, o.Address, o.DeliveryTime))
			.ToListAsync(cancellationToken);
	}
}

