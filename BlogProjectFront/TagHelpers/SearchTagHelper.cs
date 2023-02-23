using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProjectFront.TagHelpers
{
    [HtmlTargetElement("Search")]
    public class SearchTagHelper: TagHelper
    {
        private IBlogApiService _blogApiService;
        public SearchTagHelper(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public string S { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var arananlar = await _blogApiService.SearchAsync(S);

            string html = $"Şu anda içinde <strong>{S}</strong> "+
            "kelimesi geçen blogları görüntülüyorsunuz";

            output.Content.SetHtmlContent(html);
        }
    }
}