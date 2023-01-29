using Csharp_Exam.Models;
using Csharp_Exam.Services;

namespace Csharp_Exam.UnitTests
{
    [TestClass]
    public class FindBestTableTests
    {
        //Naming - Method_Scenario_ExpectedBehaviour
        [TestMethod]
        public void TableFindBestTable_AllFreeAnd2Customers_ReturnsTableFor2()
        {
            //Arrange - prepeare Object
            List<Table> availibleTables = new List<Table>();
            Customer customer = new Customer(2);

            availibleTables.Add(new Table(1, 1));
            availibleTables.Add(new Table(2, 3));
            availibleTables.Add(new Table(3, 2));
            availibleTables.Add(new Table(4, 5));
            availibleTables.Add(new Table(5, 4));

            FindBestTable findBestTable = new FindBestTable(availibleTables, customer);

            //Act - call the function
            Table result = findBestTable.TableFindBestTable();

            //Assert - confirm the result
            Assert.AreEqual(customer.CustomersNumber, result.Places);
        }

        //Naming - Method_Scenario_ExpectedBehaviour
        [TestMethod]
        public void TableFindBestTable_AllFreeAnd2Customers_ReturnsTableFor5()
        {
            //Arrange - prepeare Object
            List<Table> availibleTables = new List<Table>();
            Customer customer = new Customer(2);

            availibleTables.Add(new Table(1, 1));
            availibleTables.Add(new Table(2, 1));
            availibleTables.Add(new Table(3, 1));
            availibleTables.Add(new Table(4, 5));
            availibleTables.Add(new Table(5, 7));

            FindBestTable findBestTable = new FindBestTable(availibleTables, customer);

            var bestCaseTable = new Table(4, 5);

            //Act - call the function
            Table result = findBestTable.TableFindBestTable();

            //Assert - confirm the result
            Assert.AreEqual(bestCaseTable.Places, result.Places);
        }

        //Naming - Method_Scenario_ExpectedBehaviour
        [TestMethod]
        public void TableFindBestTable_AllFreeAnd7Customers_ReturnsNull()
        {
            //Arrange - prepeare Object
            List<Table> availibleTables = new List<Table>();
            Customer customer = new Customer(7);

            availibleTables.Add(new Table(1, 1));
            availibleTables.Add(new Table(2, 3));
            availibleTables.Add(new Table(3, 3));
            availibleTables.Add(new Table(4, 5));
            availibleTables.Add(new Table(5, 3));

            FindBestTable findBestTable = new FindBestTable(availibleTables, customer);

            //Act - call the function
            Table result = findBestTable.TableFindBestTable();

            //Assert - confirm the result
            Assert.AreEqual(null, result);
        }
    }
}