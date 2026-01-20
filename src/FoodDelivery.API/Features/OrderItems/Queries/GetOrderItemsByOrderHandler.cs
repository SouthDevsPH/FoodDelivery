using FoodDelivery.API.Features.OrderItems.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.OrderItems.Queries;

public record GetOrderItemsByOrderQuery(int OrderId) : IRequest<List<OrderItemDto>>;

public class GetOrderItemsByOrderHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrderItemsByOrderQuery, List<OrderItemDto>>
{
	public async Task<List<OrderItemDto>> Handle(GetOrderItemsByOrderQuery request, CancellationToken cancellationToken)
	{
		return await db.OrderItems.AsNoTracking()
			.Where(oi => oi.OrderId == request.OrderId)
			.Select(oi => new OrderItemDto(oi.OrderItemId, oi.OrderId, oi.ProductId, oi.Quantity, oi.Price))
			.ToListAsync(cancellationToken);
	}
}
