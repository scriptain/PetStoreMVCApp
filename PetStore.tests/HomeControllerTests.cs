using Xunit; // Import the xUnit library
using PetStoreMVCApp.Controllers; // Import the HomeController class
using PetStoreMVCApp.Models; // Import the Pet model class
using System.Linq; // Import the LINQ library for sorting
using System.Collections.Generic; // Import the collections library

namespace PetStoreMVCApp.Tests
{
    public class HomeControllerTests // Define the test class
    {
        private HomeController _controller; // Declare a variable for the HomeController

        public HomeControllerTests()
        {
            _controller = new HomeController(null, null); // Initialize the HomeController with null dependencies
        }

        // Test method to verify sorting in ascending order
        [Fact] // This keyword marks this method as a test. When we run tests, this test will be executed.
        public void SortPetsByName_SortsPetsInAscendingOrder()
            // naming convention. NameOfMethodToBeTested_DescriptionOfTheTestScenario/ExpectedBehaviour
        {
            // Start of AAA (Arrange, Act, Assert) pattern.

            // Arrange: Set up the initial data and expected results so input and output of our method 
            var pets = new Pet[] // unsorted input data
            {
                new Pet { name = "Charlie" }, // Pet with name Charlie
                new Pet { name = "Bella" }, // Pet with name Bella
                new Pet { name = "Max" } // Pet with name Max
            };

            var expected = new Pet[] // the expected result once sorted alphabetically
            {
                new Pet { name = "Bella" }, // Bella should be first in ascending order
                new Pet { name = "Charlie" }, // Charlie should be second in ascending order
                new Pet { name = "Max" } // Max should be last in ascending order
            };

            // Act: Call the method being tested
            // _controller is an instance of HomeController. _ indicates private variable
            var sortedPets = _controller.SortPetsByName(null, pets);

            // Assert: Verify that the method's result matches the expected result
            Assert.Equal(expected.Select(p => p.name), sortedPets.Select(p => p.name));
        }

        // Test method to verify sorting in descending order
        [Fact]
        public void SortPetsByName_SortsPetsInDescendingOrder()
        {
            // Arrange: Set up the initial data and expected results
            var pets = new Pet[]
            {
                new Pet { name = "Charlie" }, // Pet with name Charlie
                new Pet { name = "Bella" }, // Pet with name Bella
                new Pet { name = "Max" } // Pet with name Max
            };

            var expected = new Pet[]
            {
                new Pet { name = "Max" }, // Max should be first in descending order
                new Pet { name = "Charlie" }, // Charlie should be second in descending order
                new Pet { name = "Bella" } // Bella should be last in descending order
            };

            // Act: Call the method being tested with descending order
            var sortedPets = _controller.SortPetsByName("D", pets);

            // Assert: Verify that the method's result matches the expected result
            // In this case, we are asserting that the two operations will be equal
            // Enumerable.Select(): creates a new list based on the rule (in this case p => p.name) that is passed to it
            Assert.Equal(expected.Select(p => p.name), sortedPets.Select(p => p.name));
        }
    }
}
