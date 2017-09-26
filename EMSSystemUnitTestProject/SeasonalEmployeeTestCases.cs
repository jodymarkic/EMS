/*
 *  FILENAME        : SeasonalEmployeeTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     :  This file hold all test cases for the SeasonalEmployee class.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class SeasonalEmployeeTestCases
    /// \brief <b>Description</b>
    /// \details >Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the SeasonalEmployee Class
    /// that need potentially normal, exception, or boundary testing.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass()]
    public class SeasonalEmployeeTestCases
    {
        /// \test <b>CT.1.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path of Validate method when its passed a seasonal employee object
        /// to validates it's fields from calling other validation methods.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;SeasonalEmployee : seEmployee : "new SeasonalEmployee()" 
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;

            SeasonalEmployee seEmployee = new SeasonalEmployee();
            seEmployee.FirstName = "Jody";
            seEmployee.LastName = "Markic";
            seEmployee.DOB = DateTime.Parse("1993-08-24");
            seEmployee.SIN = "123456782";
            seEmployee.Season = Seasons.SUMMER;
            seEmployee.PiecePay = 2.50;

            actual = SeasonalEmployee.validate(seEmployee);
            Assert.AreEqual(expected, actual);
        }
        

        /// \test <b>CT.2.1</b><br />&emsp;
        /// This Unit test is to make sure the happy path for the Validate method validates parttime employee object
        /// when all of the validate methods return true booleans.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "jody" 
        /// <br />&emsp;string : lastName : "Markic"
        /// <br />&emsp;DateTime : dob : "1993-08-24"
        /// <br />&emsp;string : sin : "123456782"
        /// <br />&emsp;Seasons : season : Seasons.summer
        /// <br />&emsp;double : piecePay : 2.50
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateOverloadNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;

            string firstName = "Jody";
            string lastName = "Markic";
            DateTime dob = DateTime.Parse("1993-08-24");
            string sin = "123456782";
            Seasons season = Seasons.SUMMER;
            double piecePay = 2.50;

            actual = SeasonalEmployee.validate(firstName, lastName, dob, sin, season, piecePay);
            Assert.AreEqual(expected, actual);
        }
        

        /// \test <b>CT.2.2</b><br />&emsp;
        /// This Unit test is to make sure an exception path for the Validate method invalidates a seasonal employee object
        /// when one of the validate methods that it calls returns a false boolean.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;string : firstName : "Jody" 
        /// <br />&emsp;string : lastName : "Markic"
        /// <br />&emsp;DateTime : dob : "1993-08-24"
        /// <br />&emsp;string : sin : "123456782"
        /// <br />&emsp;Seasons : season : Seasons.summer
        /// <br />&emsp;double : piecePay : 2.50
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
            string sin = "123456784";
            Seasons season = Seasons.SUMMER;
            double piecePay = 2.50;

            actual = SeasonalEmployee.validate(firstName, lastName, dob, sin, season, piecePay);
            Assert.AreEqual(expected, actual);
        }

        
        /// \test <b>CT.3.1</b><br />&emsp;
        /// This Unit test is to make sure a happy path for the ValidateSeason method when a Seasons Enum is provided, that
        /// correspond with either summer, winter, fall, or spring.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 
        /// <br />&emsp;Seasons : summer : Seasons.summer
        /// <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod()]
        public void ValidateSeasonNormalTestCase()
        {
            bool expected = true;
            bool? actual = null;

            actual = SeasonalEmployee.validateSeason(Seasons.SUMMER);
            Assert.AreEqual(expected, actual);
        }
    }
}

