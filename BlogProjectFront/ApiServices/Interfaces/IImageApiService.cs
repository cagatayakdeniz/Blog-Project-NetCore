using System.Threading.Tasks;

namespace BlogProjectFront.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageById(int id);
    }
}