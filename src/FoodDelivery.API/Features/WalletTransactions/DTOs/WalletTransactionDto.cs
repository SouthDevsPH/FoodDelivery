namespace FoodDelivery.API.Features.WalletTransactions.DTOs;

public record WalletTransactionDto(int TransactionId, int WalletId, decimal Amount, string TransactionType, string? Description, DateTime? TransactionDate);
