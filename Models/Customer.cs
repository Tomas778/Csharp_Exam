namespace Csharp_Exam.Models
{
    public class Customer
    {
        public Order CustomerOrder { get; private set; }
        public int CustomersNumber { get; set; }
        public Table CustomersTable { get; set; }

        public Customer(int number)
        {
            CustomersNumber = number;
        }

        public void SitAtTheTable()
        {
            CustomersTable.ActivePersons = CustomersNumber;
            CustomersTable.Occupied = true;
        }
    }
}
