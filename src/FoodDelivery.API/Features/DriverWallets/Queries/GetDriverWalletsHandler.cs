using FoodDelivery.API.Features.DriverWallets.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.DriverWallets.Queries;

public record GetDriverWalletsQuery : IRequest<List<DriverWalletDto>>;

public class GetDriverWalletsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetDriverWalletsQuery, List<DriverWalletDto>>
{
	public async Task<List<DriverWalletDto>> Handle(GetDriverWalletsQuery request, CancellationToken cancellationToken)
	{
		return await db.DriverWallets.AsNoTracking()
			.Select(dw => new DriverWalletDto(dw.WalletId, dw.DriverId, dw.Balance, dw.LastUpdated))
			.ToListAsync(cancellationToken);
	}
}
