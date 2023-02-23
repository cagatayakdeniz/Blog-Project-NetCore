using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProjectFront.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        private IImageApiService _imageApiService;
        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }

        public int Id { get; set; }

        public BlogImageType BlogImageType { get; set; } = BlogImageType.BlogHome;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageById(Id);

            string html = string.Empty;

            if (blob != null)
            {
                if (BlogImageType == BlogImageType.BlogHome)
                {
                    html = $"<img src='{blob}' class='card-img-top' width='50' height='400'>";
                    output.Content.SetHtmlContent(html);
                }
                else
                {
                    html = $"<img src='{blob}' class='card-img-top' width='100' height='600'>";
                    output.Content.SetHtmlContent(html);
                }

            }
            else
            {

            }
        }
    }
}