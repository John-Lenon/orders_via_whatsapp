namespace Application.Interfaces.Utilities
{
    public interface IFileService
    {
        Task<byte[]> GetFileAsync(string folder, string fileName);
        Task<bool> SaveFileAsync(string folder, string fileName, byte[] fileData);
        bool DeleteFile(string folder, string fileName);
    }
}