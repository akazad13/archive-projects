namespace WMC.Utilities
{
    public interface IAWSS3Helper
    {
        Task PushToAmazonS3ViaRest(string fileNameToUpload, Stream stream);
        string GetFileURL(string fileNameToDownload);
    }
}