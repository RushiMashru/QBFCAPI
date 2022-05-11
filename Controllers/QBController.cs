using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QBFC.Bll.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var response = await _qbClient.PulseCheck();

                if (string.IsNullOrEmpty(response))
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }

        [HttpPut]
        [Route("SetRToken")]
        public async Task<IActionResult> SetRToken(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest();
                }

                var response = await _qbClient.SetRToken(token);

                if (response)
                {
                    return Ok("Successfull");
                }

                return NotFound("Invalid token");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }


        [HttpGet]
        [Route("GetCompanyInfo")]
        public async Task<IActionResult> GetCompanyInfo()
        {
            try
            {
                var response = await _qbClient.GetCompanyInfo();

                if (!response.Success)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }
    }
}
