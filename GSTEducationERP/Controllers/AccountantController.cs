using GSTEducationERPLibrary.Account;
using GSTEducationERPLibrary.Accountant;
using GSTEducationERPLibrary.Bind;
using GSTEducationERPLibrary.Placement;
using GSTEducationERPLibrary.Trainer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Util;

namespace GSTEducationERP.Controllers
{
    public class AccountantController : Controller
    {
        private readonly BALAccountant objbal = new BALAccountant();
        private readonly Accountant objac = new Accountant();
        public class BreadcrumbItem
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        // GET: Accountant

        #region 
        //Shreyas's Region  About Dashboard and Voucher Submodules Starts Here
        public async Task<ActionResult> AccountantDashboardAsyncSGS(int month = 0)
        {
            if (Session["StaffCode"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page if staff code is not found in session
            } else
            {
                DataSet ds = new DataSet();
                int date = DateTime.Now.Month;

                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.GetDashData(1);

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        TotalIncome = row.IsNull("TotalCredit") ? 0.0m : decimal.Parse(row["TotalCredit"].ToString()),
                        TotalExpenses = row.IsNull("TotalDebit") ? 0.0m : decimal.Parse(row["TotalDebit"].ToString()),
                        CashInHand = row.IsNull("CashInHand") ? 0.0m : decimal.Parse(row["CashInHand"].ToString()),
                        BankBalance = row.IsNull("BankBalance") ? 0.0m : decimal.Parse(row["BankBalance"].ToString())
                    };


                    AddExpense1.Add(objtn);
                    objtr1 = objtn;

                }


                // Return the view with the populated model
                return View(objtr1);
            }
        }
        
        // Action to fetch cash on hand for a specific date
        [HttpPost]
        public async Task<JsonResult> GetCashOnHandForDate(DateTime date)
        {
            var cashOnHand = await GetCashInHandAsync(date);
            return Json(cashOnHand.ToString("C", new CultureInfo("hi-IN")));
        }

        // Action to fetch bank balance for a specific account
        [HttpPost]
        public async Task<JsonResult> GetBankBalance(int accountId)
        {
            var bankBalance = await GetBankBalanceAsync(accountId);
            return Json(bankBalance.ToString("C", new CultureInfo("hi-IN")));
        }





        private async Task<decimal> GetCashInHandAsync(DateTime date)
        {
            // Replace with actual logic to fetch cash in hand for a specific date
            return await Task.FromResult(50000);  // Example value
        }

        private async Task<List<Accountant>> GetBankAccountsAsync()
        {
            // Replace with actual logic to fetch bank accounts
            return await Task.FromResult(new List<Accountant>
            {
                new Accountant { AccountId = 1, AccountName = "HDFC Bank - Savings" },
                new Accountant { AccountId = 2, AccountName = "SBI Bank - Current" }
            });
        }

        private async Task<decimal> GetBankBalanceAsync(int accountId)
        {
            // Replace with actual logic to fetch bank balance for a specific account
            return await Task.FromResult(75000);  // Example value
        }
        [HttpGet]
        public async Task<ActionResult> ListAllVouchersAsyncSGS()
        {
            //List<Accountant> vouchers = await GetVouchersList();
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
            {
                //new BreadcrumbItem { Name = "Home", Url = "/" },
                new BreadcrumbItem { Name = "AccountantDashboard", Url = "AccountantDashboardAsyncSGS" },
               new BreadcrumbItem { Name = "Voucher Managment", Url = "ListAllVouchersAsyncSGS" }, // Adjust URL as needed
            };

                ViewBag.Breadcrumbs = breadcrumbs;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListVoucherAsyncSGS()
        {
            if (Session["StaffCode"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page if staff code is not found in session
            } else
            {
                string staffCode = Session["StaffCode"].ToString(); // Retrieve staff code from session
                                                                    //string branchCode = Session["BranchCode"].ToString(); // Retrieve branch code from session

                DataSet ds = new DataSet();

                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.ListVoucherAsyncSGS();

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        VoucherId = row.IsNull("VoucherId") ? 0 : Convert.ToInt32(row["VoucherId"]),
                        VoucherCode = row.IsNull("VoucherCode") ? string.Empty : row["VoucherCode"].ToString(),

                        // Determine AmountReceiver based on VendorName and StaffName
                        AmountReceiver = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? row["VendorName"].ToString() :
                     !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? row["StaffName"].ToString() : string.Empty,

                        // Determine AmountReceiverType based on VendorName and StaffName
                        AmountReceiverType = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? "Vendor" :
                         !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? "Staff" : string.Empty,

                        VendorName = row.IsNull("VendorName") ? string.Empty : row["VendorName"].ToString(),
                        Amount = row.IsNull("Amount") ? 0.0m : decimal.Parse(row["Amount"].ToString()),
                        AmountPaidTo = row.IsNull("StaffName") ? string.Empty : row["StaffName"].ToString(),
                        Description = row.IsNull("Description") ? string.Empty : row["Description"].ToString(),
                        PaymentMode = row.IsNull("PaymentMode") ? string.Empty : row["PaymentMode"].ToString(),
                        BankId = row.IsNull("BankId") ? 0 : Convert.ToInt32(row["BankId"]),
                        ReceiverBankAccountNumber = row.IsNull("ReceiverBankAccountNumber") ? 0 : Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()),
                        ReceiverBankAccountHolderName = row.IsNull("ReceiverBankAccountHolderName") ? string.Empty : row["ReceiverBankAccountHolderName"].ToString(),
                        ReceiverBankIFSCCode = row.IsNull("ReceiverBankIFSCCode") ? string.Empty : row["ReceiverBankIFSCCode"].ToString(),
                        ReceiverBankName = row.IsNull("ReceiverBankName") ? string.Empty : row["ReceiverBankName"].ToString(),
                        ChequeDate = row.IsNull("ChequeDate") ? DateTime.MinValue : DateTime.Parse(row["ChequeDate"].ToString()),
                        Balance = row.IsNull("Balance") ? 0.0m : decimal.Parse(row["Balance"].ToString()),
                        Currency = row.IsNull("Currency") ? string.Empty : row["Currency"].ToString(),
                        TransactionId = row.IsNull("TransactionId_ChequeNo") ? string.Empty : row["TransactionId_ChequeNo"].ToString(),
                        VoucherType = row.IsNull("VoucherType") ? string.Empty : row["VoucherType"].ToString(),
                        VoucherDate = row.IsNull("VoucherDate") ? DateTime.MinValue : DateTime.Parse(row["VoucherDate"].ToString()),
                        StaffCode = row.IsNull("StaffCode") ? string.Empty : row["StaffCode"].ToString(),
                        StatusId = row.IsNull("StatusId") ? 0 : Convert.ToInt32(row["StatusId"])
                    };


                    AddExpense1.Add(objtn);
                }

                objtr1.lstVoucher = AddExpense1;

                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
            {
                //new BreadcrumbItem { Name = "Home", Url = "/" },
                new BreadcrumbItem { Name = "TrainerDashboard", Url = "TrainerDashboardAsyncTS/Trainer" },
               new BreadcrumbItem { Name = "Test Managment", Url = "ListTestManagementAsynchTS/Trainer" }, // Adjust URL as needed
            };

                ViewBag.Breadcrumbs = breadcrumbs;
                return PartialView("ListVoucherAsyncSGS", objtr1);
            }
        }


        [HttpGet]
        public async Task<ActionResult> ListPendingVoucherAsyncSGS()
        {
            if (Session["StaffCode"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page if staff code is not found in session
            } else
            {
                string staffCode = Session["StaffCode"].ToString(); // Retrieve staff code from session
                                                                    //string branchCode = Session["BranchCode"].ToString(); // Retrieve branch code from session

                DataSet ds = new DataSet();

                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.ListPendingVoucherAsyncSGS();

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        VoucherId = row.IsNull("VoucherId") ? 0 : Convert.ToInt32(row["VoucherId"]),
                        VoucherCode = row.IsNull("VoucherCode") ? string.Empty : row["VoucherCode"].ToString(),
                        // Determine AmountReceiver based on VendorName and StaffName
                        AmountReceiver = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? row["VendorName"].ToString() :
                     !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? row["StaffName"].ToString() : string.Empty,

                        // Determine AmountReceiverType based on VendorName and StaffName
                        AmountReceiverType = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? "Vendor" :
                         !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? "Staff" : string.Empty,
                        VendorName = row.IsNull("VendorName") ? string.Empty : row["VendorName"].ToString(),
                        Amount = row.IsNull("Amount") ? 0.0m : decimal.Parse(row["Amount"].ToString()),
                        AmountPaidTo = row.IsNull("StaffName") ? string.Empty : row["StaffName"].ToString(),
                        Description = row.IsNull("Description") ? string.Empty : row["Description"].ToString(),
                        PaymentMode = row.IsNull("PaymentMode") ? string.Empty : row["PaymentMode"].ToString(),
                        BankId = row.IsNull("BankId") ? 0 : Convert.ToInt32(row["BankId"]),
                        ReceiverBankAccountNumber = row.IsNull("ReceiverBankAccountNumber") ? 0 : Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()),
                        ReceiverBankAccountHolderName = row.IsNull("ReceiverBankAccountHolderName") ? string.Empty : row["ReceiverBankAccountHolderName"].ToString(),
                        ReceiverBankIFSCCode = row.IsNull("ReceiverBankIFSCCode") ? string.Empty : row["ReceiverBankIFSCCode"].ToString(),
                        ReceiverBankName = row.IsNull("ReceiverBankName") ? string.Empty : row["ReceiverBankName"].ToString(),
                        ChequeDate = row.IsNull("ChequeDate") ? DateTime.MinValue : DateTime.Parse(row["ChequeDate"].ToString()),
                        Balance = row.IsNull("Balance") ? 0.0m : decimal.Parse(row["Balance"].ToString()),
                        Currency = row.IsNull("Currency") ? string.Empty : row["Currency"].ToString(),
                        TransactionId = row.IsNull("TransactionId_ChequeNo") ? string.Empty : row["TransactionId_ChequeNo"].ToString(),
                        VoucherType = row.IsNull("VoucherType") ? string.Empty : row["VoucherType"].ToString(),
                        VoucherDate = row.IsNull("VoucherDate") ? DateTime.MinValue : DateTime.Parse(row["VoucherDate"].ToString()),
                        StaffCode = row.IsNull("StaffCode") ? string.Empty : row["StaffCode"].ToString(),
                        StatusId = row.IsNull("StatusId") ? 0 : Convert.ToInt32(row["StatusId"])
                    };

                    AddExpense1.Add(objtn);
                }

                objtr1.lstVoucher = AddExpense1;

                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
            {
                //new BreadcrumbItem { Name = "Home", Url = "/" },
                new BreadcrumbItem { Name = "TrainerDashboard", Url = "TrainerDashboardAsyncTS/Trainer" },
               new BreadcrumbItem { Name = "Test Managment", Url = "ListTestManagementAsynchTS/Trainer" }, // Adjust URL as needed
            };

                ViewBag.Breadcrumbs = breadcrumbs;
                return PartialView("ListPendingVoucherAsyncSGS", objtr1);
            }
        }
        [HttpGet]
        public async Task<ActionResult> ListCompletedVoucherAsyncSGS()
        {
            if (Session["StaffCode"] == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login page if staff code is not found in session
            } else
            {
                string staffCode = Session["StaffCode"].ToString(); // Retrieve staff code from session
                string branchCode = Session["BranchCode"].ToString(); // Retrieve branch code from session

                DataSet ds = new DataSet();

                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.ListCompletedVoucherAsyncSGS();

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        VoucherId = row.IsNull("VoucherId") ? 0 : Convert.ToInt32(row["VoucherId"]),
                        VoucherCode = row.IsNull("VoucherCode") ? string.Empty : row["VoucherCode"].ToString(),
                        // Determine AmountReceiver based on VendorName and StaffName
                        AmountReceiver = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? row["VendorName"].ToString() :
                        !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? row["StaffName"].ToString() : string.Empty,

                        // Determine AmountReceiverType based on VendorName and StaffName
                        AmountReceiverType = !row.IsNull("VendorName") && !string.IsNullOrEmpty(row["VendorName"].ToString()) ? "Vendor" :
                         !row.IsNull("StaffName") && !string.IsNullOrEmpty(row["StaffName"].ToString()) ? "Staff" : string.Empty,
                        VendorName = row.IsNull("VendorName") ? string.Empty : row["VendorName"].ToString(),
                        Amount = row.IsNull("Amount") ? 0.0m : decimal.Parse(row["Amount"].ToString()),
                        AmountPaidTo = row.IsNull("StaffName") ? string.Empty : row["StaffName"].ToString(),
                        Description = row.IsNull("Description") ? string.Empty : row["Description"].ToString(),
                        PaymentMode = row.IsNull("PaymentMode") ? string.Empty : row["PaymentMode"].ToString(),
                        BankId = row.IsNull("BankId") ? 0 : Convert.ToInt32(row["BankId"]),
                        ReceiverBankAccountNumber = row.IsNull("ReceiverBankAccountNumber") ? 0 : Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()),
                        ReceiverBankAccountHolderName = row.IsNull("ReceiverBankAccountHolderName") ? string.Empty : row["ReceiverBankAccountHolderName"].ToString(),
                        ReceiverBankIFSCCode = row.IsNull("ReceiverBankIFSCCode") ? string.Empty : row["ReceiverBankIFSCCode"].ToString(),
                        ReceiverBankName = row.IsNull("ReceiverBankName") ? string.Empty : row["ReceiverBankName"].ToString(),
                        ChequeDate = row.IsNull("ChequeDate") ? DateTime.MinValue : DateTime.Parse(row["ChequeDate"].ToString()),
                        Balance = row.IsNull("Balance") ? 0.0m : decimal.Parse(row["Balance"].ToString()),
                        Currency = row.IsNull("Currency") ? string.Empty : row["Currency"].ToString(),
                        TransactionId = row.IsNull("TransactionId_ChequeNo") ? string.Empty : row["TransactionId_ChequeNo"].ToString(),
                        VoucherType = row.IsNull("VoucherType") ? string.Empty : row["VoucherType"].ToString(),
                        VoucherDate = row.IsNull("VoucherDate") ? DateTime.MinValue : DateTime.Parse(row["VoucherDate"].ToString()),
                        StaffCode = row.IsNull("StaffCode") ? string.Empty : row["StaffCode"].ToString(),
                        StatusId = row.IsNull("StatusId") ? 0 : Convert.ToInt32(row["StatusId"])
                    };

                    AddExpense1.Add(objtn);
                }

                objtr1.lstVoucher = AddExpense1;

                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
            {
                //new BreadcrumbItem { Name = "Home", Url = "/" },
                new BreadcrumbItem { Name = "AccountantDashboard", Url = "AccountantDashboardAsyncSGS/Accountant" },
               new BreadcrumbItem { Name = "Voucher Managment", Url = "ListAllVouchersAsyncSGS/Accountant" }, // Adjust URL as needed
            };

                ViewBag.Breadcrumbs = breadcrumbs;

                return PartialView("ListCompletedVoucherAsyncSGS", objtr1);
            }
        }

        [HttpGet]
        public async Task<ActionResult> AddVoucherAsyncSGS(int voucherType, decimal amount)
        {
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                objac.StaffCode = Session["StaffCode"].ToString(); // Retrieve staff code from session
                objac.BranchCode = Session["BranchCode"].ToString(); // Retrieve branch code from session

                // Process the Voucher Type and amount here if needed
                // You can store these in the objac object or use them directly

                // Voucher Type List
                List<SelectListItem> VoucherTypeList = new List<SelectListItem>();
                DataSet ds = await objbal.VoucherTypeAsyncSGS(objac);
                List<SelectListItem> TypeList = new List<SelectListItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TypeList.Add(new SelectListItem
                    {
                        Text = dr["VoucherType"].ToString(),
                        Value = dr["VoucherTypeId"].ToString()
                    });
                }
                VoucherTypeList.AddRange(TypeList);
                ViewBag.VoucherTypeList = VoucherTypeList;


                // Voucher Code
                objac.VoucherCode = await objbal.GetMaxVoucherCodeAsyncSGS(objac);
                ViewBag.VoucherNumber = objac.VoucherCode; // This line might be redundant now

                // Staff List
                List<SelectListItem> combinedReportingList = new List<SelectListItem>();
                DataSet ds1 = await objbal.StaffNameforVoucherAsyncSGS(objac);
                List<SelectListItem> StaffList = new List<SelectListItem>();
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    StaffList.Add(new SelectListItem
                    {
                        Text = dr["StaffName"].ToString(),
                        Value = dr["StaffCode"].ToString()
                    });
                }
                combinedReportingList.AddRange(StaffList);
                ViewBag.combinedReportingList = combinedReportingList;

                // Bank Account List
                List<SelectListItem> BankAccountList = new List<SelectListItem>();
                DataSet ds2 = await objbal.BankAccountforVoucherAsyncSGS(objac);
                List<SelectListItem> BankList = new List<SelectListItem>();
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    string bankName = dr["BankName"].ToString();
                    string accountHolderName = dr["AccountHolderName"].ToString();
                    string accountNumber = dr["AccountNumber"].ToString();

                    BankList.Add(new SelectListItem
                    {
                        Text = $"{bankName} - {accountHolderName} - {accountNumber}",
                        Value = dr["BankId"].ToString()
                    });
                }
                BankAccountList.AddRange(BankList);
                ViewBag.BankAccountList = BankAccountList;

                // Breadcrumbs
                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Name = "AccountantDashboard", Url = "AccountantDashboardAsyncSGS" },
                    new BreadcrumbItem { Name = "Voucher Managment", Url = "ListAllVouchersAsyncSGS" },
                    new BreadcrumbItem { Name = "Add Voucher", Url = "AddCashVoucherAsyncSGS" },
                };

                ViewBag.Breadcrumbs = breadcrumbs;

                // You can also pass the voucherType and amount to the view if needed
                ViewBag.SelectedVoucherType = voucherType;
                ViewBag.Amount = amount;

                return PartialView(objac);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddVoucherAsyncSGS(Accountant objA)
        {
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                try
                {
                    objA.StaffCode = Session["StaffCode"].ToString();
                    objA.BranchCode = Session["BranchCode"].ToString();
                    await objbal.AddVoucherAsyncSGS(objA);
                    return RedirectToAction("ListAllVouchersAsyncSGS");
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while saving data: " + ex.Message });
                }
            }

        }

        [HttpGet]
        public async Task<ActionResult> DetailsVoucherSGS(string id)
        {
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                // Retrieve staff code and branch code from session
                objac.StaffCode = Session["StaffCode"].ToString();
                objac.BranchCode = Session["BranchCode"].ToString();

                DataSet ds = new DataSet();
                objac.VoucherCode = id;
                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.GetVoucherDataSGS(objac);

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        VoucherId = row.IsNull("VoucherId") ? 0 : Convert.ToInt32(row["VoucherId"]),
                        VoucherCode = row.IsNull("VoucherCode") ? string.Empty : row["VoucherCode"].ToString(),
                        VendorName = row.IsNull("VendorName") ? string.Empty : row["VendorName"].ToString(),
                        Amount = row.IsNull("Amount") ? 0.0m : decimal.Parse(row["Amount"].ToString()),
                        AmountPaidTo = row.IsNull("StaffName") ? string.Empty : row["StaffName"].ToString(),
                        Description = row.IsNull("Description") ? string.Empty : row["Description"].ToString(),
                        PaymentMode = row.IsNull("PaymentMode") ? string.Empty : row["PaymentMode"].ToString(),
                        BankId = row.IsNull("BankId") ? 0 : Convert.ToInt32(row["BankId"]),
                        BankName = row.IsNull("SelfBank") ? string.Empty : row["SelfBank"].ToString(),
                        ReceiverBankAccountNumber = row.IsNull("ReceiverBankAccountNumber") ? 0 : Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()),
                        ReceiverBankAccountHolderName = row.IsNull("ReceiverBankAccountHolderName") ? string.Empty : row["ReceiverBankAccountHolderName"].ToString(),
                        ReceiverBankIFSCCode = row.IsNull("ReceiverBankIFSCCode") ? string.Empty : row["ReceiverBankIFSCCode"].ToString(),
                        ReceiverBankName = row.IsNull("ReceiverBankName") ? string.Empty : row["ReceiverBankName"].ToString(),
                        ChequeDate = row.IsNull("ChequeDate") ? DateTime.MinValue : DateTime.Parse(row["ChequeDate"].ToString()),
                        Balance = row.IsNull("Balance") ? 0.0m : decimal.Parse(row["Balance"].ToString()),
                        Currency = row.IsNull("Currency") ? string.Empty : row["Currency"].ToString(),
                        TransactionId = row.IsNull("TransactionId_ChequeNo") ? string.Empty : row["TransactionId_ChequeNo"].ToString(),
                        VoucherType = row.IsNull("VoucherType") ? string.Empty : row["VoucherType"].ToString(),
                        VoucherDate = row.IsNull("VoucherDate") ? DateTime.MinValue : DateTime.Parse(row["VoucherDate"].ToString()),
                        StaffCode = row.IsNull("StaffCode") ? string.Empty : row["StaffCode"].ToString(),
                        StatusId = row.IsNull("StatusId") ? 0 : Convert.ToInt32(row["StatusId"])
                    };
                    objtr1 = objtn;
                    AddExpense1.Add(objtn);
                }

                // Set the list of vouchers in the Accountant object
                objtr1.lstVoucher = AddExpense1;

                // Create breadcrumb items
                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
                {
                    // new BreadcrumbItem { Name = "Home", Url = "/" },
                    new BreadcrumbItem { Name = "AccountantDashboard", Url = "AccountantDashboardAsyncSGS/Accountant" },
                    new BreadcrumbItem { Name = "Voucher Management", Url = "ListAllVouchersAsyncSGS/Accountant" }, // Adjust URL as needed
                };

                ViewBag.Breadcrumbs = breadcrumbs;

                // Pass the Accountant object to the partial view
                return PartialView("DetailsVoucherSGS", objtr1);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteVoucher(string voucherId)
        {
            try
            {
                if (Session["StaffCode"] == null)
                {
                    return RedirectToAction("Login", "Account");
                } else
                {
                    Accountant objT = new Accountant();
                    objT.VoucherCode = voucherId;
                    objT.BranchCode = Session["BranchCode"].ToString();
                    objT.StaffCode = Session["StaffCode"].ToString();
                    await objbal.RemoveDataVoucher(objT);
                    return Json(new { success = true, message = "Voucher deleted successfully" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> CollectVoucherAsyncSGS(string id)
        {
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                // Retrieve staff code and branch code from session
                objac.StaffCode = Session["StaffCode"].ToString();
                objac.BranchCode = Session["BranchCode"].ToString();

                DataSet ds = new DataSet();
                objac.VoucherCode = id;
                // Pass staff code and branch code to the method for fetching tests
                ds = await objbal.GetVoucherDataSGS(objac);

                Accountant objtr1 = new Accountant();
                List<Accountant> AddExpense1 = new List<Accountant>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Accountant objtn = new Accountant
                    {
                        VoucherId = row.IsNull("VoucherId") ? 0 : Convert.ToInt32(row["VoucherId"]),
                        VoucherCode = row.IsNull("VoucherCode") ? string.Empty : row["VoucherCode"].ToString(),
                        VendorName = row.IsNull("VendorName") ? string.Empty : row["VendorName"].ToString(),
                        Amount = row.IsNull("Amount") ? 0.0m : decimal.Parse(row["Amount"].ToString()),
                        AmountPaidTo = row.IsNull("StaffName") ? string.Empty : row["StaffName"].ToString(),
                        Description = row.IsNull("Description") ? string.Empty : row["Description"].ToString(),
                        PaymentMode = row.IsNull("PaymentMode") ? string.Empty : row["PaymentMode"].ToString(),
                        BankId = row.IsNull("BankId") ? 0 : Convert.ToInt32(row["BankId"]),
                        BankName = row.IsNull("SelfBank") ? string.Empty : row["SelfBank"].ToString(),
                        ReceiverBankAccountNumber = row.IsNull("ReceiverBankAccountNumber") ? 0 : Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()),
                        ReceiverBankAccountHolderName = row.IsNull("ReceiverBankAccountHolderName") ? string.Empty : row["ReceiverBankAccountHolderName"].ToString(),
                        ReceiverBankIFSCCode = row.IsNull("ReceiverBankIFSCCode") ? string.Empty : row["ReceiverBankIFSCCode"].ToString(),
                        ReceiverBankName = row.IsNull("ReceiverBankName") ? string.Empty : row["ReceiverBankName"].ToString(),
                        ChequeDate = row.IsNull("ChequeDate") ? DateTime.MinValue : DateTime.Parse(row["ChequeDate"].ToString()),
                        Balance = row.IsNull("Balance") ? 0.0m : decimal.Parse(row["Balance"].ToString()),
                        Currency = row.IsNull("Currency") ? string.Empty : row["Currency"].ToString(),
                        TransactionId = row.IsNull("TransactionId_ChequeNo") ? string.Empty : row["TransactionId_ChequeNo"].ToString(),
                        VoucherType = row.IsNull("VoucherType") ? string.Empty : row["VoucherType"].ToString(),
                        VoucherDate = row.IsNull("VoucherDate") ? DateTime.MinValue : DateTime.Parse(row["VoucherDate"].ToString()),
                        StaffCode = row.IsNull("StaffCode") ? string.Empty : row["StaffCode"].ToString(),
                        StatusId = row.IsNull("StatusId") ? 0 : Convert.ToInt32(row["StatusId"])
                    };
                    objtr1 = objtn;
                    AddExpense1.Add(objtn);
                }

                // Set the list of vouchers in the Accountant object
                objtr1.lstVoucher = AddExpense1;

                // Create breadcrumb items
                List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
                {
                    // new BreadcrumbItem { Name = "Home", Url = "/" },
                    new BreadcrumbItem { Name = "AccountantDashboard", Url = "AccountantDashboardAsyncSGS/Accountant" },
                    new BreadcrumbItem { Name = "Voucher Management", Url = "ListAllVouchersAsyncSGS/Accountant" }, // Adjust URL as needed
                };

                ViewBag.Breadcrumbs = breadcrumbs;

                // Pass the Accountant object to the partial view
                return PartialView("CollectVoucherAsyncSGS", objtr1);
            }
        }
        #endregion //Shreyas's Region About Dashboard and Voucher Submodules End here 
        #region
        // Shreyas's Not Using Code 

        //[HttpGet]
        //public async Task<ActionResult> UpdateVoucherAsyncSGS(string id)
        //{
        //    if (Session["StaffCode"] == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    } else
        //    {
        //        Accountant objT = new Accountant();
        //        objT.VoucherCode = id;
        //        objT.BranchCode = Session["BranchCode"]?.ToString();
        //        SqlDataReader row;
        //        row = await objbal.FetchVoucherAsyncPG(objT);
        //        while (row.Read())
        //        {
        //            objT.VoucherCode = row["VoucherCode"] != DBNull.Value ? row["VoucherCode"].ToString() : string.Empty;
        //            objT.VendorName = row["VendorName"] != DBNull.Value ? row["VendorName"].ToString() : string.Empty;
        //            objT.Amount = row["Amount"] != DBNull.Value ? float.Parse(row["Amount"].ToString()) : 0;
        //            objT.AmountPaidTo = row["AmountPaidTo"] != DBNull.Value ? row["AmountPaidTo"].ToString() : string.Empty;
        //            objT.Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : string.Empty;
        //            objT.PaymentMode = row["PaymentMode"] != DBNull.Value ? row["PaymentMode"].ToString() : string.Empty;
        //            objT.BankId = row["BankId"] != DBNull.Value ? Convert.ToInt32(row["BankId"].ToString()) : 0;
        //            objT.ReceiverBankAccountNumber = row["ReceiverBankAccountNumber"] != DBNull.Value ? Convert.ToInt64(row["ReceiverBankAccountNumber"].ToString()) : 0;
        //            objT.ReceiverBankAccountHolderName = row["ReceiverBankAccountHolderName"] != DBNull.Value ? row["ReceiverBankAccountHolderName"].ToString() : string.Empty;
        //            objT.ReceiverBankIFSCCode = row["ReceiverBankIFSCCode"] != DBNull.Value ? row["ReceiverBankIFSCCode"].ToString() : string.Empty;
        //            objT.ReceiverBankName = row["ReceiverBankName"] != DBNull.Value ? row["ReceiverBankName"].ToString() : string.Empty;
        //            objT.ChequeDate = row["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(row["ChequeDate"].ToString()) : DateTime.MinValue;
        //            objT.Balance = row["Balance"] != DBNull.Value ? float.Parse(row["Balance"].ToString()) : 0;
        //            objT.Currency = row["Currency"] != DBNull.Value ? row["Currency"].ToString() : string.Empty;
        //            objT.TransactionId = row["TransactionId_ChequeNo"] != DBNull.Value ? row["TransactionId_ChequeNo"].ToString() : string.Empty;
        //            objT.VoucherType = row["VoucherType"] != DBNull.Value ? row["VoucherType"].ToString() : string.Empty;
        //            objT.VoucherDate = row["VoucherDate"] != DBNull.Value ? Convert.ToDateTime(row["VoucherDate"].ToString()) : DateTime.MinValue;
        //            objT.StaffCode = row["StaffCode"] != DBNull.Value ? row["StaffCode"].ToString() : string.Empty;
        //            objT.StatusId = row["StatusId"] != DBNull.Value ? Convert.ToInt32(row["StatusId"].ToString()) : 0;
        //        }
        //        objT.StaffCode = Session["StaffCode"]?.ToString();

        //        string Staffpostion = Session["StaffPositionId"]?.ToString();
        //        List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>
        //{
        //    new BreadcrumbItem { Name = "CoordinatorDashboard", Url = Url.Action("CoordinatorDashboardPRAsync","Coordinator") },
        //    new BreadcrumbItem { Name = "Task Management", Url = Url.Action("ListTaskManagementAsyncPG", "Bind") },
        //    new BreadcrumbItem { Name = "UpdateTaskManagement", Url = Url.Action("UpdateTaskManagementAsyncPG", "Bind") }
        //};
        //        ViewBag.Breadcrumb = breadcrumbs;
        //        return PartialView("UpdateVoucherAsyncSGS", objT);
        //    }
        //}

        /// <summary>
        /// Updates task management details asynchronously.
        /// </summary>
        /// <param name="objT">The Trainer object containing the updated task management details.</param>
        /// <returns>An asynchronous ActionResult representing the result of the update process.</returns>
        /// 
        //[HttpPost]
        //public async Task<ActionResult> UpdateVoucherAsyncSGS(Accountant objT)
        //{
        //    try
        //    {
        //        if (Session["StaffCode"] == null)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        } else
        //        {
        //            objT.StaffCode = Session["StaffCode"].ToString();
        //            await objbal.UpdateCashVoucherAsyncSGS(objT);
        //            return Json(new { success = true, message = "Data saved successfully" });
        //        }
        //    } catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = "An error occurred while saving data: " + ex.Message });
        //    }
        //}
        #endregion
        [HttpGet]
        public async Task<ActionResult> AddExpensesAsyncMB()
        {
            if (Session["StaffCode"] == null)
            {
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            } else
            {
                Accountant obj = new Accountant();
                obj.Date = DateTime.Now;
                await GetExpenceCategoryMB();
                await GetRefundCandidate();
                await GetReferenceByStudentsAsyncMB();
                await ListVoucherAsyncMB();
                await GetStaffNameAsyncMB();
                return PartialView(obj);
            }

        }
        [HttpGet]
        public async Task GetExpenceCategoryMB()
        {
            DataSet ds = await objbal.GetExpenceCategoryMB();
            List<SelectListItem> Courselist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Courselist.Add(new SelectListItem { Text = dr["ExpenseCategory"].ToString(), Value = dr["ExpenseCategoryId"].ToString() });
            }
            await Task.Run(() => ViewBag.CourseList = Courselist);

        }
        [HttpGet]

        public async Task GetRefundCandidate()
        {
            DataSet ds = await objbal.GetRefundCandidateMB();
            List<SelectListItem> lstRefundCandidate = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstRefundCandidate.Add(new SelectListItem { Text = dr["FullName"].ToString(), Value = dr["CandidateCode"].ToString() });
            }

            await Task.Run(() => ViewBag.RefundCandidatelst = lstRefundCandidate);

        }
        [HttpGet]
        public async Task GetReferenceByStudentsAsyncMB()
        {
            DataSet ds = await objbal.GetReferenceByCandidatesAsyncMB();
            List<SelectListItem> lstReferenceByStudent = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstReferenceByStudent.Add(new SelectListItem { Text = dr["FullName"].ToString(), Value = dr["RefBy"].ToString() });
            }
            await Task.Run(() => ViewBag.ReferenceByStudentlst = lstReferenceByStudent);
        }

        /// <summary>
        /// Get the staffname for giving them Advance
        /// </summary>
        /// <returns>AddExpenseView </returns>
        [HttpGet]
        public async Task GetStaffNameAsyncMB()
        {
            DataSet ds = await objbal.GetStaffNameAsyncMB();
            List<SelectListItem> lstStaff = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstStaff.Add(new SelectListItem { Text = dr["StaffName"].ToString(), Value = dr["StaffCode"].ToString() });
            }
            await Task.Run(() => ViewBag.lstStaff = lstStaff);
        }
        [HttpGet]
        private async Task ListVoucherAsyncMB()
        {
            if (Session["StaffCode"] == null)
            {
                //return RedirectToAction("Login", "Account");
            } else
            {
                Accountant objac = new Accountant();

                //fetching the banks here for the add purchase 
                DataSet ds = await objbal.ListVouchersAsyncMB();
                List<SelectListItem> VoucherList = new List<SelectListItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    VoucherList.Add(new SelectListItem { Text = $"{dr["VoucherCode"].ToString() + "-" + dr["VendorName"].ToString() + "-" + dr["Balance"].ToString()}", Value = dr["VoucherCode"].ToString() });
                }
                ViewBag.VoucherCode = VoucherList;
                //return await Task.Run(() => Json(VoucherList, JsonRequestBehavior.AllowGet));
            }
        }
    }
}