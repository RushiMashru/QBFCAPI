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

                if (string.IsNullOrEmpty(oQbBillJson))
                {
                    return BadRequest("Invalid request check bill model");
                }

                var response = await _qbClient.CreateBill(oQbBillJson);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: ex.Source, statusCode: 500, title: "Error");
            }

        }
    }
}