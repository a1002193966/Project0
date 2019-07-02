using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountType;
using BankingBL;

namespace FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isContinue = loginUI();
            while(isContinue)
            {
                mainUI();
                Console.Write("==========>> Do you want to perform more transaction? [yes/no]: ");
                string str = Console.ReadLine();
                if (str == "no")
                {
                    Console.WriteLine("==========>> Have A Nice Day! <<==========");
                    break;
                } 
            }
        }

        private static BusinessLayer bl = new BusinessLayer();

        #region Deposit
        private static void depositUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                      DEPOSIT                    ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||                1. Checking Account              ||");
            Console.WriteLine("||                2. Business Account              ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string accountType = Console.ReadLine();
            if(accountType == "1" || accountType == "2")
            {
                if (isExist(accountType))
                {
                    if (accountType == "1")
                        displayCheckingInfo();
                    else
                        displayBusinessInfo();
                    try
                    {
                        Console.Write("\n==========>> Enter Account #: ");
                        int accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("==========>> Enter Deposit Amount: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        if (amount <= 0)
                            Console.WriteLine("==========> Please Enter Valid Number <==========");
                        else
                            bl.DepositBL(accountType, accountNumber, amount);
                    }
                    catch(Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    Console.WriteLine("==========>> No Existing Account <<==========");
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");
        }
        #endregion

        #region Withdraw
        private static void withdrawUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                     WITHDRAW                    ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||              1. Checking Account                ||");
            Console.WriteLine("||              2. Business Account                ||");
            Console.WriteLine("||              3. Term Deposit                    ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string accountType = Console.ReadLine();
            if (accountType == "1" || accountType == "2" || accountType == "3")
            {
                if (isExist(accountType))
                {
                    if (accountType == "1")
                        displayCheckingInfo();
                    else if (accountType == "2")
                        displayBusinessInfo();
                    else
                        displayTDInfo();
                    try
                    {
                        Console.Write("\n==========>> Enter Account #: ");
                        int accountNumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("==========>> Enter Withdraw Amount: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        if (amount <= 0)
                            Console.WriteLine("==========> Please Enter Valid Number <==========");
                        else
                            bl.WithdrawBL(accountType, accountNumber, amount);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    Console.WriteLine("==========>> No Existing Account <<==========");
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");
        }
        #endregion

        #region Pay Loan
        private static void payLoanUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                      PAY LOAN                   ||");
            Console.WriteLine("++=================================================++\n");
            if (isExist("4"))
            {
                displayLoanInfo();
                try
                {
                    Console.Write("\n==========>> Enter Account #: ");
                    int accountNumber = Convert.ToInt32(Console.ReadLine());
                    Console.Write("==========>> Enter Pay Amount: ");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    if (amount <= 0)
                        Console.WriteLine("==========> Please Enter Valid Number <==========");
                    else
                        bl.PayInstallmentBL(accountNumber, amount);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
        #endregion

        #region Transfer
        private static void transferUI()
        {
            bool stepOnePassed = false;
            bool stepTwoPassed = false;
            string fromAccountType = "";
            string toAccountType = "";
            int fromAccountNumber = 0;
            int toAccountNumber = 0;
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                      TRANSFER                   ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||          FROM:                                  ||");
            Console.WriteLine("||              1. Checking Account                ||");
            Console.WriteLine("||              2. Business Account                ||");
            Console.WriteLine("||              3. Term Deposit                    ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            fromAccountType = Console.ReadLine();
            if (fromAccountType == "1" || fromAccountType == "2" || fromAccountType == "3")
            {
                if(isExist(fromAccountType))
                {
                    if (fromAccountType == "1")
                        displayCheckingInfo();
                    else if (fromAccountType == "2")
                        displayBusinessInfo();
                    else
                        displayTDInfo();
                    try
                    {
                        Console.Write("\n==========>> Enter Account #: ");
                        fromAccountNumber = Convert.ToInt32(Console.ReadLine());
                        stepOnePassed = true;
                    }
                    catch(Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    Console.WriteLine("==========>> No Existing Account <<==========");
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");

            if (stepOnePassed)
            {
                Console.WriteLine("++=================================================++");
                Console.WriteLine("||          TO:                                    ||");
                Console.WriteLine("||              1. Checking Account                ||");
                Console.WriteLine("||              2. Business Account                ||");
                Console.WriteLine("||              4. Loan                            ||");
                Console.WriteLine("++=================================================++\n");
                Console.Write("==========>> Enter Menu #: ");
                toAccountType = Console.ReadLine();
                if (toAccountType == "1" || toAccountType == "2" || toAccountType == "4")
                {
                    if(fromAccountType == toAccountType)
                    {
                        if(isMultiple(toAccountType))
                        {
                            if (toAccountType == "1")
                                displayCheckingInfo();
                            else
                                displayBusinessInfo();
                            try
                            {
                                Console.Write("\n==========>> Enter Account #: ");
                                toAccountNumber = Convert.ToInt32(Console.ReadLine());
                                stepTwoPassed = true;
                            }
                            catch(Exception e) { Console.WriteLine(e.Message); }
                        }
                        else
                        {
                            if (toAccountType == "1")
                                Console.WriteLine("==========>> You Only Have One Active Checking Account <<==========");
                            else
                                Console.WriteLine("==========>> You Only Have One Active Business Account <<==========");
                        }
                    }
                    else
                    {
                        if (isExist(toAccountType))
                        {
                            if (toAccountType == "1")
                                displayCheckingInfo();
                            else if (toAccountType == "2")
                                displayBusinessInfo();
                            else
                                displayLoanInfo();
                            try
                            {
                                Console.Write("\n==========>> Enter Account #: ");
                                toAccountNumber = Convert.ToInt32(Console.ReadLine());
                                stepTwoPassed = true;
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                        }
                        else
                            Console.WriteLine("==========>> No Existing Account <<==========");
                    }
                }
                else
                    Console.WriteLine("==========>> Invalid Input <<==========");
            }
            if(stepTwoPassed)
            {
                if (fromAccountNumber == toAccountNumber)
                    Console.WriteLine("==========>> Transfer To Same Account Makes No Sense <<==========");
                else
                {
                    try
                    {
                        Console.Write("\n==========>> Enter Transfer Amount: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        if (amount <= 0)
                            Console.WriteLine("==========> Please Enter Valid Number <==========");
                        else                     
                            bl.TransferBL(fromAccountType, fromAccountNumber, toAccountType, toAccountNumber, amount);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }  
            }
        }
        #endregion

        #region Transaction
        private static void transactionUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                    TRANSACTION                  ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||              1. Checking Account                ||");
            Console.WriteLine("||              2. Business Account                ||");
            Console.WriteLine("||              3. Term Deposit                    ||");
            Console.WriteLine("||              4. Loan                            ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string accountType = Console.ReadLine();
            if (accountType == "1" || accountType == "2" || accountType == "3" || accountType == "4")
            {
                if(isExist(accountType, true))
                {
                    if (accountType == "1")
                        displayCheckingInfo(true);
                    else if (accountType == "2")
                        displayBusinessInfo(true);
                    else if (accountType == "3")
                        displayTDInfo(true);
                    else
                        displayLoanInfo(true);
                    try
                    {
                        Console.Write("\n==========>> Enter Account #: ");
                        int accountNumber = Convert.ToInt32(Console.ReadLine());
                        List<string> TxnList = bl.TransactionBL(accountType, accountNumber);
                        foreach (var txn in TxnList)
                            Console.WriteLine(txn);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    Console.WriteLine("==========>> No Existing Account <<==========");
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");
        }
        #endregion

        #region Close Account
        private static void closeAccountUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                   CLOSE ACCOUNT                 ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||              1. Checking Account                ||");
            Console.WriteLine("||              2. Business Account                ||");
            Console.WriteLine("||              3. Term Deposit                    ||");
            Console.WriteLine("||              4. Loan                            ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string accountType = Console.ReadLine();
            if (accountType == "1" || accountType == "2" || accountType == "3" || accountType == "4")
            {
                if (isExist(accountType))
                {
                    if (accountType == "1")
                        displayCheckingInfo();
                    else if (accountType == "2")
                        displayBusinessInfo();
                    else if (accountType == "3")
                        displayTDInfo();
                    else
                        displayLoanInfo();
                    try
                    {
                        Console.Write("\n==========>> Enter Account #: ");
                        int accountNumber = Convert.ToInt32(Console.ReadLine());
                        bl.CloseAccountBL(accountType, accountNumber);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    Console.WriteLine("==========>> No Existing Account <<==========");
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");
        }
        #endregion

        #region Display all accounts
        private static void displayAccountUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||               DISPLAY ALL ACCOUNTS              ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Do You Want To Include Inactive Accounts? [yes/no]: ");
            string show = Console.ReadLine();
            if (show == "yes")
                displayAllAccounts(true);
            else
                displayAllAccounts();
        }

        private static void displayAllAccounts(bool displayInactive=false)
        {
            if (displayInactive)
            {
                displayCheckingInfo(true);
                displayBusinessInfo(true);
                displayTDInfo(true);
                displayLoanInfo(true);     
            }
            else
            {
                displayCheckingInfo();
                displayBusinessInfo();
                displayTDInfo();
                displayLoanInfo();
            }
        }
        #endregion

        #region Helper functions
        private static void displayCheckingInfo(bool showInactive = false)
        {
            List<CheckingAccount> CList = getCheckingList();
            if (showInactive)
            {
                foreach (var acc in CList)
                    Console.WriteLine($"| ACCOUNT TYPE: CHECKING | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
            }
            else
            {
                foreach (var acc in CList)
                {
                    if (acc.isActive)
                        Console.WriteLine($"| ACCOUNT TYPE: CHECKING | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
                }
            }
        }

        private static void displayBusinessInfo(bool showInactive = false)
        {
            List<BusinessAccount> BList = getBusinessList();
            if (showInactive)
            {
                foreach (var acc in BList)
                    Console.WriteLine($"| ACCOUNT TYPE: BUSINESS | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
            }
            else
            {
                foreach (var acc in BList)
                {
                    if (acc.isActive)
                        Console.WriteLine($"| ACCOUNT TYPE: BUSINESS | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
                }
            }
        }

        private static void displayTDInfo(bool showInactive = false)
        {
            List<TermDeposit> TDList = getTDList();
            if (showInactive)
            {
                foreach (var acc in TDList)
                    Console.WriteLine($"| ACCOUNT TYPE: TERM DEPOSIT | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
            }
            else
            {
                foreach (var acc in TDList)
                {
                    if (acc.isActive)
                        Console.WriteLine($"| ACCOUNT TYPE: TERM DEPOSIT | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
                }
            }
        }

        private static void displayLoanInfo(bool showInactive = false)
        {
            List<Loan> LList = getLoanList();
            if (showInactive)
            {
                foreach (var acc in LList)
                    Console.WriteLine($"| ACCOUNT TYPE: LOAN | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
            }
            else
            {
                foreach (var acc in LList)
                {
                    if (acc.isActive)
                        Console.WriteLine($"| ACCOUNT TYPE: LOAN | ACCOUNT#: {acc.AccountNumber} | BALANCE: {acc.Balance} | isActive?: {acc.isActive} |");
                }
            }
        }

        private static List<CheckingAccount> getCheckingList()
        {
            List<CheckingAccount> CList = new List<CheckingAccount>();
            try
            {
                CList = bl.GetCheckingInfo();
                return CList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CList;
            }
        }

        private static List<BusinessAccount> getBusinessList()
        {
            List<BusinessAccount> BList = new List<BusinessAccount>();
            try
            {
                BList = bl.GetBusinessInfo();
                return BList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BList;
            }
        }

        private static List<TermDeposit> getTDList()
        {
            List<TermDeposit> TDList = new List<TermDeposit>();
            try
            {
                TDList = bl.GetTDInfo();
                return TDList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return TDList;
            }
        }

        private static List<Loan> getLoanList()
        {
            List<Loan> LList = new List<Loan>();
            try
            {
                LList = bl.GetLoanInfo();
                return LList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return LList;
            }
        }


        /// <summary>
        /// isExist "Check if certain type of account exists."
        /// </summary>
        private static bool isExist(string accountType, bool checkInactive = false)
        {
            if (checkInactive)
            {
                if (accountType == "1")
                {
                    List<CheckingAccount> CList = getCheckingList();
                    return (CList.Count != 0);
                }
                else if (accountType == "2")
                {
                    List<BusinessAccount> BList = getBusinessList();
                    return (BList.Count != 0);
                }
                else if (accountType == "3")
                {
                    List<TermDeposit> TDList = getTDList();
                    return (TDList.Count != 0);
                }
                else
                {
                    List<Loan> LList = getLoanList();
                    return (LList.Count != 0);
                }
            }
            else
            {
                if (accountType == "1")
                {
                    List<CheckingAccount> CList = getCheckingList();
                    if (CList.Count == 0)
                        return false;
                    else
                    {
                        foreach (var acc in CList)
                        {
                            if (acc.isActive)
                                return true;
                        }
                    }
                    return false;
                }
                else if (accountType == "2")
                {
                    List<BusinessAccount> BList = getBusinessList();
                    if (BList.Count == 0)
                        return false;
                    else
                    {
                        foreach (var acc in BList)
                        {
                            if (acc.isActive)
                                return true;
                        }
                    }
                    return false;
                }
                else if (accountType == "3")
                {
                    List<TermDeposit> TDList = getTDList();
                    if (TDList.Count == 0)
                        return false;
                    else
                    {
                        foreach (var acc in TDList)
                        {
                            if (acc.isActive)
                                return true;
                        }
                    }
                    return false;
                }
                else
                {
                    List<Loan> LList = getLoanList();
                    if (LList.Count == 0)
                        return false;
                    else
                    {
                        foreach (var acc in LList)
                        {
                            if (acc.isActive)
                                return true;
                        }
                    }
                    return false;
                }
            }
        }

        private static bool isMultiple(string accountType)
        {
            if (accountType == "1")
            {
                List<CheckingAccount> CList = getCheckingList();
                int activeCnt = 0;
                foreach (var acc in CList)
                {
                    if (acc.isActive)
                        activeCnt++;
                }
                return (activeCnt >= 2);
            }
            else if (accountType == "2")
            {
                List<BusinessAccount> BList = getBusinessList();
                int activeCnt = 0;
                foreach (var acc in BList)
                {
                    if (acc.isActive)
                        activeCnt++;
                }
                return (activeCnt >= 2);
            }
            else if (accountType == "3")
            {
                List<TermDeposit> TDList = getTDList();
                int activeCnt = 0;
                foreach (var acc in TDList)
                {
                    if (acc.isActive)
                        activeCnt++;
                }
                return (activeCnt >= 2);
            }
            else if (accountType == "4")
            {
                List<Loan> LList = getLoanList();
                int activeCnt = 0;
                foreach (var acc in LList)
                {
                    if (acc.isActive)
                        activeCnt++;
                }
                return (activeCnt >= 2);
            }
            return false;
        }
        #endregion

        #region Open Account
        private static void openAccountUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                    OPEN ACCOUNT                 ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||              1. Checking Account                ||");
            Console.WriteLine("||              2. Business Account                ||");
            Console.WriteLine("||              3. Term Deposit                    ||");
            Console.WriteLine("||              4. Loan                            ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Account Type #: ");
            string accountType = Console.ReadLine();
            if(accountType == "1" || accountType == "2" || accountType == "3" || accountType == "4")
            {
                try
                {
                    if(accountType =="1" || accountType == "2")
                        bl.OpenCnBBL(accountType);
                    else
                    {
                        if(accountType == "3")
                        {
                            Console.Write("==========>> Enter Deposit Amount: ");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            if (amount <= 0)
                                Console.WriteLine("==========> Please Enter Valid Number <==========");
                            else
                            {
                                Console.Write("==========>> Enter Maturity Date (Format: 20190611): ");
                                int mDate = Convert.ToInt32(Console.ReadLine());
                                Console.Write("==========>> Enter Interest Rate (Format: If it is 3%, enter 0.03): ");
                                double apr = Convert.ToDouble(Console.ReadLine());
                                bl.OpenTDnLBL(accountType, amount, mDate, apr);
                            }
                        }
                        else
                        {
                            Console.Write("==========>> Enter Loan Amount: ");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            if (amount <= 0)
                                Console.WriteLine("==========> Please Enter Valid Number <==========");
                            else
                            {
                                Console.Write("==========>> Enter Term (Format: in Month): ");
                                int term = Convert.ToInt32(Console.ReadLine());
                                Console.Write("==========>> Enter Interest Rate (Format: If it is 3%, enter 0.03): ");
                                double apr = Convert.ToDouble(Console.ReadLine());
                                bl.OpenTDnLBL(accountType, amount, term, apr);
                            }
                        }
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            else
                Console.WriteLine("==========>> Invalid Input <<==========");
        }
        #endregion

        #region Login
        private static bool loginUI()
        {
            Console.WriteLine("\n++=================================================++");
            Console.WriteLine("||                  Hello! Welcome                 ||");
            Console.WriteLine("++=================================================++\n");
            Console.WriteLine("++=================================================++");
            Console.WriteLine("||                  1. Register                    ||");
            Console.WriteLine("||                  2. Login                       ||");
            Console.WriteLine("++=================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string menu = Console.ReadLine();
            if (menu == "1")
            {
                Console.Write("==========>> Enter Your Firstname: ");
                string firstname = Console.ReadLine();
                Console.Write("==========>> Enter Your Lastname: ");
                string lastname = Console.ReadLine();
                Console.Write("==========>> Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("==========>> Enter Your Password: ");
                string password = Console.ReadLine();
                try { bl.RegisterBL(firstname, lastname, username, password); return true; }
                catch (Exception e) { Console.WriteLine(e.Message); return false; }
            }
            else if (menu == "2")
            {
                Console.Write("==========>> Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("==========>> Enter Your Password: ");
                string password = Console.ReadLine();
                try
                {
                    bool isFound = bl.Login(username, password);
                    if (isFound)
                    {
                        Console.WriteLine("==========>> Welcome Back! <<==========");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("==========>> Incorrect Username or Password <<==========");
                        return false;
                    }
                        
                }
                catch (Exception e) { Console.WriteLine(e.Message); return false; }
            }
            else
            {
                Console.WriteLine("==========>> Invalid Input <<==========");
                return false;
            }
        }
        #endregion

        #region MainUI
        private static void mainUI()
        {
            Console.WriteLine("\n++===================================================++");
            Console.WriteLine("||        1. Open a new account                      ||");
            Console.WriteLine("||        2. Close an account                        ||");
            Console.WriteLine("||        3. Withdraw                                ||");
            Console.WriteLine("||        4. Deposit                                 ||");
            Console.WriteLine("||        5. Transfer                                ||");
            Console.WriteLine("||        6. Pay Loan Installment                    ||");
            Console.WriteLine("||        7. Display list of account                 ||");
            Console.WriteLine("||        8. Display Transaction for an account      ||");
            Console.WriteLine("++===================================================++\n");
            Console.Write("==========>> Enter Menu #: ");
            string menu = Console.ReadLine();
            switch(menu)
            {
                case "1":
                    openAccountUI();
                    break;
                case "2":
                    closeAccountUI();
                    break;
                case "3":
                    withdrawUI();
                    break;
                case "4":
                    depositUI();
                    break;
                case "5":
                    transferUI();
                    break;
                case "6":
                    payLoanUI();
                    break;
                case "7":
                    displayAccountUI();
                    break;
                case "8":
                    transactionUI();
                    break;
                default:
                    Console.WriteLine("==========>> Invalid Input <<==========");
                    break;
            }
        }
        #endregion
    }
}