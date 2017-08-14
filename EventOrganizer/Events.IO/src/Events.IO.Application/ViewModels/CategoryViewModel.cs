using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Events.IO.Application.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SelectList Categories()
        {
            return new SelectList(GetCategories(), "Id", "Name");
        }

        public List<CategoryViewModel> GetCategories()
        {
            var listCategories = new List<CategoryViewModel>()
            {
                new CategoryViewModel(){Id = new Guid("526f86c4-e294-4b13-b885-f5a9fa5954b8"), Name = "Congress"},
                new CategoryViewModel(){Id = new Guid("be389cf9-490a-4c86-9ee3-2a0d5f4f2cf8"), Name = "Meetup"},
                new CategoryViewModel(){Id = new Guid("8f45ee5d-5034-4cd5-8eca-54e08f068251"), Name = "Workshop"}
            };

            return listCategories;
        }
    }
}
