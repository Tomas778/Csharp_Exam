using Csharp_Exam.Models;
using Csharp_Exam.Repositories;
using System.Data.SQLite;

namespace Csharp_Exam.Services
{
    public class GetItemFromMenu
    {
        private Drink _databasedrink = new Drink();
        private Food _databasefood = new Food();

        private Database _databaseObject;
        public Database DatabaseObject
        {
            get { return _databaseObject; }
            set { _databaseObject = value; }
        }

        public GetItemFromMenu(Database databaseObject)
        {
            DatabaseObject = databaseObject;
        }

        public Drink GetDrink(int itemNr)
        {
            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM drinks WHERE number = {itemNr}";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _databasedrink.Number = Convert.ToInt32(reader["number"]);
                            _databasedrink.Category = Convert.ToString(reader["category"]);
                            _databasedrink.Name = Convert.ToString(reader["name"]);
                            _databasedrink.Price = Convert.ToDecimal(reader["price"]);
                        }
                    }
                    return _databasedrink;
                }
            }
        }

        public Food GetFood(int itemNr)
        {
            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM food WHERE number = {itemNr}";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _databasefood.Number = Convert.ToInt32(reader["number"]);
                            _databasefood.Category = Convert.ToString(reader["category"]);
                            _databasefood.Name = Convert.ToString(reader["name"]);
                            _databasefood.Price = Convert.ToDecimal(reader["price"]);
                        }
                    }
                    return _databasefood;
                }
            }
        }

    }
}
