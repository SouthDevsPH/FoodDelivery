using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Merchants.Commands;

public record DeleteMerchantCommand(int MerchantId) : IRequest<bool>;

public class DeleteMerchantHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteMerchantCommand, bool>
{
	public async Task<bool> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Merchants.FindAsync([request.MerchantId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.Merchants.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
