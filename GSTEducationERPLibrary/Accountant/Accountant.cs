using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GSTEducationERPLibrary.Accountant;
using static System.Collections.Specialized.BitVector32;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Policy;
using System.Xml.Linq;
using GSTEducationERPLibrary.Account;

namespace GSTEducationERPLibrary.Accountant
{
    public class Accountant
    {
        #region //Shreyas's Properties region Starts Here
        //------------------SHREYAYAS Voucher Start --------------------------------------------------------------//
        public string BranchCode { get; set; }
        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        [DisplayName("Vendor Name")]
        public string VendorName { get; set; }
        public decimal? Amount { get; set; }
        public string AmountPaidTo { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseCategory { get; set; }
        public string ExpenseName { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        [DisplayName("Receiver's Name")]
        public string AmountReceiver { get; set; }
        [DisplayName("Receiver's Name")]
        public string ReceiverName { get; set; }
        [DisplayName("Receiver's Type")]
        public string AmountReceiverType { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        [DisplayName("Account Number")]
        public long? ReceiverBankAccountNumber { get; set; }
        [DisplayName("Account Holder Name")]
        public string ReceiverBankAccountHolderName { get; set; }
        [DisplayName("IFSC Code")]
        public string ReceiverBankIFSCCode { get; set; }
        [DisplayName("Bank Name")]
        public string ReceiverBankName { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public string VoucherType { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Voucher Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime VoucherDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Cheque Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ChequeDate { get; set; }
        public DateTime ChequeClearenceDate { get; set; }

        public string StaffCode { get; set; }
        public int StatusId { get; set; }
        public List<Accountant> lstVoucher { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal MonthlyCollection { get; set; }
        public decimal MonthlyOutstanding { get; set; }
        public List<Accountant> BankAccounts { get; set; }
        public List<int> FeeCollectionData { get; set; }
        public List<int> FeesOverviewData { get; set; }

        public int TotalTransactions { get; set; }
        public int PendingVouchers { get; set; }
        public List<Accountant> BankBalances { get; set; }
        // Data for the Cash Flow Chart
        public List<decimal> CashFlowData { get; set; }

        // Data for the Bank Transactions Flow Chart
        public List<decimal> BankFlowData { get; set; }

        // Data for the Income vs Expense Chart
        public List<decimal> IncomeData { get; set; }
        public List<decimal> ExpenseData { get; set; }

        // Data for the Top Expenses Donut Chart
        public string Name { get; set; }
        public List<Accountant> TopExpensesData { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal CashInHand { get; set; }
        public decimal BankBalance { get; set; }

        public double CashAsOnStart { get; set; }
        public double Incoming { get; set; }
        public double Outgoing { get; set; }
        public double CashAsOnEnd { get; set; }
        public List<double> ChartData { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public List<string> CashFlowLabels { get; set; }
       

        #endregion //Shreyas's Properties region Ends Here

        //------------------SHREYAYAS Voucher End --------------------------------------------------------------//

        #region//Mukesh Expense Modal Start Here
        //---------------------Mukesh Expence Properties---------------------------------------//
        public string ExpID { get; set; }
        public string TransactionCode { get; set; }
        public double TransactionAmount { get; set; }



        [DataType(DataType.Date)]
        [DisplayName("Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string TranscationId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Comment { get; set; }
        public string StudentName { get; set; }
        public string PaidFee { get; set; }
        public string ReferenceByName { get; set; }
        public string ReferenceToName { get; set; }
        public string CandidateCode { get; set; }
        public string TranscationChequedate { get; set; }
        public float TotalAmount { get; set; }
        public string LoginStaffCode { get; set; }
        public string StaffCode_CandidateCode { get; set; }


        public List<Accountant> lstRegularExpense { get; set; }
        public List<Accountant> lstExpenseMB { get; set; }
        public List<Accountant> ListGiveExpenseMB { get; set; }
        public List<Accountant> ListMatchVoucheToExpense { get; set; }

        public List<Accountant> lstExpenseMB1 { get; set; }
        public string ProvisionalReceiptNo { get; set; }
        public string addressPart1 { get; set; }
        public string addressPart2 { get; set; }
        public string addressPart3 { get; set; }
        public string ClientLogo { get; set; }
        public string Course { get; set; }
        //public long Amount { get; set; }
        //public DateTime ChequeDate { get; set; }
        public DateTime ProvisionalReceiptDate { get; set; }
        public string EnrollmentNumber { get; set; }
        public string Batch { get; set; }
        //public string PaymentMode { get; set; }
        //public string TransactionId { get; set; }
        //public decimal Discount { get; set; }
        public decimal RemainingFee { get; set; }
        public DateTime NextInstallmentDate { get; set; }
        public decimal NextInstallmentAmount { get; set; }
        public string InstallmentNo { get; set; }
        public string DrawnOn { get; set; }
        public string AdmissionType { get; set; }
        public int Id { get; set; }
        public string Logo { get; set; }
        public string InwordsAmmount { get; set; }
        public long Chequenumber { get; set; }
        public DateTime ChequeClearanceDate { get; set; }
        public decimal TotalFee { get; set; }
        public string Address { get; set; }
        public string Signature { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public String Remark { get; set; }
        public string Hrs { get; set; }
    

        public String Workeddays { get; set; }
        public String HalfDays { get; set; }
        public String PresentDays { get; set; }
        public String PayableDays { get; set; }
        public string StaffName { get; set; }
        public int CurrentInstallment { get; set; }

        //----------------Jayash-  Accountant -----------------------------------End //
        #endregion


    }


}
