using FoodDelivery.API.Features.Payments.Commands;
using FoodDelivery.API.Features.Payments.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class PaymentsEndpoints
{
	public static IEndpointRouteBuilder MapPaymentsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/payments");

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetPaymentsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetPaymentByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapGet("/order/{orderId}", async (int orderId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetPaymentsByOrderQuery(orderId));
			return Results.Ok(result);
		});

		group.MapPost("", async (CreatePaymentCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/payments/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdatePaymentCommand command, IMediator mediator) =>
		{
			if (command.PaymentId != id)
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
			var result = await mediator.Send(new DeletePaymentCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
