using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Amazon.S3.Transfer;
using System.Text.RegularExpressions;

namespace DTVPortalAPI.Models
{
    class clsAwsS3
    {
        IAmazonS3 _s3Client;


        public clsAwsS3()
        {
            login();
        }

        // PRD
        public string PRD_endPointURL = "https://hapbusinessportal.s3.ap-south-1.amazonaws.com/";
        private string PRD_bucketName = "hapbusinessportal";
        private string PRD_subFolder = "DigitalTVAPP";
        private string PRD_accessKey = "AKIAVTCEQX2TSD7VGJQ6";
        private string PRD_secretKey = "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";
        RegionEndpoint PRD_region = RegionEndpoint.APSouth1;

        private void login()
        {
            _s3Client = new AmazonS3Client(PRD_accessKey, PRD_secretKey, PRD_region);
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            try
            {   
                string destPath = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName;

                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = file.OpenReadStream(),
                    BucketName = PRD_bucketName ,
                    Key = destPath
                };
                PutObjectResponse response = await _s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }


        public string UploadFilesAsync(IFormFile file)
        {
            //var bucketExists = _s3Client.DoesS3BucketExistAsync(PRD_bucketName);
            string destPath = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + ReplaceWhitespace(file.FileName,"_");
            var request = new PutObjectRequest()
            {
                InputStream = file.OpenReadStream(),
                BucketName = PRD_bucketName + "/" + PRD_subFolder,
                Key = destPath
            };
            request.Metadata.Add("Content-Type", file.ContentType);
           var respo= _s3Client.PutObjectAsync(request);

            return PRD_subFolder + "/" + destPath.ToString();
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
