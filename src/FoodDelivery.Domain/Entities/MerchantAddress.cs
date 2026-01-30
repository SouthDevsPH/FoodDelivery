using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class MerchantAddress
{
    public int MerchantAddressId { get; set; }

    public int MerchantId { get; set; }

    public int AddressId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Merchant Merchant { get; set; } = null!;
}
