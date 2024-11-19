using System.ComponentModel.DataAnnotations;

namespace GroceryCalculator.Models
{
    public class Item
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.1, 1000, ErrorMessage = "Price must be between $0.1 and $1000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 25, ErrorMessage = "Quantity must be between 1 and 25.")]
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}
