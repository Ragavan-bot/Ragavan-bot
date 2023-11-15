using DTVPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DTVPortalAPI.Models.Helper;
using System.Data;
using MySqlX.XDevAPI.Relational;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using DTVPortalAPI.Models.Data;

namespace DTVPortalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        Helper _helper = new Helper();
        [HttpPost("HAPLiveHierarchy")]
        public async Task<OkObjectResult> HAPLiveHierarchy(Global CompanyID)
        {

            // List the parameter to call the procedure
            List<Helper.Param> paramlist = new List<Helper.Param>();
            paramlist.Add(new Param() { Paramname = "PCompanyID", Paramvalue = CompanyID.CompanyID });

            // Execute Sql Procedure 
            DataTable dt = _helper.mySqlProcedureExecute("HAPLiveHierarchy", paramlist);
            var json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return Ok(json);
        }

    }
}
