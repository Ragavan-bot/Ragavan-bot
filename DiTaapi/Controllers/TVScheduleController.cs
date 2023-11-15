using DTVPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DTVPortalAPI.Models.Helper;
using System.Data;
using DTVPortalAPI.Models.Data;
using Amazon.S3.Model.Internal.MarshallTransformations;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace DTVPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVScheduleController : ControllerBase
    {
        Helper _helper = new Helper();
        API_ScheduleList _APISchedule = new API_ScheduleList();
        ScheduleList _scheduleList = new ScheduleList();


        [HttpGet]
        public object GetList(string AssetID)
        { 
            DataTable dt = new DataTable();

            #region Test LIVE 
            // Execute Sql Procedure 
            //DataTable dt_Asset = _helper.mysqlDataTable_Query("SELECT * FROM ASSET_MASTER where ASSET_ID='" + AssetID + "';");

            //before add total duration
            //dt = _helper.mysqlDataTable_Query("select MM.* from  ASSET_MASTER AM join ASSETMAPPING AMM ON AM.ASSET_ID = AMM.AssetID join FeedMaster FM ON FM.FeedID = AMM.FeedID join ScheduleDtl SD ON SD.SchHdrID = ScheduleHdrID join MediaMaster MM ON MM.MediaID = SD.mediaID Where AM.ASSET_ID = '" + AssetID + "'; ");

            //Before HAP live data
            //dt = _helper.mysqlDataTable_Query("SELECT MM.*,SH.Duration FROM ASSET_MASTER AM  JOIN ASSETMAPPING AMM ON AM.ASSET_ID = AMM.AssetID JOIN  FeedMaster FM ON FM.FeedID = AMM.FeedID JOIN  ScheduleDtl SD ON SD.SchHdrID = FM.ScheduleHdrID JOIN MediaMaster MM ON MM.MediaID = SD.mediaID JOIN ScheduleHdr SH ON SH.ScheduleHdrID = FM.ScheduleHdrID WHERE  AM.ASSET_ID = '" + AssetID + "'; ");

            #endregion

            #region HAP Live data
            DataTable dt_Asset = _helper.mysqlDataTable_Query("SELECT ID FROM HAPLiveHierarchy where NodekeyLevel=7 And Name='" + AssetID + "';");

            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>();
            paramlist.Add(new Param() { Paramname = "@AssetID", Paramvalue = AssetID });
            // Execute Sql Procedure 
            dt = _helper.mySqlProcedureExecute("LiveScheduleList", paramlist);

            #endregion

            List<ScheduleList> lstGetClientList = new List<ScheduleList>();
            if (dt_Asset.Rows.Count == 0)
            {
                _APISchedule.Data = lstGetClientList;
                _APISchedule.Message = "Asset is not registered.";
                _APISchedule.Successs = false;
                _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
                _APISchedule.TotalMediaSize = 0;
            }

            if (dt.Rows.Count > 0)
            {
                 
                lstGetClientList = (from DataRow dr in dt.Rows
                                    select new ScheduleList()
                                    {
                                        //MediaID = Convert.ToInt32(dr["CategoryID"]),
                                        MediaID = Convert.ToInt32(dr["MediaID"]),
                                        MediaName = dr["MediaName"].ToString(),
                                        MediaType = dr["MediaType"].ToString(),
                                        MediaDuration = dr["MediaDuration"].ToString(),
                                        MediaLocation = _helper.PRD_endPointURL + dr["MediaLocation"].ToString(),
                                        MediaSize = Convert.ToInt32(dr["MediaSizeMB"])
                                    }).ToList();

                 Int32 TotalMediaSizecalc = dt.AsEnumerable().Sum(row => row.Field<int>("MediaSizeMB"));
                if (lstGetClientList.Count > 0)
                {

                    _APISchedule.Data = lstGetClientList;
                    _APISchedule.Message = "Data Found";
                    _APISchedule.Successs = true;
                    _APISchedule.TotalDuration = TimeSpan.Parse(dt.Rows[0]["Duration"].ToString());
                    _APISchedule.TotalMediaSize = TotalMediaSizecalc;
                 
                }

                else
                {
                    _APISchedule.Data = lstGetClientList;
                    _APISchedule.Message = "Data Not found.";
                    _APISchedule.Successs = true;
                    _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
                    _APISchedule.TotalMediaSize = 0;
                }
            }
            else
            {
                _APISchedule.Data = lstGetClientList;
                _APISchedule.Message = "Data Not found.";
                _APISchedule.Successs = true;
                _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
                _APISchedule.TotalMediaSize = 0;
            } 
            return _APISchedule; 
        }

        //[HttpPost("TVSchedule"), Authorize]
        //public object GetList(TVScheduleModel AssetID)
        //{
        //    DataTable dt = new DataTable();

        //    #region Test LIVE 
        //    // Execute Sql Procedure 
        //    //DataTable dt_Asset = _helper.mysqlDataTable_Query("SELECT * FROM ASSET_MASTER where ASSET_ID='" + AssetID + "';");

        //    //before add total duration
        //    //dt = _helper.mysqlDataTable_Query("select MM.* from  ASSET_MASTER AM join ASSETMAPPING AMM ON AM.ASSET_ID = AMM.AssetID join FeedMaster FM ON FM.FeedID = AMM.FeedID join ScheduleDtl SD ON SD.SchHdrID = ScheduleHdrID join MediaMaster MM ON MM.MediaID = SD.mediaID Where AM.ASSET_ID = '" + AssetID + "'; ");

        //    //Before HAP live data
        //    //dt = _helper.mysqlDataTable_Query("SELECT MM.*,SH.Duration FROM ASSET_MASTER AM  JOIN ASSETMAPPING AMM ON AM.ASSET_ID = AMM.AssetID JOIN  FeedMaster FM ON FM.FeedID = AMM.FeedID JOIN  ScheduleDtl SD ON SD.SchHdrID = FM.ScheduleHdrID JOIN MediaMaster MM ON MM.MediaID = SD.mediaID JOIN ScheduleHdr SH ON SH.ScheduleHdrID = FM.ScheduleHdrID WHERE  AM.ASSET_ID = '" + AssetID + "'; ");

        //    #endregion

        //    #region HAP Live data
        //    DataTable dt_Asset = _helper.mysqlDataTable_Query("SELECT ID FROM HAPLiveHierarchy where NodekeyLevel=7 And Name='" + AssetID.AssetID + "';");

        //    // List the parameter to call the procedure
        //    List<Helper.Param> paramlist = new List<Helper.Param>();
        //    paramlist.Add(new Param() { Paramname = "@AssetID", Paramvalue = AssetID.AssetID });
        //    // Execute Sql Procedure 
        //    dt = _helper.mySqlProcedureExecute("LiveScheduleList", paramlist);

        //    #endregion

        //    List<ScheduleList> lstGetClientList = new List<ScheduleList>();
        //    if (dt_Asset.Rows.Count == 0)
        //    {
        //        _APISchedule.Data = lstGetClientList;
        //        _APISchedule.Message = "Asset is not registered.";
        //        _APISchedule.Successs = false;
        //        _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        lstGetClientList = (from DataRow dr in dt.Rows
        //                            select new ScheduleList()
        //                            {
        //                                //MediaID = Convert.ToInt32(dr["CategoryID"]),
        //                                MediaID = Convert.ToInt32(dr["MediaID"]),
        //                                MediaName = dr["MediaName"].ToString(),
        //                                MediaType = dr["MediaType"].ToString(),
        //                                MediaDuration = dr["MediaDuration"].ToString(),
        //                                MediaLocation = _helper.PRD_endPointURL + dr["MediaLocation"].ToString(),
        //                                MediaSize = Convert.ToInt32(dr["MediaSizeMB"])
        //                            }).ToList();


        //        if (lstGetClientList.Count > 0)
        //        {
        //            _APISchedule.Data = lstGetClientList;
        //            _APISchedule.Message = "Data Found";
        //            _APISchedule.Successs = true;
        //            _APISchedule.TotalDuration = TimeSpan.Parse(dt.Rows[0]["Duration"].ToString());

        //        }

        //        else
        //        {
        //            _APISchedule.Data = lstGetClientList;
        //            _APISchedule.Message = "Data Not found.";
        //            _APISchedule.Successs = true;
        //            _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
        //        }
        //    }
        //    else
        //    {
        //        _APISchedule.Data = lstGetClientList;
        //        _APISchedule.Message = "Data Not found.";
        //        _APISchedule.Successs = true;
        //        _APISchedule.TotalDuration = TimeSpan.Parse("00:00:00");
        //    }



        //    return _APISchedule;




        //}

    }
}
