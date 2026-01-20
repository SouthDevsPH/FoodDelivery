using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Products.Commands;

public record UpdateProductCommand(int ProductId, int MerchantId, string Name, string? Description, decimal Price, int? StockQuantity) : IRequest<bool>;

public class UpdateProductHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateProductCommand, bool>
{
	public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Products.FindAsync([request.ProductId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		entity.MerchantId = request.MerchantId;
		entity.Name = request.Name;
		entity.Description = request.Description;
		entity.Price = request.Price;
		entity.StockQuantity = request.StockQuantity;
		entity.UpdatedAt = DateTime.Now;

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
