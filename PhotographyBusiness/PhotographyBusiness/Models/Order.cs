using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal TotalPriceWithTax { get; set; }        
        [NotMapped]
        public virtual ICollection<OrderPhoto>? OrderPhotos { get; set; }

      
    }
}
