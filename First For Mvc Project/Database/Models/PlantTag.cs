﻿using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class PlantTag : BaseEntity<int>
    {
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
