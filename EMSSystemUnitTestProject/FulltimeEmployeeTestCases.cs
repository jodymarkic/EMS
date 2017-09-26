/*
 *  FILENAME        : FulltimeEmployeeTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the FulltimeEmployee class.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class FulltimeEmployeeTestCases
    /// \brief <b>Description</b>
    /// \details >Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the FulltimeEmployee Class
    /// that need potentially normal, exception, or boundary testing.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass()]
    public class FulltimeEmployeeTestCases
    {
        /// \test <b>FT.1.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of validateDateOfHire method succesfully takes a Employee's date
        /// of hire that is not a future date and but after the Employee's date of birth.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : DateOfBirth : "1970-01-01" 
        /// <br />&emsp;DateTime : dateOfHire  : "1999-08-24"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateDateOfHireNormalTestCase()
        {
            //Arrange
            DateTime DateOfBirth = DateTime.Parse("1970-01-01");
            DateTime dateOfHire = DateTime.Parse("1999-08-24");
            bool expected = true;
            bool? actual = null;
            //Act
            actual = FulltimeEmployee.validateStartDate(DateOfBirth, dateOfHire);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.1.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the validateDateOfHire method invalidates the Employee's date
        /// of hire if it is a future date, or before the Employee's date of birth.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : dateOfHire : "2025-12-14" 
        /// <br />&emsp;DateTime : DateTime.Now  : "current date"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateDateOfHireExceptionTestCase()
        {
            //Arrange
            bool expected = false;
            bool? actual = null;
            DateTime dateOfHire = DateTime.Parse("2025-12-14");
            //Act
            actual = FulltimeEmployee.validateStartDate(dateOfHire,DateTime.Now);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.1.3</b><br />&emsp;
        /// This Unit test is to make sure an boundary value for the validateDateOfHire is a valid Employee's date
        /// of hire if it's the current date, and not a future one.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : DateTime.Now : "current date" 
        /// <br />&emsp;DateTime : DateTime.Now  : "current date"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateDateOfHireBoundaryTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            //Act
            actual = FulltimeEmployee.validateStartDate(DateTime.Now, DateTime.Now);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.2.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of validateDateOfTermination method succesfully takes a Employee's date
        /// of Termination that is not a future date and but after the Employee's date of hire.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : DateOfHire : "1999-08-24" 
        /// <br />&emsp;DateTime : dateOfTermination  : "2003-05-13"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateDateOfTerminationNormalTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            DateTime dateOfHire = DateTime.Parse("1999-08-24");
            DateTime dateOfTermination = DateTime.Parse("2003-05-13");
            //Act
            actual = FulltimeEmployee.validateStopDate(dateOfHire, dateOfTermination);
            //Assert
            Assert.AreEqual(expected, actual);

        }

        
        /// \test <b>FT.2.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the validateDateOfTermination method invalidates the Employee's date
        /// of Termination if it is a future date, or before the Employee's date of hire.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : dateOfHire : "1999-08-24" 
        /// <br />&emsp;DateTime? : dateOfTermination  : "1997-05-13"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateDateOfTerminationExceptionTestCase()
        {
            //Arrange
            bool expected = false;
            bool? actual = null;
            DateTime dateOfHire = DateTime.Parse("1999-08-24");
            DateTime? dateOfTermination = DateTime.Parse("1997-05-13");
            //Act
            actual = FulltimeEmployee.validateStopDate(dateOfHire, dateOfTermination);
            //Assert
            Assert.AreEqual(expected, actual);

        }

        
        /// \test <b>FT.2.3</b><br />&emsp;
        /// This Unit test is to make sure an boundary value for the validateDateOfTermination is a valid Employee's date
        /// of Termination if it's the same day as a Date of Hire, and not before.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : dateOfHire : "1993-08-24" 
        /// <br />&emsp;DateTime? : DateTime.Now  : "1993-08-24"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateDateOfTerminationBoundaryTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            DateTime dateOfHire = DateTime.Parse("1999-08-24");
            DateTime? dateOfTermination = DateTime.Parse("1999-08-24");
            //Act
            actual = FulltimeEmployee.validateStopDate(dateOfHire, dateOfTermination);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.2.3.1</b><br />&emsp;
        /// This Unit test is to make sure an boundary value for the validateDateOfTermination is a valid Employee's date
        /// of Termination if its value is null and not an actual date time.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;DateTime : dateOfHire : "1993-08-24" 
        /// <br />&emsp;DateTime? : dateOfTermination : "null"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateDateOfTerminationNullBoundaryTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            DateTime dateOfHire = DateTime.Parse("1999-08-24");
            DateTime? dateOfTermination = null;
            //Act
            actual = FulltimeEmployee.validateStopDate(dateOfHire, dateOfTermination);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.3.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of ValidateSalary method succesfully takes a Employee's Salary
        /// that is a non negative double value, or zero.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;double : salary : 40567.58 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateSalaryNormalTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            double salary = 40567.58;
            //Act
            actual = FulltimeEmployee.validatePay(salary);
            //Assert
            Assert.AreEqual(expected, actual);

        }

        
        /// \test <b>FT.3.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the ValidateSalary method invalidates the Employee's Salary
        /// if it is a negative valued double
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;double : Salary : -40567.58 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateSalaryExceptionTestCase()
        {
            //Arrange
            bool expected = false;
            bool? actual = null;
            double salary = -40567.58;
            //Act
            actual = FulltimeEmployee.validatePay(salary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.3.3</b><br />&emsp;
        /// This Unit test is to make sure an boundary value for the ValidateSalary is a valid Employee's Salary
        /// if that is a non negative double value, or zero.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;double : salary : 0.000001
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateSalaryBoundaryTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            double salary = 0.000001;
            //Act
            actual = FulltimeEmployee.validatePay(salary);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.4.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of Validate method when its passed a fulltime employee object
        /// to validates it's fields from calling other validation methods.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;FulltimeEmployee : ftEmployee : "new FulltimeEmployee()" 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateNormalTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            FulltimeEmployee ftEmployee = new FulltimeEmployee();
            ftEmployee.FirstName = "Jody";
            ftEmployee.LastName = "Markic";
            ftEmployee.DOB = DateTime.Parse("1993-08-24");
            ftEmployee.SIN = "123456782";
            ftEmployee.DateOfHire = DateTime.Parse("2012-04-12");
            ftEmployee.DateOfTermination = DateTime.Parse("2015-06-04");
            ftEmployee.Salary = 54750.00;
            //Act
            actual = FulltimeEmployee.validate(ftEmployee);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>FT.5.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path for the Validate method validates fulltime employee object
        /// when all of the validate methods return true booleans.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "Nathan" 
        /// <br />&emsp;string : lastName : "Bray"
        /// <br />&emsp;DateTime : dob : "1993-09-15"
        /// <br />&emsp;string : sin : "333333334"
        /// <br />&emsp;DateTime : dateOfHire : "2012-04-12"
        /// <br />&emsp;DateTime : dateOfTermination : "2015-08-13"
        /// <br />&emsp;double : salary : "54749.00"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateOverloadNormalTestCase()
        {
            //Arrange
            bool expected = true;
            bool? actual = null;
            string firstName = "Nathan";
            string lastName = "Bray";
            DateTime dob = DateTime.Parse("1993-09-15");
            string sin = "333333334";
            DateTime dateOfHire = DateTime.Parse("2012-04-12");
            DateTime dateOfTermination = DateTime.Parse("2015-08-13");
            double salary = 54749.00;
            //Act
            actual = FulltimeEmployee.validate(firstName, lastName, dob, sin, dateOfHire, dateOfTermination, salary);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        

        /// \test <b>FT.5.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the Validate method invalidates a fulltime employee object
        /// when one of the validate methods that it calls returns a false boolean.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "Nathan" 
        /// <br />&emsp;string : lastName : "Bray"
        /// <br />&emsp;DateTime : dob : "1993-09-15"
        /// <br />&emsp;string : sin : "333333334"
        /// <br />&emsp;DateTime : dateOfHire : "2015-04-12"
        /// <br />&emsp;DateTime : dateOfTermination : "201-08-13"
        /// <br />&emsp;double : salary : "54749.00"
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod()]
        public void ValidateOverloadExceptionTestCase()
        {
            //Arrange
            bool expected = false;
            bool? actual = null;
            string firstName = "Nathan";
            string lastName = "Bray";
            DateTime dob = DateTime.Parse("1993-09-15");
            string sin = "333333334";
            DateTime dateOfHire = DateTime.Parse("2015-04-12");
            DateTime dateOfTermination = DateTime.Parse("2012-08-13");
            double salary = 54749.00;
            //Act
            actual = FulltimeEmployee.validate(firstName, lastName, dob, sin, dateOfHire, dateOfTermination, salary);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
