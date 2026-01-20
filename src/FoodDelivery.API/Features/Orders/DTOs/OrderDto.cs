using FoodDelivery.Domain.Enums;

namespace FoodDelivery.API.Features.Orders.DTOs;

public record OrderDto(int OrderId, int UserId, int MerchantId, DateTime? OrderDate, OrderStatusEnum OrderStatus, decimal TotalAmount, string Address, DateTime? DeliveryTime);

