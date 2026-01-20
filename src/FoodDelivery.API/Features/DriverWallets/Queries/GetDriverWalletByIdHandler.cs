using FoodDelivery.API.Features.DriverWallets.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverWallets.Queries;

public record GetDriverWalletByIdQuery(int WalletId) : IRequest<DriverWalletDto?>;

public class GetDriverWalletByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverWalletByIdQuery, DriverWalletDto?>
{
	public async Task<DriverWalletDto?> Handle(GetDriverWalletByIdQuery request, CancellationToken cancellationToken)
	{
		var dw = await db.DriverWallets.AsNoTracking().FirstOrDefaultAsync(dw => dw.WalletId == request.WalletId, cancellationToken);

		if (dw is null)
		{
			return null;
		}

		return new DriverWalletDto(dw.WalletId, dw.DriverId, dw.Balance, dw.LastUpdated);
	}
}
