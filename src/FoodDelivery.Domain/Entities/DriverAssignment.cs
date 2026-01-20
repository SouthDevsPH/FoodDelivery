using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class DriverAssignment
{
    public int AssignmentId { get; set; }

    public int OrderId { get; set; }

    public int DriverId { get; set; }

    public DateTime? AssignmentDate { get; set; }

    public int DeliveryStatusId { get; set; }

    public virtual DeliveryStatus DeliveryStatus { get; set; } = null!;

    public virtual User Driver { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
