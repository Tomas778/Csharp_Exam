using Csharp_Exam.Models;

namespace Csharp_Exam.Services
{
    public class FindBestTable
    {
        public List<Table> AllFree { get; private set; }
        public Customer Customer { get; private set; }

        public FindBestTable(List<Table> allFree, Customer customer)
        {
            AllFree = allFree;
            Customer = customer;
        }

        public Table TableFindBestTable()
        {
            Table bestMatch = null;
            int minDiff = int.MaxValue;
            foreach (Table table in AllFree)
            {
                if (table.Places >= Customer.CustomersNumber && !table.Occupied)
                {
                    int diff = table.Places - Customer.CustomersNumber;
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        bestMatch = table;
                    }

                }
            }
            return bestMatch;
        }

    }
}
