/*
*  Filenmae        : UIMenu.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the UIMenu class that provide a User Interface for the user to actively,
*                    search, create, and modify employee's in a database.
*/
using System;
using System.Globalization;
using EMSSystem.AllEmployees;
using EMSSystem.TheCompany;
using EMSSystem.FileHandling;
using System.Collections.Generic;

namespace EMSSystem.Presentation
{
    /// <summary>
    /// \class Container
    /// This class contains the UI for the EMSSystem that allows users to search, create, and modify employee's in a database.
    /// </summary>
    public class UIMenu
    {


        private static string errorString = "";
        //this is an instance of the container
        private static Container EMSDatabase = null;
        //this is a class which displays the correct menus and prompts to the user.
        private static PromptString promptStrings = null;
       

        /// <summary>
        /// \brief <b>Description</b>
        /// \details The main menu of the EMS system. This is the first thing the User will see
        /// </summary>
        public static void mainMenu()
        {
            EMSDatabase = new Container();

            Console.OutputEncoding = System.Text.Encoding.Unicode;

            errorString = "";
            ConsoleKeyInfo UserInput;
            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("MAIN MENU");
                Console.WriteLine();
                Console.WriteLine("1. Manage EMS Database Files");
                Console.WriteLine("2. Manage Employees");
                Console.WriteLine("9. Quit");

                UserInput = Console.ReadKey();
                Console.Clear();

                switch (UserInput.KeyChar)
                {
                    case '1':
                        menuTwo();
                        break;
                    case '2':
                        menuThree();
                        break;
                    case '9':
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
            errorString = "";
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details The second menu. This is where the user can choose to either load or save the current database.
        /// </summary>
        static void menuTwo()
        {
            ConsoleKeyInfo userInput;
            bool exit = false;

            while (exit == false)
            {
                
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("FILE MANAGEMENT MENU");
                Console.WriteLine();
                Console.WriteLine("1. Load EMS Database From File");
                Console.WriteLine("2. Save Employee Set to EMS Database File");
                Console.WriteLine("9. Return to Main Menu");

                userInput = Console.ReadKey();
                Console.Clear();

                switch (userInput.KeyChar)
                {
                    case '1':
                        loadDataBaseFromFile();
                        break;
                    case '2':
                        if (EMSDatabase.Length != 0)
                            saveDataBaseToFile();
                        else
                            errorString = "No Employees in Database.";

                        break;
                    case '9':
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
            errorString = "";
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Saves the data base to file.
        /// </summary>
        static void saveDataBaseToFile()
        {
            List<string> records = EMSDatabase.concatAllRecords();

            bool saved = FileIO.SaveDB(records, @"DBase/EMS-Database.csv");

            //if the data base saved correctly or not, update the status
            if (saved == true)
                errorString = "Data Base Saved!";
            else if (saved == false)
                errorString = "Error Saving DataBase";

        } 


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Loads the data base from file.
        /// </summary>
        static void loadDataBaseFromFile()
        {
            List<string> recordSet = new List<string>();

            bool loaded = FileIO.LoadDB(out recordSet, @"DBase/EMS-Database.csv");

            //if the data base was loaded correctly or not, update the status
            if (loaded == true)
                errorString = "Data Base Loaded!";
            else if (loaded == false)
                errorString = "Error Loading DataBase";

            //if no employees where read, don't try to create the DB.
            if (recordSet.Count > 0)
            {
                EMSDatabase.createDB(recordSet);
            }
            
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details The third menu. The use is able to display, add, modify or remove employees from this menu.
        /// </summary>
        static void menuThree()
        {
            ConsoleKeyInfo userInput;
            bool exit = false;
            bool saved = false;
            while (exit == false)
            {
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("EMPLOYEE MANAGEMENT MENU");
                Console.WriteLine();
                Console.WriteLine("1. Display Employee Set");
                Console.WriteLine("2. Display One Employee's Details");
                Console.WriteLine("3. Create a NEW Employee");
                Console.WriteLine("4. Modify an EXISTING Employee");
                Console.WriteLine("5. Remove an EXISTING Employee");
                Console.WriteLine("9. Return to Main Menu");

                userInput = Console.ReadKey();
                Console.Clear();

                //if the database is empty, and they press 1,2,4,5, the status will tell them there are no employees in the DB
                if (EMSDatabase.Length == 0)
                {
                    switch (userInput.KeyChar)
                    {
                        case '1':
                        case '2':
                        case '4':
                        case '5':
                            errorString = "No Employees in Database.";
                            break;
                    }
                }
                
                switch (userInput.KeyChar)
                {
                    case '1':
                        //display all employee's info in groups of three
                        if (EMSDatabase.Length > 0)
                        {
                            displayEmployeeSet(); 
                        }
                        break;
                    case '2':
                        //display one employee's info
                        if (EMSDatabase.Length > 0)
                        {
                            displayEmployeeInfo();
                        }
                        break;
                    case '3':
                        //add an employee
                        saved = createEmployee();
                        break;
                    case '4':
                        //modify an employee
                        if (EMSDatabase.Length > 0)
                        {
                            updateEmployee();
                        }
                        break;
                    case '5':
                        //delete an employee
                        if (EMSDatabase.Length > 0)
                        {
                            deleteEmployee();
                        }
                        break;
                    case '9':
                        //go back to the last menu
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
            errorString = "";
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Displays a specific employee's details by using their SIN to look it up.
        /// </summary>
        private static void displayEmployeeInfo()
        {
            Console.Clear();
            string input = "";
            string info = "";

            while (input != "x" && input != "X")
            {
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Enter the SIN or BIN or the employee you want to display");
                Console.WriteLine("Enter 'exit' to exit");
                input = Console.ReadLine();
                Console.Clear();

                //exit if the user enters 'x'
                if(input.ToLower() == "exit" )
                {
                    Console.Clear();
                    break;
                }
                info = EMSDatabase.displayEmployeeInfo(input, true);

                //if an employee was found
                if (info != "")
                {
                    Console.WriteLine(info);
                }
                else
                {
                    errorString = "No Employee Could Be Found.";
                }
            }
            errorString = "";
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Displays the employee set in groups of 3 to the Console.
        /// </summary>
        private static void displayEmployeeSet()
        {
            int employeeCount = EMSDatabase.Length;
            string employeesToDisplay = "";
            ConsoleKeyInfo input;
            bool validChoice = false;
            bool exit = false;

            //employees are displayed in groups of three -> i += 3
            for(int i = 0; i <= employeeCount; i += 3){
                //displays employees at index 0-2, 3-5, 6-8...
                employeesToDisplay = EMSDatabase.displaySet(i);

                Console.Clear();
                //displays three employees
                Console.WriteLine(employeesToDisplay);
                validChoice = false;

                /*the user can press:
                1. show next three(if there are up to three more to show)
                2. show previous three(if there are 3 previous employees to show)
                9. Exit
                */
                
                //loop until the user presses a valid choice.
                while (validChoice == false)
                {
                    validChoice = false;

                    //display the menu
                    if (i < employeeCount - 3)
                    {
                        Console.WriteLine("1. Show Next Set");
                    }
                    if(i >= 3)
                    {
                        Console.WriteLine("2. Show Previous Set");
                    }
                    Console.WriteLine("9. Exit");

                    //get user input
                    input = Console.ReadKey();

                    
                    switch (input.KeyChar)
                    {
                        case '1':
                            //if their choice is valid
                            if (i < employeeCount - 3)
                            {
                                validChoice = true;
                            }
                            else //else clear the screen and redisplay the employee set.
                            {
                                Console.Clear();
                                Console.WriteLine(employeesToDisplay);
                            }
                            break;
                        case '2':
                            //if there choice is valid
                            if (i >= 3)
                            {
                                i -= 6;
                                validChoice = true;
                            }
                            else //else clear the screen and redisplay the employee set.
                            {
                                Console.Clear();
                                Console.WriteLine(employeesToDisplay);
                            }
                            break;
                        case '9':
                            //exit and clear the screen.
                            validChoice = true;
                            exit = true;
                            Console.Clear();
                            break;
                        default:
                            //if they press any other button, clear the screen and display the set again.
                            Console.Clear();
                            Console.WriteLine(employeesToDisplay);
                            break;
                    }
                }

                //if the user wants to exit
                if (exit)
                {
                    //break out of the loop.
                    break;
                }
            }
            errorString = "";
        }




        /// <summary>
        /// \brief <b>Description</b>
        /// \details This is where the user is promped to enter all employee details so an employee can be added to the database
        /// </summary>
        /// <returns>Returns a bool, true if the employee saved properly, false if the employee was not saved.
        static bool createEmployee()
        {
            
            errorString = "";
            
            //start off with the default menus, this will change once the user selects an employee type.
            promptStrings = new PromptString(EmployeeType.Default);

            //bool to say if the employee was saved or not.
            bool saved = false;

            //type, fName, lName, DOB, SIN/BIN
            var tempBaseEmployee = new Tuple<EmployeeType, string, string, DateTime, string>(EmployeeType.Default, "", "", default(DateTime), "");
            
            DateTime startDate = default(DateTime);
            DateTime? endDate = null;
            double pay = -1;
            Seasons season = Seasons.Default;

            ConsoleKeyInfo userInput;
            bool exit = false;

            //these flags are to keep track of which pieces of information
            //have and have not been gathered from the user
            bool dateFlag = false;
            bool payFlag = false;

            while (exit == false)
            {
                Console.Clear();

                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                //displays the correct menu for the type of employee the user is creating.
                promptStrings.displayMenu(dateFlag, payFlag);

                userInput = Console.ReadKey();
                
                switch (userInput.KeyChar)
                {
                    //get base employee details.
                    case '1':
                        tempBaseEmployee = getBaseEmployee();
			            dateFlag = false;
           		        payFlag = false;
                        break;
                    //enter start and end dates.
                    case '2':
                        //case 2 gets the start and end date for FT,PT and CT employees
                        //it also gets the season of work for SE employees
                        if (tempBaseEmployee.Item1 != EmployeeType.Default)
                        {
                            try
                            {
                                //if the employee is seasonal
                                if (tempBaseEmployee.Item1 == EmployeeType.SN)
                                {
                                    season = getSeason();
                                }
                                else //else, full time, part time, contract
                                {
                                    //1 = start date
                                    //2 = end date
                                    //tempBaseEmployee.Item4 = employee's DOB
                                    startDate = getDate(1, tempBaseEmployee.Item4, default(DateTime));
                                    
                                    endDate = getDate(2, tempBaseEmployee.Item4, startDate);
                                    if(endDate == default(DateTime))
                                    {
                                        endDate = null;
                                    }
                                }
                                dateFlag = true;
                            }
                            //if the user enters exit, an exeption is thrown, and it is caught here
                            //setting the start date, end date, and season of work to the default values.
                            catch (Exception ) {
                                season = Seasons.Default;
                                startDate = default(DateTime);
                                endDate = default(DateTime);
                            }
                        }
                        break;
                    //enter pay
                    case '3':
                        //gets the pay amount for the employee
                        if (tempBaseEmployee.Item1 != EmployeeType.Default)
                        {
                            try
                            {
                                //gets the pay amount for the employee.
                                pay = getPay();
                                payFlag = true;
                            }
                            //if the user enters exit, an exeption is thrown, and it is caught here
                            //pay is set to its default value.
                            catch (Exception) {
                                pay = -1;
                            }
                        }
                        break;
                    //save the employee
                    case '4':
                        //if the start date, end date and pay are entered, then the
                        // user is able to save the employee.
                        if (dateFlag == true && payFlag == true)
                        {
                            createFullEmployee(tempBaseEmployee, season, startDate, endDate, pay);
                            errorString = "Employee saved.";
                            Console.Clear();
                            exit = true;
                            saved = true;
                            return saved;
                        }
                        break;
                    //exit
                    case '9':
                        exit = true;
                        Console.Clear();
                        break;
                    default:
                        break;
                }//end of switch
            }// end of while
            errorString = "";
            return saved;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Creates the correct type of employee, which will then be passed to the container.
        /// </summary>
        private static void createFullEmployee(Tuple<EmployeeType, string, string, DateTime, string> baseEmployee, Seasons season, DateTime startDate, DateTime? endDate, double pay)
        {
            Employee employee  = null;

            //instantiates the correct employee type and adds it to the container
            switch (promptStrings.EType)
            {
                case EmployeeType.FT:
                    employee = new FulltimeEmployee(baseEmployee.Item2, baseEmployee.Item3, baseEmployee.Item4, baseEmployee.Item5, startDate, endDate, pay);
                    break;
                case EmployeeType.PT:
                    employee = new ParttimeEmployee(baseEmployee.Item2, baseEmployee.Item3, baseEmployee.Item4, baseEmployee.Item5, startDate, endDate, pay);
                    break;
                case EmployeeType.CT:
                    employee = new ContractEmployee(baseEmployee.Item3, baseEmployee.Item4, baseEmployee.Item5, startDate, (DateTime)endDate, pay);
                    break;
                case EmployeeType.SN:
                    employee = new SeasonalEmployee(baseEmployee.Item2, baseEmployee.Item3, baseEmployee.Item4, baseEmployee.Item5, season, pay);
                    break;
            }

            //passes the employee to the container
            EMSDatabase.addEmployee(employee);

        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function calls the functions that prompt the user to enter the information for the employee
        /// </summary>
        /// <returns>Returns a tuple which contians the employee type, first name, last name, date of birth/date of incorporation, and the SIN/BIN</returns>
        static Tuple<EmployeeType, string, string, DateTime, string> getBaseEmployee()
        {
            EmployeeType employeeType = EmployeeType.Default;
            string fName = "";
            string lName = "";
            DateTime DOB = default(DateTime);
            string SINOrBN = "";

            //gets the employee type
            employeeType = getEmployeeType();
            promptStrings = new PromptString(employeeType);

            try
            {
                //contract employees don't have first names.
                if (employeeType != EmployeeType.CT)
                {
                    fName = getName(0);
                }

                lName = getName(1);
                DOB = getDate(0, default(DateTime), default(DateTime));
                SINOrBN = getSINOrBIN(DOB);
            }
            //if the user enters exit while entering any of the base employee details, an exeption is thrown,
            //and caught here, where all the details are set to their default values.
            catch(Exception )
            {
                employeeType = EmployeeType.Default;
                fName = "";
                lName = "";
                DOB = default(DateTime);
                SINOrBN = "";
                promptStrings = new PromptString(EmployeeType.Default);
            }

            //create a new tuple with the employee details, (tuples are immutable)
            var updatedTempEmployee = new Tuple<EmployeeType, string, string, DateTime, string>(
                employeeType, fName, lName, DOB, SINOrBN);

            //pass the details back
            return updatedTempEmployee;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets the type of the employee.
        /// </summary>
        /// <returns>Returns the type of employee the user wants to create.</returns>
        static EmployeeType getEmployeeType()
        {
            EmployeeType type = EmployeeType.Default;
            bool exit = true;

            ConsoleKeyInfo userInput;

            do
            {
                exit = true;

                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("SELECT THE TYPE OF EMPLOYEE TO BE ADDED");
                Console.WriteLine("1. Full Time");
                Console.WriteLine("2. Part Time");
                Console.WriteLine("3. Contract");
                Console.WriteLine("4. Seasonal");

                userInput = Console.ReadKey();

                //determine the type of employee.
                switch (userInput.KeyChar)
                {
                    case '1': type = EmployeeType.FT; break;
                    case '2': type = EmployeeType.PT; break;
                    case '3': type = EmployeeType.CT; break;
                    case '4': type = EmployeeType.SN; break;
                    default: exit = false; errorString = "That is not a valid choice"; break;
                }
            } while (exit == false);

            errorString = "";
            return type;
        }



        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets the season of work from the user.
        /// </summary>
        /// <returns>Returns the season of work that the employee will work.</returns>
        static Seasons getSeason()
        {
            Seasons season = Seasons.Default;
            bool exit = true;

            ConsoleKeyInfo userInput;

            do
            {
                exit = true;

                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("SELECT THE TYPE OF EMPLOYEE TO BE ADDED");
                Console.WriteLine("1. Spring");
                Console.WriteLine("2. Winter");
                Console.WriteLine("3. Summer");
                Console.WriteLine("4. Fall");

                userInput = Console.ReadKey();

                //determine the correct season for the employee
                switch (userInput.KeyChar)
                {
                    case '1': season = Seasons.SPRING; break;
                    case '2': season = Seasons.WINTER; break;
                    case '3': season = Seasons.SUMMER; break;
                    case '4': season = Seasons.FALL; break;
                    default: exit = false; errorString = "That is not a valid choice"; break;
                }
            } while (exit == false);

            errorString = "";
            return season;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets the pay ammount for the employee.
        /// </summary>
        /// <returns>Returns the pay amount.</returns>
        static double getPay()
        {
            double pay = 0;
            string tempPay = "";
            bool valid = false;

            do
            {
                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Please enter the {0}.", promptStrings.Pay);
                Console.WriteLine("Not Negative or Zero");
                Console.WriteLine("Enter 'exit' to return to the main menu.");
                tempPay = Console.ReadLine();

                //if the use enters exit
                if (tempPay.ToLower() == "exit")
                {
                    errorString = "";
                    throw new ArgumentException("Exit");
                }

                //check if what the user entered is a number
                if (Double.TryParse(tempPay, out pay))
                {
                    //validate the number entered.
                    valid = FulltimeEmployee.validatePay(pay);
                }

                //if it was not a number, update the errorString
                if (!valid)
                {
                    errorString = ("That is not a valid " + promptStrings.Pay + ".");
                }

            } while (valid == false);

            errorString = "";
            return pay;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets a name for the employee.
        /// </summary>
        /// <param name="firstOrLast">This is a flag that tells us to ask for a first or lastname.</param>
        /// <returns>Returns the name the user entered</returns>
        static string getName(int firstOrLast)
        {
            string name = "";
            bool valid = false;
            
            do
            {
                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");

                // 0 = first name
                // 1 = last name/company name
                if (firstOrLast == 0)
                {
                    Console.WriteLine("Please enter the {0}.", promptStrings.FirstName);
                }
                else if (firstOrLast == 1)
                {
                    Console.WriteLine("Please enter the {0}.", promptStrings.LastName);
                }
                

                Console.WriteLine("Between 1 and 20 characters.");
                Console.WriteLine("No special characters or numbers.");
                Console.WriteLine("Enter exit to return to the main menu.");

                name = Console.ReadLine();

                //exit if entered exit
                if(name.ToLower() == "exit")
                {
                    errorString = "";
                    throw new ArgumentException("Exit");
                }

                //validate the name entered
                valid = Employee.validateName(promptStrings.EType, name);
                //if the name was not valid
                if (!valid)
                {
                    //if the first name was not valid
                    if (firstOrLast == 0)
                    {
                        errorString = ("That is not a valid first name.");
                    }
                    else if (firstOrLast == 1) //if the last name/ company name was not valid
                    {
                        if(promptStrings.EType == EmployeeType.CT)
                        {
                            errorString = ("That is not a valid " + promptStrings.LastName + ".");
                        }
                        else
                        {
                            errorString = ("That is not a valid last name.");
                        }
                        
                    }

                }

            } while (valid == false);

            errorString = "";
            return name;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets the SIN or BIN
        /// </summary>
        /// <param name="DOB">This is the Date of birth/incorportation of th employee. It is used when the type of employee is a contract employee</param>
        /// <returns>Returns the SIN/BIN of the employee</returns>
        static string getSINOrBIN(DateTime DOB)
        {
            string SIN = "";

            bool valid = false;

            do
            {
                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Please enter the {0}", promptStrings.SIN);
                Console.WriteLine("Format: nine(9) digits");
                Console.WriteLine("Enter exit to return to the main menu.");

                SIN = Console.ReadLine();

                //entered exit 
                if (SIN.ToLower() == "exit")
                {
                    throw new ArgumentException("Exit");
                }

                //validate the SIN/BIN
                valid = Employee.validateSIN(promptStrings.EType, SIN, DOB);
                if (!valid)
                {
                    //if the employee type is contract
                    if(promptStrings.EType == EmployeeType.CT)
                    {
                        errorString = ("That is not a valid BIN.");
                    }
                    else //any other type of employee.
                    {
                        errorString = ("That is not a valid SIN.");
                    }
                }

            } while (valid == false);

            errorString = "";
            return SIN;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details Gets a date from the user. This date can be the Date of birth. But it can also be the start date or end date of the employee.
        /// </summary>
        /// <param name="dateType">This is a flag to say what kind of date is going to be validated.</param>
        /// <param name="dob">This is the date of birth of the employee, if a date of birth is not set, the value will be default(DateTime).</param>
        /// <param name="startDate">This is the start date of the employee, if a start date is not set, the value will be default(DateTime).</param>
        /// <returns>Returns the date the user entered</returns>
        static DateTime getDate(int dateType, DateTime dob, DateTime startDate)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            DateTime date = default(DateTime);
            string tempDOB = "";
            bool valid = false;

            do
            {
                Console.Clear();
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");

                // 0 = DOB/date of incorporation
                // 1 = start date
                // 1 = end date
                if (dateType == 0)
                {
                    Console.WriteLine("Please enter the {0}", promptStrings.DOB);
                }
                else if (dateType == 1)
                {
                    Console.WriteLine("Please enter the {0}", promptStrings.StartDate);
                }
                else if (dateType == 2)
                {
                    Console.WriteLine("Please enter the {0}", promptStrings.EndDate);
                    if(promptStrings.EType != EmployeeType.CT) {
                        Console.WriteLine("Or press Enter for no end date.");
                    }
                    
                }
                
                Console.WriteLine("Format: YYYY-MM-DD");
                Console.WriteLine("Enter exit to return to the main menu.");

                tempDOB = Console.ReadLine();

                
                //exit when exit entered
                if (tempDOB.ToLower() == "exit")
                {
                    errorString = "";
                    throw new ArgumentException("Exit");
                }

                if (tempDOB == "" && dateType == 2 && promptStrings.EType != EmployeeType.CT)
                {
                    //return a blank date.
                    return default(DateTime);
                }
                else if (DateTime.TryParseExact(tempDOB, "yyyy-MM-dd", enUS, DateTimeStyles.None, out date) == true)
                {
                    //date of birth
                    if(dateType == 0)
                    {
                        valid = Employee.validateDob(date);
                    }

                    if (promptStrings.EType != EmployeeType.SN)
                    {
                        if (dateType == 1)//start date
                        {
                            valid = FulltimeEmployee.validateStartDate(dob, date);
                        }
                        else if (dateType == 2) //end date
                        {
                            valid = FulltimeEmployee.validateStopDate(startDate, date);
                        }
                    }
                }
                if (valid == false)
                {
                    if(promptStrings.EType == EmployeeType.CT)
                    {
                        if (dateType == 0)
                        {
                            errorString = ("That is not a valid date of incorporation.");
                        }
                        else if (dateType == 1)
                        {
                            errorString = ("That is not a valid contract start date.");
                        }
                        else if (dateType == 2)
                        {
                            errorString = ("That is not a valid contract end date.");
                        }
                    }
                    else
                    {
                        if (dateType == 0)
                        {
                            errorString = ("That is not a valid date of birth.");
                        }
                        else if (dateType == 1)
                        {
                            errorString = ("That is not a valid date of hire.");
                        }
                        else if (dateType == 2)
                        {
                            errorString = ("That is not a valid date of termination.");
                        }
                    }
                }
            } while (valid == false);

            errorString = "";
            return date;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Prompts the user to enter the SIN/BIN of the employee they want to delete. They are then asked if they are sure that want to delete this employee.
        /// </summary>
        static void deleteEmployee()
        {
            string input = "";
            ConsoleKeyInfo confirm;
            Console.Clear();

            while (true)
            {
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Enter the SIN or BIN of the employee you wish to remove.");
                Console.WriteLine("Enter X to go back to the previous menu.");
                input = Console.ReadLine();
                Console.Clear();

                if (input == "X" || input == "x")
                {
                    break;
                }


                if(EMSDatabase.tryFindEmployee(input)){

                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("------------------------------");
                        Console.WriteLine();
                        Console.WriteLine(EMSDatabase.displayEmployeeInfo(input, false)); 
                        Console.WriteLine();
                        Console.WriteLine("Are you sure you wish to delete this employee? Y/N");
                        confirm = Console.ReadKey();

                        if (confirm.KeyChar == 'Y' || confirm.KeyChar == 'y')
                        {
                            EMSDatabase.removeEmployee(input);
                            Console.Clear();
                            errorString = "The employee was removed.";
                            break;
                        }
                        else if (confirm.KeyChar == 'N' || confirm.KeyChar == 'n')
                        {
                            Console.Clear();
                            errorString = "The employee was not deleted.";
                            break;
                        }
                        else {
                            Console.Clear();
                        }

                    }
                }
                else{
                    errorString = "Could not find employee.";
                }
            }
            errorString = "";
        }


        /// <summary>
        /// /// \brief <b>Description</b>
        /// \details Prompts the user to enter the SIN/BIN of the employee they want to update.
        /// </summary>
        static void updateEmployee()
        {
            errorString = "";
            string input = "";
            ConsoleKeyInfo choice;
            Employee employee = null;
            Console.Clear();

            promptStrings = null;

            while (true)
            {
                Console.WriteLine(errorString);
                Console.WriteLine("------------------------------");
                Console.WriteLine("Enter the SIN or BIN of the employee you wish to update.");
                Console.WriteLine("Enter 'exit' to go back to the previous menu.");
                input = Console.ReadLine();
                Console.Clear();

                errorString = "";

                //exit when exit entered
                if (input.ToLower() == "exit")
                {
                    break;
                }
                
                //try to find the employee via the SIN/BIN entered,
                if(EMSDatabase.tryFindEmployee(input))
                {
                    //if found, make a copy of the current employee.
                    Employee tempEmp = (Employee)EMSDatabase.findEmployee(input).Clone();

                    //converts the employee to the correct employee Type
                    employee = EMSDatabase.findEmployee(input);
                    switch (tempEmp.GetType().Name)
                    {
                        case "FulltimeEmployee":
                            promptStrings = new PromptString(EmployeeType.FT);
                            break;
                        case "ParttimeEmployee":
                            promptStrings = new PromptString(EmployeeType.PT);
                            break;
                        case "ContractEmployee":
                            promptStrings = new PromptString(EmployeeType.CT);
                            break;
                        case "SeasonalEmployee":
                            promptStrings = new PromptString(EmployeeType.SN);
                            break;
                        default:
                            break;
                    }
                    

                    //this is the loops that displays the menu, and also prompts the use for input.
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(errorString);
                        Console.WriteLine("------------------------------");
                        Console.WriteLine();
                        Console.WriteLine(EMSDatabase.displayEmployeeInfo(input, false));
                        Console.WriteLine();
                        promptStrings.displayUpdateMenu();

                        choice = Console.ReadKey();

                        //9. exit and save
                        //x. exit and dont save
                        try
                        {
                            //save and exit
                            if (choice.KeyChar == '9')
                            {
                                errorString = "Employee Updated.";
                                EMSDatabase.updateEmployee(tempEmp);
                                Console.Clear();
                                break;
                            }
                            //dont save and exit
                            else if (choice.KeyChar.ToString().ToLower() == "x")
                            {
                                errorString = "Employee changes were discarded.";
                                tempEmp = employee;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                //display the correct menu options.
                                switch (promptStrings.EType)
                                {
                                    case EmployeeType.FT:
                                        fullTimeMenu(tempEmp, choice.KeyChar);
                                        break;
                                    case EmployeeType.PT:
                                        partTimeMenu(tempEmp, choice.KeyChar);
                                        break;
                                    case EmployeeType.CT:
                                        contractMenu(tempEmp, choice.KeyChar);
                                        break;
                                    case EmployeeType.SN:
                                        seasonalMenu(tempEmp, choice.KeyChar);
                                        break;
                                }
                            }
                            
                        }
                        //exceptions are caught when the user exits while entering a value for a field.
                        catch (Exception)
                        {

                        }
                    }
                }
                else
                {
                    //if the emplpyee could not be found.
                    errorString = "Could not find employee.";
                }
            }

            errorString = "";
        }


        /// <summary>
        /// /// \brief <b>Description</b>
        /// \details This holds the switch case to allow the user to update a full time employee
        /// </summary>
        /// <param name="employee">This is an employee object, which is then casted to the correct type of employee</param>
        /// <param name="choice">This is the users menu choice.</param>
        private static void fullTimeMenu(Employee employee, char choice)
        {
            FulltimeEmployee fullEmp = (FulltimeEmployee)employee;
            DateTime tempDate = default(DateTime);
            DateTime? tempEndDate = default(DateTime);

            switch (choice)
            {
                case '1':
                    fullEmp.FirstName = getName(0);
                    errorString = "First name updated.";
                    //first name
                    break;
                case '2':
                    fullEmp.LastName = getName(1);
                    errorString = "Last name updated.";
                    //last name
                    break;
                case '3'://date of birth
                    tempDate = getDate(0, default(DateTime), default(DateTime));

                    errorString = "";
                    //DOB has to be earlier than start and end date.
                    if (tempDate < fullEmp.DateOfHire && tempDate < fullEmp.DateOfTermination)
                    {
                        fullEmp.DOB = tempDate;
                        errorString = "Date of birth updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }
                    
                    break;
                case '4': //start date
                    tempDate = getDate(1, fullEmp.DOB, default(DateTime));
                    errorString = "";

                    //start date has to be later than DOB but earlier than date of termination.
                    if(tempDate > fullEmp.DOB && tempDate < fullEmp.DateOfTermination)
                    {
                        fullEmp.DateOfHire = tempDate;
                        errorString = "Date of hire updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }
                    break;
                case '5'://end date

                    tempEndDate = getDate(2, fullEmp.DOB, fullEmp.DateOfHire);
                    errorString = "";

                    if(tempEndDate == default(DateTime))
                    {
                        fullEmp.DateOfTermination = null;
                        errorString = "Date of termination updated.";
                    }
                    else {
                        //end date has to be later than DOB and start date
                        if (tempEndDate > fullEmp.DOB && tempEndDate > fullEmp.DateOfHire)
                        {
                            fullEmp.DateOfTermination = tempEndDate;
                            errorString = "Date of termination updated.";
                        }
                        else
                        {
                            errorString = "Invalid Date.";
                        }
                    }
                    
                    break;
                case '6':
                    //salary amount
                    fullEmp.Salary = getPay();
                    errorString = "Salary updated";
                    break;
            }
        }


        /// <summary>
        /// /// \brief <b>Description</b>
        /// \details This holds the switch case to allow the user to update a part time employee
        /// </summary>
        ///  <param name="employee">This is an employee object, which is then casted to the correct type of employee</param>
        /// <param name="choice">This is the users menu choice.</param>
        private static void partTimeMenu(Employee employee, char choice)
        {
            ParttimeEmployee PartEmp = (ParttimeEmployee)employee;
            DateTime tempDate;
            DateTime? tempEndDate;

            switch (choice)
            {
                case '1':
                    PartEmp.FirstName = getName(0);
                    errorString = "First name updated";
                    //first name
                    break;
                case '2':
                    PartEmp.LastName = getName(1);
                    errorString = "Last name updated";
                    //last name
                    break;
                case '3'://date of birth
                    tempDate = getDate(0, default(DateTime), default(DateTime));
                    errorString = "";

                    //DOB has to be earlier than start and end date.
                    if (tempDate < PartEmp.DateOfHire && tempDate < PartEmp.DateOfTermination)
                    {
                        PartEmp.DOB = tempDate;
                        errorString = "Date of birth updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }

                    break;
                case '4': //start date
                    //start date has to be later than DOB but earlier than date of termination.
                    tempDate = getDate(1, PartEmp.DOB, default(DateTime));
                    errorString = "";

                    if (tempDate > PartEmp.DOB && tempDate < PartEmp.DateOfTermination)
                    {
                        PartEmp.DateOfHire = tempDate;
                        errorString = "Date of hire updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }
                    break;
                case '5'://end date
                    tempEndDate = getDate(2, PartEmp.DOB, PartEmp.DateOfHire);
                    errorString = "";

                    if(tempEndDate == default(DateTime))
                    {
                        PartEmp.DateOfTermination = null;
                        errorString = "Date of termination updated.";
                    }
                    //end date has to be later than DOB and start date
                    else if(tempEndDate > PartEmp.DOB && tempEndDate > PartEmp.DateOfHire)
                    {
                        PartEmp.DateOfTermination = tempEndDate;
                        errorString = "Date of termination updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }

                    break;
                case '6':
                    //hourly rate
                    PartEmp.HourlyRate = getPay();
                    errorString = "Hourly rate updated";
                    //hourly or salary
                    break;
            }
        }



        /// <summary>
        /// /// \brief <b>Description</b>
        /// \details This holds the switch case to allow the user to update a seasonal employee
        /// </summary>
        /// <param name="employee">This is an employee object, which is then casted to the correct type of employee</param>
        /// <param name="choice">This is the users menu choice.</param>
        private static void seasonalMenu(Employee employee, char choice)
        {
            SeasonalEmployee seasEmp = (SeasonalEmployee)employee;
            
            switch (choice)
            {
                case '1':
                    seasEmp.FirstName = getName(0);
                    errorString = "First name updated";
                    //first name
                    break;
                case '2':
                    seasEmp.LastName = getName(1);
                    errorString = "Last name updated";
                    //last name
                    break;
                case '3':
                    seasEmp.DOB = getDate(0, default(DateTime), default(DateTime));
                    errorString = "Date of birth updated.";
                    //date of birth
                    break;
                case '4':
                    //season
                    seasEmp.Season = getSeason();
                    errorString = "Season of work updated.";
                    break;
                case '5':
                    //pay piece
                    seasEmp.PiecePay = getPay();
                    errorString = "Pay Piece updated.";
                    break;
            }
        }


        /// <summary>
        /// /// \brief <b>Description</b>
        /// \details This holds the switch case to allow the user to update a contract employee
        /// </summary>
        /// <param name="employee">This is an employee object, which is then casted to the correct type of employee</param>
        /// <param name="choice">This is the users menu choice.</param>
        private static void contractMenu(Employee employee, char choice)
        {
            ContractEmployee ContEmp = (ContractEmployee)employee;
            DateTime tempDate;
            switch (choice)
            {
                case '1':
                    //company name
                    ContEmp.LastName = getName(1);
                    errorString = "Company name updated";
                    break;
                case '2': //start date
                    tempDate = getDate(1, ContEmp.DOB, default(DateTime));

                    //start date has to be later than date of incorporation but earlier than date of termination.
                    if (tempDate > ContEmp.DOB && tempDate < ContEmp.ContractStopDate)
                    {
                        ContEmp.ContractStartDate = tempDate;
                        errorString = "Contract start date updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }
                    break;
                case '3':
                    //end date
                    tempDate = getDate(2, ContEmp.DOB, ContEmp.ContractStartDate);
                    errorString = "";

                    //end date has to be later than DOB and start date
                    if (tempDate > ContEmp.DOB && tempDate > ContEmp.ContractStartDate)
                    {
                        ContEmp.ContractStopDate = tempDate;
                        errorString = "Contract end date updated.";
                    }
                    else
                    {
                        errorString = "Invalid Date.";
                    }
                    break;
                case '4':
                    //contract amount
                    ContEmp.FixedContractAmount = getPay();
                    errorString = "Fixed contract amount updated.";
                    break;
            }
        }
    }
}