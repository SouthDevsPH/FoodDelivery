using FoodDelivery.API.Features.OrderItems.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.OrderItems.Queries;

public record GetOrderItemsQuery : IRequest<List<OrderItemDto>>;

public class GetOrderItemsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrderItemsQuery, List<OrderItemDto>>
{
	public async Task<List<OrderItemDto>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
	{
		return await db.OrderItems.AsNoTracking()
			.Select(oi => new OrderItemDto(oi.OrderItemId, oi.OrderId, oi.ProductId, oi.Quantity, oi.Price))
			.ToListAsync(cancellationToken);
	}
}
