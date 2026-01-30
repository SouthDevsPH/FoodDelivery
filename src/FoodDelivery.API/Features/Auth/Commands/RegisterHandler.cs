using FoodDelivery.API.Features.Auth.DTOs;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Features.Auth.Commands;

public record RegisterCommand(
	string Username,
	string Password,
	string? Email,
	string Role
) : IRequest<RegisterResponseDto?>;

public class RegisterHandler(FoodDeliveryDbContext db) : IRequestHandler<RegisterCommand, RegisterResponseDto?>
{
	public async Task<RegisterResponseDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		// Check if username already exists
		var existingUser = await db.Users
			.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

		if (existingUser is not null)
		{
			return null;
		}

		// Check if email already exists (if provided)
		if (!string.IsNullOrWhiteSpace(request.Email))
		{
			var existingEmail = await db.Users
				.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

			if (existingEmail is not null)
			{
				return null;
			}
		}

		// Hash the password using BCrypt
		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 11);

		// Create new user
		var user = new User
		{
			Username = request.Username,
			PasswordHash = passwordHash,
			Email = request.Email,
			Role = request.Role,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};

		db.Users.Add(user);
		await db.SaveChangesAsync(cancellationToken);

		return new RegisterResponseDto(
			user.UserId,
			user.Username,
			user.Email,
			user.Role,
			"User registered successfully"
		);
	}
}
