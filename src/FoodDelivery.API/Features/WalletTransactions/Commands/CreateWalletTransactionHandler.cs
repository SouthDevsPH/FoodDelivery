using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.WalletTransactions.Commands;

public record CreateWalletTransactionCommand(int WalletId, decimal Amount, string TransactionType, string? Description) : IRequest<int>;

public class CreateWalletTransactionHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateWalletTransactionCommand, int>
{
	public async Task<int> Handle(CreateWalletTransactionCommand request, CancellationToken cancellationToken)
	{
		var entity = new WalletTransaction
		{
			WalletId = request.WalletId,
			Amount = request.Amount,
			TransactionType = request.TransactionType,
			Description = request.Description,
			TransactionDate = DateTime.Now
		};

		db.WalletTransactions.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.TransactionId;
	}
}
