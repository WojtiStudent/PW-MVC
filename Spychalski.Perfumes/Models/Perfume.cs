using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spychalski.Perfumes.Models
{
    [Index(nameof(Name), nameof(BrandId), IsUnique = true)]
    public class Perfume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PerfumeId { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Perfume name cannot be longer than 25 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Perfume name cannot be longer than 100 characters.")]
        [DisplayName("Scent Description")]
        public string ScentDescription { get; set; }
        [Required]
        public StatusType Status { get; set; }
        [Required]
        [Range(0, 9999)]
        [ValidAmount]
        public int Amount { get; set; }
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }
    }

   
}
