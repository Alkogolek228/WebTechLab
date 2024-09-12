using System.ComponentModel.DataAnnotations;

namespace Web_253502_Alkhovik.Domain.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public short Amount { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
    }
}