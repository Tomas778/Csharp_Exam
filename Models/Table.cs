namespace Csharp_Exam.Models
{
    public class Table
    {
        public int Number { get; private set; }
        public int Places { get; private set; }
        public int ActivePersons { get; set; }
        public bool Occupied { get; set; } = false;

        public Table(int number, int places)
        {
            Number = number;
            Places = places;
        }
    }
}
