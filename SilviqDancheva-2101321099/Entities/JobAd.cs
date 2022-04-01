
using System.ComponentModel.DataAnnotations.Schema;

namespace SilviqDancheva_2101321099.Entities
{
    public class JobAd : BaseEntity
    {
      
        public int OwnerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
