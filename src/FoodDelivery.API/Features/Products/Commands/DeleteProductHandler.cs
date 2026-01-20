using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Products.Commands;

public record DeleteProductCommand(int ProductId) : IRequest<bool>;

public class DeleteProductHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteProductCommand, bool>
{
	public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Products.FindAsync([request.ProductId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.Products.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
