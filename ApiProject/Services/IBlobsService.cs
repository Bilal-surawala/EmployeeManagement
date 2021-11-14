using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApiProject.Services
{
    public interface IBlobsService
    {
        public Task<string> UploadFile(IFormFile formFile);

        public Task<bool> DeleteFile(string fileName);
    }
}
