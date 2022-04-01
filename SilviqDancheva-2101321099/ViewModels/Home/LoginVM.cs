
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilviqDancheva_2101321099.ViewModels.Home
{
    public class LoginVM
    {
        [DisplayName("Потребителско име: ")]
        [Required(ErrorMessage = "*Това поле е задължтелно!")]
        public string Username { get; set; }

        [DisplayName("Парола: ")]
        [Required(ErrorMessage = "*Това поле е задължтелно!")]
        public string Password { get; set; }
    }
}