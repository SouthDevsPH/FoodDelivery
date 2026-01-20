using FoodDelivery.API.Features.Merchants.Commands;
using FoodDelivery.API.Features.Merchants.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class MerchantsEndpoints
{
	public static IEndpointRouteBuilder MapMerchantsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/merchants");

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetMerchantsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetMerchantByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapPost("", async (CreateMerchantCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/merchants/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdateMerchantCommand command, IMediator mediator) =>
		{
			if (command.MerchantId != id)
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
			var result = await mediator.Send(new DeleteMerchantCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
