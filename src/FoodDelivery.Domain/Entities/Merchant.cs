using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Merchant
{
    public int MerchantId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<MerchantAddress> MerchantAddresses { get; set; } = new List<MerchantAddress>();

    public virtual ICollection<MerchantStoreHour> MerchantStoreHours { get; set; } = new List<MerchantStoreHour>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;
}
