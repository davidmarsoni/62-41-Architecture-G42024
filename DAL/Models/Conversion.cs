namespace DAL.Models
{
    public class Conversion
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Value { get; set; } = 0!;
    }
}