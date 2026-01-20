using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using FoodDelivery.API.Enums;
using MediatR;

namespace FoodDelivery.API.Features.DriverAssignments.Commands;

public record CreateDriverAssignmentCommand(int OrderId, int DriverId) : IRequest<int>;

public class CreateDriverAssignmentHandler(FoodDeliveryDbContext db) : IRequestHandler<CreateDriverAssignmentCommand, int>
{
	public async Task<int> Handle(CreateDriverAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = new DriverAssignment
		{
			OrderId = request.OrderId,
			DriverId = request.DriverId,
			AssignmentDate = DateTime.Now,
			DeliveryStatusId = (int)DeliveryStatusEnum.Assigned
		};

		db.DriverAssignments.Add(entity);
		await db.SaveChangesAsync(cancellationToken);

		return entity.AssignmentId;
	}
}

