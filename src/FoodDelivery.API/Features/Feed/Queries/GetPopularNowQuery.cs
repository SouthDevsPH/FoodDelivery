using FoodDelivery.API.Features.Feed.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Feed.Queries;

public record GetPopularNowQuery(int UserId, int? AddressId = null) : IRequest<List<PopularNowDto>>;

public class GetPopularNowHandler(FoodDeliveryDbContext db) : IRequestHandler<GetPopularNowQuery, List<PopularNowDto>>
{
    private const double PopularNowRadiusKm = 5.0;

    public async Task<List<PopularNowDto>> Handle(GetPopularNowQuery request, CancellationToken cancellationToken)
    {
        var userAddress = await db.UserAddresses
            .Include(ua => ua.Address)
            .Where(ua => ua.UserId == request.UserId && (request.AddressId == null || ua.AddressId == request.AddressId))
            .Select(ua => ua.Address)
            .FirstOrDefaultAsync(cancellationToken);

        if (userAddress is null)
        {
            return [];
        }

        var userLat = (double)userAddress.Latitude;
        var userLng = (double)userAddress.Longitude;

        var merchantList = await db.MerchantAddresses
            .Include(ma => ma.Address)
            .Where(ma => ma.Address.IsActive && ma.Merchant.IsActive)
            .Select(ma => new
            {
                ma.Merchant,
                ma.Address,
                Distance = 6371 * 2 *
                    Math.Asin(
                        Math.Sqrt(
                            Math.Pow(Math.Sin((Math.PI / 180) * (userLat - (double)ma.Address.Latitude) / 2), 2) +
                            Math.Cos((Math.PI / 180) * userLat) * Math.Cos((Math.PI / 180) * (double)ma.Address.Latitude) *
                            Math.Pow(Math.Sin((Math.PI / 180) * (userLng - (double)ma.Address.Longitude) / 2), 2)
                        )
                    )
            })
            .Where(x => x.Distance <= PopularNowRadiusKm)
            .ToListAsync(cancellationToken);

        var merchantIds = merchantList.Select(x => x.Merchant.MerchantId).Distinct().ToList();

        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);
        var orderCounts = await db.Orders
            .Where(o => merchantIds.Contains(o.MerchantId) &&
                (o.OrderDate >= yesterday && o.OrderDate < today.AddDays(1)))
            .GroupBy(o => o.MerchantId)
            .Select(g => new { MerchantId = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var orderCountDict = orderCounts.ToDictionary(x => x.MerchantId, x => x.Count);

        var result = merchantList
            .GroupBy(x => x.Merchant.MerchantId)
            .Select(g =>
            {
                var first = g.First();
                return new PopularNowDto(
                    first.Merchant.MerchantId,
                    first.Merchant.Name,
                    first.Merchant.Email,
                    first.Merchant.PhoneNumber,
                    first.Address.Title,
                    (double)first.Address.Latitude,
                    (double)first.Address.Longitude,
                    orderCountDict.TryGetValue(first.Merchant.MerchantId, out var cnt) ? cnt : 0
                );
            })
            .ToList();

        var ordered = result
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.MerchantId)
            .Take(10)
            .ToList();

        if (ordered.All(x => x.OrderCount == 0))
        {
            ordered = [.. result
                .OrderBy(x => x.MerchantId)
                .Take(10)];
        }

        return ordered;
    }
}
