/*
 *  FILENAME        : ContainerTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the Container class.
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.AllEmployees;
using EMSSystem.TheCompany;
using System.Collections.Generic;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class ContainerTestCases
    /// \brief <b>Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the Container Class
    /// that need potentially normal, exception, or boundary testing. This Class provides unit
    /// tests for Adding, Finding, Trying to Find, Updating, Deleting, and Determining the type
    /// of employee for the Container class.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass()]
    public class ContainerTestCases
    {
        Container dBase;

        #region Add        
        /// \test <b>DBase.1.1</b><br />&emsp;
        /// This test asserts that when adding an Employee to the collection, given a valid Employee object,
        /// it will successfully be added to the collection
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> INPUTS <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - 1 : indicating the single employee added is in the collection <br />&emsp;
        /// Actual - 1         
        [TestMethod]
        public void validateAddNormalTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);
            // Act
            dBase.addEmployee(temp);
            int expected = 1;
            int actual = dBase.Length;
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>DBase.2.1</b><br />&emsp;
        /// This test asserts that when adding an Employee given the parameters of either a Fulltime, Parttime, or Contract Employee
        /// it will successfully be added to the database
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> INPUTS <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - 1 : indicating the single employee added is in the collection <br />&emsp;
        /// Actual - 1     
        [TestMethod]
        public void validateAddOverloadAllNormalTest()
        {
            // Arrange
            dBase = new Container();
            // Act
            dBase.addEmployee(EmployeeType.PT, "bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54.00);

            int expected = 1;
            int actual = dBase.Length;
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>DBase.3.1</b><br />&emsp;
        /// This test asserts that when adding an Employee given the parameters of specifically a Seasonal Employee
        /// it will successfully be added to the database
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> INPUTS <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - 1 : indicating the single employee added is in the collection <br />&emsp;
        /// Actual - 1
        [TestMethod]
        public void validateAddOverloadSENormalTest()
        {
            // Arrange
            dBase = new Container();
            // Act
            dBase.addEmployee("bobby", "tables", DateTime.Now, "123456782",
                Seasons.FALL, 999.32);

            int expected = 1;
            int actual = dBase.Length;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region Find
        /// \test <b>DBase.4.1</b><br />&emsp;
        /// This method asserts that, when given a SIN that belongs to an employee in the database,
        ///  it will return the employee object for use
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 123456782 - representing a Valid Employee object currently in the database <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - A Valid fulltime employee object with a SIN of 123456782  <br />&emsp;
        /// Actual - A Valid fulltime employee object with a SIN of 123456782
        [TestMethod]
        public void validateFindNormalTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee expected = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "333333334",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp2 = new FulltimeEmployee("bobby", "tables", DateTime.Now, "935345678",
                DateTime.Now, null, 54000.00);

            // Act
            dBase.addEmployee(temp);
            dBase.addEmployee(expected);
            dBase.addEmployee(temp2);

            FulltimeEmployee actual = (FulltimeEmployee)dBase.findEmployee("123456782");
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>DBase.4.2</b><br />&emsp;
        /// This method asserts that, when given a SIN that doesn't to an employee in the database,
        ///  it will return null indicating an entry with that SIN does not exist.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 123456784 - A SIN not belonging to anyone in the datatbase <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - null - indicating an employee not found <br />&emsp;
        /// Actual - null
        [TestMethod]
        public void validateFindExceptionTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee expected = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "333333334",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp2 = new FulltimeEmployee("bobby", "tables", DateTime.Now, "935345678",
                DateTime.Now, null, 54000.00);

            // Act
            dBase.addEmployee(temp);
            dBase.addEmployee(expected);
            dBase.addEmployee(temp2);

            FulltimeEmployee actual = (FulltimeEmployee)dBase.findEmployee("123456784");
            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        #endregion


        #region TryFind
        /// \test <b>DBase.5.1</b><br />&emsp;
        /// This method asserts that, when given a SIN that belongs to an employee in the database,
        ///  it will return true indicating an entry with that SIN exists.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 123456782 - A Valid SIN belonging to an Employee in the temporary database <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True : indicating the employee exists in the database <br />&emsp;
        /// Actual - True
        [TestMethod]
        public void validateTryFindNormalTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp2 = new FulltimeEmployee("bobby", "tables", DateTime.Now, "935345678",
                DateTime.Now, null, 54000.00);

            // Act
            dBase.addEmployee(temp);
            dBase.addEmployee(temp2);

            bool expected = true;
            bool actual = dBase.tryFindEmployee("123456782");
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>DBase.5.2</b><br />&emsp;
        /// This method asserts that, when given a SIN that does not belong to an employee in the database,
        ///  it will return false.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> 123456784 - A SIN that does not belong to any employee in the current instance of our database <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False : indicating it couldn't find it <br />&emsp;
        /// Actual - False
        [TestMethod]
        public void validateTryFindExceptionTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee temp2 = new FulltimeEmployee("Robert", "Surface", DateTime.Now, "935345678",
                DateTime.Now, null, 55000.00);

            // Act
            dBase.addEmployee(temp);
            dBase.addEmployee(temp2);

            bool expected = true;
            bool actual = dBase.tryFindEmployee("123456784");
            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        #endregion


        #region Update
        /// \test <b>DBase.6.1</b><br />&emsp;
        /// This method asserts that an employee's data is updated when given an employee has the
        /// SIN value that the employee that needs updating has.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> An Employee object changed from the original one added<br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod]
        public void validateUpdateNormalTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee tempOld = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            FulltimeEmployee tempNew = new FulltimeEmployee("Robert", "Surface", DateTime.Now, "123456782",
                DateTime.Now, null, 55000.00);

            // Act
            dBase.addEmployee(tempOld);

            bool expected = true;
            bool actual = dBase.updateEmployee(tempNew);
            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region Delete
        /// \test <b>DBase.7.1</b><br />&emsp;
        /// This method checks the Delete method to ensure it fails cleanly when it 
        /// is called to remove an Employye object from the database
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> The database with a single Fulltime Employee entry <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True: indicating a removed employee <br />&emsp;
        /// Actual - True
        [TestMethod]
        public void validateDeleteNormalTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            // Act
            dBase.addEmployee(temp);

            bool expected = true;
            bool actual = dBase.removeEmployee("123456782");
            // Assert
            Assert.AreEqual(expected, actual);
        }


        /// \test <b>DBase.7.2</b><br />&emsp;
        /// This method checks the Delete method to ensure it fails cleanly when it 
        /// is called to remove an entry that does not exist
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> A valid SIN of an employee object already removed <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - False <br />&emsp;
        /// Actual - False
        [TestMethod]
        public void validateDeleteExceptionTest()
        {
            // Arrange
            dBase = new Container();
            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);
            // Act
            dBase.addEmployee(temp);
            dBase.removeEmployee("123456782");
            bool expected = true;
            bool actual = dBase.removeEmployee("123456782");
            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        #endregion


        #region Type
        /// \test <b>DBase.8.1</b><br />&emsp;
        /// This method checks the getType method to see of it will properly return the
        /// correct type of derived class the Employee object is
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> A valid Fulltime employee object as an Employee <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - True <br />&emsp;
        /// Actual - True
        [TestMethod]
        public void validateTypeNormalTest()
        {
            // Arrange
            dBase = new Container();
            Employee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);
            // Act
            EmployeeType expected = EmployeeType.FT;
            EmployeeType actual = dBase.getType(temp);
            // Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion


        /// \test <b>DBase.8.1</b><br />&emsp;
        /// This method checks the Join methods to see of it will properly return the
        /// correct string containing the information of an employee
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> A valid Fulltime employee object as an Employee <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - FT|tables|bobby|123456782|Datetime.Now|Datetime.Now|N/A|54000 <br />&emsp;
        /// Actual - FT|tables|bobby|123456782|Datetime.Now|Datetime.Now|N/A|54000
        [TestMethod]
        public void validateJoinNormalTest()
        {
            // Arrange
            List<string> actual = new List<string>();
            dBase = new Container();
            List<string> expected = new List<string>() { "FT|tables|bobby|123456782|" + DateTime.Now.ToString("yyyy-MM-dd") + "|"
                + DateTime.Now.ToString("yyyy-MM-dd") + "|N/A|54000" };

            FulltimeEmployee temp = new FulltimeEmployee("bobby", "tables", DateTime.Now, "123456782",
                DateTime.Now, null, 54000.00);

            // Act
            dBase.addEmployee(temp);
            
            actual = dBase.concatAllRecords();
            // Assert
            Assert.AreEqual(expected[0], actual[1]);
        }



        /// \test <b>DBase.8.1</b><br />&emsp;
        /// This method checks the Join methods to see of it will properly return the
        /// correct string containing the information of an employee
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> A valid Fulltime employee object as an Employee <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - FT|tables|bobby|123456782|Datetime.Now|Datetime.Now|N/A|54000 <br />&emsp;
        /// Actual - FT|tables|bobby|123456782|Datetime.Now|Datetime.Now|N/A|54000
        [TestMethod]
        public void validateLoadNormalTest()
        {
            // Arrange
            dBase = new Container();
            List<string> expected = new List<string>() {
                "FT|gabe|paq|046454286|1234-12-12|2000-12-12|2001-12-12|1000",
                "SN|p|gabe|123456782|1234-12-12|WINTER|3",
                "PT|mark|jody|637076654|1999-12-12|2000-12-12|N/A|123",
                "SN|asdfasdf|asdfasdfasf|923980197|1234-12-01|SPRING|1234",
                "SN|qwerty|gabe|303233829|1234-12-12|WINTER|3",
                "CT|gabe||930547021|1993-12-12|1999-12-12|2000-12-12|123",
                "SN|zxcvbgfd|zxcvb|415703065|1994-12-12|SUMMER|1234",
                "PT|time|part|997994751|1234-12-12|2000-12-12|2001-12-12|1234"
            };
            // Act
            //dBase.addEmployee(temp);
            int actual = dBase.createDB(expected);
            // Assert
            Assert.AreEqual(0, actual);
        }



    }
}
