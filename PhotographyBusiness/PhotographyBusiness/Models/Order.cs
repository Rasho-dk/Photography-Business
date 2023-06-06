using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PhotographyBusiness.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }  
        public Booking Booking { get; set; }
        [Required]
        [AllowNull]
        public decimal TotalPriceWithTax { get; set; }
        public bool Condition { get; set; }
        [NotMapped]
        public virtual ICollection<OrderPhoto>? OrderPhotos { get; set; }

        public Order()
        {
            
        }

    }
}
