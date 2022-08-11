using Amazon.SQS.Model;

namespace WMC.Utilities
{
    public interface IAWSSQSHelper
    {
        Task<bool> SendMessageAsync(string message);
        Task<List<Message>> ReceiveMessageAsync();
        Task<bool> DeleteMessageAsync(string messageReceiptHandle);

    }
}