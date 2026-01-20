namespace FoodDelivery.API.Features.DriverWallets.DTOs;

public record DriverWalletDto(int WalletId, int DriverId, decimal? Balance, DateTime? LastUpdated);
