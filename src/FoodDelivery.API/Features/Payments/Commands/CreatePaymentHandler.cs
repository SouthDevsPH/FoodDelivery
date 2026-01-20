using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.API.Features.Payments.Commands;

public record CreatePaymentCommand(int OrderId, string PaymentMethod, decimal Amount) : IRequest<int>;

public class CreatePaymentHandler(FoodDeliveryDbContext db) : IRequestHandler<CreatePaymentCommand, int>
{
	public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
	{
		var entity = new Payment
		{
			OrderId = request.OrderId,
			PaymentMethod = request.PaymentMethod,
			PaymentStatus = "Pending",
			Amount = request.Amount,
			PaymentDate = DateTime.Now
		};

		db.Payments.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.PaymentId;
	}
}
