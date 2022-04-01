using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace SilviqDancheva_2101321099.ViewModels.JobAds
{
    public class FilterVM
    {
        [DisplayName("Заглавие: ")]
        public string Title { get; set; }

        [DisplayName("Описание: ")]
        public string Description { get; set; }

        [DisplayName("Категория: ")]      
        public int CategoryId { get; set; }

        [DisplayName("Публикувана от: ")]
        public string Owner { get; set; }

        public IEnumerable <SelectListItem> ValidCategories { get; set; }
    }
}