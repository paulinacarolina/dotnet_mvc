using System.ComponentModel.DataAnnotations;

namespace Platzi_Dotnet.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Name { get; set; }
        
        public double Price { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }

    }
}

