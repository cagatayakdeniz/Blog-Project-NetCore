using System.ComponentModel.DataAnnotations;

namespace BlogProjectFront.Models
{
    public class CategoryUpdateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Ad alanı boş geçilemez.")]
        public string Name { get; set; }
    }
}