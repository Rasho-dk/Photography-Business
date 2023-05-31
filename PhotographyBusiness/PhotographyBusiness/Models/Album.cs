using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Description")]

        public string Description { get; set; }
        [ForeignKey(nameof(User))]
        public int BookingId { get; set; }
        [NotMapped]
        [BindNever]
        public Booking Booking { get; set; }  
        [NotMapped]
        [BindNever]
        public virtual ICollection<Photo> Photos { get;}
        public Album()
        {

        }
        private static int nextId = 1;
        public Album(string name, string description, int bookingId)
        {
            Id = nextId++;  
            Name = name;
            Description = description;
            BookingId = bookingId;
        }


    }
}
