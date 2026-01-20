using FoodDelivery.API.Features.WalletTransactions.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.WalletTransactions.Queries;

public record GetWalletTransactionByIdQuery(int TransactionId) : IRequest<WalletTransactionDto?>;

public class GetWalletTransactionByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetWalletTransactionByIdQuery, WalletTransactionDto?>
{
	public async Task<WalletTransactionDto?> Handle(GetWalletTransactionByIdQuery request, CancellationToken cancellationToken)
	{
		var wt = await db.WalletTransactions.AsNoTracking().FirstOrDefaultAsync(wt => wt.TransactionId == request.TransactionId, cancellationToken);

		if (wt is null)
		{
			return null;
		}

		return new WalletTransactionDto(wt.TransactionId, wt.WalletId, wt.Amount, wt.TransactionType, wt.Description, wt.TransactionDate);
	}
}
