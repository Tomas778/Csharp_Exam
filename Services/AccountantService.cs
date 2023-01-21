using Csharp_Exam.Models;
using Csharp_Exam.Repositories;
using System.Data.SQLite;

namespace Csharp_Exam.Services
{
    public class AccountantService
    {
        private Database _databaseObject;
        public Database DatabaseObject
        {
            get { return _databaseObject; }
            set { _databaseObject = value; }
        }
        public Order OutgoingOrder { get; private set; }

        public AccountantService(Database databaseObject, Order outgoingOrder)
        {
            DatabaseObject = databaseObject;
            OutgoingOrder = outgoingOrder;
        }

        public void CheckOutOrder()
        {

            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                command.CommandText = $"INSERT INTO accounting (date, sum) VALUES (@date, @sum)";

                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@sum", new CheckService(OutgoingOrder).Pay());
                command.ExecuteNonQuery();
            }
            new CheckService(OutgoingOrder).CreateCheck();
        }
    }
}
