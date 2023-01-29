namespace Csharp_Exam.Interfaces
{
    public interface IMenuItem
    {
        public int Number { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
