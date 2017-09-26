/*
*  FILENAME        : ParttimeEmployee.cs
*  PROJECT         : EMSSystem
*  PROGRAMMER      : The Donkey Apocalypse
*  FIRST VERSION   : 2016/05/11
*  DESCRIPTION     : This file holds the ParttimeEmployee class. The ParttimeEmployee class is derived from
*                    the abstract Employee class. the ParttimeEmployee class has properties to access and mutate
*                    its attributes dateofHire, dateofTermination, and hourlyRate. It provides an default constructor,
*                    and two overloaded constructor. The ParttimeEmployee class, lastly has methods to validate,
*                    the values stored in each of it attributes and a display method.
*/
using System;
using EMSSystem.Logger;

namespace EMSSystem.AllEmployees
{
    /// <summary>
    /// \class ParttimeEmployee
    /// \brief <b>Description</b>
    /// \details This class represents a part-time employee, it has two constructors a generic and overloaded.
    /// It also provides properties to access and mutate attributes of the part-time employee class.
    /// It has validator methods that ensure data integrity of all of the part-time employee attributes. 
    /// It lastly has display method that formats all part-time employee details into a string ready for
    /// the presentation class to display them.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    public class ParttimeEmployee : Employee
    {
        //private attributes
        private DateTime dateOfHire;
        private DateTime? dateOfTermination;
        private double hourlyRate;

        public DateTime DateOfHire
        {
            get { return dateOfHire; }
            set
            {
                if (FulltimeEmployee.validateStartDate(this.DOB, value))
                {
                    dateOfHire = value;
                }
                else
                {
                    Logging.Log("[ParttimeEmployee.DateOfHire] DateOfHire - " + value + " - INVALID");
                    throw new Exception("Invalid Date Of Hire");
                }
            }
        }


        public DateTime? DateOfTermination
        {
            get { return dateOfTermination; }
            set
            {
                if (FulltimeEmployee.validateStopDate(this.DateOfHire, value))
                {
                    dateOfTermination = value;
                }
                else
                {
                    Logging.Log("[ParttimeEmployee.DateOfTermination] DateOfTermination - " + value + " - INVALID");
                    throw new Exception("Invalid Date Of Termination");
                }
            }
        }


        public double HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (FulltimeEmployee.validatePay(value))
                {
                    hourlyRate = value;
                }
                else
                {
                    Logging.Log("[ParttimeEmployee.HourlyRate] HourlyRate - " + value + " - INVALID");
                    throw new Exception("Invalid Hourly Rate");
                }
            }
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Default Constructor class for the Parttime Employee, derived from the abstract Employee class
        /// sets the attributes of type DateTime to the MinValue and doubles to 0.00, uses the Employee constructor
        /// to set all remaining attributes.
        /// </summary>
        public ParttimeEmployee() : base()
        {
            this.Type = EmployeeType.PT;
            dateOfHire = DateTime.MinValue;
            dateOfTermination = null;
            hourlyRate = 0.00;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the ParttimeEmployee class, derived from the abstract Employee class
        /// sets the attributes of the PartimeEmployee and Abstract class to values provided in the parameters
        /// </summary>
        /// <param name="firstName"> - first name of the employee</param>
        /// <param name="string"> - last name of the employee</param>
        /// <param name="DateTime"> - Date of birth of th eemployee</param>
        /// <param name="string"> - Social Insurance Number of the employee</param>
        /// <param name="DateTime"> - Date of Hire of the employee</param>
        /// <param name="DateTime"> - Date of termination of the employee</param>
        /// <param name="double"> - Annual salary of the employee</param>
        public ParttimeEmployee(string firstName, string lastName, DateTime dob, string sin,
            DateTime dateOfHire, DateTime? dateOfTermination, double hourlyRate) : base()
        {
            this.Type = EmployeeType.PT;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DOB = dob;
            this.SIN = sin;
            DateOfHire = dateOfHire;
            DateOfTermination = dateOfTermination;
            this.hourlyRate = hourlyRate;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the ParttimeEmployee class, derived from the abstract Employee class
        /// sets the attributes of the ParttimeEmployee and Abstract class to values provided in the parameters, the rest
        /// are defaulted
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public ParttimeEmployee(string firstName, string lastName) : base()
        {
            this.Type = EmployeeType.PT;
            this.FirstName = firstName;
            this.LastName = lastName;
            dateOfHire = DateTime.MinValue;
            dateOfTermination = null;
            hourlyRate = 0.00;
        }


        // TODO
        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details Validates all attributes associated with a ParttimeEmployee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dob"></param>
        /// <param name="sin"></param>
        /// <param name="dateOfHire"></param>
        /// <param name="dateOfTermination"></param>
        /// <param name="hourlyRate"></param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(string firstName, string lastName, DateTime dob, string sin,
            DateTime dateOfHire, DateTime? dateOfTermination, double hourlyRate)
        {
            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(EmployeeType.PT,firstName),
                validateName(EmployeeType.PT,lastName),
                validateDob(dob),
                validateSIN(EmployeeType.PT, sin, dob),
                FulltimeEmployee.validateStartDate(dob, dateOfHire),
                FulltimeEmployee.validateStopDate(dateOfHire, dateOfTermination),
                FulltimeEmployee.validatePay(hourlyRate)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[ParttimeEmployee.Validate] Employee - " + lastName + ", "
                        + firstName + " (" + sin + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details Validates all attributes associated with a ParttimeEmployee
        /// </summary>
        /// <param name="ptEmployee"></param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(ParttimeEmployee ptEmployee)
        {
            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(ptEmployee.Type,ptEmployee.FirstName),
                validateName(ptEmployee.Type,ptEmployee.LastName),
                validateDob(ptEmployee.DOB),
                validateSIN(ptEmployee.Type, ptEmployee.SIN, ptEmployee.DOB),
                FulltimeEmployee.validateStartDate(ptEmployee.DOB, ptEmployee.DateOfHire),
                FulltimeEmployee.validateStopDate(ptEmployee.DateOfHire, ptEmployee.DateOfTermination),
                FulltimeEmployee.validatePay(ptEmployee.HourlyRate)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[ParttimeEmployee.Validate] Employee - " + ptEmployee.LastName + ", "
                        + ptEmployee.FirstName + " (" + ptEmployee.SIN + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes all values in a parttime Employee and constructs a string
        /// to be used in the presentation class to display employee details
        /// </summary>
        /// <param name="employee">Parttime employee object</param>
        /// <returns>string thats formated to be displayed in the presentation class</returns>
        public static string display(ParttimeEmployee employee, bool shouldLog)
        {
            string sinTemp = employee.SIN;
            sinTemp = sinTemp.Insert(6, " ");
            sinTemp = sinTemp.Insert(3, " ");
            string print =
                      "Employee Classification : Part Time \n"
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
            print += "Hourly Rate             : " + employee.HourlyRate.ToString() + "\n"; ;
            if (shouldLog)
            {
                //logger.Log("[ParttimeEmployee.Display] Employee: \n" + print); 
            }
            return print;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes a Parttimeemployee object and puts all attributes into a string[],
        ///  and then joins all items in the string array into a "|" delimited string.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>returns the string that holds all the attributes of the employee object delimited by "|"</returns>
        public static string join(ParttimeEmployee emp)
        {
            string term = "N/A";
            if (emp.DateOfTermination != null)
            {
                DateTime date = (DateTime)emp.DateOfTermination;
                term = date.ToString(DateFormat);
            }

            string[] item = new string[] {
                emp.Type.ToString(),emp.LastName,emp.FirstName,emp.SIN,emp.DOB.ToString(DateFormat),
                emp.DateOfHire.ToString(DateFormat),term,emp.HourlyRate.ToString()
            };

            return string.Join("|", item);
        }
    }
}
