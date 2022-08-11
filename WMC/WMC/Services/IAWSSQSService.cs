using Amazon.SQS.Model;

namespace WMC.Services
{
    public interface IAWSSQSService
    {
        Task<bool> PostMessageAsync(string message);
        Task<List<Message>> GetAllMessagesAsync();
        Task<bool> DeleteMessageAsync(string messageReceiptHandle);
    }
}