namespace TakeItToTheCloud.Utilities
{
    public interface IAWSS3Helper
    {
        Task<string> PushToAmazonS3ViaRest(string fileNameToUpload, Stream stream);
        string GetFileURL(string fileNameToDownload);
        Task<bool> DeleteFileS3(string filename);
    }
}