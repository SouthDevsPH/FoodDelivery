using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class MerchantStoreHour
{
    public int StoreHourId { get; set; }

    public int MerchantId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly OpenTime { get; set; }

    public TimeOnly CloseTime { get; set; }

    public bool IsClosed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Merchant Merchant { get; set; } = null!;
}
