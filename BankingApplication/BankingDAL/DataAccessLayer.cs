using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using AccountType;

namespace BankingDAL
{
    public class DataAccessLayer
    {
        const string path = @"D:\User.json"; //CHANGE IT TO YOUR OWN DIRECTORY IF ERROR OCCURS

        #region EVERYTHING
        //DEPOSIT
        public void DepositDAL(string accountType, int accountNumber, double amount)
        {
            try
            {
                Customer c = GetData();
                if (accountType == "1")
                {
                    if (!isFound(accountType, accountNumber))
                        throw new Exception("==========>> Account Not Found <<==========");
                    else
                    {
                        int i = GetIndex(accountType, accountNumber);
                        c.CheckingAccount[i].Balance += amount;
                        c.CheckingAccount[i].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                        SaveData(c);
                        throw new Exception("==========>> Done <<==========");
                    }
                }
                if (accountType == "2")
                {
                    if (!isFound(accountType, accountNumber))
                        throw new Exception("==========>> Account Not Found <<==========");
                    else
                    {
                        int i = GetIndex(accountType, accountNumber);
                        c.BusinessAccount[i].Balance += amount;
                        c.BusinessAccount[i].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                        SaveData(c);
                        throw new Exception("==========>> Done <<==========");
                    }
                }
            }
            catch (Exception e) { throw; }
        }


        //WITHDRAW
        public void WithdrawDAL(string accountType, int accountNumber, double amount)
        {
            try
            {
                Customer c = GetData();
                if (accountType == "1")
                {
                    if (!isFound(accountType, accountNumber))
                        throw new Exception("==========>> Account Not Found <<==========");
                    else
                    {
                        int i = GetIndex(accountType, accountNumber);
                        if (c.CheckingAccount[i].Balance < amount)
                            throw new Exception("==========>> Overdraft Not Allowed <<==========");
                        else
                        {
                            c.CheckingAccount[i].Balance -= amount;
                            c.CheckingAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Done <<==========");
                        }
                    }
                }
                if (accountType == "2")
                {
                    if (!isFound(accountType, accountNumber))
                        throw new Exception("==========>> Account Not Found <<==========");
                    else
                    {
                        int i = GetIndex(accountType, accountNumber);
                        c.BusinessAccount[i].Balance -= amount;
                        c.BusinessAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                        SaveData(c);
                        throw new Exception("==========>> Done <<==========");
                    }
                }
                if (accountType == "3")
                {
                    if (!isFound(accountType, accountNumber))
                        throw new Exception("==========>> Account Not Found <<==========");
                    else
                    {
                        int i = GetIndex(accountType, accountNumber);
                        int Now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        if (Now < c.TermDeposit[i].MaturityDate)
                            throw new Exception("==========>> Early Withdraw Not Allowed <<==========");
                        else if (c.TermDeposit[i].Balance < amount)
                            throw new Exception("==========>> Overdraft Not Allowed <<==========");
                        else
                        {
                            c.TermDeposit[i].Balance -= amount;
                            c.TermDeposit[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Done <<==========");
                        }
                    }
                }
            }
            catch (Exception e) { throw; }
        }


        //PAYLOAN
        public void PayInstallmentDAL(int accountNumber, double amount)
        {
            try
            {
                Customer c = GetData();
                if (!isFound("4", accountNumber))
                    throw new Exception("==========>> Account Not Found <<==========");
                else
                {
                    int i = GetIndex("4", accountNumber);
                    if (c.Loan[i].Balance < amount)
                        throw new Exception("==========>> Overpay Not Allowed <<==========");
                    else
                    {
                        c.Loan[i].Balance -= amount;
                        c.Loan[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                        SaveData(c);
                        throw new Exception("==========>> Done. Thank You For The Payment <<==========");
                    }
                }
            }
            catch (Exception e) { throw; }
        }


        //TRANSFER
        public void TransferDAL(string fromAccountType, int fromAccountNumber, string toAccountType, int toAccountNumber, double amount)
        {
            try
            {
                if (!isFound(fromAccountType, fromAccountNumber) || !isFound(toAccountType, toAccountNumber))
                    throw new Exception("==========>> Account Not Found <<==========");
                else
                {
                    Customer c = GetData();
                    if (fromAccountType == "1")
                    {
                        int i = GetIndex(fromAccountType, fromAccountNumber);
                        if (c.CheckingAccount[i].Balance < amount)
                            throw new Exception("==========>> Overdraft Not Allowed <<==========");
                        else
                        {
                            if (toAccountType == "1")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                c.CheckingAccount[i].Balance -= amount;
                                c.CheckingAccount[j].Balance += amount;
                                c.CheckingAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                c.CheckingAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                                SaveData(c);
                                throw new Exception("==========>> Done <<==========");
                            }
                            if (toAccountType == "2")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                c.CheckingAccount[i].Balance -= amount;
                                c.BusinessAccount[j].Balance += amount;
                                c.CheckingAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                c.BusinessAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                                SaveData(c);
                                throw new Exception("==========>> Done <<==========");
                            }
                            if (toAccountType == "4")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                if (c.Loan[j].Balance < amount)
                                    throw new Exception("==========>> Overpay Not Allowed <<==========");
                                else
                                {
                                    c.CheckingAccount[i].Balance -= amount;
                                    c.Loan[j].Balance -= amount;
                                    c.CheckingAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                    c.Loan[j].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                    SaveData(c);
                                    throw new Exception("==========>> Done. Thank You For The Payment <<==========");
                                }
                            }
                        }
                    }
                    if (fromAccountType == "2")
                    {
                        int i = GetIndex(fromAccountType, fromAccountNumber);
                        if (toAccountType == "1")
                        {
                            int j = GetIndex(toAccountType, toAccountNumber);
                            c.BusinessAccount[i].Balance -= amount;
                            c.CheckingAccount[j].Balance += amount;
                            c.BusinessAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                            c.CheckingAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Done <<==========");
                        }
                        if (toAccountType == "2")
                        {
                            int j = GetIndex(toAccountType, toAccountNumber);
                            c.BusinessAccount[i].Balance -= amount;
                            c.BusinessAccount[j].Balance += amount;
                            c.BusinessAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                            c.BusinessAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Done <<==========");
                        }
                        if (toAccountType == "4")
                        {
                            int j = GetIndex(toAccountType, toAccountNumber);
                            if (c.Loan[j].Balance < amount)
                                throw new Exception("==========>> Overpay Not Allowed <<==========");
                            else
                            {
                                c.BusinessAccount[i].Balance -= amount;
                                c.Loan[j].Balance -= amount;
                                c.BusinessAccount[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                c.Loan[j].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                SaveData(c);
                                throw new Exception("==========>> Done. Thank You For The Payment <<==========");
                            }
                        }
                    }
                    if (fromAccountType == "3")
                    {
                        int i = GetIndex(fromAccountType, fromAccountNumber);
                        int Now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        if (Now < c.TermDeposit[i].MaturityDate)
                            throw new Exception("==========>> Early Withdraw Not Allowed <<==========");
                        else if (c.TermDeposit[i].Balance < amount)
                            throw new Exception("==========>> Overdraft Not Allowed <<==========");
                        else
                        {
                            if (toAccountType == "1")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                c.TermDeposit[i].Balance -= amount;
                                c.CheckingAccount[j].Balance += amount;
                                c.TermDeposit[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                c.CheckingAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                                SaveData(c);
                                throw new Exception("==========>> Done <<==========");
                            }
                            if (toAccountType == "2")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                c.TermDeposit[i].Balance -= amount;
                                c.BusinessAccount[j].Balance += amount;
                                c.TermDeposit[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                c.BusinessAccount[j].Transaction.Add($"+${amount} | {DateTime.Now.ToString()}");
                                SaveData(c);
                                throw new Exception("==========>> Done <<==========");
                            }
                            if (toAccountType == "4")
                            {
                                int j = GetIndex(toAccountType, toAccountNumber);
                                if (c.Loan[j].Balance < amount)
                                    throw new Exception("==========>> Overpay Not Allowed <<==========");
                                else
                                {
                                    c.TermDeposit[i].Balance -= amount;
                                    c.Loan[j].Balance -= amount;
                                    c.TermDeposit[i].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                    c.Loan[j].Transaction.Add($"-${amount} | {DateTime.Now.ToString()}");
                                    SaveData(c);
                                    throw new Exception("==========>> Done. Thank You For The Payment <<==========");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) { throw; }
        }


        //TRANSACTION
        public List<string> TransactionDAL(string accountType, int accountNumber)
        {
            List<string> TxnList = new List<string>();
            if (!isFound(accountType, accountNumber, true))
                throw new Exception("==========>> Account Not Found <<==========");
            else
            {
                try
                {
                    int i = GetIndex(accountType, accountNumber);
                    if (accountType == "1")
                        TxnList = GetCheckingInfo()[i].Transaction;
                    else if (accountType == "2")
                        TxnList = GetBusinessInfo()[i].Transaction;
                    else if (accountType == "3")
                        TxnList = GetTDInfo()[i].Transaction;
                    else
                        TxnList = GetLoanInfo()[i].Transaction;
                }
                catch (Exception e) { throw; }
            }
            return TxnList;
        }


        //CLOSE ACCOUNT
        public void CloseAccountDAL(string accountType, int accountNumber)
        {
            try
            {
                if (!isFound(accountType, accountNumber))
                    throw new Exception("==========>> Account Not Found <<==========");
                else
                {
                    Customer c = GetData();
                    int i = GetIndex(accountType, accountNumber);
                    if (accountType == "1")
                    {
                        if (c.CheckingAccount[i].Balance != 0)
                            throw new Exception("==========>> Unable To Close Account With Balance <<==========");
                        else
                        {
                            c.CheckingAccount[i].isActive = false;
                            c.CheckingAccount[i].Transaction.Add($"Account Closed on {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Account Closed <<==========");
                        }
                    }
                    else if (accountType == "2")
                    {
                        if (c.BusinessAccount[i].Balance != 0)
                            throw new Exception("==========>> Unable To Close Account With Balance <<==========");
                        else
                        {
                            c.BusinessAccount[i].isActive = false;
                            c.BusinessAccount[i].Transaction.Add($"Account Closed on {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Account Closed <<==========");
                        }
                    }
                    else if (accountType == "3")
                    {
                        if (c.TermDeposit[i].Balance != 0)
                            throw new Exception("==========>> Unable To Close Account With Balance <<==========");
                        else
                        {
                            c.TermDeposit[i].isActive = false;
                            c.TermDeposit[i].Transaction.Add($"Account Closed on {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Account Closed <<==========");
                        }
                    }
                    else
                    {
                        if (c.Loan[i].Balance != 0)
                            throw new Exception("==========>> Unable To Close Account With Balance <<==========");
                        else
                        {
                            c.Loan[i].isActive = false;
                            c.Loan[i].Transaction.Add($"Account Closed on {DateTime.Now.ToString()}");
                            SaveData(c);
                            throw new Exception("==========>> Account Closed <<==========");
                        }
                    }
                }
            }
            catch (Exception e) { throw; }
        }


        //OPEN ACCOUNT CHECK AND BUSINESS
        public void OpenCnBDAL(string accountType)
        {
            try
            {
                Customer c = GetData();
                if (accountType == "1")
                {
                    CheckingAccount check = new CheckingAccount();
                    c.CheckingAccount.Add(check);
                    SaveData(c);
                    throw new Exception("==========>> One Checking Account Opened <<==========");
                }
                else
                {
                    BusinessAccount business = new BusinessAccount();
                    c.BusinessAccount.Add(business);
                    SaveData(c);
                    throw new Exception("==========>> One Business Account Opened <<==========");
                }
            }
            catch (Exception e) { throw; }
        }


        //OPEN ACCOUNT TD AND LOAN
        public void OpenTDnLDAL(string accountType, double init, int time, double apr)
        {
            try
            {
                Customer c = GetData();
                if (accountType == "3")
                {
                    TermDeposit td = new TermDeposit(init, time, apr);
                    c.TermDeposit.Add(td);
                    SaveData(c);
                    throw new Exception("==========>> One Term Deposit Opened <<==========");
                }
                else
                {
                    Loan loan = new Loan(init, time, apr);
                    c.Loan.Add(loan);
                    SaveData(c);
                    throw new Exception("==========>> One Loan Account Opened <<==========");
                }
            }
            catch (Exception e) { throw; }
        }


        //REGISTER
        public void RegisterDAL(string firstname, string lastname, string username, string password)
        {
            Customer c = new Customer(firstname, lastname, username, password);
            try { SaveData(c); }
            catch (Exception e) { throw; }
        }


        //LOGIN
        public bool LoginDAL(string username, string password)
        {
            try
            {
                Customer c = GetData();
                return (c.Username == username && c.Password == password);
            }
            catch (Exception e) { throw new Exception("==========>> User Not Found <<=========="); }
        }


        public Customer GetData()
        {
            try
            {
                JsonSerializer js = new JsonSerializer();
                StreamReader sr = new StreamReader(path);
                JsonReader jr = new JsonTextReader(sr);
                Customer c = js.Deserialize(jr, typeof(Customer)) as Customer;
                jr.Close();
                sr.Close();
                return c;
            }
            catch (Exception e) { throw; }
        }


        public void SaveData(Customer c)
        {
            JsonSerializer js = new JsonSerializer();
            StreamWriter sw = new StreamWriter(path);
            JsonWriter jw = new JsonTextWriter(sw);
            js.Serialize(jw, c);
            jw.Close();
            sw.Close();
        }
        #endregion

        #region Helper functions
        public List<CheckingAccount> GetCheckingInfo()
        {
            try
            {
                Customer c = GetData();
                return c.CheckingAccount;
            }
            catch (Exception e) { throw; }
        }

        public List<BusinessAccount> GetBusinessInfo()
        {
            try
            {
                Customer c = GetData();
                return c.BusinessAccount;
            }
            catch (Exception e) { throw; }
        }

        public List<Loan> GetLoanInfo()
        {
            try
            {
                Customer c = GetData();
                return c.Loan;
            }
            catch (Exception e) { throw; }
        }

        public List<TermDeposit> GetTDInfo()
        {
            try
            {
                Customer c = GetData();
                return c.TermDeposit;
            }
            catch (Exception e) { throw; }
        }


        /// <summary>
        /// isFound "Check if an account exists with account number."
        /// </summary>
        public bool isFound(string AccountType, int AccountNumber, bool checkInactive = false)
        {
            if (checkInactive)
            {
                if (AccountType == "1")
                {
                    List<CheckingAccount> CList = GetCheckingInfo();
                    foreach (var acc in CList)
                    {
                        if (acc.AccountNumber == AccountNumber)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "2")
                {
                    List<BusinessAccount> BList = GetBusinessInfo();
                    foreach (var acc in BList)
                    {
                        if (acc.AccountNumber == AccountNumber)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "3")
                {
                    List<TermDeposit> TDList = GetTDInfo();
                    foreach (var acc in TDList)
                    {
                        if (acc.AccountNumber == AccountNumber)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "4")
                {
                    List<Loan> LList = GetLoanInfo();
                    foreach (var acc in LList)
                    {
                        if (acc.AccountNumber == AccountNumber)
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            else
            {
                if (AccountType == "1")
                {
                    List<CheckingAccount> CList = GetCheckingInfo();
                    foreach (var acc in CList)
                    {
                        if ((acc.AccountNumber == AccountNumber) && acc.isActive)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "2")
                {
                    List<BusinessAccount> BList = GetBusinessInfo();
                    foreach (var acc in BList)
                    {
                        if ((acc.AccountNumber == AccountNumber) && acc.isActive)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "3")
                {
                    List<TermDeposit> TDList = GetTDInfo();
                    foreach (var acc in TDList)
                    {
                        if ((acc.AccountNumber == AccountNumber) && acc.isActive)
                            return true;
                    }
                    return false;
                }
                else if (AccountType == "4")
                {
                    List<Loan> LList = GetLoanInfo();
                    foreach (var acc in LList)
                    {
                        if ((acc.AccountNumber == AccountNumber) && acc.isActive)
                            return true;
                    }
                    return false;
                }
                else
                    return false;
            }
        }


        public int GetIndex(string AccountType, int AccountNumber)
        {
            if (AccountType == "1")
            {
                List<CheckingAccount> CList = GetCheckingInfo();
                for (int i = 0; i < CList.Count; i++)
                {
                    if (CList[i].AccountNumber == AccountNumber)
                        return i;
                }
                return -1;
            }
            else if (AccountType == "2")
            {
                List<BusinessAccount> BList = GetBusinessInfo();
                for (int i = 0; i < BList.Count; i++)
                {
                    if (BList[i].AccountNumber == AccountNumber)
                        return i;
                }
                return -1;
            }
            else if (AccountType == "3")
            {
                List<TermDeposit> TDList = GetTDInfo();
                for (int i = 0; i < TDList.Count; i++)
                {
                    if (TDList[i].AccountNumber == AccountNumber)
                        return i;
                }
                return -1;
            }
            else if (AccountType == "4")
            {
                List<Loan> LList = GetLoanInfo();
                for (int i = 0; i < LList.Count; i++)
                {
                    if (LList[i].AccountNumber == AccountNumber)
                        return i;
                }
                return -1;
            }
            else
                return -1;
        }
        #endregion
    }
}
