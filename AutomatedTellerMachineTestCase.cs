// ===============================
// Title of the Assesment : ATM Functionality
// Auther                 : Vinoth Kanth
// Starting Date          : 7/8/2018
//
// It is an Test case file for Automated Teller Machine Class,
// All members functions of ATM class are tested using mock value.
// ================================

/// <summary>
/// The IndianBankTestCase Namespance
/// </summary>
namespace IndianBankTestCase
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using IndianBank;

    /// <summary>
    /// The Automated Teller Machine Test Case class
    /// </summary>
    [TestClass]
    public class AutomatedTellerMachineTestCase
    {
        AutomatedTellerMachine atm = new AutomatedTellerMachine();

        /// <summary>
        /// To check the user details has been successfully added or not
        /// </summary>
        [TestMethod]
        public void TestCheckAddCustomer()
        {
            string firstUser = "NVK";
            string secondUser = "VVK";
            int firstUserPinNumber = 1234;
            int secondUserPinNumber = 5252;

            Assert.IsTrue(atm.IsAddCustomerDetail(firstUser, firstUserPinNumber));
            Assert.IsTrue(atm.IsAddCustomerDetail(secondUser, secondUserPinNumber));
        }

        /// <summary>
        /// To check the given user details is valid or not
        /// </summary>
        [TestMethod]
        public void TestValidUser()
        {
            string userName = "NVK";
            int userPinNumber = 1234;

            Assert.IsTrue(atm.IsValidCustomer(userName, userPinNumber));

            Assert.IsFalse(atm.IsValidCustomer(userName, 5252));
            Assert.IsFalse(atm.IsValidCustomer("vvk", userPinNumber));
        }

        /// <summary>
        /// To check the customer account balance
        /// </summary>
        [TestMethod]
        public void TestCheckBalance()
        {
            string userName = "Vj";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));

            Assert.AreEqual("100000", atm.GetAccountBalance(userName, userPinNumber));

            atm.GetWithdrawAmount(userName, userPinNumber, 4000);
            Assert.AreEqual("96000", atm.GetAccountBalance(userName, userPinNumber));

            Assert.AreNotEqual("2000", atm.GetAccountBalance(userName, userPinNumber));

            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", atm.GetAccountBalance("Name", userPinNumber));
            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", atm.GetAccountBalance(userName, 5766));
        }

        /// <summary>
        /// To check the reset pin function 
        /// </summary>
        [TestMethod]
        public void TestPinReset()
        {
            string userName = "Vj";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));
            string validPinNumber = atm.ResetPinNumber(userName, userPinNumber, 2525);
            string invalidPinNumber = atm.ResetPinNumber(userName, 3333, 2525);

            Assert.AreEqual(" YOUR ATM-PIN NUMBER HAS BEEN RESETED SUCCESSFULLY..!!!", validPinNumber);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", validPinNumber);

            Assert.AreNotEqual(" YOUR ATM-PIN NUMBER HAS BEEN RESETED SUCCESSFULLY..!!!", invalidPinNumber);
            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", invalidPinNumber);

        }

        /// <summary>
        /// To check deposit method
        /// </summary>
        [TestMethod]
        public void TestDeposit()
        {
            string userName = "Vk";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));
            string validAmount = atm.AddDepositAmount(userName, userPinNumber, 5000.00);
            string invalidAmount = atm.AddDepositAmount(userName, userPinNumber, -5000.00);
            string invalidUser = atm.AddDepositAmount("Rk", 3388, 5000.00);

            DateTime currentDateAndTime = DateTime.Now;
            string statment = currentDateAndTime.ToString()+" "+5000+" + (DEPOSIT) ";

            Assert.AreEqual(statment, validAmount);
            Assert.AreNotEqual("ENTER VALID AMOUNT", validAmount);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", validAmount);

            Assert.AreEqual("ENTER VALID AMOUNT", invalidAmount);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", invalidAmount);
            Assert.AreNotEqual(statment, invalidAmount); 

            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", invalidUser);
            Assert.AreNotEqual("ENTER VALID AMOUNT", invalidUser);
            Assert.AreNotEqual(statment, invalidUser); 

        }

        /// <summary>
        /// To check Withdraw method
        /// </summary>
        [TestMethod]
        public void TestWithdraw()
        {
            string userName = "Vk";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));
            string validAmount = atm.GetWithdrawAmount(userName, userPinNumber, 5000.00);
            string validAmountOne = atm.GetWithdrawAmount(userName, userPinNumber, -5000.00);
            string invalidUserName = atm.GetWithdrawAmount("Rk", 3388, 5000.00);
            string amountOutOfRange = atm.GetWithdrawAmount(userName, userPinNumber, 10000000);
            string multipleByHundred = atm.GetWithdrawAmount(userName, userPinNumber, 640);

            DateTime currentDateAndTime = DateTime.Now;
            string statment = currentDateAndTime.ToString() + " " + 5000 + " -( WITHDRAW ) ";
            
            Assert.AreEqual(statment, validAmount);
            Assert.AreNotEqual("ENTER VALID AMOUNT", validAmount);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", validAmount);

            Assert.AreEqual(statment, validAmountOne);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", validAmountOne);

            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", invalidUserName);
            Assert.AreNotEqual("ENTER VALID AMOUNT", invalidUserName);
            Assert.AreNotEqual(statment, invalidUserName);

            Assert.AreEqual("INSUFFICENT BALANCE", amountOutOfRange);
            Assert.AreNotEqual("ENTER VALID AMOUNT", amountOutOfRange);

            Assert.AreEqual(" PLEASE ENTER THE AMOUNT IN MULTIPLES OF 100", multipleByHundred);
            Assert.AreNotEqual("PLEASE CHECK YOUR PIN-NUMBER", multipleByHundred);
            Assert.AreNotEqual("INSUFFICENT BALANCE", multipleByHundred);

        }

        /// <summary>
        /// To check the mini statement has been added successfully
        /// </summary>
        [TestMethod]
        public void TestSetMiniStatement()
        {
            string userName = "Vk";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));
            DateTime currentDateAndTime = DateTime.Now;

            string summaryOne = atm.SetMiniStatement(" -( WITHDRAW ) ",5000.00);
            string statmentOne = currentDateAndTime.ToString() + " " + 5000 + " -( WITHDRAW ) ";

            string summaryTwo = atm.SetMiniStatement(" + (DEPOSIT) ", 5000.00);
            string statmentTwo = currentDateAndTime.ToString() + " " + 5000 + " + (DEPOSIT) ";
            Assert.AreEqual(statmentTwo, summaryTwo);
            

        }

        /// <summary>
        /// To check the mini statement get successfully or not
        /// </summary>
        [TestMethod]
        public void TestGetMiniStatement()
        {
            string userName = "Vk";
            int userPinNumber = 5252;
            Assert.IsTrue(atm.IsAddCustomerDetail(userName, userPinNumber));
            DateTime currentDateAndTime = DateTime.Now;

            string summaryDeposit = atm.AddDepositAmount(userName, userPinNumber, 5000.00);
            string summaryWithdraw = atm.GetWithdrawAmount(userName, userPinNumber, 5000.00);
            string summaryNotEqual = atm.GetWithdrawAmount("RP", userPinNumber, 5000.00);

            string checkSummary = summaryDeposit + "\n" + summaryWithdraw + "\n";
            string summary = atm.GetMiniStatement(userName, userPinNumber);
            Assert.AreEqual(checkSummary, summary);
            Assert.AreEqual("PLEASE CHECK YOUR PIN-NUMBER", summaryNotEqual);


        }


    }
}
