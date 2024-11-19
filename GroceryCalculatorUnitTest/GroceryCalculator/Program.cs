using GroceryCalculator.Services;
using GroceryCalculator.Dependencies;

namespace GroceryCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConsole console = new ConsoleWrapper(); // Create an instance of ConsoleWrapper
            Calculator calculator = new Calculator(console); // Pass it to Calculator

            bool continueAdding = true;

            Console.WriteLine("Welcome to the Grocery Shop Calculator!");

            while (continueAdding)
            {
                calculator.AddItem();

                Console.Write("Do you want to add another item? (y/n): ");
                string choice = Console.ReadLine()?.ToLower();
                continueAdding = choice == "y";
            }

            calculator.DisplaySummary();

            Console.WriteLine("\nThank you for using the Grocery Shop Calculator!");
        }
    }
}
