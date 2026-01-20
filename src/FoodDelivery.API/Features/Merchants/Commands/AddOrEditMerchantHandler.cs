using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Merchants.Commands;

public record AddOrEditMerchantCommand(int? MerchantId, int UserId, string Name, string? Address, string? PhoneNumber, string? Email) : IRequest<int>;

public class AddOrEditMerchantHandler(FoodDeliveryDbContext db) : IRequestHandler<AddOrEditMerchantCommand, int>
{
    public async Task<int> Handle(AddOrEditMerchantCommand request, CancellationToken cancellationToken)
    {
        Merchant? entity;

        if (request.MerchantId.HasValue && request.MerchantId.Value > 0)
        {
            entity = await db.Merchants.FindAsync([request.MerchantId.Value], cancellationToken);

            if (entity is null)
            {
                return 0;
            }

            entity.UserId = request.UserId;
            entity.Name = request.Name;
            entity.Address = request.Address;
            entity.PhoneNumber = request.PhoneNumber;
            entity.Email = request.Email;
            entity.UpdatedAt = DateTime.Now;
        }
        else
        {
            entity = new()
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
        }

        await db.SaveChangesAsync(cancellationToken);

        return entity.MerchantId;
    }
}
