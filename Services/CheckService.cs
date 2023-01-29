using Csharp_Exam.Interfaces;
using Csharp_Exam.Models;

namespace Csharp_Exam.Services
{
    public class CheckService
    {
        public Order OrderTable { get; private set; }


        public CheckService(Order order)
        {
            OrderTable = order;
        }


        public void CreateCheck()
        {
            using (StreamWriter sw = new StreamWriter($"{OrderTable.OrderDateTime.ToString("yyyy-MM-dd_HH-mm-ss")}_Table_{OrderTable.OrderTable.Number}_check.txt"))
            {
                Console.WriteLine($"================UAB Skanaus===============");
                sw.WriteLine($"=============UAB Skanaus============");
                var drinkgroups = OrderTable.OrderDrinks.GroupBy(p => p.Name)
                         .Select(g => new
                         {
                             Name = g.Key,
                             Count = g.Count(),
                             Productsprice = g.Sum(p => p.Price)
                         });

                foreach (var group in drinkgroups)
                {
                    Console.WriteLine($"{group.Name,-20} - Qty: {group.Count}, = {group.Productsprice:N2} eur");
                    sw.WriteLine($"{group.Name,-20} - Qty: {group.Count}, = {group.Productsprice:N2} eur");
                }

                var foodgroups = OrderTable.OrderMeals.GroupBy(p => p.Name)
                          .Select(g => new
                          {
                              Name = g.Key,
                              Count = g.Count(),
                              Productsprice = g.Sum(p => p.Price)
                          });

                foreach (var group in foodgroups)
                {
                    Console.WriteLine($"{group.Name,-20} - Qty: {group.Count}, = {group.Productsprice:N2} eur");
                    sw.WriteLine($"{group.Name,-20} - Qty: {group.Count}, = {group.Productsprice:N2} eur");
                }
                Console.WriteLine($">>You have to pay: {Pay():N2} eur.");
                sw.WriteLine($">>Total paid: {Pay():N2} eur.");
                Console.WriteLine($"========Check Debug Folder For Bill========");
                sw.WriteLine($"=====================================");
            }
            if (false) //If you want to enable this service make sure you have valid e-mail service instructions inside in the class "E-mailService.cs" 
            {
                var SendBill = new E_mailService();
                SendBill.SendEmail($"{OrderTable.OrderDateTime.ToString("yyyy-MM-dd_HH-mm-ss")}_Table_{OrderTable.OrderTable.Number}_check.txt");
            }
            else Console.WriteLine("The Bill was not send via e-mail");
        }

        public decimal Pay()
        {
            decimal sum = 0;
            foreach (IMenuItem item in OrderTable.OrderDrinks)
            {
                sum += item.Price;
            }

            foreach (IMenuItem item in OrderTable.OrderMeals)
            {
                sum += item.Price;
            }
            return sum;
        }
    }
}
