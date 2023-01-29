using Csharp_Exam.Models;
using Csharp_Exam.Repositories;
using System.Data.SQLite;

namespace Csharp_Exam.Services
{
    public class TableReservation
    {
        private Database _databaseObject;
        public Database DatabaseObject
        {
            get { return _databaseObject; }
            set { _databaseObject = value; }
        }
        public Table ReservedTable { get; private set; }

        public int RemoveTablAtNumber { get; private set; }

        public TableReservation(Database databaseObject, Table reservedTable)
        {
            DatabaseObject = databaseObject;
            ReservedTable = reservedTable;
        }
        public TableReservation(Database databaseObject, int removeTablAtNumber)
        {
            DatabaseObject = databaseObject;
            RemoveTablAtNumber = removeTablAtNumber;
        }
        /// <summary>
        /// Set Reservation in DB table
        /// </summary>
        public void ReservIt()
        {

            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE tables SET occupied = 1 WHERE number = {ReservedTable.Number}";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Remove Reservation in DB table
        /// </summary>
        public void RemoveReservation()
        {

            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE tables SET occupied = 0 WHERE number = {ReservedTable.Number}";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Remove Reservation in DB table based on int number at class construct
        /// </summary>
        public void RemoveReservationAtNumber()
        {

            using (var connection = DatabaseObject.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE tables SET occupied = 0 WHERE number = {RemoveTablAtNumber}";
                try
                {
                    connection.Open();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error opening connection to the database: " + ex.Message);
                }
                command.ExecuteNonQuery();
            }
        }

    }
}
