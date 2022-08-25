using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QBFC.Bll.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBFCAPI.Controllers
{
    [Route("api/v1/[controller]")]
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
        public async Task<IActionResult> PulseCheck(int AccountId)
        {
            try
            {
                if (AccountId <= 0)
                    return BadRequest("Invalid AccountId");

                var response = await _qbClient.PulseCheck(AccountId);

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
        public async Task<IActionResult> GetCompanyInfo(int AccountId)
        {
            try
            {
                if (AccountId <= 0)
                    return BadRequest("Invalid AccountId");

                var response = await _qbClient.GetCompanyInfo(AccountId);

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


        [HttpGet]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomerById(int CustomerId, int AccountId)
        {
            try
            {
                if (CustomerId <= 0 || AccountId <= 0)
                    return BadRequest("Invalid Request");

                var response = await _qbClient.GetCustomerById(CustomerId, AccountId);

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


        [HttpGet]
        [Route("GetVendor")]
        public async Task<IActionResult> GetVendorById(int VendorId, int AccountId)
        {
            try
            {
                if (VendorId <= 0 || AccountId <= 0)
                    return BadRequest("Invalid Request");

                var response = await _qbClient.GetVendorById(VendorId, AccountId);

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

        [HttpGet]
        [Route("GetExpenceAccount")]
        public async Task<IActionResult> GetExpenceAccountById(int ExpenceAccountId, int AccountId)
        {
            try
            {
                if (ExpenceAccountId <= 0 || AccountId <= 0)
                    return BadRequest("Invalid Request");

                var response = await _qbClient.GetExpenceAccountById(ExpenceAccountId, AccountId);

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

        [HttpPost]
        [Route("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] object requestbody, int AccountId)
        {
            try
            {
                var content = JsonConvert.SerializeObject(requestbody, Formatting.Indented);

                if (!string.IsNullOrWhiteSpace(content) && AccountId <= 0)
                {
                    var response = await _qbClient.CreateBill(content, AccountId);

                    if (!response.Success)
                    {
                        return Unauthorized();
                    }

                    return Ok(response);
                }
                else
                {
                    return BadRequest("Invalid request body");
                }

            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }

        [HttpGet]
        [Route("GetBill")]
        public async Task<IActionResult> GetBillById(int BillId, int AccountId)
        {
            try
            {
                if (BillId <= 0 || AccountId <= 0)
                    return BadRequest("Invalid Request");

                var response = await _qbClient.GetBillById(BillId, AccountId);

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
