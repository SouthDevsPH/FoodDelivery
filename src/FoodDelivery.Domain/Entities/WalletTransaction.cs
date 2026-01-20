using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class WalletTransaction
{
    public int TransactionId { get; set; }

    public int WalletId { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual DriverWallet Wallet { get; set; } = null!;
}
