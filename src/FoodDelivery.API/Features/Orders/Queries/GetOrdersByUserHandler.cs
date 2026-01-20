using FoodDelivery.API.Features.Orders.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Orders.Queries;

public record GetOrdersByUserQuery(int UserId) : IRequest<List<OrderDto>>;

public class GetOrdersByUserHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrdersByUserQuery, List<OrderDto>>
{
	public async Task<List<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
	{
		return await db.Orders.AsNoTracking()
			.Where(o => o.UserId == request.UserId)
			.Select(o => new OrderDto(o.OrderId, o.UserId, o.MerchantId, o.OrderDate, (OrderStatusEnum)o.OrderStatusId, o.TotalAmount, o.Address, o.DeliveryTime))
			.ToListAsync(cancellationToken);
	}
}

