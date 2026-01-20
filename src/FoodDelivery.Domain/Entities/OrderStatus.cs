using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class OrderStatus
{
    public int OrderStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
