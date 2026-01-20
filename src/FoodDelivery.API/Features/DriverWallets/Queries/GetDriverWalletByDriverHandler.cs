using FoodDelivery.API.Features.DriverWallets.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverWallets.Queries;

public record GetDriverWalletByDriverQuery(int DriverId) : IRequest<DriverWalletDto?>;

public class GetDriverWalletByDriverHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverWalletByDriverQuery, DriverWalletDto?>
{
	public async Task<DriverWalletDto?> Handle(GetDriverWalletByDriverQuery request, CancellationToken cancellationToken)
	{
		var dw = await db.DriverWallets.AsNoTracking().FirstOrDefaultAsync(dw => dw.DriverId == request.DriverId, cancellationToken);

		if (dw is null)
		{
			return null;
		}

		return new DriverWalletDto(dw.WalletId, dw.DriverId, dw.Balance, dw.LastUpdated);
	}
}
