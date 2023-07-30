namespace Pronia.ExtentionServices.Interfaces
{
    public interface IFileService
    {
        Task SaveAsync(IFormFile file,string path);
        Task<string> UploadAsync(IFormFile file, string contentType,string path,int mb);
        void Delete(string path);
    }
}
