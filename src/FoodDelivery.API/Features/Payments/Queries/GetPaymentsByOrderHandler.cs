using FoodDelivery.API.Features.Payments.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Payments.Queries;

public record GetPaymentsByOrderQuery(int OrderId) : IRequest<List<PaymentDto>>;

public class GetPaymentsByOrderHandler(FoodDeliveryDbContext db) : IRequestHandler<GetPaymentsByOrderQuery, List<PaymentDto>>
{
	public async Task<List<PaymentDto>> Handle(GetPaymentsByOrderQuery request, CancellationToken cancellationToken)
	{
		return await db.Payments.AsNoTracking()
			.Where(p => p.OrderId == request.OrderId)
			.Select(p => new PaymentDto(p.PaymentId, p.OrderId, p.PaymentMethod, p.PaymentStatus, p.Amount, p.PaymentDate))
			.ToListAsync(cancellationToken);
	}
}
