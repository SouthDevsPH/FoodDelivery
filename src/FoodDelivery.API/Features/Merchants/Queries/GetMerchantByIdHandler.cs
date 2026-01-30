using FoodDelivery.API.Features.Merchants.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Merchants.Queries;

public record GetMerchantByIdQuery(int MerchantId) : IRequest<MerchantDto?>;

public class GetMerchantByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetMerchantByIdQuery, MerchantDto?>
{
    public async Task<MerchantDto?> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
    {
        var m = await db.Merchants.AsNoTracking().FirstOrDefaultAsync(m => m.MerchantId == request.MerchantId && m.IsActive, cancellationToken);

        if (m is null)
        {
            return null;
        }

        return new MerchantDto(m.MerchantId, m.UserId, m.Name, m.Address, m.PhoneNumber, m.Email);
    }
}
