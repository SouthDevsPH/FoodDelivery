using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using FoodDelivery.API.Enums;
using MediatR;

namespace FoodDelivery.API.Features.Payments.Commands;

public record CreatePaymentCommand(int OrderId, PaymentMethodEnum PaymentMethod, decimal Amount) : IRequest<int>;

public class CreatePaymentHandler(FoodDeliveryDbContext db) : IRequestHandler<CreatePaymentCommand, int>
{
	public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
	{
		var entity = new Payment
		{
			OrderId = request.OrderId,
			PaymentMethodId = (int)request.PaymentMethod,
			PaymentStatusId = (int)PaymentStatusEnum.Pending,
			Amount = request.Amount,
			PaymentDate = DateTime.Now
		};

		db.Payments.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.PaymentId;
	}
}


