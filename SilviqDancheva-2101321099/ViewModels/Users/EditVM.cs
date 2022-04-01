using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilviqDancheva_2101321099.ViewModels.Users
{
    public class EditVM
    {
        public int Id { get; set; }

        [DisplayName("Потребителско име: ")]
        [Required(ErrorMessage = " *Това поле е задължително")]
        public string Username { get; set; }

        [DisplayName("Парола: ")]
        [Required(ErrorMessage = " **Това поле е задължително!")]
        public string Password { get; set; }

        [DisplayName("Име: ")]
        [Required(ErrorMessage = " **Това поле е задължително!")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия: ")]
        [Required(ErrorMessage = " *Това поле е задължително!")]
        public string LastName { get; set; }

        public int RoleId { get; set; }
    }
}