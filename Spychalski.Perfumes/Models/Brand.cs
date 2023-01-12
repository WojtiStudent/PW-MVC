using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spychalski.Perfumes.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Brand name cannot be longer than 25 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Country name cannot be longer than 25 characters.")]
        public string Country { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Headquarters name cannot be longer than 25 characters.")]
        public string Headquarters { get; set; }

        public List<Perfume> Perfumes { get; set; } = new();
    }
}
