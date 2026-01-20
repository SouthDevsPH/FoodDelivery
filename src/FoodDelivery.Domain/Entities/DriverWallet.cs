using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class DriverWallet
{
    public int WalletId { get; set; }

    public int DriverId { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual User Driver { get; set; } = null!;

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
