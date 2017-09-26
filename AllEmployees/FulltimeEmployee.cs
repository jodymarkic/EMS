/*
*  FILENAME        : FulltimeEmployee.cs
*  PROJECT         : EMSSystem
*  PROGRAMMER      : The Donkey Apocalypse
*  FIRST VERSION   : 2016/05/11
*  DESCRIPTION     : This file holds the FulltimeEmployee class. The FulltimeEmployee class is derived from
*                    the abstract Employee class. the FulltimeEmployee class has properties to access and mutate
*                    its attributes dateofHire, dateofTermination, and salary. It provides an default constructor,
*                    and two overloaded constructor. The FulltimeEmployee class, lastly has methods to validate,
*                    the values stored in each of it attributes and a display method.
*/
using System;
using EMSSystem.Logger;

namespace EMSSystem.AllEmployees
{
    /// <summary>
    /// \class FulltimeEmployee
    /// \brief <b>Description</b>
    /// \details This class represents a full time employee, it holds to constructors, a generic and overloaded
    /// it provide properties to access and mutate fulltime employee attributes.
    /// it additionally provides validation functions for each attribute and a method that formats a string
    /// that holds all employee details ready to be outputted in the presentation class
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    public class FulltimeEmployee : Employee
    {
        //private attributes
        private DateTime dateOfHire;
        private DateTime? dateOfTermination;
        private double salary;

        public DateTime DateOfHire
        {
            get { return dateOfHire; }
            set
            {
                if (validateStartDate(this.DOB, value))
                {
                    dateOfHire = value;
                }
                else
                {
                    Logging.Log("[FulltimeEmployee.DateOfHire] DateOfHire - " + value + " - INVALID");
                    throw new Exception("Invalid Date Of Hire");
                }
            }
        }


        public DateTime? DateOfTermination
        {
            get { return dateOfTermination; }
            set
            {
                if (validateStopDate(this.DateOfHire, value))
                {
                    dateOfTermination = value;
                }
                else
                {
                    Logging.Log("[FulltimeEmployee.DateOfTermination] DateOfTermination - " + value + " - INVALID");
                    throw new Exception("Invalid Date Of Termination");
                }
            }
        }


        public double Salary
        {
            get { return salary; }
            set
            {
                if (validatePay(value))
                {
                    salary = value;
                }
                else
                {                    
                    Logging.Log("[FulltimeEmployee.Salary] Salary - " + value + " - INVALID");
                    throw new Exception("Invalid Salary");
                }
            }
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Default Constructor class for the Fulltime Employee, derived from the abstract Employee class
        /// sets the attributes of type DateTime to the MinValue and doubles to 0.00, uses the Employee constructor
        /// to set all remaining attributes.
        /// </summary>
        public FulltimeEmployee() : base()
        {
            this.Type = EmployeeType.FT;
            dateOfHire = DateTime.MinValue;
            dateOfTermination = DateTime.MinValue;
            salary = 0.00;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the FulltimeEmployee class, derived from the abstract Employee class
        /// sets the attributes of the FulltimeEmployee and Abstract class to values provided in the parameters
        /// </summary>
        /// <param name="firstName"> - first name of the employee</param>
        /// <param name="lastName"> - last name of the employee</param>
        /// <param name="dob"> - Date of birth of th eemployee</param>
        /// <param name="sin"> - Social Insurance Number of the employee</param>
        /// <param name="dateOfHire"> - Date of Hire of the employee</param>
        /// <param name="dateOfTermination"> - Date of termination of the employee</param>
        /// <param name="salary"> - Annual salary of the employee</param>
        public FulltimeEmployee(string firstName, string lastName, DateTime dob, string sin,
            DateTime dateOfHire, DateTime? dateOfTermination, double salary) : base()
        {
            this.Type = EmployeeType.FT;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DOB = dob;
            this.SIN = sin;
            DateOfHire = dateOfHire;
            DateOfTermination = dateOfTermination;
            Salary = salary;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the FulltimeEmployee class, derived from the abstract Employee class
        /// sets the attributes of the FulltimeEmployee and Abstract class to values provided in the parameters, the rest
        /// are defaulted
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public FulltimeEmployee(string firstName, string lastName) : base()
        {
            this.Type = EmployeeType.FT;
            this.FirstName = firstName;
            this.LastName = lastName;
            dateOfHire = DateTime.MinValue;
            dateOfTermination = DateTime.MinValue;
            salary = 0.00;
        }


        // TODO
        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details Validates all attributes associated with a FulltimeEmployee
        /// </summary>
        /// <param name="firstName"> - first name of the employee</param>
        /// <param name="lastName"> - last name of the employee</param>
        /// <param name="dob"> - Date of birth of th eemployee</param>
        /// <param name="sin"> - Social Insurance Number of the employee</param>
        /// <param name="dateOfHire"> - Date of Hire of the employee</param>
        /// <param name="dateOfTermination"> - Date of termination of the employee</param>
        /// <param name="salary"> - Annual salary of the employee</param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(string firstName, string lastName, DateTime dob, string sin,
            DateTime dateOfHire, DateTime? dateOfTermination, double salary)
        {
            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(EmployeeType.FT,firstName),
                validateName(EmployeeType.FT,lastName),
                validateDob(dob),
                validateSIN(EmployeeType.FT, sin, dob),
                validateStartDate(dob, dateOfHire),
                validateStopDate(dateOfHire, dateOfTermination),
                validatePay(salary)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[FulltimeEmployee.Validate] Employee - " + lastName + ", "
                        + firstName + " (" + sin + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details Validates all attributes associated with a FulltimeEmployee
        /// </summary>
        /// <param name="ftEmployee"></param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(FulltimeEmployee ftEmployee)
        {

            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(ftEmployee.Type, ftEmployee.FirstName),
                validateName(ftEmployee.Type, ftEmployee.LastName),
                validateDob(ftEmployee.DOB),
                validateSIN(ftEmployee.Type, ftEmployee.SIN, ftEmployee.DOB),
                validateStartDate(ftEmployee.DOB, ftEmployee.DateOfHire),
                validateStopDate(ftEmployee.DateOfHire, ftEmployee.DateOfTermination),
                validatePay(ftEmployee.Salary)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[FulltimeEmployee.Validate] Employee - " + ftEmployee.LastName + ", "
                        + ftEmployee.FirstName + " (" + ftEmployee.SIN + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validate user input for a Date of Hire of a fulltime employee
        ///  </summary>
        /// <param name="DoB"> - Date of Birth for the fulltime employee</param>
        /// <param name="dateOfHire"> - Date of Hire for the fulltime employee</param>
        /// <return> - boolean that indicates whether Date of Hire is an acceptable value</return>
        public static bool validateStartDate(DateTime DoB, DateTime dateOfHire)
        {
            bool confirmed = false;
            //confirm that date of hire is sometime after date of birth
            int resultOne = DateTime.Compare(DoB, dateOfHire);
            //confirm that date of hire is not after the present date
            if (resultOne <= 0)
            {
                int resultTwo = DateTime.Compare(dateOfHire, DateTime.Now);
                if (resultTwo <= 0)
                {
                    confirmed = true;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validate user input for a Date of Termination of a fulltime employee
        /// </summary>
        /// <param name="dateOfHire">Date of Hire for a fulltime employee</param>
        /// <param name="dateOfTermination">Date of Termination for a fulltime employee</param>
        /// <returns>boolean that indicates whether date of termination is an acceptable value</returns>
        public static bool validateStopDate(DateTime dateOfHire, DateTime? dateOfTermination)
        {
            bool confirmed = false;
            // allow a date of termination to have no value
            if (dateOfTermination == null)
            {
                confirmed = true;
            }
            //otherwise
            else
            {
                DateTime date = (DateTime)dateOfTermination;
                int resultOne = DateTime.Compare(dateOfHire, date);
                //confirm the date of termination is not past the current date
                if (resultOne <= 0)
                {
                    int resultTwo = DateTime.Compare(date, DateTime.Now);
                    if (resultTwo <= 0)
                    {
                        confirmed = true;
                    }
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validate user input for a Salary of a fulltime employee
        /// </summary>
        /// <param name="salary"></param>
        /// <returns>boolean that indicates whether salary is an acceptable value</returns>
        public static bool validatePay(double salary)
        {
            bool confirmed = false;
            if (salary > 0)
            {
                confirmed = true;
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes all values in a fulltime Employee and constructs a string
        /// to be used in the presentation class to display employee details
        /// </summary>
        /// <returns>string thats formated to be displayed in the presentation class</returns>
        public static string display(FulltimeEmployee employee, bool shouldLog)
        {
            string sinTemp = employee.SIN;
            sinTemp = sinTemp.Insert(6, " ");
            sinTemp = sinTemp.Insert(3, " ");
            string print =
                  "Employee Classification : Full Time \n"
                + "First Name              : " + employee.FirstName + "\n"
                + "Last Name               : " + employee.LastName + "\n"
                + "Date Of Birth           : " + employee.DOB.ToString(DateFormat) + "\n"
                + "SIN                     : " + sinTemp + "\n"
                + "Date Of Hire            : " + employee.DateOfHire.ToString(DateFormat) + "\n";

            if (employee.DateOfTermination != null)
            {
                DateTime date = (DateTime)employee.DateOfTermination;
                print += "Date Of Termination     : " + date.ToString(DateFormat) + "\n";
            }
            print += "Salary                  : " + employee.Salary.ToString() + "\n";
            if (shouldLog)
            {
                Logging.Log("[FulltimeEmployee.Display] Employee: \n" + print); 
            }
            return print;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes a Fulltimeemployee object and puts all attributes into a string[],
        ///  and then joins all items in the string array into a "|" delimited string.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>returns the string that holds all the attributes of the employee object delimited by "|"</returns>
        public static string join(FulltimeEmployee emp)
        {
            string term = "N/A";
            if (emp.DateOfTermination != null)
            {
                DateTime date = (DateTime)emp.DateOfTermination;
                term = date.ToString(DateFormat);
            }

            string[] item = new string[] {
                emp.Type.ToString(),emp.LastName,emp.FirstName,emp.SIN,emp.DOB.ToString(DateFormat),
                emp.DateOfHire.ToString(DateFormat),term,emp.Salary.ToString()
            };

            return string.Join("|", item);
        }
    }
}


