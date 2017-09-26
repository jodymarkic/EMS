/*
 *  FILENAME        : ContractEmployeeTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the ContractEmployee class.
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;


namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class ContractEmployeeTestCases
    /// \brief <b>Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the ContractEmployee Class
    /// that need potentially normal, exception, or boundary testing.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass()]
    public class ContractEmployeeTestCases
    {        
        /// \test <b>CE.1.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of Validate method when its passed a contract employee object
        /// to validates it's fields from calling other validation methods.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;ContractEmployee : ctEmployee : "new ContractEmployee()" 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;

            ContractEmployee ctEmployee = new ContractEmployee();

            ctEmployee.LastName = "TDA Limited";
            ctEmployee.DOB = DateTime.Parse("2012-08-24");
            ctEmployee.SIN = "123456782";
            ctEmployee.ContractStartDate = DateTime.Parse("2013-08-24");
            ctEmployee.ContractStopDate = DateTime.Parse("2015-06-13");
            ctEmployee.FixedContractAmount = 123560.00;

            actual = ContractEmployee.validate(ctEmployee);
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>CE.2.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path for the Validate method validates contract employee object
        /// when all of the validate methods return true booleans.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : lastName : "TDA Limited"
        /// <br />&emsp;DateTime : dob : "2012-08-24"
        /// <br />&emsp;string : sin : "123456782"
        /// <br />&emsp;DateTime : contractStartDate : "2013-08-24"
        /// <br />&emsp;DateTime : contractStopDate : "2015-06-13"
        /// <br />&emsp;double : fixedContractAmount : 123560.00
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateOverloadNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;
            string lastName = "TDA Limited";
            DateTime dob = DateTime.Parse("2012-08-24");
            string SIN = "123456782";
            DateTime contractStartDate = DateTime.Parse("2013-08-24");
            DateTime contractStopDate = DateTime.Parse("2015-06-13");
            double fixedContractAmount = 123560.00;

            actual = ContractEmployee.validate(lastName, dob, SIN, contractStartDate, contractStopDate, fixedContractAmount);
            Assert.AreEqual(actual, expected);


        }

        
        /// \test <b>CE.2.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the Validate method invalidates a contract employee object
        /// when one of the validate methods that it calls returns a false boolean.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : lastName : "TDA Limited"
        /// <br />&emsp;DateTime : dob : "1993-08-24"
        /// <br />&emsp;string : sin : "932345678"
        /// <br />&emsp;DateTime : contractStartDate : "2012-08-24"
        /// <br />&emsp;DateTime : contractStopDate : "2015-06-13"
        /// <br />&emsp;double : fixedContractAmount : 123560.00
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateOverloadExceptionTestCase()
        {
            bool expected = false;
            bool? actual = null;
            string lastName = "TDA Limited";
            DateTime dob = DateTime.Parse("1993-08-24");
            string SIN = "932345678";
            DateTime contractStartDate = DateTime.Parse("2012-08-24");
            DateTime contractStopDate = DateTime.Parse("2015-06-13");
            double fixedContractAmount = 123560.00;

            actual = ContractEmployee.validate(lastName, dob, SIN, contractStartDate, contractStopDate, fixedContractAmount);
            Assert.AreEqual(expected, actual);

        }
    }
}
