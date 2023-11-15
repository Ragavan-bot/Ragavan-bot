using DTVPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static DTVPortalAPI.Models.Helper;
using System.Data;
using System;
using System.Text.Json.Nodes;
using Amazon.Auth.AccessControlPolicy;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using DTVPortalAPI.Models.Data;
using Org.BouncyCastle.Asn1.Crmf;

namespace DTVPortalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListsController : ControllerBase
    {
        Helper _helper = new Helper();

        #region Schedule Header

        [HttpPost("GetScheduleHeader")]
        public IActionResult GetScheduleHeader(PlayList CompanyID)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "GET" },
                new Param() { Paramname = "PCompanyID", Paramvalue = CompanyID.CompanyID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = CompanyID.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleHeaderName", Paramvalue = CompanyID.ScheduleHeaderName }
            };


            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleHeader", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        [HttpPost("PostScheduleHeader")]
        public IActionResult PostScheduleHeader(PlayList ScheduleHeaderName)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "POST" },
                new Param() { Paramname = "PCompanyID", Paramvalue = ScheduleHeaderName.CompanyID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = ScheduleHeaderName.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleHeaderName", Paramvalue = ScheduleHeaderName.ScheduleHeaderName }
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleHeader", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        [HttpDelete("DeleteScheduleHeader")]
        public IActionResult DeleteScheduleHeader(PlayList ScheduleHeaderID)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "DELETE" },
                new Param() { Paramname = "PCompanyID", Paramvalue = ScheduleHeaderID.CompanyID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = ScheduleHeaderID.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleHeaderName", Paramvalue = ScheduleHeaderID.ScheduleHeaderName }
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleHeader", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        #endregion

        #region Schedule Media List

        [HttpPost("ScheduleMediaList")]
        public IActionResult GetScheduleMediaList(CategoryID categoryID)
        {

            string CombineCategoryIDS = _helper.LoadHeirarychyIDCombine(categoryID.CategoryId.ToString());
            string _query = string.Format(_helper.GetMediaListBasedOnID, CombineCategoryIDS);
            DataTable dt = _helper.mysqlDataTable_Query(_query);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        #endregion

        #region Schedule List
        [HttpPost("GetScheduleList")]
        public IActionResult GetScheduleList(PlayList playList)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "GET" },
                new Param() { Paramname = "ScheduleDetailID", Paramvalue = playList.ScheduleDetailID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = playList.ScheduleHeaderID },
                new Param() { Paramname = "OrderNo", Paramvalue = playList.OrderNo },
                new Param() { Paramname = "MediaID", Paramvalue = playList.MediaID },
                new Param() { Paramname = "MediaName", Paramvalue = playList.MediaName },
                new Param() { Paramname = "MediaType", Paramvalue = playList.MediaType },
                new Param() { Paramname = "MediaDuration", Paramvalue = playList.MediaDuration }
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleList", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        [HttpPost("PostScheduleList")]
        //public IActionResult PostScheduleList([FromBody] JsonArray ScheduleDetails)
        public IActionResult PostScheduleList(PlayList playList)
        {

           _helper.AddScheduleList(playList.ScheduleHeaderID, playList.OrderNo);
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "POST" },
                new Param() { Paramname = "ScheduleDetailID", Paramvalue = playList.ScheduleDetailID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = playList.ScheduleHeaderID },
                new Param() { Paramname = "OrderNo", Paramvalue = playList.OrderNo },
                new Param() { Paramname = "MediaID", Paramvalue = playList.MediaID },
                new Param() { Paramname = "MediaName", Paramvalue = playList.MediaName },
                new Param() { Paramname = "MediaType", Paramvalue = playList.MediaType },
                new Param() { Paramname = "MediaDuration", Paramvalue = playList.MediaDuration }
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleList", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        [HttpDelete("DeleteScheduleList")]
        public IActionResult DeleteScheduleList(PlayList playList)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "DELETE" },
                new Param() { Paramname = "ScheduleDetailID", Paramvalue = playList.ScheduleDetailID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue = playList.ScheduleHeaderID },
                new Param() { Paramname = "OrderNo", Paramvalue = playList.OrderNo },
                new Param() { Paramname = "MediaID", Paramvalue = playList.MediaID },
                new Param() { Paramname = "MediaName", Paramvalue = playList.MediaName },
                new Param() { Paramname = "MediaType", Paramvalue = playList.MediaType },
                new Param() { Paramname = "MediaDuration", Paramvalue = playList.MediaDuration }
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleList", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }

        [HttpPut("PutScheduleList")]
        public IActionResult ScheduleList(PlayList playList)
        {
            if (playList.ScheduleHeaderID != "" &&
                playList.OldOrderNo != "" &&
                playList.OrderNo != ""
                )
            {
                _helper.OrderNoUpdate(playList.OrderNo, playList.OldOrderNo, playList.ScheduleHeaderID);
                 
            }
            //// List the parameter to call the procedure
            //List<Helper.Param> paramlist = new List<Helper.Param>
            //{
            //    new Param() { Paramname = "ActionType", Paramvalue = "UPDATE" },
            //    new Param() { Paramname = "ScheduleDetailID", Paramvalue = playList.ScheduleDetailID },
            //    new Param() { Paramname = "ScheduleHeaderID", Paramvalue = playList.ScheduleHeaderID },
            //    new Param() { Paramname = "OrderNo", Paramvalue = playList.OrderNo },
            //    new Param() { Paramname = "MediaID", Paramvalue = playList.MediaID },
            //    new Param() { Paramname = "MediaName", Paramvalue = playList.MediaName },
            //    new Param() { Paramname = "MediaType", Paramvalue = playList.MediaType },
            //    new Param() { Paramname = "MediaDuration", Paramvalue = playList.MediaDuration }
            //};

            //// Execute Sql Procedure 
            //DataTable dt = _helper.mySqlProcedureExecute("ScheduleList", paramlist);

            //var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok("Updated...");

        }
         

        #endregion
    }
}
