using FoodDelivery.API.Features.DriverAssignments.Commands;
using FoodDelivery.API.Features.DriverAssignments.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class DriverAssignmentsEndpoints
{
	public static IEndpointRouteBuilder MapDriverAssignmentsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/driverassignments");

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverAssignmentsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverAssignmentByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapGet("/driver/{driverId}", async (int driverId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverAssignmentsByDriverQuery(driverId));
			return Results.Ok(result);
		});

		group.MapPost("", async (CreateDriverAssignmentCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/driverassignments/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdateDriverAssignmentCommand command, IMediator mediator) =>
		{
			if (command.AssignmentId != id)
			{
				return Results.BadRequest();
			}

			var result = await mediator.Send(command);

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		group.MapDelete("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new DeleteDriverAssignmentCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
