﻿using First_For_Mvc_Project.Database.Models.Common;

namespace First_For_Mvc_Project.Database.Models
{
    public class BlogVideo : BaseEntity<int>, IAuditable
    {
        public string VideoName { get; set; } = null!;
        public string VideoNameInFileSystem { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
