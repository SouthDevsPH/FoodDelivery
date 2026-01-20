using FoodDelivery.Domain.Data;
using MediatR;

namespace FoodDelivery.API.Features.Payments.Commands;

public record DeletePaymentCommand(int PaymentId) : IRequest<bool>;

public class DeletePaymentHandler(FoodDeliveryDbContext db) : IRequestHandler<DeletePaymentCommand, bool>
{
	public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.Payments.FindAsync([request.PaymentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		db.Payments.Remove(entity);
		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}
