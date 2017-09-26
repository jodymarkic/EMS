/*
 *  FILENAME        : EmployeeTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the Employee class.
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class EmployeeTestCases
    /// \brief <b>Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the Employee Class
    /// that need potentially normal, exception, or boundary testing.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass]
    public class EmployeeTestCases
    {
        #region Validate Name
        /// \test <b>EMP.1.1.1</b><br />&emsp;
        /// This test asserts that a persons' name will be properly validated if it is in fact a valid name
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> "Robert" <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True <br />
        [TestMethod]
        public void validateNameNormalTest()
        {
            // Assemble
            bool expected = true;
            string name = "Robert";
            // Act
            bool actual = Employee.validateName(EmployeeType.FT,name);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>EMP.1.1.2</b><br />&emsp;
        /// This test asserts that when a company name is passed in, it will successfully validate
        /// that with the alternate regular expression
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> "Robert's Store 98" <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True <br />
        [TestMethod]
        public void validateNameNormalTestCT()
        {
            // Assemble
            bool expected = true;
            string name = "Robert's Store 98";
            // Act
            bool actual = Employee.validateName(EmployeeType.CT, name);
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>EMP.1.2</b><br />&emsp;
        /// This test asserts that a the validate name methos will properly indicate an invalid name
        /// if it has characters that are invalid
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> "1233456" <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False <br />
        [TestMethod]
        public void validateNameFTExceptionTest()
        {
            // Assemble
            bool expected = false;
            string name = "123456";
            // Act
            bool actual = Employee.validateName(EmployeeType.FT,name);
            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region Validate DOB
        /// \test <b>EMP.2.1</b><br />&emsp;
        /// This test asserts that a valid date can be used as a date of birth and will return true
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> "1970-01-01" <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True <br />
        [TestMethod]
        public void validateDOBNormalTest()
        {
            // Assemble
            bool expected = true;
            DateTime dob = DateTime.Parse("1970-01-01");
            // Act
            bool actual = Employee.validateDob(dob);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>EMP.2.2</b><br />&emsp;
        /// This method asserts that the validation of a Date of birth can not be in the furture
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs: </b>
        /// <br />&emsp;"2021-02-01" This a date in the furture
        /// <b>Output: </b><br />&emsp;
        /// Expected -False: Indicating the date passed in is an invalid date of birth  <br />&emsp;
        /// Actual - False <br />
        /// </summary>
        [TestMethod]
        public void validateDOBFuturetExceptionTest()
        {
            // Assemble
            bool expected = false;
            bool? actual = null;
            DateTime dob = DateTime.Parse("2021-02-01");
            actual = Employee.validateDob(dob);
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>EMP.2.3</b><br />&emsp;
        /// This method asserts that the validation of a Date of birth can be current date
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs: </b>
        /// <br />&emsp;The current date
        /// <b>Output: </b><br />&emsp;
        /// Expected - True: Indicating the date passed in is valid to be a date of birth <br />&emsp;
        /// Actual -True <br />
        /// </summary>
        [TestMethod]
        public void validateDOBBoundaryTest()
        {
            // Assemble
            bool expected = true;
            bool? actual = null;
            actual = Employee.validateDob(DateTime.Now);
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region Validate SIN
        /// \test <b>EMP.3.1.1</b><br />&emsp;
        /// This method asserts that the validation of a SIN will properly be validated.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs: </b>
        /// <br />&emsp;A Fulltime Employee type
        /// <br />&emsp;123456792 as a valid SIN <br />
        /// <br />&emsp;The Date of birth of the employee being tested 
        /// <b>Output: </b><br />&emsp;
        /// Expected - True: Indicating the SIN is valid <br />&emsp;
        /// Actual - True <br />
        /// </summary>
        [TestMethod]
        public void validateSinNormalTest()
        {
            // Assemble
            bool expected = true;
            bool? actual = null;
            string sin = "123456782";
            actual = Employee.validateSIN(EmployeeType.FT,sin,DateTime.Now);
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>EMP.3.1.2</b><br />&emsp;
        /// This method asserts that the validation of a valid SIN compared to the date for an employee
        /// will pass properly
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs: </b>
        /// <br />&emsp;A Contract Employee type
        /// <br />&emsp;123456792 as a valid SIN <br />
        /// <br />&emsp;The Date of birth of the employee being tested 
        /// <b>Output: </b><br />&emsp;
        /// Expected - True: indicating the SIN is valid compaired the the date passed in<br />&emsp;
        /// Actual - True <br />
        /// </summary>
        [TestMethod]
        public void validateSinNormalTestCT()
        {
            // Assemble
            bool expected = true;
            bool? actual = null;
            string sin = "123456782";
            DateTime date = DateTime.Parse("2012-02-02");
            actual = Employee.validateSIN(EmployeeType.CT, sin, date);
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>EMP.3.2.1</b><br />&emsp;
        /// This method asserts that the validation of an invalid SIN compared to the date for an employee
        /// will properly fail
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;A Fulltime Employee type
        /// <br />&emsp;333333333 as the invalid SIN
        /// <br />&emsp;and today as the date of birth <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False: indicating that the BN does not match up with the date given<br />&emsp;
        /// Actual - False <br />
        [TestMethod]
        public void validateSinExceptionTest()
        {
            // Assemble
            bool expected = false;
            string sin = "333333333";
            // Act
            bool actual = Employee.validateSIN(EmployeeType.FT, sin, DateTime.Now);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>EMP.3.2.2</b><br />&emsp;
        /// This method asserts that the validation of an invalid BN compared to the date for a contract employee
        /// will properly fail
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;A Contract Employee type
        /// <br />&emsp;333333334 as the BN
        /// <br />&emsp;and today as the date of birth <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False: indicating that the BN does not match up with the date given<br />&emsp;
        /// Actual - False <br />
        [TestMethod]
        public void validateSinExceptionTestCT()
        {
            // Assemble
            bool expected = false;
            string sin = "333333334";
            // Act
            bool actual = Employee.validateSIN(EmployeeType.CT, sin, DateTime.Now);
            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
