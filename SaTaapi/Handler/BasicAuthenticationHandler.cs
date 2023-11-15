using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using CLSSContentSchedulerSTT_API.Models.Helper;

namespace DTVPortalAPI.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //private VigneshContext _context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            //_context = context;
        }
        public string BAusername = "DotnetDTV";
        public string BApassword = "DNDTV#092023";

        Helper _helper = new Helper();
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Empty Header");
            var _headerKey = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(_headerKey.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] _array = credentials.Split(':');
                string username = _array[0];
                string password = _array[1];
                //var _user = _helper.BAusername==username;
                //var _pass=_helper.BApassword==password;

                if (BAusername != username || BApassword != password)
                    return AuthenticateResult.Fail("User Password is invalid");
                var Claims = new[] { new Claim(ClaimTypes.Name, username) };
                var Identity = new ClaimsIdentity(Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(Identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("");
        }
    }
}