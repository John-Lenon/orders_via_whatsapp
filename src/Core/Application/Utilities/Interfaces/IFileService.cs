namespace Application.Utilities.Utilities
{
    public interface IFileService
    {
        Task<byte[]> GetFileAsync(string filePath);
        Task<bool> SaveFileAsync(string folder, string fileName, byte[] fileData);
        bool DeleteFile(string folder, string fileName);
    }
}