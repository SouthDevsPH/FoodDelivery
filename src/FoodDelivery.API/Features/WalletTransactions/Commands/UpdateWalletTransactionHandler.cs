using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.WalletTransactions.Commands;

public record UpdateWalletTransactionCommand(int TransactionId, string? Description) : IRequest<bool>;

public class UpdateWalletTransactionHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateWalletTransactionCommand, bool>
{
	public async Task<bool> Handle(UpdateWalletTransactionCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.WalletTransactions.FindAsync([request.TransactionId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.Description is not null)
		{
			entity.Description = request.Description;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
