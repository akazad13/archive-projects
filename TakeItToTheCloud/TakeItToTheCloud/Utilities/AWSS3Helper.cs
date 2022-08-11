using Amazon.S3;
using Amazon.S3.Model;

namespace TakeItToTheCloud.Utilities
{
    public class AWSS3Helper : IAWSS3Helper
    {
        private readonly IConfiguration _configuration;
        public AWSS3Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AmazonS3Client InitializeS3()
        {
            var region = _configuration["AWS:Region"];
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "https://s3.amazonaws.com";
            config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region);
            string publicKey = _configuration["AWS:PublicKey"];
            string secretKey = _configuration["AWS:SecretKey"];
            AmazonS3Client s3Client = new AmazonS3Client(publicKey, secretKey, config);
            return s3Client;
        }

        public async Task<string> PushToAmazonS3ViaRest(string fileNameToUpload, Stream stream)
        {
            stream.Position = 0;
            Exception error;
            try
            {
                AmazonS3Client s3Client = InitializeS3();

                var putRequest = new PutObjectRequest()
                {
                    BucketName = _configuration["AWS:ImageBucketName"],
                    Key = fileNameToUpload,
                    InputStream = stream
                };

                PutObjectResponse response2 = await s3Client.PutObjectAsync(putRequest);

                var url = "https://" + _configuration["AWS:ImageBucketName"] + ".s3." + _configuration["AWS:Region"] + ".amazonaws.com/" + fileNameToUpload;
                return url;
            }
            catch (AmazonS3Exception awsEx)
            {
                error = awsEx;
                return error.Message;
            }
            catch (Exception Ex)
            {
                error = Ex;
                return error.Message;
            }

        }

        public async Task<bool> DeleteFileS3(string filename)
        {
            try
            {
                bool Exist = await CheckFileExistInFolderS3(filename);
                if (Exist)
                {
                    string S3BucketName = _configuration["AWS:ImageBucketName"];
                    var Client = InitializeS3();
                    var Request = new DeleteObjectRequest()
                    {
                        BucketName = S3BucketName,
                        Key = filename
                    };
                    DeleteObjectResponse response = await Client.DeleteObjectAsync(Request);
                    Client.Dispose();
                    return true;
                }
                else
                {
                    return Exist;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckFileExistInFolderS3(string filename)
        {
            try
            {
                string S3BucketName = _configuration["AWS:ImageBucketName"];
                var Client = InitializeS3();
                var Request = new GetObjectMetadataRequest()
                {
                    BucketName = S3BucketName,
                    Key = filename,
                };
                GetObjectMetadataResponse response = await Client.GetObjectMetadataAsync(Request);
                ///TODO: check the meta data response
                Client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetFileURL(string fileNameToDownload)
        {
            string url = "";
            try
            {
                var Client = InitializeS3();

                var expiryUrlRequest = new GetPreSignedUrlRequest();
                expiryUrlRequest.BucketName = _configuration["AWS:ImageBucketName"];
                expiryUrlRequest.Key = fileNameToDownload;
                expiryUrlRequest.Protocol = Amazon.S3.Protocol.HTTPS;
                expiryUrlRequest.Verb = Amazon.S3.HttpVerb.GET;
                expiryUrlRequest.Expires = DateTime.Now.AddMilliseconds(50000);


                url = Client.GetPreSignedURL(expiryUrlRequest);
            }
            catch (Exception ex)
            {
                url = "File Link Error: " + ex.Message;
            }


            return url;
        }


    }
}
