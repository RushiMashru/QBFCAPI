using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QBFCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QBController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            OAuth2Client oauthClient =
               new OAuth2Client("ABG7Es1CuYrdN8bqulLHniRgZi3EdS8oZCo74csmrP3CiARpwn", "8GhqRPVdcRlV1Gz0UlIug1R9UXWS2FcGXdhJ6RJE", "https://localhost:44385/", "sandbox");

            var refToken = "AB11660283249O1M3RjIlPkv5HLm4R7c2GjIPusFSp5Vcf3k9Q";

           
            var tokenResponseFromRef = await oauthClient.RefreshTokenAsync(refToken);

            if(string.IsNullOrEmpty(tokenResponseFromRef.AccessToken) && string.IsNullOrEmpty(tokenResponseFromRef.RefreshToken))
            {
                return Unauthorized();
            }          
          
            return Ok("Pulse Check !!");
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
