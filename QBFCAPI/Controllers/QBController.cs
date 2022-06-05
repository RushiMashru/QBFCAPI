﻿using Microsoft.AspNetCore.Mvc;
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

        
        [HttpGet]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var response = await _qbClient.GetCustomerById(id);

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
        public async Task<IActionResult> GetVendorById(int id)
        {
            try
            {
                var response = await _qbClient.GetVendorById(id);

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
        [Route("GetAccount")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var response = await _qbClient.GetAccountById(id);

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
        public async Task<IActionResult> CreateBill([FromBody] object requestbody)
        {
            try
            {
                var content = JsonConvert.SerializeObject(requestbody, Formatting.Indented);

                if (!string.IsNullOrWhiteSpace(content))
                {
                    var response = await _qbClient.CreateBill(content);

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
        public async Task<IActionResult> GetBillById(int id)
        {
            try
            {
                var response = await _qbClient.GetBillById(id);

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
