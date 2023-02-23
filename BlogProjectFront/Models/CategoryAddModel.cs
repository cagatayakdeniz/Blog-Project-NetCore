using System.ComponentModel.DataAnnotations;

namespace BlogProjectFront.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage="Ad alanı boş geçilemez.")]
        public string Name { get; set; }
    }
}