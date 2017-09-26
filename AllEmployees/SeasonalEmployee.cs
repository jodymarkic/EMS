//
//  FILENAME        : SeasonalEmployee.cs
//  PROJECT         : EMSSystem
//  PROGRAMMER      : The Donkey Apocalypse
//  FIRST VERSION   : 2016/05/11
//  DESCRIPTION     : This file holds the SeasonalEmployee class. The SeasonalEmployee class is derived from
//                    the abstract Employee class. the SeasonalEmployee class has properties to access and mutate
//                    its attributes season and piecePay. It provides an default constructor,and two overloaded 
//                    constructors.The FulltimeEmployee class, lastly has methods to validate, the values stored in
//                    each of it attributes and a display method.
using System;
using EMSSystem.Logger;

namespace EMSSystem.AllEmployees
{
    /// <summary>
    /// \class SeasonalEmployee
    /// \brief <b>Description</b>
    /// \details This class represents a seasonal employee, it has two constructors a generic and overloaded.
    /// It also provides properties to access and mutate attributes of the seasonal employee class.
    /// It has validator methods that ensure data integrity of all of the seasonal employee attributes. 
    /// It lastly has display method that formats all seasonal employee details into a string ready for
    /// the presentation class to display them.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    public class SeasonalEmployee : Employee
    {

        private Seasons season;
        private double piecePay;

        public Seasons Season
        {
            get { return season; }
            set
            {
                switch (value)
                {
                    case Seasons.FALL:
                    case Seasons.WINTER:
                    case Seasons.SPRING:
                    case Seasons.SUMMER:
                        season = value;
                        break;
                    default:
                        Logging.Log("[SeasonalEmployee.Season] Season - " + value + " - INVALID");
                        throw new Exception("Improper Season");
                }
            }
        }


        public double PiecePay
        {
            get { return piecePay; }
            set
            {
                if (FulltimeEmployee.validatePay(value))
                {
                    piecePay = value;
                }
                else
                {
                    Logging.Log("[SeasonalEmployee.PiecePay] PiecePay - " + value + " - INVALID");
                    throw new Exception("Invalid Hourly Rate");
                }
            }

        }


        /// <summary>
        /// \brief b>Description</b>
        /// \details Default Constructor class for the Seasonal Employee, derived from the abstract Employee class
        /// sets the attributes of type string to "" and doubles to 0.00, uses the Employee constructor
        /// to set all remaining attributes.
        /// </summary>
        public SeasonalEmployee() : base()
        {
            this.Type = EmployeeType.SN;
            season = Seasons.Default;
            piecePay = 0.00;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the SeasonalEmployee class, derived from the abstract Employee class
        /// sets the attributes of the SeasonalEmployee and Abstract class to values provided in the parameters, the rest
        /// are defaulted
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public SeasonalEmployee(string firstName, string lastName) : base()
        {
            this.Type = EmployeeType.SN;
            this.FirstName = firstName;
            this.LastName = lastName;
            season = Seasons.Default;
            piecePay = 0.00;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Overloaded Constructor for the SeasonalEmployee class, derived from the abstract Employee class
        ///  sets the attributes of the SeasonalEmployee and Abstract class to values provided in the parameters
        /// </summary>
        /// <param name="firstName">first name of the employee</param>
        /// <param name="lastName">last name of the employee</param>
        /// <param name="dob">Date of birth of the employee</param>
        /// <param name="sin">Social Insurance Number of the employee</param>
        /// <param name="season">season the employee works in</param>
        /// <param name="piecePay">piecePay of the employee</param>
        public SeasonalEmployee(string firstName, string lastName, DateTime dob, string sin,
            Seasons season, double piecePay) : base()
        {
            this.Type = EmployeeType.SN;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DOB = dob;
            this.SIN = sin;
            this.season = season;
            this.piecePay = piecePay;
        }


        // TODO
        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validates all attributes associated with a SeasonalEmployee
        /// </summary>
        /// <param name="firstName">first name of the employee</param>
        /// <param name="lastName">last name of the employee</param>
        /// <param name="dob">Date of birth of the employee</param>
        /// <param name="sin">Social Insurance Number of the employee</param>
        /// <param name="season">season the employee works in</param>
        /// <param name="piecePay">piecePay of the employee</param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(string firstName, string lastName, DateTime dob, string sin,
            Seasons season, double piecePay)
        {
            bool confirmed = true;

            bool[] test = new bool[]{
                validateName(EmployeeType.SN,firstName),
                validateName(EmployeeType.SN,lastName),
                validateDob(dob),
                validateSIN(EmployeeType.SN, sin, dob),
                validateSeason(season),
                FulltimeEmployee.validatePay(piecePay)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[SeasonalEmployee.Validate] Employee - " + lastName + ", "
                        + firstName + " (" + sin + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validates all attributes associated with a SeasonalEmployee
        /// </summary>
        /// <param name="seEmployee"></param>
        /// <returns>a bool that indicates whether the values to validate are acceptable</returns>
        public static bool validate(SeasonalEmployee seEmployee)
        {
            bool confirmed = true;

            bool[] test = new bool[]{
                validateName(seEmployee.Type,seEmployee.FirstName),
                validateName(seEmployee.Type,seEmployee.LastName),
                validateDob(seEmployee.DOB),
                validateSIN(seEmployee.Type, seEmployee.SIN, seEmployee.DOB),
                validateSeason(seEmployee.Season),
                FulltimeEmployee.validatePay(seEmployee.PiecePay)
            };

            foreach (bool flag in test)
            {
                if (!flag)
                {
                    confirmed = false;
                    Logging.Log("[SeasonalEmployee.Validate] Employee - " + seEmployee.LastName + ", "
                        + seEmployee.FirstName + " (" + seEmployee.SIN + ") - INVALID");
                    break;
                }
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Validate user input for the working season of a seasonal employee
        /// </summary>
        /// <param name="season">working season for the seasonal employee</param>
        /// <returns> boolean that indicates whether the season is an acceptable value or not</returns>
        static public bool validateSeason(Seasons season)
        {
            bool confirmed = false;
            switch (season)
            {
                case Seasons.FALL:
                case Seasons.WINTER:
                case Seasons.SPRING:
                case Seasons.SUMMER:
                    confirmed = true;
                    break;
                default:
                    break;
            }
            return confirmed;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes all values in a Seasonal Employee and constructs a string
        /// to be used in the presentation class to display employee details
        /// </summary>
        /// <param name="season">working season for the seasonal employee</param>
        /// <returns>string thats formated to be displayed in the presentation class</returns>
        public static string display(SeasonalEmployee employee, bool shouldLog)
        {
            string sinTemp = employee.SIN;
            sinTemp = sinTemp.Insert(6, " ");
            sinTemp = sinTemp.Insert(3, " ");
            string print =
                   "Employee Classification : Seasonal \n"
                 + "First Name              : " + employee.FirstName + "\n"
                 + "Last Name               : " + employee.LastName + "\n"
                 + "Date Of Birth           : " + employee.DOB.ToString("yyyy-MM-dd") + "\n"
                 + "SIN                     : " + sinTemp + "\n"
                 + "Season                  : " + employee.Season + "\n"
                 + "Piece Pay               : " + employee.PiecePay.ToString() + "\n";
            if (shouldLog)
            {
                Logging.Log("[SeasonalEmployee.Display] Employee: \n" + print); 
            }
            return print;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details takes a Seasonalemployee object and puts all attributes into a string[],
        ///  and then joins all items in the string array into a "|" delimited string.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>returns the string that holds all the attributes of the employee object delimited by "|"</returns>
        public static string join(SeasonalEmployee emp)
        {
            string[] item = new string[] {
                emp.Type.ToString(),emp.LastName,emp.FirstName,emp.SIN,emp.DOB.ToString(DateFormat),
                emp.Season.ToString().ToUpper(),emp.PiecePay.ToString()
            };
            return string.Join("|", item);
        }
    }
}
