using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class UserAddress
{
    public int UserAddressId { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
