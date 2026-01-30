using FoodDelivery.API.Features.DriverWallets.Commands;
using FoodDelivery.API.Features.DriverWallets.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class DriverWalletsEndpoints
{
	public static IEndpointRouteBuilder MapDriverWalletsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/driverwallets")
			.RequireAuthorization();

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverWalletsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverWalletByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapGet("/driver/{driverId}", async (int driverId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetDriverWalletByDriverQuery(driverId));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapPost("", async (CreateDriverWalletCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/driverwallets/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdateDriverWalletCommand command, IMediator mediator) =>
		{
			if (command.WalletId != id)
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
			var result = await mediator.Send(new DeleteDriverWalletCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
