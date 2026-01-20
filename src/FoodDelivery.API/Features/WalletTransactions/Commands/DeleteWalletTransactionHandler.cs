using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.WalletTransactions.Commands;

public record DeleteWalletTransactionCommand(int TransactionId) : IRequest<bool>;

public class DeleteWalletTransactionHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteWalletTransactionCommand, bool>
{
	public async Task<bool> Handle(DeleteWalletTransactionCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.WalletTransactions.FindAsync([request.TransactionId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.WalletTransactions.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
