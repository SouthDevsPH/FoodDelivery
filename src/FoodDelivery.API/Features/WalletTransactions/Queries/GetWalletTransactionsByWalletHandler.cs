using FoodDelivery.API.Features.WalletTransactions.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.WalletTransactions.Queries;

public record GetWalletTransactionsByWalletQuery(int WalletId) : IRequest<List<WalletTransactionDto>>;

public class GetWalletTransactionsByWalletHandler(FoodDeliveryDbContext db) : IRequestHandler<GetWalletTransactionsByWalletQuery, List<WalletTransactionDto>>
{
	public async Task<List<WalletTransactionDto>> Handle(GetWalletTransactionsByWalletQuery request, CancellationToken cancellationToken)
	{
		return await db.WalletTransactions.AsNoTracking()
			.Where(wt => wt.WalletId == request.WalletId)
			.Select(wt => new WalletTransactionDto(wt.TransactionId, wt.WalletId, wt.Amount, wt.TransactionType, wt.Description, wt.TransactionDate))
			.ToListAsync(cancellationToken);
	}
}
