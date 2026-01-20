using FoodDelivery.API.Features.Payments.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Payments.Queries;

public record GetPaymentsQuery : IRequest<List<PaymentDto>>;

public class GetPaymentsHandler(FoodDeliveryDbContext db) : IRequestHandler<GetPaymentsQuery, List<PaymentDto>>
{
	public async Task<List<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
	{
		return await db.Payments.AsNoTracking()
			.Select(p => new PaymentDto(p.PaymentId, p.OrderId, (PaymentMethodEnum)p.PaymentMethodId, (PaymentStatusEnum)p.PaymentStatusId, p.Amount, p.PaymentDate))
			.ToListAsync(cancellationToken);
	}

}

