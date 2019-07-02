using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingDAL;
using AccountType;

namespace BankingBL
{
    public class BusinessLayer
    {
        DataAccessLayer dal = new DataAccessLayer();

        #region EVERYTHING
        //DEPOSIT
        public void DepositBL(string accountType, int accountNumber, double amount)
        {
            try { dal.DepositDAL(accountType, accountNumber, amount); }
            catch(Exception e) { throw; }
        }


        //WITHDRAW
        public void WithdrawBL(string accountType, int accountNumber, double amount)
        {
            try { dal.WithdrawDAL(accountType, accountNumber, amount); }
            catch(Exception e) { throw; }
        }


        //PAYLOAN
        public void PayInstallmentBL(int accountNumber, double amount)
        {
            try { dal.PayInstallmentDAL(accountNumber, amount); }
            catch(Exception e) { throw; }
        }

       
        //TRANSFER
        public void TransferBL(string fromAccountType, int fromAccountNumber, string toAccountType, int toAccountNumber, double amount)
        {
            try { dal.TransferDAL(fromAccountType, fromAccountNumber, toAccountType, toAccountNumber, amount); }
            catch (Exception e) { throw; }
        }


        //TRANSACTION
        public List<string> TransactionBL(string accountType, int accountNumber)
        {
            List<string> TxnList = new List<string>();
            try
            {
                TxnList = dal.TransactionDAL(accountType, accountNumber);
            }
            catch (Exception e) { throw; }
            return TxnList;
        }


        //CLOSE ACCOUNT
        public void CloseAccountBL(string accountType, int accountNumber)
        {
            try { dal.CloseAccountDAL(accountType, accountNumber); }
            catch (Exception e) { throw; }
        }


        //OPEN ACCOUNT C and B
        public void OpenCnBBL(string accountType)
        {
            try { dal.OpenCnBDAL(accountType); }
            catch(Exception e) { throw; }
        }


        //OPEN ACCOUNT TD AND L
        public void OpenTDnLBL(string accountType, double init, int time, double apr)
        {
            try { dal.OpenTDnLDAL(accountType, init, time, apr); }
            catch (Exception e) { throw; }
        }


        //REGISTER
        public void RegisterBL(string firstname, string lastname, string username, string password)
        {
            try { dal.RegisterDAL(firstname, lastname, username, password); }
            catch (Exception e) { throw; }
        }


        //LOGIN
        public bool Login(string username, string password)
        {
            try { return dal.LoginDAL(username, password); }
            catch (Exception e) { throw; }
        }
        #endregion

        #region Helper functions
        public List<CheckingAccount> GetCheckingInfo()
        {
            try
            {
                var CList = dal.GetCheckingInfo();
                return CList;
            } catch (Exception e) { throw; }
        }

        public List<BusinessAccount> GetBusinessInfo()
        {
            try
            {
                var BList = dal.GetBusinessInfo();
                return BList;
            }
            catch (Exception e) { throw; }
        }

        public List<Loan> GetLoanInfo()
        {
            try
            {
                var LList = dal.GetLoanInfo();
                return LList;
            }
            catch (Exception e) { throw; }
        }

        public List<TermDeposit> GetTDInfo()
        {
            try
            {
                var TDList = dal.GetTDInfo();
                return TDList;
            }
            catch (Exception e) { throw; }
        }
        #endregion
    }
}
