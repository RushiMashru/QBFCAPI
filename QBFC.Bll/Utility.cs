using QBFC.Bll.Base;
using QBFC.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace QBFC.Bll
{
    public class Utility : IUtility
    {
        private readonly IConfiguration _configuration;
        private readonly IQbClientBll _qbClient;

        public Utility(IConfiguration configuration, IQbClientBll qbClient)
        {
            _configuration = configuration;
            _qbClient = qbClient;
        }

        public async Task<string> GetQBBillModel(BillRequestModel model)
        {
            if (model?.BillModel != null && model?.BillModel.Count != 0)
            {
                dynamic oQBBillModel = new JObject();

                oQBBillModel.DocNumber = model.BillModel[0].RefNumber;
                oQBBillModel.PrivateNote = model.BillModel[0].PrivateNote;
                oQBBillModel.TxnDate = model.BillModel[0].TxnDate;
                oQBBillModel.DueDate = model.BillModel[0].DueDate;

                oQBBillModel.SalesTermRef = new JObject();
                oQBBillModel.SalesTermRef.name = model.BillModel[0].SalesTerm;
                oQBBillModel.SalesTermRef.value = await GetSalesTermId(model.BillModel[0].SalesTerm);

                oQBBillModel.VendorRef = new JObject();
                oQBBillModel.VendorRef.name = model.BillModel[0].Vendor;
                oQBBillModel.VendorRef.value = await GetVendorId(model.BillModel[0].VendorValue);

                oQBBillModel.Line = new JArray();

                int itemCount = 0;
                foreach (var item in model.BillModel)
                {
                    itemCount++;
                    dynamic olineModel = new JObject();

                    olineModel.Id = itemCount.ToString();
                    olineModel.DetailType = "AccountBasedExpenseLineDetail";
                    olineModel.Amount = Convert.ToDouble(item.ExpenseAmount);
                    olineModel.Description = item.ExpenseDesc;

                    olineModel.AccountBasedExpenseLineDetail = new JObject();

                    olineModel.AccountBasedExpenseLineDetail.AccountRef = new JObject();
                    olineModel.AccountBasedExpenseLineDetail.AccountRef.name = item.ExpenseAccount;
                    olineModel.AccountBasedExpenseLineDetail.AccountRef.value = await GetAccountId(item.ExpenseAccount);

                    olineModel.AccountBasedExpenseLineDetail.ClassRef = new JObject();
                    olineModel.AccountBasedExpenseLineDetail.ClassRef.name = item.ExpenseClass;
                    olineModel.AccountBasedExpenseLineDetail.ClassRef.value = await GetExpenseClassId(item.ExpenseClassValue);

                    oQBBillModel.Line.Add(olineModel);
                }

                var requestJson = JsonConvert.SerializeObject(oQBBillModel);
                return Task.FromResult(requestJson);
            }

            return null;
        }

        private async Task<string> GetVendorId(string Vendor)
        {
            if (!string.IsNullOrEmpty(Vendor))
            {
                var vendor_query = $"select * from vendor where DisplayName = '{Vendor}'";

                var response = await _qbClient.GetByQuery(vendor_query);

                if (response != null && response.Success)
                {
                    var id = response.Data;
                }
            }

            return "";
        }

        private async Task<string> GetSalesTermId(string SalesTermName)
        {
            if (!string.IsNullOrEmpty(SalesTermName))
            {
                var sales_query = $"select * from Term where Name = '{SalesTermName}'";

                var response = await _qbClient.GetByQuery(sales_query);

                if (response != null && response.Success)
                {
                    var id = response.Data;
                }
            }
            
            return "";
        }

        private async Task<string> GetAccountId(string AccountName)
        {
            if (!string.IsNullOrEmpty(AccountName))
            {
                var account_query = $"select * from Account where FullyQualifiedName = '{AccountName}'";

                var response = await _qbClient.GetByQuery(account_query);

                if (response != null && response.Success)
                {
                    var id = response.Data;
                }
            }

            return "";
        }

        private async Task<string> GetExpenseClassId(string ExpenseClass)
        {
            if (!string.IsNullOrEmpty(ExpenseClass))
            {
                var class_query = $"select * from Class where FullyQualifiedName = '{ExpenseClass}'";

                var response = await _qbClient.GetByQuery(class_query);

                if (response != null && response.Success)
                {
                    var id = response.Data;
                }
            }

            return "";
        }
    }
}
