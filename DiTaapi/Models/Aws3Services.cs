using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3;
using System.Net;
using Amazon;
using System.Text.RegularExpressions;
using MySqlX.XDevAPI;
using System;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Mvc;

namespace DTVPortalAPI.Models
{
    public class Aws3Services
    { 
        private readonly string _bucketName;
        private readonly IAmazonS3 _awsS3Client;
  
        // PRD
        public string PRD_endPointURL = "https://hapbusinessportal.s3.ap-south-1.amazonaws.com/";
        public string PRD_bucketName = "hapbusinessportal";
        public string PRD_subFolder = "DigitalTVAPP";
        private string PRD_accessKey = "AKIAVTCEQX2TSD7VGJQ6";
        private string PRD_secretKey = "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";
        RegionEndpoint PRD_region = RegionEndpoint.APSouth1;

        public Aws3Services()
        {
            _bucketName = PRD_bucketName;
            _awsS3Client = new AmazonS3Client(PRD_accessKey, PRD_secretKey, PRD_region);
        }

        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream ms = null;

            try
            {
                //using (var response = await _awsS3Client.GetObjectAsync(AwsS3Request.S3GetObjectRequest(_bucketName, file)))
                //{
                //    if (response.HttpStatusCode == HttpStatusCode.OK)
                //    {
                //        using (ms = new MemoryStream())
                //        {
                //            await response.ResponseStream.CopyToAsync(ms);
                //        }
                //    }
                //}

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                string destPath = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + ReplaceWhitespace(file.FileName, "");
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key =  PRD_subFolder + "/" + destPath,
                        BucketName = _bucketName ,
                        ContentType = file.ContentType
                    };
                    
                    var fileTransferUtility = new TransferUtility(_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return PRD_subFolder + "/" + destPath;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, string versionId = "")
        {
            try
            {
                if (!IsFileExists(fileName, versionId))
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileName));

                if (string.IsNullOrEmpty(versionId))
                {
                    await DeleteFile(fileName, versionId);

                    return true;
                }

                var listVersionsRequest = new ListVersionsRequest { BucketName = _bucketName, Prefix = fileName };

                var listVersionsResponse = _awsS3Client.ListVersionsAsync(listVersionsRequest).Result;

                foreach (S3ObjectVersion versionIDs in listVersionsResponse.Versions)
                {
                    if (versionIDs.IsDeleteMarker)
                    {
                        await DeleteFile(fileName, versionIDs.VersionId);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DeleteFile(string fileName, string versionId)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            if (!string.IsNullOrEmpty(versionId))
                request.VersionId = versionId;

            await _awsS3Client.DeleteObjectAsync(request);
        }

        public bool IsFileExists(string fileName, string versionId)
        {
            try
            {
                GetObjectMetadataRequest request = new GetObjectMetadataRequest()
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    VersionId = !string.IsNullOrEmpty(versionId) ? versionId : null
                };

                var response = _awsS3Client.GetObjectMetadataAsync(request).Result;

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
                {
                    if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                        return false;

                    else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                        return false;
                }

                throw;
            }
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
