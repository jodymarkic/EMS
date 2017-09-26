/*
*  Filenmae        : Employee.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the abstract employee class that all other
*                    employee classes are derived from in the EMSSystem solution.
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using EMSSystem.Logger;

namespace EMSSystem.AllEmployees 
{
    /// <summary>
    /// This enum is to keep track of the type of employee a user can
    /// input into the database
    /// </summary>
    public enum EmployeeType
    {
        Default, FT, PT, CT, SN
    };

    public enum Seasons
    {
        Default, SPRING, WINTER, SUMMER, FALL
    };

    /// <summary>
    /// \class Employee
    /// This <i>abstract</i> class handles the base employee details and their methods of validation.
    /// This class is very trivial such that you can only get or set the properties associated with it
    /// (the validations are static and used without an 'Employee' object existing)
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    public abstract class Employee : ICloneable
    {

        private const int SINSIZE = 9;
        public static string DateFormat { get { return "yyyy-MM-dd"; } }
        public static CultureInfo EnUS { get { return new CultureInfo("en-US"); } }

        // Properties
        private EmployeeType type;
        private string firstName;
        private string lastName;
        private DateTime dob;
        private string sin;

        // Properties for an employee
        #region Properties
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (validateName(type, value))
                {
                    firstName = value;
                }
                else
                {
                    Logging.Log("[Employee.FirstName] FirstName - " + value + " - INVALID");
                    throw new Exception("Your first name contains invalid characters");
                }
            }
        }


        public string LastName
        {
            get { return lastName; }
            set
            {
                if (validateName(type, value))
                {
                    lastName = value;
                }
                else
                {
                    Logging.Log("[Employee.LastName] LastName - " + value + " - INVALID");
                    throw new Exception("Your last name contains invalid characters");
                }
            }
        }


        public DateTime DOB
        {
            get { return dob; }
            set
            {
                if (validateDob(value))
                {
                    dob = value;
                }
                else
                {
                    Logging.Log("[Employee.DOB] DateOfBirth - " + value + " - INVALID");
                    throw new Exception("Your Date of Birth can not be a future date");
                }
            }
        }


        public string SIN
        {
            get { return sin; }
            set
            {
                if (validateSIN(type, value, dob))
                {
                    sin = value;
                }
                else
                {
                    Logging.Log("[Employee.SIN] SocialInsuranceNumber - " + value + " - INVALID");
                    throw new Exception("Your SIN is not valid");
                }
            }
        }


        protected EmployeeType Type
        {
            get { return type; }
            set
            {
                switch (value)
                {
                    case EmployeeType.FT:
                    case EmployeeType.PT:
                    case EmployeeType.CT:
                    case EmployeeType.SN:
                        type = value;
                        break;
                    default:
                        throw new Exception("Improper EmployeeType");
                }
            }
        } 
        #endregion


        // Constructors
        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details This contructor intantiates an <b>Employee</b> to a default, or empty, set of data
        /// </summary>
        protected Employee()
        {
            this.firstName = "";
            this.lastName = "";
            this.dob = DateTime.MinValue;
            this.sin = "";
        }



        public object Clone()
        {
            Employee newEmp = (Employee)this.MemberwiseClone();
            return newEmp;
        }


        // Validation Methods
        /// <summary>
        /// \brief <b>Description</b>
        ///  \details This method is used to validate the Name fields of a given entry.<br />
        ///  It will make sure the a valid name can only contain number,letters, spaces,
        ///  apostrophes', or dashes<br />
        ///  In the case of a contract employee, we allow spaces and numbers as well
        /// </summary>
        /// <param name="type">The type of employee being validated. This changes what the method has to validate for.</param>
        /// <param name="name">The name being validated (can be FirstName or LastName)</param>
        /// <returns>A bool indicating whether the passed in name is valid</returns>
        public static bool validateName(EmployeeType type, string name)
        {
            bool retCode = false;
            string expression = @"^[a-zA-Z\'\-]*$";
            if (name != "")
            {
                // if the type is Contract
                if (type == EmployeeType.CT)
                {
                    // Then the string can have numbers and spaces
                    expression = @"^[.0-9a-zA-Z\s*\'\-]*$";
                }
                // Validate the name using the expression
                retCode = Regex.IsMatch(name, expression);
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        ///  \details This method validates that an inputted date of birth is in the correct format  not a future date.
        ///  Further validation is not required.
        /// </summary>
        /// <param name="dob"> The string passed in representing the date of birth in a yyyy-MM-dd format</param>
        /// <returns>A bool indicating whether the passed in date is valid</returns>
        public static bool validateDob(DateTime dob)
        {
            bool retCode = false;
            // compare the date to now, and make sure it is a date prior to, or today
            int compare = DateTime.Compare(dob, DateTime.Now);
            if (compare <= 0)
            {
                retCode = true;
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        ///  \details This method validates the SIN or BN of an Employee-Type entry.
        ///  If it is a contract employee, it uses the validateBN method to make sure the year of incorporation matches
        ///  with the BN, and then vlidate the remaining part of the number the same way as a SIN
        /// </summary>
        /// <param name="type">The type of employee being validated. This changes what the method has to validate for.</param>
        /// <param name="sin">The SIN number of the employee</param>
        /// <param name="dob">The string passed in representing the date of birth in a yyyy-MM-dd format.
        ///  It is used to validate contract employees' BN numbers</param>
        /// <returns>A bool indicating whether the passed in SIN is valid</returns>
        public static bool validateSIN(EmployeeType type, string sin, DateTime dob)
        {
            bool valid = false;
            int[] compare = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int[] digitResults = new int[SINSIZE];
            bool check = true;
            if (sin.Length == 9 && isDigitsOnly(sin))
            {
                if (type == EmployeeType.CT)
                {
                    check = validateBN(sin, dob.ToString(DateFormat));
                }

                if (check)
                {
                    int temp = 0;
                    // Multiply each digit of the sin/bin, with the corresponding digit in the compare array 
                    for (int i = 0; i < SINSIZE; i++)
                    {
                        temp = (sin[i] - '0') * compare[i];
                        // If the result turns out to be a double digit, we will subtract 9 to make it a single digit
                        // (alternatively, you can add the two digits of the result together, and get the same result
                        // i.e 9 * 2 = 18, 18 - 9 = 9, or 1 + 8 = 9) <-- fun fact
                        if (temp >= 10)
                        {
                            temp -= 9;
                        }
                        digitResults[i] = temp;
                    }
                    temp = 0;
                    // Add each resulting digit together
                    for (int i = 0; i < SINSIZE; i++)
                    {
                        temp += digitResults[i];
                    }
                    // If the result is divisible by 10, without a remainder
                    if (temp % 10 == 0)
                    {
                        valid = true;
                    }
                } 
            }            
            return valid;
        }


        /// <summary>
        /// This method is specifically used for Contract employees. It will take the BN and Date of incorporation, 
        /// and it will check that the first two digits in the BN are the same as the last two digits of the year
        ///  in the date of incorporation. If they do not match, it will simply return false.
        /// </summary>
        /// <param name="BN">The Business number of a contract employee</param>
        /// <param name="doi">The string passed in representing the date of incorporation in a yyyy-MM-dd format.</param>
        /// <returns>A bool indicating if the BN passed in matches the corresponding date of incorporation</returns>
        private static bool validateBN(string BN, string doi)
        {
            bool valid = false;
            string date = "";
            string BINCheck = "";
            // Since doi is in YYYY-mm-DD foramt, substring wil get the last two digits of the year.
            date = doi.Substring(2, 2);
            BINCheck = BN.Substring(0, 2);
            if (BINCheck == date)
            {
                valid = true;
            }
            return valid;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool isDigitsOnly(string str)
        {
            bool retCode = true;
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    retCode = false;
                    break;
                }
            }
            return retCode;
        }
    }
}