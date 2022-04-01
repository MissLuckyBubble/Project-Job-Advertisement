using SilviqDancheva_2101321099.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SilviqDancheva_2101321099.ViewModels.Users
{
    public class RegisterVM
    {
        [DisplayName("Потребителско име: ")]
        [Required(ErrorMessage = " *Това поле е задължтелно!")]
        public string Username { get; set; }

        [DisplayName("Парола: ")]
        [Required(ErrorMessage = " *Това поле е задължтелно!")]
        public string Password { get; set; }

        [DisplayName("Име: ")]
        [Required(ErrorMessage = " *Това поле е задължтелно!")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия: ")]
        [Required(ErrorMessage = " *Това поле е задължтелно!")]
        public string LastName { get; set; }

        [DisplayName("Роля: ")]
        [Required(ErrorMessage = " *Това поле е задължтелно!")]
        public int RoleId{ get; set; }
    }
}