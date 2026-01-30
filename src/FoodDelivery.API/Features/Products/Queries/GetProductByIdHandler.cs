using FoodDelivery.API.Features.Products.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Products.Queries;

public record GetProductByIdQuery(int ProductId) : IRequest<ProductDto?>;

public class GetProductByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
	public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
	{
		var p = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == request.ProductId && p.IsActive, cancellationToken);

		if (p is null)
		{
			return null;
		}

		return new ProductDto(p.ProductId, p.MerchantId, p.Name, p.Description, p.Price, p.StockQuantity);
	}
}
