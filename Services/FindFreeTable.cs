using Csharp_Exam.Models;
using Csharp_Exam.Repositories;

namespace Csharp_Exam.Services
{
    public class FindFreeTable
    {
        private Database _databaseObject;
        public Database DatabaseObject
        {
            get { return _databaseObject; }
            set { _databaseObject = value; }
        }

        public FindFreeTable(Database databaseObject)
        {
            DatabaseObject = databaseObject;
        }

        public List<Table> GetAll()
        {
            List<Table> allFreeTables = new List<Table>();

            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tables";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["occupied"]) == 0)
                            {
                                allFreeTables.Add(new Table(
                                    Convert.ToInt32(reader["number"]),
                                    Convert.ToInt32(reader["places"])));
                            }
                        }
                    }
                    return allFreeTables;
                }
            }
        }
    }
}
