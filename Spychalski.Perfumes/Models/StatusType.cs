using System.ComponentModel.DataAnnotations;

namespace Spychalski.Perfumes.Models
{
    public enum StatusType
    {
        [Display(Name = "Out of stock")]
        OutOfStock,
        [Display(Name = "Ordered")]
        Ordered,
        [Display(Name = "In Stock")]
        InStock
    }
}

