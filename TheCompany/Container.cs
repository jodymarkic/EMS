/*
*  Filenmae        : Container.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the container class that stores employee records in memory, before being written
*                    to a flat file database.
*/
using EMSSystem.AllEmployees;
using EMSSystem.Logger;
using EMSSystem.FileHandling;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EMSSystem.TheCompany
{
    /// <summary>
    /// \class Container
    /// This class contains the database and any functions that read, write, update, or delete from the database.
    /// </summary>
    public class Container
    {
        //CRUD, preformat msg, find, tryFind, join and put all in array
        private Dictionary<string, Employee> EmployeeList;
        private Dictionary<int, string> commentList;
        public int Length { get { return EmployeeList.Count; } }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details this is the constuctor for the Container class.
        /// </summary>
        public Container()
        {
            EmployeeList = new Dictionary<string, Employee>();
            commentList = new Dictionary<int, string>();

        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function adds an employee to the collection
        /// </summary>
        /// <param name="emp">This is the employee that will be added</param>
        /// <returns>True if the employee was added successfully</returns>
        public bool addEmployee(Employee emp)
        {
            bool retCode = false;
            switch (getType(emp))
            {
                case EmployeeType.FT:
                    retCode = FulltimeEmployee.validate((FulltimeEmployee)emp);
                    break;
                case EmployeeType.PT:
                    retCode = ParttimeEmployee.validate((ParttimeEmployee)emp);
                    break;
                case EmployeeType.CT:
                    retCode = ContractEmployee.validate((ContractEmployee)emp);
                    break;
                case EmployeeType.SN:
                    retCode = SeasonalEmployee.validate((SeasonalEmployee)emp);
                    break;
                default:
                    break;
            }

            if (retCode)
            {
                if (EmployeeList.ContainsKey(emp.SIN))
                {
                    retCode = false;
                }
                else
                {
                    EmployeeList.Add(emp.SIN, emp);
                    Logging.Log("[Container.AddEmployee] Employee Added - " + emp.LastName + ", "
                    + emp.FirstName + " (" + emp.SIN + ") - VALID");
                }
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function adds an employee to the collection given all the values
        /// of a valid Fulltime, Parttime, or Contract Employee
        /// </summary>
        /// <param name="type">the type of employee (Fulltime, Parttime, or Contract)</param>
        /// <param name="firstName"> - first name of the employee</param>
        /// <param name="lastName"> - last name of the employee</param>
        /// <param name="dob"> - Date of birth of the employee</param>
        /// <param name="sin"> - Social Insurance Number of the employee</param>
        /// <param name="startDate"> - Date of Hire of the employee</param>
        /// <param name="stopDate"> - Date of termination of the employee</param>
        /// <param name="payAmount"> - Annual salary of the employee</param>
        /// <returns>True if the employee was added successfully</returns>
        public bool addEmployee(EmployeeType type, string fName, string lName, DateTime dob, string sin,
            DateTime startDate, DateTime? stopDate, double payAmount)
        {
            bool retCode = false;
            Employee emp = null;
            switch (type)
            {
                case EmployeeType.FT:
                    if (retCode = FulltimeEmployee.validate(fName, lName, dob, sin, startDate, stopDate, payAmount))
                    {
                        emp = new FulltimeEmployee(fName, lName, dob, sin, startDate, stopDate, payAmount);
                    }
                    break;
                case EmployeeType.PT:
                    if (retCode = ParttimeEmployee.validate(fName, lName, dob, sin, startDate, stopDate, payAmount))
                    {
                        emp = new ParttimeEmployee(fName, lName, dob, sin, startDate, stopDate, payAmount);
                    }
                    break;
                case EmployeeType.CT:
                    if (stopDate != null)
                    {
                        if (retCode = ContractEmployee.validate(lName, dob, sin, startDate, (DateTime)stopDate, payAmount))
                        {
                            emp = new ContractEmployee(lName, dob, sin, startDate, (DateTime)stopDate, payAmount);
                        }
                    }
                    break;
                default:
                    break;
            }
            if (retCode)
            {
                if (EmployeeList.ContainsKey(emp.SIN))
                {
                    retCode = false;
                }
                else
                {
                    EmployeeList.Add(emp.SIN, emp);
                    Logging.Log("[Container.AddEmployee] Employee Added - " + emp.LastName + ", "
                        + emp.FirstName + " (" + emp.SIN + ") - VALID");
                }
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function adds an employee to the collection given all the values for a Seasonal employee
        /// </summary>
        /// <param name="firstName">first name of the employee</param>
        /// <param name="lastName">last name of the employee</param>
        /// <param name="dob">Date of birth of the employee</param>
        /// <param name="sin">Social Insurance Number of the employee</param>
        /// <param name="season">season the employee works in</param>
        /// <param name="piecePay">piecePay of the employee</param>
        /// <returns>True if the employee was added successfully</returns>
        public bool addEmployee(string fName, string lName, DateTime dob, string sin,
            Seasons season, double payAmount)
        {
            bool retCode = false;
            Employee emp = null;
            if (retCode = SeasonalEmployee.validate(fName, lName, dob, sin, season, payAmount))
            {
                emp = new SeasonalEmployee(fName, lName, dob, sin, season, payAmount);
                if (EmployeeList.ContainsKey(emp.SIN))
                {
                    retCode = false;
                }
                else
                {
                    EmployeeList.Add(emp.SIN, emp);
                    Logging.Log("[Container.AddEmployee] Employee Added - " + emp.LastName + ", "
                        + emp.FirstName + " (" + emp.SIN + ") - VALID");
                }
            }
            return retCode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string displayEmployeeInfo(string input, bool shouldLog)
        {
            string ret = "";

            if (tryFindEmployee(input))
            {
                Employee emp = findEmployee(input);
                switch (emp.GetType().Name)
                {
                    case "FulltimeEmployee":
                        return FulltimeEmployee.display((FulltimeEmployee)emp, shouldLog);
                    //break;
                    case "ParttimeEmployee":
                        return ParttimeEmployee.display((ParttimeEmployee)emp, shouldLog);
                    //break;
                    case "ContractEmployee":
                        return ContractEmployee.display((ContractEmployee)emp, shouldLog);
                    //break;
                    case "SeasonalEmployee":
                        return SeasonalEmployee.display((SeasonalEmployee)emp, shouldLog);
                    //break;
                    default:
                        break;
                }
            }

            return ret;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function finds a specific employee via the SIN or BIN and returns that employee.
        /// </summary>
        /// <param name="key"> This is the SIN or BIN of the employee.</param>
        /// <returns>Returns the Employee that we looked for.</returns>
        public Employee findEmployee(string key)
        {
            Employee temp = null;
            if (EmployeeList.ContainsKey(key))
            {
                EmployeeList.TryGetValue(key, out temp);
            }
            return temp;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details Trys to find an employee in the collection.
        /// </summary>
        /// <param name="Key"> This is the SIN or BIN of the employee we are looking for.</param>
        /// <returns>Returns true if the employee is found and false if the employee is not found.</returns>
        public bool tryFindEmployee(string key)
        {
            return EmployeeList.ContainsKey(key);
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This method will update the info of an employee finding it using the SIN
        /// </summary>
        /// <param name="updatedEmployee"></param>
        /// <returns>Returns true if the employee is updated, false otherwise</returns>
        public bool updateEmployee(Employee updatedEmployee)
        {
            bool retCode = false;
            if (tryFindEmployee(updatedEmployee.SIN))
            {
                Employee temp = findEmployee(updatedEmployee.SIN);
                removeEmployee(updatedEmployee.SIN);
                retCode = addEmployee(updatedEmployee);
                Logging.Log("[Container.UpdateEmployee] Employee Updated - " + updatedEmployee.LastName + ", "
                         + updatedEmployee.FirstName + " (" + updatedEmployee.SIN + ") - VALID");
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function removes an employee from the collection
        /// </summary>
        /// <returns>Retuns true if the remove was successful and false if it failed</returns>
        public bool removeEmployee(string key)
        {
            bool retCode = EmployeeList.Remove(key);
            if (retCode)
            {
                Logging.Log("[Container.RemoveEmployee] Employee Removed at SIN - " + key);
            }
            return retCode;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This function takes in the index to start at in the collection, and returns 3 pre formatted employees to the UI class.
        /// </summary>
        /// <param name="index">This is the index that the collection will start reading at.</param>
        /// <returns>Returns 3 pre-formatted string that contain information for 3 different employees.</returns>
        public string displaySet(int index)
        {
            string displayString = "";
            Employee currentEmployee = null;
            for (int i = index; (i < (index + 3)) && (i <= EmployeeList.Count - 1); i++)
            {
                currentEmployee = EmployeeList.ElementAt(i).Value;
                displayString += displayEmployeeInfo(currentEmployee.SIN, false) + "\n";
            }
            return displayString;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This method checks the type of an employee, and returns the equivelant enum value
        /// </summary>
        /// <param name="emp">The Employee object</param>
        public EmployeeType getType(Employee emp)
        {
            EmployeeType curType = EmployeeType.Default;
            Type type = emp.GetType();
            switch (type.Name)
            {
                case "FulltimeEmployee":
                    curType = EmployeeType.FT;
                    break;
                case "ParttimeEmployee":
                    curType = EmployeeType.PT;
                    break;
                case "ContractEmployee":
                    curType = EmployeeType.CT;
                    break;
                case "SeasonalEmployee":
                    curType = EmployeeType.SN;
                    break;
                default:
                    break;
            }
            return curType;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details this Method contatinates all records in the employeelist and add's them to the Container
        /// </summary>
        /// <returns></returns>
        public List<string> concatAllRecords()
        {
            List<string> container = new List<string>();
            EmployeeType type = EmployeeType.Default;
            int i = 0;
            foreach (Employee currentEmployee in EmployeeList.Values)
            {
                type = getType(currentEmployee);
                switch (type)
                {
                    case EmployeeType.FT:
                        container.Add(FulltimeEmployee.join((FulltimeEmployee)currentEmployee));
                        break;
                    case EmployeeType.PT:
                        container.Add(ParttimeEmployee.join((ParttimeEmployee)currentEmployee));
                        break;
                    case EmployeeType.CT:
                        container.Add(ContractEmployee.join((ContractEmployee)currentEmployee));
                        break;
                    case EmployeeType.SN:
                        container.Add(SeasonalEmployee.join((SeasonalEmployee)currentEmployee));
                        break;
                    default:
                        break;
                }
                i++;
            }

            container.Insert(0, "");
            for (i=0;i<commentList.Count;i++)
            {
                KeyValuePair<int, string> item = commentList.ElementAt(i);
                if (item.Key > container.Count)
                {
                    commentList.Remove(item.Key);
                    commentList.Add(container.Count, item.Value);
                    i--;
                }
                else
                {
                    container.Insert(item.Key, item.Value);
                }
                
            }
            return container;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="datesToCheck"></param>
        /// <returns></returns>
        private bool parseDate(params string[] datesToCheck)
        {
            bool retCode = true;
            DateTime date = DateTime.MinValue;
            foreach (string item in datesToCheck)
            {
                if (item.ToUpper() != "N/A")
                {
                    if (!DateTime.TryParseExact(item, Employee.DateFormat, Employee.EnUS, System.Globalization.DateTimeStyles.None, out date))
                    {
                        retCode = false;
                        break;
                    }
                }
            }
            return retCode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        public int createDB(List<string> fileContents)
        {
            bool? validity = false;
            bool cancelLog = false;
            int invalidCount = 0;
            int commentCount = 0;
            commentList.Clear();
            foreach (string item in fileContents)
            {
                validity = false;
                cancelLog = false;
                double payAmount = 0;
                string[] row = item.Split('|');
                if (item == "" || item.StartsWith("; Date Created : "))
                {
                    validity = null;
                }
                else if (item.StartsWith(";"))
                {
                    validity = null;
                    int key = getCommentKey(fileContents, item);
                    commentList.Add(key, item);
                    commentCount++;
                }
                else
                {
                    if (row.Count() < 7 || row.Count() > 8)
                    {
                        cancelLog = true;
                    }
                    else
                    {
                        EmployeeType type = EmployeeType.Default;
                        if (Enum.TryParse(row[0], out type))
                        {
                            if (Double.TryParse(row.Last(), out payAmount))
                            {
                                if (parseDate(row[4]))
                                {
                                    DateTime? Dot = null;
                                    switch (type)
                                    {
                                        case EmployeeType.FT:
                                        case EmployeeType.PT:
                                        case EmployeeType.CT:
                                            if (parseDate(row[5], row[6]))
                                            {
                                                if (row[6].ToUpper() == "N/A")
                                                {
                                                    if (type == EmployeeType.CT)
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Dot = DateTime.Parse(row[6]);
                                                }
                                                validity = addEmployee(type, row[2], row[1], DateTime.Parse(row[4]), row[3],
                                                    DateTime.Parse(row[5]), Dot, payAmount);
                                                cancelLog = true;
                                            }
                                            break;
                                        case EmployeeType.SN:
                                            Seasons season = Seasons.Default;
                                            if (Enum.TryParse(row[5], out season))
                                            {
                                                validity = addEmployee(row[2], row[1], DateTime.Parse(row[4]), row[3], season, payAmount);
                                                cancelLog = true;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (validity == false)
                {
                    if (!cancelLog)
                    {
                        Logging.Log("[Container.CreateDB] Employee - " + row[2] + ", " + row[1] + " (" + row[3] + ") - INVALID");
                    }
                    invalidCount++;
                }
            }
            FileIO.logLoadStats(this.Length,(fileContents.Count-commentCount )- invalidCount, invalidCount);
            return invalidCount;
        }

        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details this method gets the key of a specfic file record
        /// </summary>
        /// <param name="fileContents"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private int getCommentKey(List<string> fileContents, string item)
        {
            int num = 0;
            do
            {
                num++;
                num = fileContents.IndexOf(item,num);
            } while (commentList.ContainsKey(num));
            return num;
        }

        /// <summary>
        ///  \brief <b>Description</b>
        ///  \details this method check if an employee exists within the database.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool containsKey(string key)
        {
            return EmployeeList.ContainsKey(key);
        }
    }
}
