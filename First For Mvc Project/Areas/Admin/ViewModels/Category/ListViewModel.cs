﻿namespace Pronia.Areas.Admin.ViewModels.Category
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
