using FoodDelivery.API.Features.Products.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;

public class GetProductsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
	public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		return await db.Products.AsNoTracking()
			.Where(p => p.IsActive)
			.Select(p => new ProductDto(p.ProductId, p.MerchantId, p.Name, p.Description, p.Price, p.StockQuantity))
			.ToListAsync(cancellationToken);
	}
}
