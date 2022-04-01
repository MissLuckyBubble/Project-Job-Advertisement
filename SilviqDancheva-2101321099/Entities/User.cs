
using System.ComponentModel.DataAnnotations.Schema;

namespace SilviqDancheva_2101321099.Entities
{
    public class User : BaseEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
