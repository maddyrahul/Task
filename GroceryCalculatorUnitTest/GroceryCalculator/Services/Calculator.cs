using GroceryCalculator.Dependencies;
using GroceryCalculator.Models;
using System.ComponentModel.DataAnnotations;

namespace GroceryCalculator.Services
{
    public class Calculator
    {
        private readonly List<Item> _items;
        private readonly IConsole _console;

        public Calculator(IConsole console)
        {
            _items = new List<Item>();
            _console = console;
        }

        // Retrieves the list of items currently added to the calculator.
        public List<Item> GetItems() // Add this method to expose items
        {
            return _items;
        }

        // Validates the inputs and adds the item to the internal list.
        public void AddItem()
        {
            Console.Write("Enter item name:");
            var name = _console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Item name cannot be empty.");
            }

            Console.Write("Enter item price:");
            if (!decimal.TryParse(_console.ReadLine(), out var price) || price <= 0)
            {
                throw new FormatException("Price must be a positive decimal value.");
            }

            Console.Write("Enter item quantity:");
            if (!int.TryParse(_console.ReadLine(), out var quantity) || quantity <= 0)
            {
                throw new FormatException("Quantity must be a positive integer.");
            }

            // Add the validated item to the list
            _items.Add(new Item { Name = name, Price = price, Quantity = quantity });
        }

        // Displays a summary of all items including their names, quantities, prices, and total costs.
        public void DisplaySummary()
        {
            if (_items.Count == 0)
            {
                _console.WriteLine("No items to display.");
                return;
            }

            _console.WriteLine("\nGrocery Summary:");
            decimal totalCost = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                _console.WriteLine($"{i + 1}. {item.Name} - {item.Quantity} @ ${item.Price:F2} each = ${item.TotalPrice:F2}");
                totalCost += item.TotalPrice;
            }

            _console.WriteLine($"Total Cost: ${totalCost:F2}");
        }

        // Throws a validation exception if the object fails validation.
        private void ValidateItem(Item item)
        {
            var context = new ValidationContext(item);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(item, context, results, true))
            {
                foreach (var result in results)
                {
                    throw new ValidationException(result.ErrorMessage);
                }
            }
        }

        // Handles invalid inputs gracefully by displaying an error message.
        private decimal GetValidDecimalInput()
        {
            while (true)
            {
                try
                {
                    string input = _console.ReadLine();
                    if (decimal.TryParse(input, out var result) && result > 0)
                        return result;

                    throw new FormatException("Price must be a positive decimal value.");
                }
                catch (FormatException ex)
                {
                    _console.WriteLine($"Input Error: {ex.Message}");
                }
            }
        }

        // Handles invalid inputs gracefully by displaying an error message.
        private int GetValidIntegerInput()
        {
            while (true)
            {
                try
                {
                    string input = _console.ReadLine();
                    if (int.TryParse(input, out var result) && result > 0)
                        return result;

                    throw new FormatException("Quantity must be a positive integer.");
                }
                catch (FormatException ex)
                {
                    _console.WriteLine($"Input Error: {ex.Message}");
                }
            }
        }
    }
}
