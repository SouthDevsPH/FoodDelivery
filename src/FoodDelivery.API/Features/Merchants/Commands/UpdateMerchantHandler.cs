using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Merchants.Commands;

public record UpdateMerchantCommand(int MerchantId, int UserId, string Name, string? Address, string? PhoneNumber, string? Email) : IRequest<bool>;

public class UpdateMerchantHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateMerchantCommand, bool>
{
	public async Task<bool> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Merchants.FindAsync([request.MerchantId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		entity.UserId = request.UserId;
		entity.Name = request.Name;
		entity.Address = request.Address;
		entity.PhoneNumber = request.PhoneNumber;
		entity.Email = request.Email;
		entity.UpdatedAt = DateTime.Now;

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
