﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Multi_VendorE_CommercePlatform.Models;

public class Card
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CardItem> CardItems { get; set; }
}