/*
 *  FILENAME        : ParttimeEmployeeTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the PartimeEmployee class.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class ParttimeEmployeeTestCases
    /// \brief <b>Description</b>
    /// \details
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass()]
    public class ParttimeEmployeeTestCases
    {
        /// \test <b>PT.1.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of Validate method when its passed a parttime employee object
        /// to validates it's fields from calling other validation methods.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;ParttimeEmployee : ctEmployee : "new ParttimeEmployee()" 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;
            ParttimeEmployee ptEmployee = new ParttimeEmployee();
            ptEmployee.FirstName = "Jody";
            ptEmployee.LastName = "Markic";
            ptEmployee.DOB = DateTime.Parse("1993-08-24");
            ptEmployee.SIN = "123456782";
            ptEmployee.DateOfHire = DateTime.Parse("2012-04-12");
            ptEmployee.DateOfTermination = DateTime.Parse("2015-06-04");
            ptEmployee.HourlyRate = 20.50;

            actual = ParttimeEmployee.validate(ptEmployee);
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>PT.2.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path for the Validate method validates parttime employee object
        /// when all of the validate methods return true booleans.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "jody" 
        /// <br />&emsp;string : lastName : "Markic"
        /// <br />&emsp;DateTime : dob : "1993-08-24"
        /// <br />&emsp;string : sin : "123456782"
        /// <br />&emsp;DateTime : dateOfHire : "2012-04-12"
        /// <br />&emsp;DateTime : dateOfTermination : "2015-06-04"
        /// <br />&emsp;double : hourlyRate : 20.50
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateOverloadedNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;
            string firstName = "Jody";
            string lastName = "Markic";
            DateTime dob = DateTime.Parse("1993-08-24");
            string sin = "123456782";
            DateTime dateOfHire = DateTime.Parse("2012-04-12");
            DateTime dateOfTermination = DateTime.Parse("2015-06-04");
            double hourlyRate = 20.50;

            actual = ParttimeEmployee.validate(firstName, lastName, dob, sin, dateOfHire, dateOfTermination, hourlyRate);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// \test <b>PT.2.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the Validate method invalidates a parttime employee object
        /// when one of the validate methods that it calls returns a false boolean.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "Nathan" 
        /// <br />&emsp;string : lastName : "Bray"
        /// <br />&emsp;DateTime : dob : "1993-09-15"
        /// <br />&emsp;string : sin : "333333334"
        /// <br />&emsp;DateTime : dateOfHire : "2015-04-12"
        /// <br />&emsp;DateTime : dateOfTermination : "201-08-13"
        /// <br />&emsp;double : hourlyRate : 20.50
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateOverloadExceptionTestCase()
        {
            bool expected = false;
            bool? actual = null;
            string firstName = "Jody";
            string lastName = "Markic";
            DateTime dob = DateTime.Parse("1993-08-24");
            string sin = "123456782";
            DateTime dateOfHire = DateTime.Parse("2015-04-12");
            DateTime dateOfTermination = DateTime.Parse("2012-06-04");
            double hourlyRate = 20.50;

            actual = ParttimeEmployee.validate(firstName, lastName, dob, sin, dateOfHire, dateOfTermination, hourlyRate);
            Assert.AreEqual(expected, actual);
        }
    }
}
