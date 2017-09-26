//
//  FILENAME        : ContractEmployee.cs
//  PROJECT         : EMSSystem
//  PROGRAMMER      : The Donkey Apocalypse
//  FIRST VERSION   : 2016/05/11
//  DESCRIPTION     : This file holds the ContractEmployee class. The ContractEmployee class is derived from
//                    the abstract Employee class. The ContractEmployee class has properties to access and mutate
//                    its attributes contractStartDate, contractStopDate, and fixedContractAmount. It provides an default constructor,
//                    and two overloaded constructor.The ContractEmployee class, lastly has methods to validate,
//                    the values stored in each of it attributes and a display method.
using System;
using EMSSystem.Logger;

namespace EMSSystem.AllEmployees
{
    /// <summary>
    /// \class ContractEmployee
    /// \brief <b>Description</b>
    /// \details This class represents a contract employee.he ContractEmployee class is derived from
    ///  the abstract Employee class. The ContractEmployee class has properties to access and mutate
    ///  its attributes contractStartDate, contractStopDate, and fixedContractAmount. It provides an default constructor,
    ///  and two overloaded constructor.The ContractEmployee class, lastly has methods to validate,
    ///  the values stored in each of it attributes and a display method.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    public class ContractEmployee : Employee
    {
        //attributes
        private DateTime contractStartDate;
        private DateTime contractStopDate;
        private double fixedContractAmount;

        public DateTime ContractStartDate
        {
            get { return contractStartDate; }
            set
            {
                if (FulltimeEmployee.validateStartDate(this.DOB, value))
                {
                    contractStartDate = value;
                }
                else
                {
                    Logging.Log("[ContractEmployee.ContractStartDate] ContractStartDate - " + value + " - INVALID");
                    throw new Exception("Invalid Salary");
                }
            }
        }


        public DateTime ContractStopDate
        {
            get { return contractStopDate; }
            set
            {
                if (FulltimeEmployee.validateStopDate(this.contractStartDate, value))
                {
                    contractStopDate = value;
                }
                else
                {
                    Logging.Log("[ContractEmployee.ContractStopDate] ContractStopDate - " + value + " - INVALID");
                    throw new Exception("Invalid Date Of Termination");
                }
            }
        }


        public double FixedContractAmount
        {
            get { return fixedContractAmount; }
            set
            {
                if (FulltimeEmployee.validatePay(value))
                {
                    fixedContractAmount = value;
                }
                else
                {
                    Logging.Log("[ContractEmployee.FixedContractAmount] FixedContractAmount - " + value + " - INVALID");
                    throw new Exception("Invalid Salary");
                }
            }
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Default Constructor class for the Contract Employee, derived from the abstract Employee class
        ///  Sets the attributes of type DateTime to the MinValue and doubles to 0.00, uses the Employee constructor
        ///  to set all remaining attributes.
        /// </summary>
        public ContractEmployee() : base()
        {
            this.Type = EmployeeType.CT;
            ContractStartDate = DateTime.MinValue;
            ContractStopDate = DateTime.MinValue;
            fixedContractAmount = 0.00;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the Contract class, derived from the abstract Employee class.
        /// \details Sets the attributes of the ContractEmployee and Abstract class to values provided in the parameters
        /// </summary>
        /// <param name="lastName">last name of the employee</param>
        /// <param name="dob">Date of birth of the employee</param>
        /// <param name="sin">Social Insurance Number of the employee</param>
        /// <param name="contractStartDate">contract start date of the employee</param>
        /// <param name="contractStopDate">contract stop date of the employee</param>
        /// <param name="fixedContractAmount">fixed contract amount of the employee</param>
        public ContractEmployee(string lastName, DateTime dob, string sin,
            DateTime contractStartDate, DateTime contractStopDate, double fixedContractAmount) : base()
        {
            this.Type = EmployeeType.CT;
            this.LastName = lastName;
            this.DOB = dob;
            this.SIN = sin;
            ContractStartDate = contractStartDate;
            ContractStopDate = contractStopDate;
            this.fixedContractAmount = fixedContractAmount;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the ContractEmployee class, derived from the abstract Employee class
        /// sets the attributes of the ContractEmployee and Abstract class to values provided in the parameters, the rest
        /// are defaulted
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public ContractEmployee(string firstName, string lastName) : base()
        {
            this.Type = EmployeeType.CT;
            this.FirstName = firstName;
            this.LastName = lastName;
            ContractStartDate = DateTime.MinValue;
            ContractStopDate = DateTime.MinValue;
            fixedContractAmount = 0.00;
        }


        // TODO
        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validates all attributes associated with a SeasonalEmployee
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="dob"></param>
        /// <param name="sin"></param>
        /// <param name="contractStartDate"></param>
        /// <param name="contractStopDate"></param>
        /// <param name="fixedContractAmount"></param>
        /// <returns> a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(string lastName, DateTime dob, string sin,
            DateTime contractStartDate, DateTime contractStopDate, double fixedContractAmount)
        {
            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(EmployeeType.CT,lastName),
                validateDob(dob),
                validateSIN(EmployeeType.CT, sin, dob),
                FulltimeEmployee.validateStartDate(dob, contractStartDate),
                FulltimeEmployee.validateStopDate(contractStartDate, contractStopDate),
                FulltimeEmployee.validatePay(fixedContractAmount)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[ContractEmployee.Validate] Employee - " + lastName+" ("+sin+") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validates all attributes associated with a SeasonalEmployee
        /// </summary>
        /// <param name="ctEmployee"></param>
        /// <returns> a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(ContractEmployee ctEmployee)
        {
            bool confirmed = true;
            bool[] test = new bool[]{
                validateName(ctEmployee.Type,ctEmployee.LastName),
                validateDob(ctEmployee.DOB),
                validateSIN(ctEmployee.Type, ctEmployee.SIN, ctEmployee.DOB),
                FulltimeEmployee.validateStartDate(ctEmployee.DOB, ctEmployee.ContractStartDate),
                FulltimeEmployee.validateStopDate(ctEmployee.ContractStartDate, ctEmployee.ContractStopDate),
                FulltimeEmployee.validatePay(ctEmployee.FixedContractAmount)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[ContractEmployee.Validate] Employee - " + ctEmployee.LastName + " (" + ctEmployee.SIN + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes all values in a Contract Employee and constructs a string
        /// to be used in the presentation class to display employee details
        /// </summary>
        /// <param name="employee">ContractEmployee object</param>
        /// <returns>string thats formated to be displayed in the presentation class</returns>
        public static string display(ContractEmployee employee, bool shouldLog)
        {
            string sinTemp = employee.SIN;
            sinTemp = sinTemp.Insert(5, " ");
            string msg =
                   "Employee Classification : Contract \n"
                 + "Company Name            : " + employee.LastName + "\n"
                 + "Date Of Birth           : " + employee.DOB.ToString(DateFormat) + "\n"
                 + "SIN                     : " + sinTemp + "\n"
                 + "Contract Start Date     : " + employee.ContractStartDate.ToString(DateFormat) + "\n"
                 + "Contract Stop Date      : " + employee.ContractStopDate.ToString(DateFormat) + "\n"
                 + "Fixed Contract Amount   : " + employee.FixedContractAmount.ToString() + "\n";
            if (shouldLog)
            {
                Logging.Log("[ContractEmployee.Display] Employee: \n" + msg); 
            }
            return msg;            
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes a Contractemployee object and puts all attributes into a string[],
        ///  and then joins all items in the string array into a "|" delimited string.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>returns the string that holds all the attributes of the employee object delimited by "|"</returns>
        public static string join(ContractEmployee emp)
        {
            string[] item = new string[] {
                emp.Type.ToString(),emp.LastName,emp.FirstName,emp.SIN,emp.DOB.ToString(DateFormat),
                emp.ContractStartDate.ToString(DateFormat),emp.ContractStopDate.ToString(DateFormat),emp.FixedContractAmount.ToString()
            };
            return string.Join("|", item);
        }
    }
}
