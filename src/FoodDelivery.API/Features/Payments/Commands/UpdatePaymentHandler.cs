using FoodDelivery.Domain.Data;
using FoodDelivery.API.Enums;
using MediatR;

namespace FoodDelivery.API.Features.Payments.Commands;

public record UpdatePaymentCommand(int PaymentId, PaymentStatusEnum? PaymentStatus) : IRequest<bool>;

public class UpdatePaymentHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdatePaymentCommand, bool>
{
	public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Payments.FindAsync([request.PaymentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.PaymentStatus.HasValue)
		{
			entity.PaymentStatusId = (int)request.PaymentStatus.Value;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}

