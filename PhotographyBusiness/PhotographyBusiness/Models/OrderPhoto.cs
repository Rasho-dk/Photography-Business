﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class OrderPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public decimal Price { get; set; } = decimal.Zero;
        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public OrderPhoto()
        {
        }


    }
}
