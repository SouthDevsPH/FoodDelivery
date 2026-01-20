using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DriverAssignment> DriverAssignments { get; set; } = new List<DriverAssignment>();

    public virtual ICollection<DriverWallet> DriverWallets { get; set; } = new List<DriverWallet>();

    public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
