using System.Data.SQLite;

namespace Csharp_Exam.Repositories
{
    public class Database
    {
        public string DatabaseName { get; private set; }
        public bool NewDbCreated { get; private set; } = false;

        public Database(string databaseName)
        {
            DatabaseName = databaseName;

            if (!File.Exists($"./{DatabaseName}.sqlite"))
            {
                SQLiteConnection.CreateFile($"{DatabaseName}.sqlite");
                NewDbCreated = true;
                Console.WriteLine($"{DatabaseName}.sqlite created!");
            }
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection($"Data Source = {DatabaseName}.sqlite");
        }
    }
}
