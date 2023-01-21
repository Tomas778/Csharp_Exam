using Csharp_Exam.Models;

namespace Csharp_Exam.Repositories
{
    public class ActiveCustommers
    {
        public List<Order> ActiveOrders { get; set; } = new List<Order>();

        public void AddToActiveOrders(Order activeOrder)
        {
            ActiveOrders.Add(activeOrder);
        }
    }
}
