namespace Csharp_Exam.Models
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderDateTime { get; private set; }

        public Table OrderTable { get; private set; }

        public List<Drink> OrderDrinks = new List<Drink>();

        public List<Food> OrderMeals = new List<Food>();

        public Order(int id, DateTime orderDateTime, Table orderTable)
        {
            Id = id;
            OrderDateTime = orderDateTime;
            OrderTable = orderTable;
        }

        public void AddDrink(Drink drink)
        {

            OrderDrinks.Add(drink);
        }
        public void AddFood(Food food)
        {

            OrderMeals.Add(food);
        }

    }
}
