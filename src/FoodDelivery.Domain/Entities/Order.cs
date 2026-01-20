using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int MerchantId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Address { get; set; } = null!;

    public DateTime? DeliveryTime { get; set; }

    public int OrderStatusId { get; set; }

    public virtual ICollection<DriverAssignment> DriverAssignments { get; set; } = new List<DriverAssignment>();

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
