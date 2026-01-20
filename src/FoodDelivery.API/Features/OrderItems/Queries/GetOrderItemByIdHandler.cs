using FoodDelivery.API.Features.OrderItems.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.OrderItems.Queries;

public record GetOrderItemByIdQuery(int OrderItemId) : IRequest<OrderItemDto?>;

public class GetOrderItemByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto?>
{
	public async Task<OrderItemDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
	{
		var oi = await db.OrderItems.AsNoTracking().FirstOrDefaultAsync(oi => oi.OrderItemId == request.OrderItemId, cancellationToken);

		if (oi is null)
		{
			return null;
		}

		return new OrderItemDto(oi.OrderItemId, oi.OrderId, oi.ProductId, oi.Quantity, oi.Price);
	}
}
