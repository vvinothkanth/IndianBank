// ===============================
// Title of the Assesment : ATM Functionality
// Auther                 : Vinoth Kanth
// Starting Date          : 7/8/2018
//
//  This class file is used to perform the basic operation of ATM,
//  such as withdrawal , deposit, check balance, get mini statement and reset atm pin number.
// ================================

/// <summary>
/// The IndianBank NameSpace
/// </summary>
namespace IndianBank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The Automated Teller Machine Class
    /// </summary>
    public class AutomatedTellerMachine
    {
        /// <summary>
        /// To initialize atm pin number
        /// </summary>
        private int _atmPinNumber;

        /// <summary>
        /// To initialize customer name
        /// </summary>
        private string _customerName;

        /// <summary>
        /// To initialize account balance
        /// </summary>
        private double _accountBalance = 100000.00;

        /// <summary>
        /// To initialize mini statement
        /// </summary>
        private Dictionary<int, string> _miniStatement = new Dictionary<int, string>();
        
        /// <summary>
        /// To get and set atm pin number
        /// </summary>
        private int _AtmPinNumber
        {
            get { return _atmPinNumber; }
            set { _atmPinNumber = value; }
        }

        /// <summary>
        /// To get and set customer name
        /// </summary>
        private string _CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        /// <summary>
        /// To get and set account balance
        /// </summary>
        private double _AccountBalance
        {
            get { return _accountBalance; }
            set { _accountBalance = value; }
        }

        /// <summary>
        /// To get and set mini statement
        /// </summary>
        private Dictionary<int, string> _MiniStatement
        {
            get { return _miniStatement; }
        }

        /// <summary>
        /// To add customer details
        /// </summary>
        /// <param name="customerName"> Customer name</param>
        /// <param name="atmPinNumber"> Atm pin number</param>
        /// <returns>boolan value</returns>
        public bool IsAddCustomerDetail( string customerName, int atmPinNumber )
        {
            bool isAddedOrNot = false;
            try
            {
                _CustomerName = customerName;
                _AtmPinNumber = atmPinNumber;
                isAddedOrNot = true;
            }
            catch ( ArgumentNullException )
            {
                throw new ArgumentNullException();
            }

            return isAddedOrNot;

        }

        /// <summary>
        /// To check the customer is valid or not
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="atmPinNumber">Atm pin number</param>
        /// <returns>true/false</returns>
        public bool IsValidCustomer( string customerName, int atmPinNumber )
        {
            bool isValidCustomerOrNot = false;
            try
            {
                if ( atmPinNumber.Equals( _AtmPinNumber ) && customerName.Contains( _CustomerName ) )
                {
                    isValidCustomerOrNot = true;
                }
                else
                {
                    isValidCustomerOrNot = false;
                }
            }
            catch ( ArgumentException )
            {
                throw new ArgumentException( );
            }

            return isValidCustomerOrNot;
        }

        /// <summary>
        /// To return the account balance of the customer
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="atmPinNumber">Pin number</param>
        /// <returns>Current balance</returns>
        public string GetAccountBalance( string customerName, int atmPinNumber )
        {
            string accountBalanceStatement = string.Empty;
            try
            {
                if ( IsValidCustomer( customerName, atmPinNumber ) )
                {
                    accountBalanceStatement = Convert.ToString( _AccountBalance );
                }
                else
                {
                    accountBalanceStatement = "PLEASE CHECK YOUR PIN-NUMBER";
                }

            }
            catch ( ArgumentException )
            {
                throw new ArgumentException( );
            }

            return accountBalanceStatement;
        }

        /// <summary>
        /// To reset the atm pin number
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="oldPinNumber">Old pin number</param>
        /// <param name="newPinNumber">New pin number</param>
        /// <returns> Reset summary </returns>
        public string ResetPinNumber( string customerName, int oldPinNumber, int newPinNumber )
        {
            string pinNumberResetStatement = string.Empty;
            try
            {
                if ( IsValidCustomer( customerName, oldPinNumber ) )
                {
                    _AtmPinNumber = newPinNumber;
                    pinNumberResetStatement = " YOUR ATM-PIN NUMBER HAS BEEN RESETED SUCCESSFULLY..!!!";
                }
                else
                {
                    pinNumberResetStatement = "PLEASE CHECK YOUR PIN-NUMBER";
                }

            }
            catch ( ArgumentException )
            {
                throw new ArgumentException( );
            }

            return pinNumberResetStatement;

        }

        /// <summary>
        /// To deposit the amount to customer account
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="pinNumber">Pin number</param>
        /// <param name="depositAmount">Deposit amount</param>
        /// <returns>Deposit summary</returns>
        public string AddDepositAmount( string customerName, int pinNumber, double depositAmount )
        {
            string depositStatement = string.Empty;
            try
            {
                if ( IsValidCustomer( customerName, pinNumber ) )
                {
                    if ( depositAmount > 0.0 )
                    {
                        _AccountBalance += depositAmount;
                        depositStatement = SetMiniStatement( " + (DEPOSIT) ", depositAmount );
                    }
                    else
                    {
                        depositStatement = "ENTER VALID AMOUNT";
                    }
                }
                else
                {
                    depositStatement = "PLEASE CHECK YOUR PIN-NUMBER";
                }
            }
            catch ( ArithmeticException )
            {
                throw new ArithmeticException( );
            }

            return depositStatement;
        }

        /// <summary>
        /// To withdraw the amount from customer account
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="pinNumber">Pin number</param>
        /// <param name="withdrawAmount">Withdraw amount</param>
        /// <returns>Withdraw summary</returns>
        public string GetWithdrawAmount( string customerName, int pinNumber, double withdrawAmount )
        {
            string withdrawStatement = string.Empty;
            try
            {
                if ( IsValidCustomer( customerName, pinNumber ) )
                {
                    long checkAmount =Convert.ToInt64( withdrawAmount );
                    if ( checkAmount % 100 == 0 )
                    {
                        if ( withdrawAmount < 0.0 )
                        {
                            withdrawAmount = -withdrawAmount;
                        }
                        if ( withdrawAmount <= _AccountBalance )
                        {
                            _AccountBalance -= withdrawAmount;
                            withdrawStatement = SetMiniStatement( " -( WITHDRAW ) ", withdrawAmount );
                        }
                        else
                        {
                            withdrawStatement = "INSUFFICENT BALANCE";
                        }
                    }
                    else
                    {
                        withdrawStatement = " PLEASE ENTER THE AMOUNT IN MULTIPLES OF 100";
                    }
                }
                else
                {
                    withdrawStatement = "PLEASE CHECK YOUR PIN-NUMBER";
                }
            }
            catch ( ArithmeticException )
            {
                throw new ArithmeticException( );
            }

            return withdrawStatement;
        }

        /// <summary>
        /// To set the mini statement for customer action like deposit and withdraw
        /// </summary>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="transactionAmount">TransactionAmount</param>
        /// <returns>Ministatement</returns>
        public string SetMiniStatement( string transactionType, double transactionAmount )
        {
            string miniStatement = string.Empty;
            try
            {
                DateTime currentDateAndTime = DateTime.Now;
                string statment = currentDateAndTime.ToString() + " " + transactionAmount + transactionType;
                int miniStatementCount = _MiniStatement.Count;

                _MiniStatement.Add(miniStatementCount, statment);
                miniStatement = _MiniStatement[miniStatementCount];
            }
            catch ( IndexOutOfRangeException )
            {
                throw new IndexOutOfRangeException( );
            }

            return miniStatement;
        }

        /// <summary>
        /// To get ministatement
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="pinNumber">Pin number</param>
        /// <returns>Mini statement</returns>
        public string GetMiniStatement( string customerName, int pinNumber  )
        {
            string miniStatmentSummary = string.Empty;
            try
            {
                if ( IsValidCustomer( customerName, pinNumber) )
                {
                    for ( int miniStatementCount = 0; miniStatementCount < _MiniStatement.Count; miniStatementCount++ )
                    {
                        miniStatmentSummary += _MiniStatement[ _MiniStatement.Keys.ElementAt( miniStatementCount ) ] + "\n";
                    }
                }
                else
                {
                    miniStatmentSummary = "YOU ARE NOT VALID USER";
                }
            }
            catch ( IndexOutOfRangeException )
            {
                throw new IndexOutOfRangeException( );
            }

            return miniStatmentSummary;
        }
    }
}
