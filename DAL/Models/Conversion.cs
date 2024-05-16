namespace DAL.Models
{
    public class Conversion
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Value { get; set; } = 0!;
    }
}