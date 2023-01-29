using Csharp_Exam.Interfaces;

namespace Csharp_Exam.Models
{
    public class Drink : IMenuItem
    {
        public int Number { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
