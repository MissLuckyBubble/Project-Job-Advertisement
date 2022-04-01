
using System.ComponentModel.DataAnnotations.Schema;

namespace SilviqDancheva_2101321099.Entities
{
    public class UserToJobAd : BaseEntity
    {
       
        public int UserId { get; set; }
        public int JobAdId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("JobAdId")]
        public virtual JobAd JobAd { get; set; }
    }
}