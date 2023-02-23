using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogProjectFront.Models
{
    public class BlogAddModel
    {
        public int AppUserId { get; set; }
        [Required(ErrorMessage="Title alanı boş geçilemez.")]
        public string Title { get; set; }
        [Required(ErrorMessage="Kısa Açıklama alanı boş geçilemez.")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage="Açıklama alanı boş geçilemez.")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
    }
}