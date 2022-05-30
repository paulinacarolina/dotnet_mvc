using System.ComponentModel.DataAnnotations;

namespace Platzi_Dotnet.VIewModels
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
    }
}
