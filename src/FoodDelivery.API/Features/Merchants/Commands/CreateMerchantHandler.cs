using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Merchants.Commands;

public record CreateMerchantCommand(int UserId, string Name, string? Address, string? PhoneNumber, string? Email) : IRequest<int>;

public class CreateMerchantHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateMerchantCommand, int>
{
	public async Task<int> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
	{
		var entity = new Merchant
		{
			UserId = request.UserId,
			Name = request.Name,
			Address = request.Address,
			PhoneNumber = request.PhoneNumber,
			Email = request.Email,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};

		db.Merchants.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.MerchantId;
	}
}
