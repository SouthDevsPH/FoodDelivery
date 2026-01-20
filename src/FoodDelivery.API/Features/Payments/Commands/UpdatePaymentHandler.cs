using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Payments.Commands;

public record UpdatePaymentCommand(int PaymentId, string? PaymentStatus) : IRequest<bool>;

public class UpdatePaymentHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdatePaymentCommand, bool>
{
	public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Payments.FindAsync([request.PaymentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.PaymentStatus is not null)
		{
			entity.PaymentStatus = request.PaymentStatus;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
