using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Enums;
using MediatR;

namespace FoodDelivery.API.Features.DriverAssignments.Commands;

public record UpdateDriverAssignmentCommand(int AssignmentId, DeliveryStatusEnum? DeliveryStatus) : IRequest<bool>;

public class UpdateDriverAssignmentHandler(FoodDeliveryDbContext db) : IRequestHandler<UpdateDriverAssignmentCommand, bool>
{
	public async Task<bool> Handle(UpdateDriverAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await db.DriverAssignments.FindAsync([request.AssignmentId], cancellationToken);

		if (entity is null)
		{
			return false;
		}

		if (request.DeliveryStatus.HasValue)
		{
			entity.DeliveryStatusId = (int)request.DeliveryStatus.Value;
		}

		await db.SaveChangesAsync(cancellationToken);

		return true;
	}
}

