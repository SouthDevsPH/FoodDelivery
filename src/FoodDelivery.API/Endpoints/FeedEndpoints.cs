using FoodDelivery.API.Features.Feed.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class FeedEndpoints
{
	public static IEndpointRouteBuilder MapFeedEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/feed")
			.RequireAuthorization();

		group.MapGet("/popular-now", async (int userId, int? addressId, IMediator mediator) =>
		{
			var result = await mediator.Send(new GetPopularNowQuery(userId, addressId));
			return Results.Ok(result);
		});

		return app;
	}
}
