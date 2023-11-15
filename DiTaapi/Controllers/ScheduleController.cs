using DTVPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static DTVPortalAPI.Models.Helper;
using System.Data;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Authorization;
using DTVPortalAPI.Models.Data;
using System.ComponentModel.Design;
using System.Text;

namespace DTVPortalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        Helper _helper = new Helper();

        [HttpPost("GetLoadSchedule")]
        //public IActionResult GetLoadSchedule(CategoryID CategoryId)
        //{
        //// List the parameter to call the procedure
        //List<Helper.Param> paramlist = new List<Helper.Param>();
        //paramlist.Add(new Param() { Paramname = "PSchedulerLibraryId", Paramvalue = CategoryId.CategoryId.ToString() });

        //// Execute Sql Procedure 
        //DataTable dt = _helper.mySqlProcedureExecute("GetLoadSchedule", paramlist);
        //var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
        //return Ok(json);
        //}
        public IActionResult GetLoadSchedule(Schedule schedule)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "GET" },
                new Param() { Paramname = "ID", Paramvalue = schedule.ID},
                new Param() { Paramname = "StartDate", Paramvalue = schedule.StartDate!=null?null : schedule.StartDate.ToString() },
                new Param() { Paramname = "EndDate", Paramvalue = schedule.EndDate!=null?null:schedule.EndDate.ToString()},
                new Param() { Paramname = "AllDay", Paramvalue = schedule.AllDay },
                new Param() { Paramname = "Subject", Paramvalue = schedule.Subject },
                new Param() { Paramname = "Location", Paramvalue = schedule.Location },
                new Param() { Paramname = "Description", Paramvalue = schedule.Description },
                new Param() { Paramname = "ResourceID", Paramvalue = schedule.ResourceID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue =schedule.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleLibraryID", Paramvalue = schedule.ScheduleLibraryID}
            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleLive", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);


        }

        [HttpPost("PostSaveSchedule")]
        public IActionResult PostSaveSchedule(Schedule schedule)
        {
            string startDate = "", endDate = "";
            if (schedule.StartDate != null)
            {
                startDate = _helper.datetimeconvertion(schedule.StartDate.ToString());// DateTime.ParseExact(schedule.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");
                endDate = _helper.datetimeconvertion(schedule.EndDate.ToString());// DateTime.ParseExact(schedule.EndDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");
            }
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "POST" },
                new Param() { Paramname = "ID", Paramvalue = schedule.ID},
                new Param() { Paramname = "StartDate", Paramvalue = startDate},
                new Param() { Paramname = "EndDate", Paramvalue = endDate},
                new Param() { Paramname = "AllDay", Paramvalue = schedule.AllDay },
                new Param() { Paramname = "Subject", Paramvalue = schedule.Subject },
                new Param() { Paramname = "Location", Paramvalue = schedule.Location },
                new Param() { Paramname = "Description", Paramvalue = schedule.Description },
                new Param() { Paramname = "ResourceID", Paramvalue = schedule.ResourceID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue =schedule.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleLibraryID", Paramvalue = schedule.ScheduleLibraryID}
            };
            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleLive", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        [HttpDelete("DeleteSchedule")]
        public IActionResult DeleteSchedule(Schedule schedule)
        {

            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "DELETE" },
                new Param() { Paramname = "ID", Paramvalue = schedule.ID},
                new Param() { Paramname = "StartDate", Paramvalue = schedule.StartDate!=null?null : schedule.StartDate.ToString() },
                new Param() { Paramname = "EndDate", Paramvalue = schedule.EndDate!=null?null:schedule.EndDate.ToString()},
                new Param() { Paramname = "AllDay", Paramvalue = schedule.AllDay },
                new Param() { Paramname = "Subject", Paramvalue = schedule.Subject },
                new Param() { Paramname = "Location", Paramvalue = schedule.Location },
                new Param() { Paramname = "Description", Paramvalue = schedule.Description },
                new Param() { Paramname = "ResourceID", Paramvalue = schedule.ResourceID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue =schedule.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleLibraryID", Paramvalue = schedule.ScheduleLibraryID}
            };
            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleLive", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);

        }
        [HttpPut("PutSchedule")]
        public IActionResult PutSchedule(Schedule schedule)
        {
            string startDate = "", endDate = "";
            if (schedule.StartDate != null)
            {
                startDate = _helper.datetimeconvertion(schedule.StartDate.ToString());// DateTime.ParseExact(schedule.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");
                endDate = _helper.datetimeconvertion(schedule.EndDate.ToString());// DateTime.ParseExact(schedule.EndDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");
            }
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "UPDATE" },
                new Param() { Paramname = "ID", Paramvalue = schedule.ID},
                new Param() { Paramname = "StartDate", Paramvalue = startDate},
                new Param() { Paramname = "EndDate", Paramvalue =endDate},
                new Param() { Paramname = "AllDay", Paramvalue = schedule.AllDay },
                new Param() { Paramname = "Subject", Paramvalue = schedule.Subject },
                new Param() { Paramname = "Location", Paramvalue = schedule.Location },
                new Param() { Paramname = "Description", Paramvalue = schedule.Description },
                new Param() { Paramname = "ResourceID", Paramvalue = schedule.ResourceID },
                new Param() { Paramname = "ScheduleHeaderID", Paramvalue =schedule.ScheduleHeaderID },
                new Param() { Paramname = "ScheduleLibraryID", Paramvalue = schedule.ScheduleLibraryID}
            };
            DataTable dt = _helper.mySqlProcedureExecute("ScheduleLive", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        [HttpPost("CopySchedule")]
        public IActionResult CopySchedule(CopySchedule copySchedule)
        {
            string CstartDate = "", CendDate = "", AstartDate = "", AendDate = "";
            if (copySchedule.CopyStartDate != null && copySchedule.CopyEndDate != null)
            {
                CstartDate = _helper.datetimeconvertion(copySchedule.CopyStartDate.ToString());// DateTime.ParseExact(schedule.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");
                CendDate = _helper.datetimeconvertion(copySchedule.CopyEndDate.ToString());// DateTime.ParseExact(schedule.EndDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd hh:mm:ss");

                var sdate = DateTime.Parse(copySchedule.ActiveStartDate.ToString());
                var edate = DateTime.Parse(copySchedule.ActiveEndDate.ToString());
                sdate = new DateTime(sdate.Year, sdate.Month, sdate.Day, 0, 0, 0);
                edate = new DateTime(edate.Year, edate.Month, edate.Day, 23, 59, 59);

                AstartDate = _helper.datetimeconvertion(sdate.ToString());
                AendDate = _helper.datetimeconvertion(edate.ToString());
            }

            if (copySchedule.TVName == null)
            {
                DataTable DTActiveDateSchedule = _helper.mysqlDataTable_Query(string.Format(_helper.getSchedulefromFPCDate, copySchedule.ScheduleLibraryID, AstartDate, AendDate));
                StringBuilder _SB = new StringBuilder();
                for (int count = 0; count < DTActiveDateSchedule.Rows.Count; count++)
                {
                    _SB.Clear();
                    var list = _helper.GetDatesBetween(DateTime.Parse(CstartDate), DateTime.Parse(CendDate), DateTime.Parse(DTActiveDateSchedule.Rows[count]["StartDate"].ToString()).DayOfWeek);
                    foreach (DateTime _date in list)
                    {


                        DateTime _startdatecalc = DateTime.Parse(_date.ToShortDateString()) + DateTime.Parse(DTActiveDateSchedule.Rows[count]["StartDate"].ToString()).TimeOfDay;
                        DateTime _Enddatecalc = DateTime.Parse(_date.ToShortDateString()) + DateTime.Parse(DTActiveDateSchedule.Rows[count]["EndDate"].ToString()).TimeOfDay;
                        _SB.AppendFormat(string.Format(_helper.InsertFPCDB, "0", _startdatecalc.ToString("yyyy-MM-dd HH:mm:ss"), _Enddatecalc.ToString("yyyy-MM-dd HH:mm:ss"), "0", DTActiveDateSchedule.Rows[count]["Subject"].ToString(), DTActiveDateSchedule.Rows[count]["Location"].ToString(), DTActiveDateSchedule.Rows[count]["Description"].ToString(), DTActiveDateSchedule.Rows[count]["Status"].ToString(), DTActiveDateSchedule.Rows[count]["Label"].ToString(), "0", DTActiveDateSchedule.Rows[count]["ScheduleHdrID"].ToString(), DTActiveDateSchedule.Rows[count]["SCHLibraryId"].ToString()) + Environment.NewLine);
                    }
                    _helper.mysqlDataTable_Query(_SB.ToString());
                }
            }

            // DataTable dt = _helper.mySqlProcedureExecute("ScheduleLive", paramlist);
            //var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            //return Ok(json);

            return Ok("");
        }

    }
}

 