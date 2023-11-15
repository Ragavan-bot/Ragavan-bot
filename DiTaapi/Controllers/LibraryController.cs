using DTVPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static DTVPortalAPI.Models.Helper;
using System.ComponentModel.Design;
using System.Data; 
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using DTVPortalAPI.Models.Data;

namespace DTVPortalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        Helper _helper = new Helper();
        // clsAwsS3 _awss3 = new clsAwsS3();

        Aws3Services awsS3service = new Aws3Services();

        [HttpPost("GetLibraryData")]
        public IActionResult GetLibraryData(Library Library)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "PActionType", Paramvalue = "GET" },
                new Param() { Paramname = "PCategoryId", Paramvalue = Library.CategoryID},
                new Param() { Paramname = "PMediaID", Paramvalue = Library.MediaID },
                new Param() { Paramname = "PMediaName", Paramvalue = Library.MediaName },
                new Param() { Paramname = "PMediaType", Paramvalue = Library.MediaType },
                new Param() { Paramname = "PMediaDuration", Paramvalue = Library.MediaDuration },
                new Param() { Paramname = "PMediaLocation", Paramvalue = Library.MediaLocation },
                new Param() { Paramname = "PMediaSizeMB", Paramvalue = Library.MediaSizeMB },

            }; 
            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("LibraryMedia", paramlist);
            var count = dt.Rows.Count;
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        [HttpPost("PostLibraryDataFiles")]
        [RequestSizeLimit(4294967295)]
        [RequestFormLimits(MultipartBodyLengthLimit = 4294967295)]
        public async Task<IActionResult> UploadFileAsync(IFormCollection _collection)
        {
            bool Result = false;
            try
            {
                var files = HttpContext.Request.Form.Files;
                //var FileLists = _collection.Files["file"];
                var _categoryID= _collection["CategoryID"].ToString();

                foreach (var fileitem in files)
                {
                    var awss3_fileLocation = await awsS3service.UploadFileAsync(fileitem);
                    if (awss3_fileLocation != null)
                    {
                        long filesizeInBytes = fileitem.Length;
                        double filesizeMB = fileitem.Length / 1048576d;
                        var MediaName = Path.GetFileNameWithoutExtension(fileitem.FileName);
                        var MediaType = fileitem.ContentType.ToString().Split('/');

                        string MediaDuration = TimeSpan.Parse("00:00:00").ToString(); //TagLib.File.Create(awsS3service.PRD_endPointURL+awsS3service.PRD_bucketName+"/"+ awss3_fileLocation);
                        var filelocationfromAWS = awsS3service.PRD_endPointURL + awss3_fileLocation;
                        try
                        {
                            var ffProbe = new NReco.VideoInfo.FFProbe();
                            var videoInfo = ffProbe.GetMediaInfo(filelocationfromAWS);
                            MediaDuration = TimeSpan.Parse(videoInfo.Duration.ToString()).ToString(@"hh\:mm\:ss");
                        }
                        catch (Exception ex)
                        {
                        }

                        List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "PActionType", Paramvalue ="POST"},
                new Param() { Paramname = "PCategoryId", Paramvalue = _categoryID },
                new Param() { Paramname = "PMediaID", Paramvalue =null},
                new Param() { Paramname = "PMediaName", Paramvalue = MediaName },
                new Param() { Paramname = "PMediaType", Paramvalue = MediaType[1].ToString() },
                new Param() { Paramname = "PMediaDuration", Paramvalue = MediaDuration.ToString() },
                new Param() { Paramname = "PMediaLocation", Paramvalue = awss3_fileLocation },
                new Param() { Paramname = "PMediaSizeMB", Paramvalue = Convert.ToInt32(filesizeMB).ToString() }

            }; 
                        // Execute Sql Procedure 
                        DataTable dt = _helper.mySqlProcedureExecute("LibraryMedia", paramlist);

                    }
                    Result = true;
                } 
            }
            catch (Exception e) {
            return Ok(e.Message);
            }
            return (Result) ? Ok("File Uploaded...") : Ok("File Error...");
           
        }


        [HttpDelete("DeleteLibraryMedia")]
        public IActionResult DeleteLibraryMedia(Library Library)
        {
            ////List the parameter to call the procedure
            //List<Helper.Param> paramlist = new List<Helper.Param>
            //{
            //    new Param() { Paramname = "ActionType", Paramvalue = "DELETE" }, 
            //    new Param() { Paramname = "MediaID", Paramvalue = MediaID.MediaID }, 
            //};

            //// Execute Sql Procedure 
            //DataTable dt = _helper.mysqlDataTable_Query("update MediaMaster set status='0' where MediaID='"+MediaID+"'; ");
            //var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            //return Ok(json);

            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "PActionType", Paramvalue = "DELETE" },
                new Param() { Paramname = "PCategoryId", Paramvalue = Library.CategoryID},
                new Param() { Paramname = "PMediaID", Paramvalue = Library.MediaID },
                new Param() { Paramname = "PMediaName", Paramvalue = Library.MediaName },
                new Param() { Paramname = "PMediaType", Paramvalue = Library.MediaType },
                new Param() { Paramname = "PMediaDuration", Paramvalue = Library.MediaDuration },
                new Param() { Paramname = "PMediaLocation", Paramvalue = Library.MediaLocation },
                new Param() { Paramname = "PMediaSizeMB", Paramvalue = Library.MediaSizeMB },

            };
            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("LibraryMedia", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }
    }
}
