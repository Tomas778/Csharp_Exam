using Csharp_Exam.Models;
using Csharp_Exam.Repositories;
using Csharp_Exam.Services;

namespace Csharp_Exam;
class Program
{
    static async Task Main(string[] args)
    {
        //Database
        Database databaseObject = new Database("RestaurantDB");
        DatabaseService databaseService = new DatabaseService(databaseObject);

        //If there is no database create tables and initial data
        if (databaseObject.NewDbCreated)
        {
            //Create tables
            databaseService.CreateDBtables();

            //Add initial data to DB
            databaseService.CreateInitialData();
        }
        //Random generator
        var randomNum = new Random();

        //Create Instance of Active Orders in Restaurant
        ActiveCustommers activeOrders = new ActiveCustommers();

        //Start Simulation here
        var taskIn = GenerateCustomersFlow();
        var taskOut = CustomersGoingOut();
        await Task.WhenAll(taskIn, taskOut);

        Console.WriteLine("Simulation finished...Thank you, come again!");
        Console.ReadKey();


        async Task GenerateCustomersFlow()
        {
            int i = 0;
            while (i < 8)
            {
                Customer customer = new Customer(randomNum.Next(2, 8));
                Console.WriteLine($"New: {customer.CustomersNumber} customers");
                List<Table> allFreeTables = new FindFreeTable(databaseObject).GetAll();
                customer.CustomersTable = new FindBestTable(allFreeTables, customer).TableFindBestTable();
                if (customer.CustomersTable == null)
                {
                    Console.WriteLine("Can't offer a Table for You, sorry...");
                }
                else
                {
                    Console.WriteLine($"New Guests!!! Welcome and please sit @ Table: {customer.CustomersTable.Number}, there are {customer.CustomersTable.Places} free places");
                    new TableReservation(databaseObject, customer.CustomersTable).ReservIt();
                    customer.SitAtTheTable();
                    activeOrders.AddToActiveOrders(new Order(i, DateTime.Now, customer.CustomersTable));
                    for (int j = 1; j <= customer.CustomersNumber; j++)
                    {
                        var softDrink = new GetItemFromMenu(databaseObject).GetDrink(randomNum.Next(70, 72));
                        var hotDrink = new GetItemFromMenu(databaseObject).GetDrink(randomNum.Next(50, 52));
                        var snacks = new GetItemFromMenu(databaseObject).GetFood(randomNum.Next(1, 3));
                        var main = new GetItemFromMenu(databaseObject).GetFood(randomNum.Next(20, 25));
                        var dessert = new GetItemFromMenu(databaseObject).GetFood(randomNum.Next(40, 42));
                        activeOrders.ActiveOrders.FirstOrDefault(x => x.OrderTable == customer.CustomersTable)?.AddDrink(softDrink);
                        activeOrders.ActiveOrders.FirstOrDefault(x => x.OrderTable == customer.CustomersTable)?.AddDrink(hotDrink);
                        activeOrders.ActiveOrders.FirstOrDefault(x => x.OrderTable == customer.CustomersTable)?.AddFood(snacks);
                        activeOrders.ActiveOrders.FirstOrDefault(x => x.OrderTable == customer.CustomersTable)?.AddFood(main);
                        activeOrders.ActiveOrders.FirstOrDefault(x => x.OrderTable == customer.CustomersTable)?.AddFood(dessert);
                    }
                }
                i++;
                await Task.Delay(5000);
            }
            Console.WriteLine("===================================================>>>>>All new customers done");
        }

        async Task CustomersGoingOut()
        {
            while (activeOrders.ActiveOrders.Count <= 0 && !taskIn.IsCompleted)
            {
                //Wait here for active Tables
            }
            int i = 0;
            while (activeOrders.ActiveOrders.Count > 0)
            {
                await Task.Delay(18000);
                int randInt = randomNum.Next(0, activeOrders.ActiveOrders.Count);
                Order outgoingOrder = activeOrders.ActiveOrders[randInt];
                Console.WriteLine($"Saying good bye for: {outgoingOrder.OrderTable.ActivePersons} customers @ table: {outgoingOrder.OrderTable.Number}");
                new AccountantService(databaseObject, outgoingOrder).CheckOutOrder();
                new TableReservation(databaseObject, outgoingOrder.OrderTable).RemoveReservation();
                activeOrders.ActiveOrders.RemoveAt(randInt);
                i++;
            }
            Console.WriteLine("<<<<<=================================================All customers went home");
        }
    }
}
