using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FilePath { get; set; }
        public IFormFile ImageFile { get; set; }
        [Required]
        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
      
        public Photo()
        {
            Id = nextId++;
        }   
        private static int nextId = 1;
        public Photo(int albumId)
        {
            Id = nextId++;
            AlbumId = albumId;
        }
    }
}
