using System.ComponentModel.DataAnnotations;

namespace Web_253502_Alkhovik.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}