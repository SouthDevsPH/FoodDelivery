using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public int MerchantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
