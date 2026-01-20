using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int PaymentStatusId { get; set; }

    public int? PaymentMethodId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual PaymentStatus PaymentStatus { get; set; } = null!;
}
