using FoodDelivery.API.Enums;

namespace FoodDelivery.API.Features.DriverAssignments.DTOs;

public record DriverAssignmentDto(int AssignmentId, int OrderId, int DriverId, DateTime? AssignmentDate, DeliveryStatusEnum DeliveryStatus);

