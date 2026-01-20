using FoodDelivery.API.Features.Payments.DTOs;
using FoodDelivery.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Payments.Queries;

public record GetPaymentByIdQuery(int PaymentId) : IRequest<PaymentDto?>;

public class GetPaymentByIdHandler(FoodDeliveryDbContext db) : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
{
	public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
	{
		var p = await db.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, cancellationToken);

		if (p is null)
		{
			return null;
		}

		return new PaymentDto(p.PaymentId, p.OrderId, p.PaymentMethod, p.PaymentStatus, p.Amount, p.PaymentDate);
	}
}
