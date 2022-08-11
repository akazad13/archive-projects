using Amazon.SQS.Model;
using WMC.Utilities;

namespace WMC.Services
{
    public class AWSSQSService : IAWSSQSService
    {
        private readonly IAWSSQSHelper _AWSSQSHelper;
        public AWSSQSService(IAWSSQSHelper AWSSQSHelper)
        {
            this._AWSSQSHelper = AWSSQSHelper;
        }
        public async Task<bool> PostMessageAsync(string message)
        {
            try
            {
                return await _AWSSQSHelper.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Message>> GetAllMessagesAsync()
        {
            try
            {
                List<Message> messages = await _AWSSQSHelper.ReceiveMessageAsync();
                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
        {
            try
            {
                return await _AWSSQSHelper.DeleteMessageAsync(messageReceiptHandle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
