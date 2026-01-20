using FoodDelivery.API.Features.WalletTransactions.Commands;
using FoodDelivery.API.Features.WalletTransactions.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class WalletTransactionsEndpoints
{
	public static IEndpointRouteBuilder MapWalletTransactionsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/wallettransactions");

		group.MapGet("", async (IMediator mediator) =>
		{
			var result = await mediator.Send(new GetWalletTransactionsQuery());
			return Results.Ok(result);
		});

		group.MapGet("/{id}", async (int id, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetWalletTransactionByIdQuery(id));

			if (result is null)
			{
				return Results.NotFound();
			}

			return Results.Ok(result);
		});

		group.MapGet("/wallet/{walletId}", async (int walletId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetWalletTransactionsByWalletQuery(walletId));
			return Results.Ok(result);
		});

		group.MapPost("", async (CreateWalletTransactionCommand command, IMediator mediator) =>
		{
			var id = await mediator.Send(command);
			return Results.Created($"/api/wallettransactions/{id}", id);
		});

		group.MapPut("/{id}", async (int id, UpdateWalletTransactionCommand command, IMediator mediator) =>
		{
			if (command.TransactionId != id)
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
			var result = await mediator.Send(new DeleteWalletTransactionCommand(id));

			if (!result)
			{
				return Results.NotFound();
			}

			return Results.NoContent();
		});

		return app;
	}
}
