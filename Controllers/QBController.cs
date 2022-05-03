using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QBFC.Bll.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QBFCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QBController : ControllerBase
    {
        private readonly IQbClientBll _qbClient;

        public QBController(IQbClientBll qbClient)
        {
            _qbClient = qbClient;
        }


        [HttpGet]
        [Route("PulseCheck")]
        public async Task<IActionResult> PulseCheck()
        {
            var response = await _qbClient.PulseCheck();

            if (string.IsNullOrEmpty(response))
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
