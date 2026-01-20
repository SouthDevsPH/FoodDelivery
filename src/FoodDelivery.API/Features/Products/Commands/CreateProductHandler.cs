using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Products.Commands;

public record CreateProductCommand(int MerchantId, string Name, string? Description, decimal Price, int? StockQuantity) : IRequest<int>;

public class CreateProductHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateProductCommand, int>
{
	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		var entity = new Product
		{
			MerchantId = request.MerchantId,
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			StockQuantity = request.StockQuantity,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};

		db.Products.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.ProductId;
	}
}
