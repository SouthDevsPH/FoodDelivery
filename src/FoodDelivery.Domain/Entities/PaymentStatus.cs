using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class PaymentStatus
{
    public int PaymentStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
