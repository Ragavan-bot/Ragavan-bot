using DTVPortalAPI.Models;
using DTVPortalAPI.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using static DTVPortalAPI.Models.Helper;

namespace DTVPortalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortalLoginController : ControllerBase
    {
        Helper _helper = new Helper();

        //[HttpGet("PortalLogin")]
        //public IActionResult PortalLoginvalidate  (string username,string password)
        //{
        //    // List the parameter to call the procedure
        //    List<Helper.Param> paramlist = new List<Helper.Param>();
        //    paramlist.Add(new Param() { Paramname = "Pusername", Paramvalue = username });
        //    paramlist.Add(new Param() { Paramname = "PPassword", Paramvalue = password });
        //    // Execute Sql Procedure 
        //   DataTable  dt = _helper.mySqlProcedureExecute("PortalLogin", paramlist);
        //    bool validation = false;
        //    if(dt.Rows.Count>0)
        //        validation = true;
        //    return Ok(validation);
        //}

        [HttpPost("login"), Authorize]
        public IActionResult Portallogin(login login)
        {

            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>();
            paramlist.Add(new Param() { Paramname = "Pusername", Paramvalue = login.PuserName });
            paramlist.Add(new Param() { Paramname = "PPassword", Paramvalue = login.Ppassword });

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("PortalLogin", paramlist);
            bool validation = false;
            if (dt.Rows.Count > 0)
                validation = true;

            return Ok(dt);
        }

        [HttpPost("GetUserCreation")]
        public IActionResult GetUserCreation(UserCreation userCreation)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "GET" },
                new Param() { Paramname = "UserID", Paramvalue = userCreation.UserID},
                new Param() { Paramname = "UserName", Paramvalue = userCreation.UserName},
                new Param() { Paramname = "Password", Paramvalue = userCreation.Password },
                new Param() { Paramname = "MailAddress", Paramvalue = userCreation.MailAddress },
                new Param() { Paramname = "MobileNumber", Paramvalue = userCreation.MobileNumber },
                new Param() { Paramname = "UserTypeId", Paramvalue = userCreation.UserTypeID }

            };


            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("UserMasterCreation", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }
        [HttpPost("GetUserType")]
        public IActionResult GetUserType(UserCreation userCreation)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "GETUSERTYPE" },
                new Param() { Paramname = "UserID", Paramvalue = userCreation.UserID},
                new Param() { Paramname = "UserName", Paramvalue = userCreation.UserName},
                new Param() { Paramname = "Password", Paramvalue = userCreation.Password },
                new Param() { Paramname = "MailAddress", Paramvalue = userCreation.MailAddress },
                new Param() { Paramname = "MobileNumber", Paramvalue = userCreation.MobileNumber },
                new Param() { Paramname = "UserTypeId", Paramvalue = userCreation.UserTypeID }

            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("UserMasterCreation", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        [HttpPost("PostUserCreation")]
        public IActionResult PostUserCreation(UserCreation userCreation)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "POST" },
                new Param() { Paramname = "UserID", Paramvalue = userCreation.UserID},
                new Param() { Paramname = "UserName", Paramvalue = userCreation.UserName},
                new Param() { Paramname = "Password", Paramvalue = userCreation.Password },
                new Param() { Paramname = "MailAddress", Paramvalue = userCreation.MailAddress },
                new Param() { Paramname = "MobileNumber", Paramvalue = userCreation.MobileNumber },
                new Param() { Paramname = "UserTypeId", Paramvalue = userCreation.UserTypeID }

            };
            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("UserMasterCreation", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }


        [HttpPut("PutUserCreation")]
        public IActionResult PutUserCreation(UserCreation userCreation)
        {
            DataTable dt = new DataTable();
            try
            {
                // List the parameter to call the procedure
                List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "PUT" },
                new Param() { Paramname = "UserID", Paramvalue = userCreation.UserID==null ?string.Empty: userCreation.UserID},
                new Param() { Paramname = "UserName", Paramvalue = userCreation.UserName==null ?string.Empty: userCreation.UserName  },
                new Param() { Paramname = "Password", Paramvalue =userCreation.Password==null ?string.Empty: userCreation.Password   },
                new Param() { Paramname = "MailAddress", Paramvalue =userCreation.MailAddress==null ?string.Empty: userCreation.MailAddress     },
                new Param() { Paramname = "MobileNumber", Paramvalue =userCreation.MobileNumber==null ?string.Empty: userCreation.MobileNumber   },
                new Param() { Paramname = "UserTypeId", Paramvalue = userCreation.UserTypeID==null ?string.Empty: userCreation.UserTypeID  }

            };
                dt = _helper.mySqlProcedureExecute("UserMasterCreation", paramlist);
            }
            catch (Exception ex)
            {

            }

            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

        [HttpDelete("DeleteUserType")]
        public IActionResult DeleteUserType(UserCreation userCreation)
        {
            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>
            {
                new Param() { Paramname = "ActionType", Paramvalue = "DELETE" },
                new Param() { Paramname = "UserID", Paramvalue = userCreation.UserID},
                new Param() { Paramname = "UserName", Paramvalue = userCreation.UserName},
                new Param() { Paramname = "Password", Paramvalue = userCreation.Password },
                new Param() { Paramname = "MailAddress", Paramvalue = userCreation.MailAddress },
                new Param() { Paramname = "MobileNumber", Paramvalue = userCreation.MobileNumber },
                new Param() { Paramname = "UserTypeId", Paramvalue = userCreation.UserTypeID }

            };

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("UserMasterCreation", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }


    }
}
