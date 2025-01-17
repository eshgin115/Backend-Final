﻿using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class PlantSize : BaseEntity<int>
    {
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public int SizeId { get; set; }
        public Size? Size { get; set; }
    }
}
