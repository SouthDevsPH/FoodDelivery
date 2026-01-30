using FoodDelivery.API.Features.Auth.Commands;
using FoodDelivery.API.Features.Auth.DTOs;
using FoodDelivery.API.Features.Auth.Queries;
using MediatR;

namespace FoodDelivery.API.Endpoints;

public static class AuthEndpoints
{
	public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/auth");

		group.MapPost("/login", async (LoginRequestDto request, IMediator mediator) =>
		{
			var result = await mediator.Send(new LoginCommand(request.Username, request.Password));

			if (result is null)
			{
				return Results.Unauthorized();
			}

			return Results.Ok(result);
		});

		group.MapPost("/logout", async (RefreshTokenRequestDto request, IMediator mediator) =>
		{
			var result = await mediator.Send(new LogoutCommand(request.RefreshToken));

			if (!result)
			{
				return Results.BadRequest(new { Message = "Invalid refresh token" });
			}

			return Results.Ok(new { Message = "Logged out successfully" });
		});

		group.MapPost("/refresh", async (RefreshTokenRequestDto request, IMediator mediator) =>
		{
			var result = await mediator.Send(new RefreshTokenCommand(request.RefreshToken));

			if (result is null)
			{
				return Results.Unauthorized();
			}

			return Results.Ok(result);
		});

		group.MapPost("/validate", async (HttpRequest httpRequest, IMediator mediator) =>
		{
			var authHeader = httpRequest.Headers.Authorization.ToString();

			if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
			{
				return Results.BadRequest(new { Message = "Invalid authorization header" });
			}

			var token = authHeader["Bearer ".Length..].Trim();
			var result = await mediator.Send(new ValidateTokenQuery(token));

			return Results.Ok(result);
		});

        group.MapPost("/register", async (RegisterRequestDto request, IMediator mediator) =>
        {
            var result = await mediator.Send(new RegisterCommand(
                request.Username,
                request.Password,
                request.Email,
                request.Role
            ));

            if (result is null)
            {
                return Results.Conflict(new { Message = "Username or email already exists" });
            }

            return Results.Created($"/api/users/{result.UserId}", result);
        });

        return app;
	}
}
