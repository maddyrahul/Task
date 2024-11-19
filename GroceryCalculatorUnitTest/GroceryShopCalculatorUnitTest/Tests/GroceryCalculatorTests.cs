using GroceryCalculator.Dependencies;
using GroceryCalculator.Models;
using GroceryCalculator.Services;
using Moq;

namespace GroceryShopCalculatorUnitTest.Tests
{
    public class GroceryCalculatorTests
    {
        // Test case: Successfully add an item
        [Fact]
        public void AddItem_ShouldAddItemToList()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("Apple")     
                .Returns("2.5")       
                .Returns("3");       
            var calculator = new Calculator(consoleMock.Object);

            // Act
            calculator.AddItem();
            var items = calculator.GetItems();

            // Assert
            Assert.Single(items);
            Assert.Equal("Apple", items[0].Name);
            Assert.Equal(2.5m, items[0].Price);
            Assert.Equal(3, items[0].Quantity);
        }

        // Test case: Add multiple items
        [Fact]
        public void AddItem_ShouldAddMultipleItemsToList()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("Apple")     
                .Returns("2.5")       
                .Returns("3")        
                .Returns("Banana")    
                .Returns("1.2")      
                .Returns("5");        
            var calculator = new Calculator(consoleMock.Object);

            // Act
            calculator.AddItem();
            calculator.AddItem();
            var items = calculator.GetItems();

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Equal("Apple", items[0].Name);
            Assert.Equal(2.5m, items[0].Price);
            Assert.Equal(3, items[0].Quantity);

            Assert.Equal("Banana", items[1].Name);
            Assert.Equal(1.2m, items[1].Price);
            Assert.Equal(5, items[1].Quantity);
        }

        // Test case: Validation Error for Empty Item Name
        [Fact]
        public void AddItem_ShouldThrowValidationErrorForEmptyName()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("")        
                .Returns("2.5")    
                .Returns("3");      
            var calculator = new Calculator(consoleMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => calculator.AddItem());
            Assert.Equal("Item name cannot be empty.", exception.Message);
        }

        // Test case: Validation Error for Negative Price
        [Fact]
        public void AddItem_ShouldThrowValidationErrorForNegativePrice()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("Apple")   
                .Returns("-5")      
                .Returns("3");       
            var calculator = new Calculator(consoleMock.Object);

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => calculator.AddItem());
            Assert.Equal("Price must be a positive decimal value.", exception.Message);
        }


        // Test case: Display Summary with Items
        [Fact]
        public void DisplaySummary_ShouldDisplaySummaryForItems()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.Setup(c => c.WriteLine(It.IsAny<string>())).Verifiable();
            var calculator = new Calculator(consoleMock.Object);

            // Add items
            calculator.GetItems().Add(new Item { Name = "Apple", Price = 2.5m, Quantity = 3 });
            calculator.GetItems().Add(new Item { Name = "Banana", Price = 1.2m, Quantity = 5 });

            // Act
            calculator.DisplaySummary();

            // Assert
            consoleMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("1. Apple - 3 @ $2.50 each = $7.50"))), Times.Once);
            consoleMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("2. Banana - 5 @ $1.20 each = $6.00"))), Times.Once);
            consoleMock.Verify(c => c.WriteLine(It.Is<string>(s => s.Contains("Total Cost: $13.50"))), Times.Once);
        }

        // Test case: Display Summary with No Items
        [Fact]
        public void DisplaySummary_ShouldDisplayMessageForNoItems()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.Setup(c => c.WriteLine(It.IsAny<string>())).Verifiable();
            var calculator = new Calculator(consoleMock.Object);

            // Act
            calculator.DisplaySummary();

            // Assert
            consoleMock.Verify(c => c.WriteLine("No items to display."), Times.Once);
        }

        // Test case: Add Item with Boundary Values for Quantity
        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        public void AddItem_ShouldAcceptBoundaryValuesForQuantity(int quantity)
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("Apple")    
                .Returns("2.5")       
                .Returns(quantity.ToString());  
            var calculator = new Calculator(consoleMock.Object);

            // Act
            calculator.AddItem();
            var items = calculator.GetItems();

            // Assert
            Assert.Single(items);
            Assert.Equal(quantity, items[0].Quantity);
        }

        // Test case: Add Item with Boundary Values for Price
        [Theory]
        [InlineData(0.1)]
        [InlineData(1000)]
        public void AddItem_ShouldAcceptBoundaryValuesForPrice(decimal price)
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("Apple")   
                .Returns(price.ToString())  
                .Returns("5");       
            var calculator = new Calculator(consoleMock.Object);

            // Act
            calculator.AddItem();
            var items = calculator.GetItems();

            // Assert
            Assert.Single(items);
            Assert.Equal(price, items[0].Price);
        }
    }
}
