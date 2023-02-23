using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProjectFront.TagHelpers
{
    [HtmlTargetElement("GetCategory")]
    public class GetCategoryTagHelper: TagHelper
    {
        private ICategoryApiService _categoryApiService;
        public GetCategoryTagHelper(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public int CategoryId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var category = await _categoryApiService.GetByIdAsync(CategoryId);
            
            string html = $"Şu anda kategorisi <strong>{category.Name}</strong> "+
            "olan blogları görüntülüyorsunuz";

            output.Content.SetHtmlContent(html);
        }
    }
}