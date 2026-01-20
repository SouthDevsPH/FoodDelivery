using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.DriverWallets.Commands;

public record CreateDriverWalletCommand(int DriverId, decimal? Balance) : IRequest<int>;

public class CreateDriverWalletHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateDriverWalletCommand, int>
{
	public async Task<int> Handle(CreateDriverWalletCommand request, CancellationToken cancellationToken)
	{
		var entity = new DriverWallet
		{
			DriverId = request.DriverId,
			Balance = request.Balance ?? 0,
			LastUpdated = DateTime.Now
		};

		db.DriverWallets.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.WalletId;
	}
}
