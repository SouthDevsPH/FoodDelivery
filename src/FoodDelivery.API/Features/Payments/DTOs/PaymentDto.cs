namespace FoodDelivery.API.Features.Payments.DTOs;

public record PaymentDto(int PaymentId, int OrderId, string PaymentMethod, string? PaymentStatus, decimal Amount, DateTime? PaymentDate);
