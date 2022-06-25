using QBFC.Bll.Base;
using QBFC.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace QBFC.Bll
{
    public class Utility : IUtility
    {
        public Task<string> GetQBBillModel(BillRequestModel model)
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
                oQBBillModel.SalesTermRef.value = model.BillModel[0].SalesTermValue;

                oQBBillModel.VendorRef = new JObject();
                oQBBillModel.VendorRef.name = model.BillModel[0].Vendor;
                oQBBillModel.VendorRef.value = model.BillModel[0].VendorValue;

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
                    olineModel.AccountBasedExpenseLineDetail.AccountRef.value = item.ExpenseAccountValue;

                    olineModel.AccountBasedExpenseLineDetail.ClassRef = new JObject();
                    olineModel.AccountBasedExpenseLineDetail.ClassRef.name = item.ExpenseClass;
                    olineModel.AccountBasedExpenseLineDetail.ClassRef.value = item.ExpenseClassValue;

                    oQBBillModel.Line.Add(olineModel);
                }

                var requestJson = JsonConvert.SerializeObject(oQBBillModel);
                return Task.FromResult(requestJson);
            }

            return null;
        }
    }
}
