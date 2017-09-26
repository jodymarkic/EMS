/*
*  Filenmae        : PromptString.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the PromptString class that displays some of the menu, and options
*                    provided by the UIMenu class.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSSystem.AllEmployees;
using EMSSystem.TheCompany;

namespace EMSSystem.Presentation
{
    /// <summary>
    /// \class Container
    /// This class contains some of the menu's for the UI of EMSSystem.
    /// </summary>
    class PromptString
    {
        private EmployeeType employeeType;
        private string firstName;
        private string lastName;
        private string dob;
        private string sin;
        private string startDate;
        private string endDate;
        private string pay;

        public EmployeeType EType { get { return employeeType; } }
        public string FirstName { get { return firstName; } }
        public string LastName { get { return lastName; } }
        public string SIN { get { return sin; } }
        public string DOB { get { return dob; } }
        public string StartDate { get { return startDate; } }
        public string EndDate { get { return endDate; } }
        public string Pay { get { return pay; } }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details this method determines which message to show to user depending on the derived employee selected
        /// </summary>
        /// <param name="type"></param>
        public PromptString(EmployeeType type)
        {
            employeeType = type;
            if (type == EmployeeType.FT || type == EmployeeType.PT || type == EmployeeType.SN)
            {
                firstName ="employee's first name";
                lastName = "employee's last name";
                dob = "employee's date of birth";
                sin = "employee's SIN";
                
                if (type == EmployeeType.FT || type == EmployeeType.PT)
                {
                    startDate = "employee's date of hire";
                    endDate = "employee's date of termination";
                    if(type == EmployeeType.FT)
                    {
                        pay = "salary for the employee";
                    }
                    else
                    {
                        pay = "hourly rate for the employee";
                    }
                }
                else
                {
                    pay = "pay piece price for the employee";
                }
            }
            else if (type == EmployeeType.CT)
            {
                firstName = "";
                lastName = "company name";
                dob = "date of incorporation";
                sin = "business number";
                startDate = "contract start date";
                endDate = "contract end date";
                pay = "contract pay amount";
            }
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details this method displays the Employee Details Menu
        /// </summary>
        /// <param name="dateFlag"></param>
        /// <param name="payFlag"></param>
        public void displayMenu(bool dateFlag, bool payFlag)
        {
            string baseEmployeeFilledCheck = " ";
            string dateFilledOutCheck = " ";
            string payFilledOutCheck = " ";


            if (employeeType != EmployeeType.Default)
            {
                baseEmployeeFilledCheck = "[\u221A]";
            }

            if (dateFlag == true)
            {
                dateFilledOutCheck = "[\u221A]";
            }

            if (payFlag == true)
            {
                payFilledOutCheck = "[\u221A]";
            }



            Console.WriteLine("EMPLOYEE DETAILS MENU");
            Console.WriteLine();

            Console.WriteLine("1. Specify Base Employee Details " + baseEmployeeFilledCheck);

            if (employeeType != EmployeeType.Default)
            {
                switch (employeeType)
                {
                    case EmployeeType.FT:
                        Console.WriteLine("2. Specify Date of Hire and Date of Termination " + dateFilledOutCheck);
                        Console.WriteLine("3. Specify Salary " + payFilledOutCheck);
                        break;
                    case EmployeeType.PT:
                        Console.WriteLine("2. Specify Date of Hire and Date of Termination " + dateFilledOutCheck);
                        Console.WriteLine("3. Specify Hourly Rate " + payFilledOutCheck);
                        break;
                    case EmployeeType.CT:
                        Console.WriteLine("2. Specify Contract Start and End Date " + dateFilledOutCheck);
                        Console.WriteLine("3. Specify Fixed Contract Amount " + payFilledOutCheck);
                        break;
                    case EmployeeType.SN:
                        Console.WriteLine("2. Specify Season of Work " + dateFilledOutCheck);
                        Console.WriteLine("3. Specify Piece Pay " + payFilledOutCheck);
                        break;
                }
                
                if (dateFlag == true && payFlag == true)
                {
                    Console.WriteLine("4. Save Employee");
                }
            }

            Console.WriteLine("9. Return to Employee Management Menu");
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details this method displays the Update Employee Details Menu
        /// </summary>
        public void displayUpdateMenu()
        {
            Console.WriteLine("UPDATE EMPLOYEE DETAILS MENU");
            Console.WriteLine();


            if(EType == EmployeeType.FT || EType == EmployeeType.PT || EType == EmployeeType.SN)
            {
                Console.WriteLine("1. First Name");
                Console.WriteLine("2. Last Name");
                Console.WriteLine("3. Date Of Birth");
                if(EType == EmployeeType.SN)
                {
                    Console.WriteLine("4. Season");
                    Console.WriteLine("5. Pay Piece");
                }
                else
                {
                    Console.WriteLine("4. Start Date");
                    Console.WriteLine("5. End Date");
                    if(EType == EmployeeType.PT)
                    {
                        Console.WriteLine("6. Salary");
                    }
                    else
                    {
                        Console.WriteLine("6. Hourly Rate");
                    }
                }
            }
            else if(EType == EmployeeType.CT)
            {
                Console.WriteLine("1. Company Name");
                Console.WriteLine("2. Contract Start Date");
                Console.WriteLine("3. Contract End Date");
                Console.WriteLine("4. Contract Pay Amount");
            }

            Console.WriteLine("9. Save Changes");
            Console.WriteLine("Press 'X' to exit without saving changes.");
        }
    }
}
