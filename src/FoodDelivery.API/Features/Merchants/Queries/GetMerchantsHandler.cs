using FoodDelivery.API.Features.Merchants.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Merchants.Queries;

public record GetMerchantsQuery : IRequest<List<MerchantDto>>;

public class GetMerchantsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetMerchantsQuery, List<MerchantDto>>
{
    public async Task<List<MerchantDto>> Handle(GetMerchantsQuery request, CancellationToken cancellationToken)
    {
        return await db.Merchants.AsNoTracking()
            .Select(m => new MerchantDto(m.MerchantId, m.UserId, m.Name, m.Address, m.PhoneNumber, m.Email))
            .ToListAsync(cancellationToken);
    }
}

