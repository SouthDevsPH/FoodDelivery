using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.DriverWallets.Commands;

public record DeleteDriverWalletCommand(int WalletId) : IRequest<bool>;

public class DeleteDriverWalletHandler(FoodDeliveryDbContext db) : IRequestHandler<DeleteDriverWalletCommand, bool>
{
	public async Task<bool> Handle(DeleteDriverWalletCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.DriverWallets.FindAsync([request.WalletId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.DriverWallets.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
