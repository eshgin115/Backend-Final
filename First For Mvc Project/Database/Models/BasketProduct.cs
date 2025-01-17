﻿using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BasketProduct : BaseEntity<int>, IAuditable
    {
        public int BasketId { get; set; }
        public Basket? Basket { get; set; }

        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public int? Quantity { get; set; }
        public int? SizeId { get; set; }
        public Size? Size { get; set; }

        public int? ColorId { get; set; }
        public Color? Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
