
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace DTVPortalAPI.Models
{
    public class Helper : Aws3Services
    {
        public Helper()
        {
            CompanyID = 1;
        }
        //Quality
        //public static string mysqlconstr = "server ='54.175.39.204';Port=3306; database = 'hap_digitaltv_signs'; uid = 'sportsuser';password = 'ClsS@12345';Connect Timeout=100";

        //Production
        public static string mysqlconstr = "server ='192.168.1.76';Port=3306; database = 'hap_digitaltv_signs'; uid = 'clss';password = 'clss1234';Connect Timeout=300";

        public int CompanyID { get; set; }
        public string GetLibraryData = "select * from HAPLiveHierarchy where CompanyID='{0}';";
        public string GetMediaListBasedOnID = "select MM.MediaID,MM.MediaType,MM.MediaName,MM.MediaDuration,MM.MediaSizeMB,MM.MediaLocation,SL.Name,SL.ID from MediaMaster as MM join HAPLiveHierarchy SL ON MM.categoryID=SL.ID where MM.categoryID in ({0})";
        public string scheduleDtl_Delete = "delete from ScheduleDtl where SchHdrID ={0};";

        public string getSchedulefromFPCDate = "SELECT *,weekday(StartDate) as 'DayName'  FROM appointments where SCHLibraryId='{0}' and Date(StartDate)>='{1}' and Date(EndDate) <='{2}' order by StartDate ";
        public string InsertFPCDB = "insert ignore  into appointments (Type, StartDate, EndDate, AllDay, Subject, Location, Description, Status, Label, ResourceId,ScheduleHdrID, SCHLibraryId) select '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' where not exists (select * from appointments where Type='{0}' and  StartDate='{1}' and  EndDate='{2}' and  AllDay='{3}' and  Subject='{4}' and Location='{5}' and  Description='{6}' and  Status='{7}' and Label='{8}' and  ResourceId='{9}' and ScheduleHdrID='{10}' and  SCHLibraryId='{11}');";


        public string BAusername = "DotnetDTV";
        public string BApassword = "DNDTV#092023";

        public MySqlConnection DB_Connection()
        {
            MySqlConnection Connection = new MySqlConnection();
            //Connection.ConnectionString = "server ='" + Properties.Settings.Default.DB_ServerIP + "';" + "database = '" + Properties.Settings.Default.DB_Database + "';" + "uid = '" + Properties.Settings.Default.DB_UserName + "';" + "password = '" + Properties.Settings.Default.DB_Password + "'; Connect Timeout=500";
            Connection.ConnectionString = mysqlconstr;
            return Connection;
        }
        public DataTable mySqlProcedureExecute(string query, List<Param> Parameter)
        {
            DataTable dt = new DataTable();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = DB_Connection();
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (Param pp in Parameter)
            {
                cmd.Parameters.AddWithValue(pp.Paramname, pp.Paramvalue);
            }
            MySqlDataAdapter dat = new MySqlDataAdapter(cmd);
            dat.Fill(dt);
            return dt;
        }
        public DataTable mysqlDataTable_Query(string query)
        {

            MySqlConnection Connection = DB_Connection();
            Connection.Open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(query, Connection);
            DataTable DT = new DataTable();
            Adapter.Fill(DT);
            Connection.Close();
            return DT;
        }


        public string GetUUID()
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            return myuuidAsString;
        }
        private static Guid GuidFromString(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(input));
                return new Guid(hash);
            }
        }
        public class Param
        {
            public string Paramname { get; set; }
            public string Paramvalue { get; set; }
        }
        public static object ConfigurationManager { get; private set; }
        //public async Task <TimeSpan> GetVideoDuration(string filePath)
        //{

        //    try
        //    {
        //        filePath = "https://hapbusinessportal.s3.ap-south-1.amazonaws.com/DigitalTVAPP/20230303_200136_IBACO_-_Blackforest_Ice_Cream_Cakes.mp4";
        //        IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);
        //    var videoDuration = mediaInfo.VideoStreams.First().Duration;
        //    return videoDuration;

        //        using (var shell = ShellObject.FromParsingName(filePath))
        //        {
        //            IShellProperty prop = shell.Properties.System.Media.Duration;
        //            var t = (ulong)prop.ValueAsObject;

        //            return TimeSpan.FromTicks((long)t);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //   return TimeSpan.Parse("00:00:00");  
        //}

        public string LoadHeirarychyIDCombine(string RootCategoryID)
        {
            string CombineIDs = string.Empty;
            string _query = string.Format(GetLibraryData, CompanyID);
            DataTable _dtGetLibraryData = mysqlDataTable_Query(_query);
            //Get only heirarchy ids
            var _CombineIDString = HeirarchyIDchiltoParent(RootCategoryID, _dtGetLibraryData, "");

            return _CombineIDString;
        }
        public DataTable ChildrenOf(string ID, DataTable dtFolders, string CombineIDs)
        {
            DataTable result = new DataTable();
            try
            {
                if (dtFolders != null)
                {
                    result = dtFolders.Clone();
                Repeat:
                    foreach (DataRow child in dtFolders.Rows)
                    {
                        if (child["ID"].ToString() == ID)
                        {
                            CombineIDs += ID;
                            result.Rows.Add(child.ItemArray);
                            ID = child["ParentID"].ToString();
                            if (ID != "0")
                                goto Repeat;
                        }

                    }

                }
            }
            catch (Exception)
            { }
            return result;
        }

        private string HeirarchyIDchiltoParent(string ID, DataTable dtHeirarchy, string CombineIDs)
        {

            List<String> list = new List<String>();
            if (dtHeirarchy != null)
            {
            Repeat:
                foreach (DataRow child in dtHeirarchy.Rows)
                {
                    if (child["ID"].ToString() == ID)
                    {
                        list.Add(ID);
                        ID = child["ParentID"].ToString();
                        if (ID != "0")
                            goto Repeat;
                    }
                }
            }

            return string.Join(",", list);
        }


        public string selectscheduledtl = "SELECT * FROM scheduledtl where schhdrid='{0}' and orderno between '{1}' and '{2}' order by OrderNo asc;";
        public string updatescheduledtl = "update scheduledtl set OrderNo='{0}' where SchDtlID='{1}'";

        public string selectScheduleListAdd = "SELECT * FROM scheduledtl where schhdrid='{0}' and orderno >= '{1}' order by OrderNo asc;";

        public void AddScheduleList(string ScheduleHeaderID, string OrderNo)
        {
            DataTable dtOrnumberUpdate = mysqlDataTable_Query(string.Format(selectScheduleListAdd, ScheduleHeaderID, OrderNo));// mysqlDataTable_Query("SELECT * FROM scheduledtl where schhdrid='" + ScheduleHeaderID + "' and orderno between '" + oldOrderNo + "' and '" + NewOrderNo + "';");
            for (int i = 0; i < dtOrnumberUpdate.Rows.Count; i++)
            {
                int Neworderno = Convert.ToInt32(dtOrnumberUpdate.Rows[i]["OrderNo"]) + 1;
                mysqlDataTable_Query(string.Format(updatescheduledtl, Neworderno, dtOrnumberUpdate.Rows[i]["SchDtlID"].ToString()));
            }
        }
        public void OrderNoUpdate(string OrderNo, string OldOrderNo, string ScheduleHeaderID)
        {
            //string _query = string.Format(_masterQuery, _clientID);
            //DataTable _dtable = _helper.mysqlDataTable_Query(_query);

            int oldOrderNo = Convert.ToInt32(OldOrderNo);
            int NewOrderNo = Convert.ToInt32(OrderNo);
            if (oldOrderNo < NewOrderNo)
            {
                DataTable dtOrnumberUpdate = mysqlDataTable_Query(string.Format(selectscheduledtl, ScheduleHeaderID, oldOrderNo, NewOrderNo));// mysqlDataTable_Query("SELECT * FROM scheduledtl where schhdrid='" + ScheduleHeaderID + "' and orderno between '" + oldOrderNo + "' and '" + NewOrderNo + "';");
                for (int i = 0; i < dtOrnumberUpdate.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        mysqlDataTable_Query(string.Format(updatescheduledtl, NewOrderNo, dtOrnumberUpdate.Rows[i]["SchDtlID"].ToString()));
                    }
                    else
                    {
                        //sortedTasks[i].OrderIndex++;
                        int Neworderno = Convert.ToInt32(dtOrnumberUpdate.Rows[i]["OrderNo"]) - 1;
                        mysqlDataTable_Query(string.Format(updatescheduledtl, Neworderno, dtOrnumberUpdate.Rows[i]["SchDtlID"].ToString()));
                    }
                };

            }
            else
            {
                DataTable dtOrnumberUpdate = mysqlDataTable_Query(string.Format(selectscheduledtl, ScheduleHeaderID, NewOrderNo, oldOrderNo));

                for (int i = 0; i < dtOrnumberUpdate.Rows.Count; i++)
                {
                    if (i == dtOrnumberUpdate.Rows.Count - 1)
                    {
                        mysqlDataTable_Query(string.Format(updatescheduledtl, NewOrderNo, dtOrnumberUpdate.Rows[i]["SchDtlID"].ToString()));
                    }
                    else
                    {
                        //sortedTasks[i].OrderIndex++;
                        int Neworderno = Convert.ToInt32(dtOrnumberUpdate.Rows[i]["OrderNo"]) + 1;
                        mysqlDataTable_Query(string.Format(updatescheduledtl, Neworderno, dtOrnumberUpdate.Rows[i]["SchDtlID"].ToString()));
                    }

                };
            }

        }

        public string datetimeconvertion(string datetime)
        {

            string result = DateTime.Parse(datetime).ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        public DateTime[] GetDatesBetween(DateTime startDate, DateTime endDate, DayOfWeek DayName)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayName)
                    allDates.Add(date);
            }
            return allDates.ToArray();
        }

    }

}
