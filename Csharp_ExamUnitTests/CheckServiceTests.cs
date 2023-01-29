using Csharp_Exam.Models;
using Csharp_Exam.Services;

namespace Csharp_Exam.UnitTests
{
    [TestClass]
    public class CheckServiceTests
    {
        //Naming - Method_Scenario_ExpectedBehaviour
        [TestMethod]
        public void Pay_OrderFullfilled_ReturnsSum()
        {
            //Arrange - prepeare Object
            Order order = new Order(1, DateTime.Now, new Table(1, 4));

            order.AddDrink(new Drink { Name = "Colla", Price = 2.5M });
            order.AddDrink(new Drink { Name = "Colla", Price = 2.5M });
            order.AddFood(new Food { Name = "Pizza", Price = 15M });
            order.AddFood(new Food { Name = "Pasta", Price = 11M });

            CheckService checkService = new CheckService(order);

            //Act - call the function
            decimal result = checkService.Pay();

            //Assert - confirm the result
            Assert.AreEqual(31, result);
        }
        //Naming - Method_Scenario_ExpectedBehaviour
        [TestMethod]
        public void Pay_OrderEmpty_Returns0()
        {
            //Arrange - prepeare Object
            Order order = new Order(2, DateTime.Now, new Table(2, 4));

            CheckService checkService = new CheckService(order);

            //Act - call the function
            decimal result = checkService.Pay();

            //Assert - confirm the result
            Assert.AreEqual(0, result);
        }
    }
}