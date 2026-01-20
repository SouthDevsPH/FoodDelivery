using FoodDelivery.API.Features.Payments.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Payments.Queries;

public record GetPaymentsQuery : IRequest<List<PaymentDto>>;

public class GetPaymentsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetPaymentsQuery, List<PaymentDto>>
{
	public async Task<List<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
	{
		return await db.Payments.AsNoTracking()
			.Select(p => new PaymentDto(p.PaymentId, p.OrderId, p.PaymentMethod, p.PaymentStatus, p.Amount, p.PaymentDate))
			.ToListAsync(cancellationToken);
	}
}
