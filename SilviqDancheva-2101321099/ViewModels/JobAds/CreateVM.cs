using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilviqDancheva_2101321099.ViewModels.JobAds
{
    public class CreateVM
    {
        public int OwnerId { get; set; }

        [DisplayName("Заглавие: ")]
        [Required(ErrorMessage = "*Това поле е задължително!")]
        public string Title { get; set; }

        [DisplayName("Описание: ")]
        [Required(ErrorMessage = "*Това поле е задължително!")]
        public string Description { get; set; }

        [DisplayName("Категория: ")]
        [Required(ErrorMessage = "*Това поле е задължително!")]
        public int CategoryId { get; set; }
    }
}