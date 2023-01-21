using Csharp_Exam.Repositories;
using System.Data.SQLite;

namespace Csharp_Exam.Services
{
    public class DatabaseService
    {
        public Database DatabaseObject { get; private set; }

        public DatabaseService(Database databaseObject)
        {
            DatabaseObject = databaseObject;
        }

        //Create tables
        private void CreateTable(string tableName, string tableColumns)
        {
            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} ({tableColumns})";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                int result = command.ExecuteNonQuery();
            }
        }

        public void CreateDBtables()
        {
            CreateTable("food", "id INTEGER PRIMARY KEY AUTOINCREMENT, number INTEGER, category TEXT, name TEXT, price REAL");
            CreateTable("drinks", "id INTEGER PRIMARY KEY AUTOINCREMENT, number INTEGER, category TEXT, name TEXT, price REAL");
            CreateTable("tables", "id INTEGER PRIMARY KEY AUTOINCREMENT, number INTEGER, occupied INTEGER, places INTEGER");
            CreateTable("accounting", "id INTEGER PRIMARY KEY AUTOINCREMENT, date NUMERIC, sum REAL");
        }

        //Create initial data
        private List<string> InitFoodsSQLs()
        {
            List<string> foodQLs = new List<string>();
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (1, 'Snacks', 'Cheese Balls', 3.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (2, 'Snacks', 'French fries', 2.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (3, 'Snacks', 'Nachos', 5.50)");

            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (20, 'Main', 'Pizza', 15.00)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (21, 'Main', 'Pasta', 11.00)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (22, 'Main', 'Gnocchi', 12.00)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (23, 'Main', 'Lasagna', 14.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (24, 'Main', 'Bolognese', 12.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (25, 'Main', 'Risotto', 10.50)");

            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (40, 'Dessert', 'Strawberry Gelato', 5.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (41, 'Dessert', 'Tiramisu', 6.50)");
            foodQLs.Add("INSERT INTO food (number, category, name, price) VALUES (42, 'Dessert', 'Lemon cake', 5.00)");
            return foodQLs;
        }

        private List<string> InitDrinksSQLs()
        {
            List<string> drinksQLs = new List<string>();
            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (50, 'Hot', 'Cappuccino', 3.50)");
            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (51, 'Hot', 'Espresso', 2.50)");
            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (52, 'Hot', 'Tea', 2.50)");

            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (70, 'Soft Drink', 'Colla', 2.00)");
            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (71, 'Soft Drink', 'Fanta', 2.20)");
            drinksQLs.Add("INSERT INTO drinks (number, category, name, price) VALUES (72, 'Soft Drink', 'Sprite', 2.40)");
            return drinksQLs;
        }

        private List<string> InitTablesSQLs()
        {
            List<string> tablesQLs = new List<string>();
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (1, 0, 2)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (2, 0, 3)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (3, 0, 2)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (4, 0, 4)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (5, 0, 3)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (6, 0, 4)");
            tablesQLs.Add("INSERT INTO tables (number, occupied, places) VALUES (7, 0, 5)");
            return tablesQLs;
        }

        public void ExecuteNonQuery(List<string> sql)
        {
            using (var connection = DatabaseObject.CreateConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                foreach (string sqlItem in sql)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlItem;
                        int result = command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void CreateInitialData()
        {
            ExecuteNonQuery(InitFoodsSQLs());
            ExecuteNonQuery(InitDrinksSQLs());
            ExecuteNonQuery(InitTablesSQLs());
        }
    }
}
