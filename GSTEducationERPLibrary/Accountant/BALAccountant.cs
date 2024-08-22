using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Helper;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

namespace GSTEducationERPLibrary.Accountant
{
	public class BALAccountant
	{
        MSSQL DBHelper = new MSSQL();
        Dictionary<string, string> Param = new Dictionary<string, string>();

        #region //Shreyas's Dashboard and Voucher Submodules Properties Start Here
        public async Task AddVoucherAsyncSGS(Accountant ObjT)
        {
            if (ObjT.AmountReceiver == "Staff") {
                if (ObjT.PaymentMode == "Cash")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@AmountPaidTo", ObjT.AmountPaidTo.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "CASH");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "Bank") {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@AmountPaidTo", ObjT.AmountPaidTo.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "BANK");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@ReceiverBankAccountNumber", ObjT.ReceiverBankAccountNumber.ToString());
                        Param.Add("@ReceiverBankAccountHolderName", ObjT.ReceiverBankAccountHolderName.ToString());
                        Param.Add("@ReceiverBankIFSCCode", ObjT.ReceiverBankIFSCCode.ToString());
                        Param.Add("@ReceiverBankName", ObjT.ReceiverBankName.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "UPI")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@AmountPaidTo", ObjT.AmountPaidTo.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "UPI");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "Cheque")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@AmountPaidTo", ObjT.AmountPaidTo.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "CHEQUE");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@ChequeDate", ObjT.ChequeDate.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                }

            } else {
                if (ObjT.PaymentMode == "Cash")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@VendorName", ObjT.VendorName.ToString());
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "CASH");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "Bank")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@VendorName", ObjT.VendorName.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "BANK");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@ReceiverBankAccountNumber", ObjT.ReceiverBankAccountNumber.ToString());
                        Param.Add("@ReceiverBankAccountHolderName", ObjT.ReceiverBankAccountHolderName.ToString());
                        Param.Add("@ReceiverBankIFSCCode", ObjT.ReceiverBankIFSCCode.ToString());
                        Param.Add("@ReceiverBankName", ObjT.ReceiverBankName.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "UPI")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@VendorName", ObjT.VendorName.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "UPI");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                } else if (ObjT.PaymentMode == "Cheque")
                {
                    try
                    {
                        Param.Add("@flag", "AddVoucherAsyncSGS");
                        Param.Add("@VoucherCode", ObjT.VoucherCode);
                        Param.Add("@Amount", ObjT.Amount.ToString());
                        Param.Add("@VendorName", ObjT.VendorName.ToString());
                        Param.Add("@Description", ObjT.Description.ToString());
                        Param.Add("@PaymentMode", "CHEQUE");
                        Param.Add("@StaffCode", ObjT.StaffCode.ToString());
                        Param.Add("@BankId", ObjT.BankId.ToString());
                        Param.Add("@ChequeDate", ObjT.ChequeDate.ToString());
                        Param.Add("@Balance", ObjT.Amount.ToString());
                        Param.Add("@Currency", "INR");
                        Param.Add("@TransactionId", ObjT.TransactionId.ToString());
                        Param.Add("@VoucherTypeId", ObjT.VoucherType.ToString());
                        Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        Param.Add("@StatusId", 44.ToString());
                        await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
                    } catch (Exception ex)
                    {
                        throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
                    }
                }

            }
            
        }

        public async Task<DataSet> GetDashData(int month)
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "GetDataForDashboardAsyncSGS");
            Param.Add("@Month", month.ToString());
            //Param.Add("@BranchCode", branchCode); // Pass branch code parameter
            //Param.Add("@StaffCode", staffCode); // Pass staff code parameter
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> GetCashFlowData()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "CashFlowDataForDashboardAsyncSGS");
            //Param.Add("@BranchCode", branchCode); // Pass branch code parameter
            //Param.Add("@StaffCode", staffCode); // Pass staff code parameter
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> ListVoucherAsyncSGS()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "ListVoucherAsyncSGS");
            //Param.Add("@BranchCode", branchCode); // Pass branch code parameter
            //Param.Add("@StaffCode", staffCode); // Pass staff code parameter
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> ListPendingVoucherAsyncSGS()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "ListPendingVoucherAsyncSGS");
            //Param.Add("@BranchCode", branchCode); // Pass branch code parameter
            //Param.Add("@StaffCode", staffCode); // Pass staff code parameter
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> ListCompletedVoucherAsyncSGS()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "ListCompletedVoucherAsyncSGS");
            //Param.Add("@BranchCode", branchCode); // Pass branch code parameter
            //Param.Add("@StaffCode", staffCode); // Pass staff code parameter
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> StaffNameforVoucherAsyncSGS(Accountant objT)
        {
            try
            {
                Dictionary<string, string> Param = new Dictionary<string, string>();
                Param.Add("@flag", "GetStaffDataAsyncSGS");
                Param.Add("@StaffCode", objT.StaffCode);
                Param.Add("@BranchCode", objT.BranchCode.ToString());
                DataSet ds1 = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
                return ds1;
            } catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving assigned projects. Details: " + ex.Message);
            }
        }
        public async Task<DataSet> BankAccountforVoucherAsyncSGS(Accountant objT)
        {
            try
            {
                Dictionary<string, string> Param = new Dictionary<string, string>();
                Param.Add("@flag", "GetBankDataAsyncSGS");
                Param.Add("@StaffCode", objT.StaffCode);
                DataSet ds1 = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
                return ds1;
            } catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving assigned projects. Details: " + ex.Message);
            }
        }
        public async Task<DataSet> VoucherTypeAsyncSGS(Accountant objT)
        {
            try
            {
                Dictionary<string, string> Param = new Dictionary<string, string>();
                Param.Add("@flag", "GetVoucherTypeAsyncSGS");
                DataSet ds1 = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
                return ds1;
            } catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving assigned projects. Details: " + ex.Message);
            }
        }
        public async Task<DataSet> ExpenseTypeAsyncSGS(Accountant objT)
        {
            try
            {
                Dictionary<string, string> Param = new Dictionary<string, string>();
                Param.Add("@flag", "GetExpenseTypeAsyncSGS");
                DataSet ds1 = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
                return ds1;
            } catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving assigned projects. Details: " + ex.Message);
            }
        }
        public async Task<string> GetMaxVoucherCodeAsyncSGS(Accountant obj)
        {
            Dictionary<String, String> Param = new Dictionary<String, String>();
            Param.Add("@Flag", "GetMaxVoucherCodeAsyncSGS");
            //Param.Add("@BranchCode", ObjA.BranchCode);
            SqlDataReader ds = await DBHelper.ExecuteStoreProcedureReturnDataReader("GSTAccountant", Param);
            string LastTransactionCode = "";
            while (ds.Read())
            {
                LastTransactionCode = ds["VoucherCode"].ToString();
            }
            string newPurchaseCode = IncrementPurchaseCodeSGS(LastTransactionCode);
            return newPurchaseCode;
        }
        public static string IncrementPurchaseCodeSGS(string lastPurchaseCode)
        {
            // Define a regular expression to extract the numeric part
            Regex regex = new Regex(@"(\D+)(\d+)");
            Match match = regex.Match(lastPurchaseCode);

            if (match.Success)
            {
                string prefix = match.Groups[1].Value; // The non-numeric prefix (e.g., "PUR")
                string numberPart = match.Groups[2].Value; // The numeric part (e.g., "017")

                // Parse the numeric part to an integer
                int number = int.Parse(numberPart);

                // Increment the number
                number++;

                // Determine the length of the original numeric part to maintain leading zeros
                int lengthOfNumberPart = numberPart.Length;

                // Format the new number with leading zeros
                string newNumberPart = number.ToString().PadLeft(lengthOfNumberPart, '0');

                // Reassemble the new purchase code
                string newPurchaseCode = prefix + newNumberPart;

                return newPurchaseCode;
            } else
            {
                throw new ArgumentException("Invalid purchase code format.");
            }
        }
        public async Task<DataSet> GetVoucherDataSGS(Accountant obj)
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "DetailVoucherAsyncSGS");
            Param.Add("@StaffCode", obj.StaffCode);
            Param.Add("@BranchCode", obj.BranchCode);
            Param.Add("@VoucherCode", obj.VoucherCode);
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task RemoveDataVoucher(Accountant objT)
        {
            try
            {
                Dictionary<string, string> Param = new Dictionary<string, string>();
                Param.Add("@flag", "RemoveVoucherDataAsyncSGS");
                Param.Add("@VoucherCode", objT.VoucherCode.ToString());
                await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
            } catch (Exception ex)
            {
                throw new Exception("An error occurred while removing task. Details: " + ex.Message);
            }
        }
        //public async Task<SqlDataReader> FetchVoucherAsyncPG(Accountant objT)
        //{
        //    try
        //    {
        //        Dictionary<string, string> Param = new Dictionary<string, string>();
        //        Param.Add("@flag", "FetchVoucherData");
        //        Param.Add("@VoucherCode", objT.VoucherCode.ToString());
        //        Param.Add("@BranchCode", objT.BranchCode.ToString());
        //        SqlDataReader dr = await DBHelper.ExecuteStoreProcedureReturnDataReader("GSTAccountant", Param);
        //        return dr;
        //    } catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while fetching assigned project details. Details: " + ex.Message);
        //    }
        //}
        //public async Task UpdateCashVoucherAsyncSGS(Accountant ObjT)
        //{
        //    try
        //    {
        //        Dictionary<string, string> Param = new Dictionary<string, string>();
        //        if (ObjT.AmountReceiver == "Staff")
        //        {
        //            try
        //            {
        //                Param.Add("@flag", "UpdateVoucher");
        //                Param.Add("@VoucherCode", ObjT.VoucherCode);
        //                Param.Add("@Amount", ObjT.Amount.ToString());
        //                Param.Add("@AmountPaidTo", ObjT.AmountPaidTo.ToString());
        //                Param.Add("@Description", ObjT.Description.ToString());
        //                Param.Add("@PaymentMode", "CASH");
        //                Param.Add("@StaffCode", ObjT.StaffCode.ToString());
        //                Param.Add("@Balance", ObjT.Amount.ToString());
        //                Param.Add("@Currency", "INR");
        //                Param.Add("@VoucherType", ObjT.VoucherType.ToString());
        //                Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        //                Param.Add("@StatusId", 44.ToString());
        //                await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
        //            } catch (Exception ex)
        //            {
        //                throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
        //            }
        //        } else
        //        {
        //            try
        //            {
        //                Param.Add("@flag", "UpdateVoucher");
        //                Param.Add("@VoucherCode", ObjT.VoucherCode);
        //                Param.Add("@VendorName", ObjT.VendorName.ToString());
        //                Param.Add("@Amount", ObjT.Amount.ToString());
        //                Param.Add("@Description", ObjT.Description.ToString());
        //                Param.Add("@PaymentMode", "CASH");
        //                Param.Add("@StaffCode", ObjT.StaffCode.ToString());
        //                Param.Add("@Balance", ObjT.Amount.ToString());
        //                Param.Add("@Currency", "INR");
        //                Param.Add("@VoucherType", ObjT.VoucherType.ToString());
        //                Param.Add("@VoucherDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        //                Param.Add("@StatusId", 44.ToString());
        //                await DBHelper.ExecuteStoreProcedure("GSTAccountant", Param);
        //            } catch (Exception ex)
        //            {
        //                throw new Exception("An error occurred while registering the assigned project. Details: " + ex.Message);
        //            }

        //            await DBHelper.ExecuteStoreProcedure("GSTBind", Param);
        //        }
        //    } catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while updating the assigned project. Details: " + ex.Message);
        //    }
        //}
        #endregion //Shreyas's Dashboard and Voucher Submodules Properties Ends Here

        public async Task<DataSet> ListVouchersAsyncMB()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "ListVouchersAsyncMB");
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> GetReferenceByCandidatesAsyncMB()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "GetReferenceByCandidates");
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);

            return ds;
        }
        public async Task<DataSet> GetStaffNameAsyncMB()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "GetStaffNameAsyncMB");
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);

            return ds;
        }
        public async Task<DataSet> GetRefundCandidateMB()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();
            Param.Add("@Flag", "GetRefundCandidateMB");
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }
        public async Task<DataSet> GetExpenceCategoryMB()
        {
            Dictionary<string, string> Param = new Dictionary<string, string>();

            Param.Add("@Flag", "GetExpenceCategoryMB");
            DataSet ds = await DBHelper.ExecuteStoreProcedureReturnDS("GSTAccountant", Param);
            return ds;
        }


}
}
