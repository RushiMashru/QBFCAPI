using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QBFC.Bll.Base;
using QBFC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBFCAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IQbClientBll _qbClient;
        private readonly IUtility _utility;

        public BillController(IQbClientBll qbClient, IUtility utility)
        {
            _qbClient = qbClient;
            _utility = utility;
        }


        [HttpPost]
        [Route("CreateBill")]
        public async Task<IActionResult> CreateQBBill(BillRequestModel billRequestModel)
        {
            try
            {
                var oQbBillJson = await _utility.GetQBBillModel(billRequestModel);

                if (string.IsNullOrEmpty(oQbBillJson) || billRequestModel.AccountId <= 0)
                {
                    return BadRequest("Invalid request check bill model");
                }

                var response = await _qbClient.CreateBill(oQbBillJson, billRequestModel.AccountId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }

        [HttpPost]
        [Route("UpsertAuth")]
        public async Task<IActionResult> UpsertAuth(AuthModel authModel)
        {
            try
            {
                if (authModel == null)
                {
                    return BadRequest("Invalid request check auth model");
                }

                var result = await _qbClient.UpsertAuthDetails(authModel);

                Response<int> response = new Response<int>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }

        [HttpGet]
        [Route("GetAuthDetails")]
        public async Task<IActionResult> GetAuthDetails(int accountId, string qbEnv)
        {
            try
            {
                if (accountId != 0 && !string.IsNullOrEmpty(qbEnv))
                {
                    var response = await _qbClient.GetAuthByAccountId(accountId, qbEnv);

                    return Ok(response);
                }

                return BadRequest("Invalid request");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }
        }

        [HttpPut]
        [Route("UpdateRefreshToken")]
        public async Task<IActionResult> UpdateRefreshToken(int Id, string RefreshToken)
        {
            try
            {
                if (Id > 0 && string.IsNullOrEmpty(RefreshToken))
                {
                    return BadRequest("Invalid request");
                }

                var result = await _qbClient.UpdateRefreshToken(Id, RefreshToken);

                Response<int> response = new Response<int>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }
    }
}