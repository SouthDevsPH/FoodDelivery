using FoodDelivery.API.Features.Products.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Products.Queries;

public record GetProductsByMerchantQuery(int MerchantId) : IRequest<List<ProductDto>>;

public class GetProductsByMerchantHandler(FoodDeliveryDbContext db) : IRequestHandler<GetProductsByMerchantQuery, List<ProductDto>>
{
	public async Task<List<ProductDto>> Handle(GetProductsByMerchantQuery request, CancellationToken cancellationToken)
	{
		return await db.Products.AsNoTracking()
			.Where(p => p.MerchantId == request.MerchantId && p.IsActive)
			.Select(p => new ProductDto(p.ProductId, p.MerchantId, p.Name, p.Description, p.Price, p.StockQuantity))
			.ToListAsync(cancellationToken);
	}
}
