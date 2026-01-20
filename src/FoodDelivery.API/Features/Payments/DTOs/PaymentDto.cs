using FoodDelivery.API.Enums;

namespace FoodDelivery.API.Features.Payments.DTOs;

public record PaymentDto(int PaymentId, int OrderId, PaymentMethodEnum PaymentMethod, PaymentStatusEnum PaymentStatus, decimal Amount, DateTime? PaymentDate);


