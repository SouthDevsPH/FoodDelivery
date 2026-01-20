using FoodDelivery.API.Features.WalletTransactions.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.WalletTransactions.Queries;

public record GetWalletTransactionsQuery : IRequest<List<WalletTransactionDto>>;

public class GetWalletTransactionsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetWalletTransactionsQuery, List<WalletTransactionDto>>
{
	public async Task<List<WalletTransactionDto>> Handle(GetWalletTransactionsQuery request, CancellationToken cancellationToken)
	{
		return await db.WalletTransactions.AsNoTracking()
			.Select(wt => new WalletTransactionDto(wt.TransactionId, wt.WalletId, wt.Amount, wt.TransactionType, wt.Description, wt.TransactionDate))
			.ToListAsync(cancellationToken);
	}
}
