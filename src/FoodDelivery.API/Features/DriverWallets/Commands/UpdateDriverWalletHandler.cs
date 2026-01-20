using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.DriverWallets.Commands;

public record UpdateDriverWalletCommand(int WalletId, decimal Balance) : IRequest<bool>;

public class UpdateDriverWalletHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateDriverWalletCommand, bool>
{
	public async Task<bool> Handle(UpdateDriverWalletCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.DriverWallets.FindAsync([request.WalletId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		entity.Balance = request.Balance;
		entity.LastUpdated = DateTime.Now;

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
