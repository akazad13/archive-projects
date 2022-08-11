using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace WMC.Utilities
{
    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IConfiguration _configuration;
        public AWSSQSHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AmazonSQSClient InitializeSQS()
        {
            var region = _configuration["AWS:Region"];
            AmazonSQSConfig config = new AmazonSQSConfig();
            config.ServiceURL = "https://s3.amazonaws.com";
            config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region);
            string publicKey = _configuration["AWS:PublicKey"];
            string secretKey = _configuration["AWS:SecretKey"];
            string sessionToken = _configuration["AWS:SessionToken"];
            AmazonSQSClient s3Client = new AmazonSQSClient(publicKey, secretKey, sessionToken, config);
            return s3Client;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            try
            {
                AmazonSQSClient s3Client = InitializeSQS();
                var sendRequest = new SendMessageRequest(_configuration["AWS:SQSQueueUrl"], message);
                // Post message or payload to queue  
                var sendResult = await s3Client.SendMessageAsync(sendRequest);

                return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                AmazonSQSClient s3Client = InitializeSQS();
                //Create New instance  
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _configuration["AWS:SQSQueueUrl"],
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 10
                };
                //CheckIs there any new message available to process  
                var result = await s3Client.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
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
                AmazonSQSClient s3Client = InitializeSQS();
                //Deletes the specified message from the specified queue  
                var deleteResult = await s3Client.DeleteMessageAsync(_configuration["AWS:SQSQueueUrl"], messageReceiptHandle);
                return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
