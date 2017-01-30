using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Logic
{
    public class LogicClass
    {
        List<DateTime> startDate = new List<DateTime>(); // List to store all the start dates parsed from the files
        List<DateTime> endDate = new List<DateTime>(); // List to store all the end dates parsed from the files

        List<TimeSpan> startTime = new List<TimeSpan>(); // List to store all the start times parsed from the files
        List<TimeSpan> endTime = new List<TimeSpan>(); // List to store all the end dates parsed from the files

        List<string> device = new List<string>(); // List to store all the device names parsed from the files
        List<string> username = new List<string>(); // List to store all the usernames parsed from the files
        List<string> group = new List<string>(); // List to store all the departments parsed from the files

        List<string> masterUserFileList = new List<string>();

        // ***THIS NEEDS TO BE CHANGED FOR FINAL RELEASE - NEED TO PUT THE FILE IN A UNIVERSAL FOLDER***
        StreamReader configFile = new StreamReader(@"C:\Users\Owner\Source\NMR-Billing\NMRBillingApp\NMRBillingApp\Program Data\Config.txt");
        // ***THIS NEEDS TO BE CHANGED FOR FINAL RELEASE - NEED TO PUT THE FILE IN A UNIVERSAL FOLDER***
        StreamReader masterUserFile = new StreamReader(@"C:\Users\Owner\Source\NMR-Billing\NMRBillingApp\NMRBillingApp\Program Data\Master User List.txt");

        private double standardHourlyRate = 0.00;
        private double overcharge1HourlyRate = 0.00;
        private double overcharge2HourlyRate = 0.00;
        private double standardMinuteRate = 0.00;
        private double overcharge1MinuteRate = 0.00;
        private double overcharge2MinuteRate = 0.00;
        private double standardSecondRate = 0.00;
        private double overcharge1SecondRate = 0.00;
        private double overcharge2SecondRate = 0.00;

        private int noChargeMinutes = 0;
        private int standardChargeMinutes = 0;
        private int overcharge1Minutes = 0;
        private int overcharge2Minutes = 0;

        char[] delimiterChar = { '=' }; // Array of characters used to parse the file, currently using only a space character to split up the file, but can be changed to any character(s)

        public LogicClass()
        {

        }

        // Constructor to import data for manipulation in the rest of the class methods
        public LogicClass(List<DateTime> startD, List<DateTime> endD, List<TimeSpan> startT, List<TimeSpan> endT, List<string> deviceName, List<string> user, List<string> groupName)
        {
            startDate = startD;
            endDate = endD;
            startTime = startT;
            endTime = endT;
            device = deviceName;
            username = user;
            group = groupName;

            masterUserFileList = GetMasterUserFile();

            string line = "";

            try
            {
                while (!configFile.EndOfStream)
                {
                    int count = 0;

                    line = configFile.ReadLine();

                    string[] parsedLine = line.Split(delimiterChar); // Create an array to store each parsed item. Items will be parsed based on the delimiter character(s) stored in delimiterChar

                    if (parsedLine[count] == "STANDARDCHARGE")
                    {
                        standardHourlyRate = Convert.ToDouble(parsedLine[++count]);
                        standardMinuteRate = standardHourlyRate / 60.00;
                        standardSecondRate = standardMinuteRate / 60.00;
                    }

                    if (parsedLine[count] == "OVERCHARGE1")
                    {
                        overcharge1HourlyRate = Convert.ToDouble(parsedLine[++count]);
                        overcharge1MinuteRate = overcharge1HourlyRate / 60.00;
                        overcharge1SecondRate = overcharge1MinuteRate / 60.00;
                    }

                    if (parsedLine[count] == "OVERCHARGE2")
                    {
                        overcharge2HourlyRate = Convert.ToDouble(parsedLine[++count]);
                        overcharge2MinuteRate = overcharge2HourlyRate / 60.00;
                        overcharge2SecondRate = overcharge2MinuteRate / 60.00;
                    }

                    if (parsedLine[count] == "NOCHARGEMINUTES")
                    {
                        noChargeMinutes = Convert.ToInt32(parsedLine[++count]);
                    }

                    if (parsedLine[count] == "STANDARDMINUTES")
                    {
                        standardChargeMinutes = Convert.ToInt32(parsedLine[++count]);
                    }

                    if (parsedLine[count] == "OVERMINUTES1")
                    {
                        overcharge1Minutes = Convert.ToInt32(parsedLine[++count]);
                    }

                    if (parsedLine[count] == "OVERMINUTES2")
                    {
                        overcharge2Minutes = Convert.ToInt32(parsedLine[++count]);
                    }
                }
            }
            catch (Exception exception) // If a file isn't found, throw this error
            {
                System.Windows.Forms.MessageBox.Show("The configuration file could not be read: " + exception.Message);
            }

            configFile.Close();
        }

        public double GetStandardHourlyRate()
        {
            return standardHourlyRate;
        }

        public double GetOvercharge1HourlyRate()
        {
            return overcharge1HourlyRate;
        }

        public double GetOvercharge2HourlyRate()
        {
            return overcharge2HourlyRate;
        }

        public double GetNoChargeMinutes()
        {
            return noChargeMinutes;
        }

        public double GetStandardChargeMinutes()
        {
            return standardChargeMinutes;
        }

        public double GetOvercharge1Minutes()
        {
            return overcharge1Minutes;
        }

        public double GetOvercharge2Minutes()
        {
            return overcharge2Minutes;
        }

        public List<string> GetMasterUserFile()
        {
            List<string> lineList = new List<string>();
            List<string> name = new List<string>();

            string line = "";
            int count = 0;

            try
            {
                while (!masterUserFile.EndOfStream)
                {
                    line = masterUserFile.ReadLine();

                    string[] parsedLine = line.Split('\t'); // Create an array to store each parsed item. Items will be parsed based on the delimiter character(s)

                    foreach (var item in parsedLine)
                    {
                        if (item != "")
                        {
                            masterUserFileList.Add(item);
                            count++;
                        }
                    }
                }

                if (count % 4 != 0)
                {
                    System.Windows.Forms.MessageBox.Show("The master user list is incomplete. Verify every line has a username, full name, group, and department separated by one or more tabs. Outputs will NOT be accurate until this is resolved.");
                }
            }
            catch (Exception exception) // If a file isn't found, throw this error
            {
                System.Windows.Forms.MessageBox.Show("The master user list file could not be read: " + exception.Message);
            }

            masterUserFile.Close();

            return masterUserFileList;
        }

        public List<string> GetMasterUsers()
        {
            List<string> masterUserList = new List<string>();

            int count = 0;
            int target = 4;

            foreach (var list in masterUserFileList)
            {
                if (count == target)
                {
                    masterUserList.Add(list);
                    target += 4;
                }

                count++;
            }

            return masterUserList;
        }

        public List<string> GetMasterFullNames()
        {
            List<string> masterFullNameList = new List<string>();

            int count = 0;
            int target = 5;

            foreach (var list in masterUserFileList)
            {
                if (count == target)
                {
                    masterFullNameList.Add(list);
                    target += 4;
                }

                count++;
            }

            return masterFullNameList;
        }

        public List<string> GetMasterGroups()
        {
            List<string> masterGroupList = new List<string>();

            int count = 0;
            int target = 6;

            foreach (var list in masterUserFileList)
            {
                if (count == target)
                {
                    masterGroupList.Add(list);
                    target += 4;
                }

                count++;
            }

            return masterGroupList;
        }

        public List<string> GetUniqueMasterGroups()
        {
            return GetMasterGroups().Distinct().ToList();
        }

        public List<string> GetGroupsByDepartment(string department)
        {
            List<string> groupsByDepartment = new List<string>();

            int index = 0;

            foreach(var dept in GetUserDepartments())
            {
                if(dept == department)
                {
                    groupsByDepartment.Add(group[index]);
                }

                index++;

                if(index == GetUserDepartments().Count - 1 && groupsByDepartment.Count == 0)
                {
                    groupsByDepartment.Add("N/A");
                }
            }

            return groupsByDepartment;
        }

        public List<string> GetUniqueGroupsByDepartment(string department)
        {
            return GetGroupsByDepartment(department).Distinct().ToList();
        }

        public List<string> GetMasterDepartments()
        {
            List<string> masterDepartmentList = new List<string>();

            int count = 0;
            int target = 7;

            foreach (var list in masterUserFileList)
            {
                if (count == target)
                {
                    masterDepartmentList.Add(list);
                    target += 4;
                }

                count++;
            }

            return masterDepartmentList;
        }
        
        public List<string> GetUniqueMasterDepartments()
        {
            return GetMasterDepartments().Distinct().ToList();
        }

        public List<string> GetUserDepartments()
        {
            List<string> userDepartmentList = new List<string>();

            int index = 0;

            foreach(var user in username)
            {
                foreach (var person in GetMasterUsers())
                {
                    if (user == person)
                    {
                        userDepartmentList.Add(GetMasterDepartments()[index]);
                        string temp = GetMasterDepartments()[index];
                    }
                    index++;
                }
                index = 0;
            }

            return userDepartmentList;
        }

        public List<List<string>> GetUsersByGroup()
        {
            List<List<string>> usersByGroup = new List<List<string>>();
            //List<string> uniqueUsers = username.Distinct().ToList();
            List<string> uniqueGroups = group.Distinct().ToList();
            List<string> groupOfUsers = new List<string>();
            List<string> groupOfUniqueUsers = new List<string>();

            int userCounter = 0;

            usersByGroup.Add(uniqueGroups);


            foreach (var uniqueDepartment in uniqueGroups)
            {
                foreach (var user in group)
                {
                    if (user.Equals(uniqueDepartment))
                    {
                        groupOfUsers.Add(username[userCounter]);
                    }

                    userCounter++;
                }

                groupOfUniqueUsers = groupOfUsers.Distinct().ToList();

                usersByGroup.Add(groupOfUniqueUsers);
                groupOfUsers = new List<string>();
                userCounter = 0;
            }

            return usersByGroup;
        }

        public List<string> GetUniqueUsers()
        {
            List<string> uniqueUsers = username.Distinct().ToList();

            return uniqueUsers;
        }
        
        public List<string> GetUniqueDevices()
        {
            return device.Distinct().ToList();
        }

        public List<double> GetNMRTimesByDepartment(string nmr)
        {
            List<List<TimeSpan>> allNRMDepartmentTimes = new List<List<TimeSpan>>();
            List<TimeSpan> individualNRMDepartmentTimes = new List<TimeSpan>();
            List<double> nmrTimesByDepartment = new List<double>();
            
            int index = 0;

            foreach (var uniqueDept in GetUniqueMasterDepartments())
            {
                foreach (var dept in GetUserDepartments())
                {
                    if (uniqueDept == dept && device[index] == nmr)
                    {
                        individualNRMDepartmentTimes.Add(CalculateTimeDifference()[index]);
                    }

                    index++;

                    if (index == GetUserDepartments().Count && individualNRMDepartmentTimes.Count == 0)
                    {
                        individualNRMDepartmentTimes.Add(new TimeSpan(0, 0, 0));
                    }
                }
                index = 0;
                allNRMDepartmentTimes.Add(individualNRMDepartmentTimes);
                individualNRMDepartmentTimes = new List<TimeSpan>();
            }

            foreach (var list in allNRMDepartmentTimes)
            {
                double totalTime = 0.0;
                string mergeHoursAndMinutes = "";
                int hours = 0;
                int minutes = 0;

                foreach (var item in list)
                {
                    int days = 0;

                    days = item.Days;
                    hours += item.Hours + (days * 24);
                    minutes += item.Minutes;

                    while (minutes >= 60)
                    {
                        minutes -= 60;
                        hours += 1;
                    }
                }

                if (minutes < 10)
                {
                    mergeHoursAndMinutes = hours + ".0" + minutes;
                }
                else
                {
                    mergeHoursAndMinutes = hours + "." + minutes;
                }

                totalTime = Convert.ToDouble(mergeHoursAndMinutes);

                nmrTimesByDepartment.Add(totalTime);
            }

            return nmrTimesByDepartment;
        }

        public List<double> CalculateNMRChargeByDepartment(string nmr)
        {
            List<List<double>> allNRMDepartmentCharges = new List<List<double>>();
            List<double> individualNRMDepartmentCharge = new List<double>();
            List<double> nmrChargeByDepartment = new List<double>();

            int index = 0;

            foreach (var uniqueDept in GetUniqueMasterDepartments())
            {
                foreach (var dept in GetUserDepartments())
                {
                    if (uniqueDept == dept && device[index] == nmr)
                    {
                        individualNRMDepartmentCharge.Add(CalculateIndividualCharges()[index]);
                    }

                    index++;

                    if (index == GetUserDepartments().Count && individualNRMDepartmentCharge.Count == 0)
                    {
                        individualNRMDepartmentCharge.Add(0.0);
                    }
                }
                index = 0;
                allNRMDepartmentCharges.Add(individualNRMDepartmentCharge);
                individualNRMDepartmentCharge = new List<double>();
            }

            foreach (var list in allNRMDepartmentCharges)
            {
                double totalCharge = 0.0;

                foreach (var item in list)
                {
                    totalCharge += item;
                }

                nmrChargeByDepartment.Add(totalCharge);
            }

            return nmrChargeByDepartment;
        }

        public double CalculateTotalNMRChargeByDepartment(string nmr)
        {
            double totalCharge = 0.0;

            foreach(var charge in CalculateNMRChargeByDepartment(nmr))
            {
                totalCharge += charge;
            }

            return totalCharge;
        }

        public double GetNMRTotalTimeByDepartment(string nmr)
        {
            double nmrTotalTimeByDepartment = 0.0;

            int hours = 0;
            int minutes = 0;
            string mergeHoursAndMinutes = "";

            foreach (var item in GetNMRTimesByDepartment(nmr))
            {
                string[] parsedLine = item.ToString().Split('.');

                if (parsedLine.Length == 2)
                {
                    hours += Convert.ToInt32(parsedLine[0]);
                    minutes += Convert.ToInt32(parsedLine[1]);

                    switch(minutes)
                    {
                        case 1:
                            minutes = 10;
                            break;
                        case 2:
                            minutes = 20;
                            break;
                        case 3:
                            minutes = 30;
                            break;
                        case 4:
                            minutes = 40;
                            break;
                        case 5:
                            minutes = 50;
                            break;
                    }
                }

                while (minutes >= 60)
                {
                    minutes -= 60;
                    hours += 1;
                }
            }

            if (minutes < 10)
            {
                mergeHoursAndMinutes = hours + ".0" + minutes;
            }
            else
            {
                mergeHoursAndMinutes = hours + "." + minutes;
            }

            nmrTotalTimeByDepartment = Convert.ToDouble(mergeHoursAndMinutes);

            return nmrTotalTimeByDepartment;
        }

        public List<double> GetTotalDepartmentTimes()
        {
            List<List<TimeSpan>> allDepartmentTimes = new List<List<TimeSpan>>();
            List<TimeSpan> individualDepartmentTimes = new List<TimeSpan>();
            List<double> totalDepartmentTimes = new List<double>();
            
            int index = 0;
            
            foreach(var uniqueDept in GetUniqueMasterDepartments())
            {
                foreach (var dept in GetUserDepartments())
                {
                    if(uniqueDept == dept)
                    {
                        individualDepartmentTimes.Add(CalculateTimeDifference()[index]);
                    }

                    index++;

                    if (index == GetUserDepartments().Count && individualDepartmentTimes.Count == 0)
                    {
                        individualDepartmentTimes.Add(new TimeSpan(0, 0, 0));
                    }
                }
                index = 0;
                allDepartmentTimes.Add(individualDepartmentTimes);
                individualDepartmentTimes = new List<TimeSpan>();
            }

            foreach (var list in allDepartmentTimes)
            {
                double totalTime = 0.0;
                string mergeHoursAndMinutes = "";
                int hours = 0;
                int minutes = 0;

                foreach (var item in list)
                {
                    int days = 0;

                    days = item.Days;
                    hours += item.Hours + (days * 24);
                    minutes += item.Minutes;

                    while (minutes >= 60)
                    {
                        minutes -= 60;
                        hours += 1;
                    }
                }

                if(minutes < 10)
                {
                    mergeHoursAndMinutes = hours + ".0" + minutes;
                }
                else
                {
                    mergeHoursAndMinutes = hours + "." + minutes;
                }

                totalTime = Convert.ToDouble(mergeHoursAndMinutes);
                
                totalDepartmentTimes.Add(totalTime);
            }

            return totalDepartmentTimes;
        }

        public List<DateTime> MergeStartDateAndTime()
        {
            List<DateTime> mergedStartDateTime = new List<DateTime>();

            int count = 0;

            while (count < startDate.Count)
            {
                mergedStartDateTime.Add(new DateTime(startDate[count].Year, startDate[count].Month, startDate[count].Day, startTime[count].Hours, startTime[count].Minutes, startTime[count].Seconds));
                count++;
            }

            return mergedStartDateTime;
        }


        public List<DateTime> MergeEndDateAndTime()
        {
            List<DateTime> mergedEndDateTime = new List<DateTime>();

            int count = 0;

            while (count < endDate.Count)
            {
                mergedEndDateTime.Add(new DateTime(endDate[count].Year, endDate[count].Month, endDate[count].Day, endTime[count].Hours, endTime[count].Minutes, endTime[count].Seconds));
                count++;
            }

            return mergedEndDateTime;
        }

        public List<TimeSpan> CalculateTimeDifference()
        {
            List<TimeSpan> timeDifference = new List<TimeSpan>();

            List<DateTime> startDateTime = MergeStartDateAndTime();
            List<DateTime> endDateTime = MergeEndDateAndTime();

            int count = 0;

            while (count < MergeStartDateAndTime().Count)
            {
                timeDifference.Add(endDateTime[count].Subtract(startDateTime[count]));
                count++;
            }

            return timeDifference;
        }

        public double CalculateTotalMonthlyTimeUsage()
        {
            double totalTime = 0.0;
            string mergeHoursAndMinutes = "";
            List<TimeSpan> timeUsed = CalculateTimeDifference();

            int days = 0;
            int hours = 0;
            int minutes = 0;
            int count = 0;

            while (count < timeUsed.Count)
            {
                days = timeUsed[count].Days;
                hours += timeUsed[count].Hours + (days * 24);
                minutes += timeUsed[count].Minutes;

                count++;
            }

            while (minutes >= 60)
            {
                minutes -= 60;
                hours += 1;
            }

            if (minutes < 10)
            {
                mergeHoursAndMinutes = hours + ".0" + minutes;
            }
            else
            {
                mergeHoursAndMinutes = hours + "." + minutes;
            }

            totalTime = Convert.ToDouble(mergeHoursAndMinutes);
            
            return totalTime;
        }

        public List<double> CalculateIndividualCharges()
        {
            List<double> individualCharges = new List<double>();
            List<TimeSpan> timeUsed = CalculateTimeDifference();

            double days = 0;
            double hours = 0;
            double hoursToMinutes = 0;
            double standardMinutesToSeconds = 0;
            double over1MinutesToSeconds = 0;
            double over2MinutesToSeconds = 0;
            double minutes = 0;
            double standardMinutes = 0;
            double over1Minutes = 0;
            double over2Minutes = 0;
            double total = 0;
            int count = 0;

            while (count < timeUsed.Count)
            {
                days = timeUsed[count].Days;
                hours = timeUsed[count].Hours + (days * 24);
                hoursToMinutes = hours * 60.00;
                minutes = timeUsed[count].Minutes + hoursToMinutes;

                while (minutes >= overcharge2Minutes)
                {
                    over2Minutes++;
                    minutes--;
                }

                while (minutes >= overcharge1Minutes)
                {
                    over1Minutes++;
                    minutes--;
                }

                if (minutes <= 2)
                {
                    standardMinutes = 0;
                }
                else if (minutes > 2 && minutes <= 20)
                {
                    standardMinutes = 20;
                }
                else
                {
                    standardMinutes = minutes;
                }

                standardMinutesToSeconds = standardMinutes * 60.00;
                over1MinutesToSeconds = over1Minutes * 60.00;
                over2MinutesToSeconds = over2Minutes * 60.00;
                total = (standardMinutesToSeconds * standardSecondRate) + (over1MinutesToSeconds * overcharge1SecondRate) + (over2MinutesToSeconds * overcharge2SecondRate);

                // Use this if you decide to charge by the minute instead of the second (Results are the same unless you actually use the seconds from the logs)
                //total = (standardMinutes * standardMinuteRate) + (over1Minutes * overcharge1MinuteRate) + (over2Minutes * overcharge2MinuteRate);

                individualCharges.Add(total);

                // Something is being stored in one of these variables that shouldn't be. Resetting them all to 0 to ensure the next iteration is clean.
                days = 0;
                hours = 0;
                minutes = 0;
                hoursToMinutes = 0;
                standardMinutesToSeconds = 0;
                over1Minutes = 0;
                over2Minutes = 0;
                over1MinutesToSeconds = 0;
                over2MinutesToSeconds = 0;
                total = 0;

                count++;
            }

            return individualCharges;
        }

        public List<double> CalculateUserTotalCharge()
        {
            List<string> uniqueUsers = GetUniqueUsers();
            List<double> userCharges = CalculateIndividualCharges();
            List<double> totalUserCharge = new List<double>();

            int userCounter = 0;
            int chargeCounter = 0;
            double userTotal = 0.00;

            foreach(var uniqueUser in uniqueUsers)
            {
                foreach(var user in username)
                {
                    if(uniqueUser.Equals(user))
                    {
                        userTotal += userCharges[chargeCounter];
                    }

                    chargeCounter++;
                }

                totalUserCharge.Add(userTotal);

                userTotal = 0;
                chargeCounter = 0;
                userCounter++;
            }

            return totalUserCharge;
        }

        public double CalculateTotalMonthlyCharge()
        {
            double totalMonthlyCharge = 0.0;

            foreach(var charge in CalculateUserTotalCharge())
            {
                totalMonthlyCharge += charge;
            }

            return totalMonthlyCharge;
        }

        public List<string> GetUniqueGroups()
        {
            List<string> uniqueGroups = group.Distinct().ToList();

            return uniqueGroups;
        }

        public List<double> CalculateGroupTotalCharge()
        {
            List<string> individualGroups = GetUniqueGroups();
            List<double> userCharges = CalculateIndividualCharges();
            List<double> totalGroupCharge = new List<double>();

            int userCounter = 0;
            int chargeCounter = 0;
            double groupTotal = 0.00;

            foreach (var uniqueGroup in individualGroups)
            {
                foreach (var member in group)
                {
                    if (uniqueGroup.Equals(member))
                    {
                        groupTotal += userCharges[chargeCounter];
                    }

                    chargeCounter++;
                }

                totalGroupCharge.Add(groupTotal);

                groupTotal = 0;
                chargeCounter = 0;
                userCounter++;
            }

            return totalGroupCharge;
        }

        public List<TimeSpan> CalculateGroupTotalTime()
        {
            List<TimeSpan> timeUsed = CalculateTimeDifference();
            List<TimeSpan> totalGroupTime = new List<TimeSpan>();
            List<string> individualGroups = GetUniqueGroups();

            int userCounter = 0;
            int index = 0;
            TimeSpan groupTotal = new TimeSpan();

            foreach (var uniqueGroup in individualGroups)
            {
                foreach (var member in group)
                {
                    if (uniqueGroup.Equals(member))
                    {
                        groupTotal += timeUsed[index];
                    }

                    index++;
                }

                totalGroupTime.Add(groupTotal);

                groupTotal = new TimeSpan();
                index = 0;
                userCounter++;
            }

            return totalGroupTime;
        }

        public int CalculateYears()
        {
            int years = 0;
            int count = 0;

            while (count < startDate.Count())
            {
                if(startDate[count].Year < endDate[count].Year)
                {
                    years = endDate[count].Year - startDate[count].Year;
                }

                count++;
            }

            return years;
        }

        public int CalculateMonths()
        {
            int months = 0;
            int count = 0;

            while (count < startDate.Count())
            {
                if (startDate[count].Month < endDate[count].Month)
                {
                    months = endDate[count].Month - startDate[count].Month;
                }

                count++;
            }

            return months;
        }

        // Need to add in support for leap years, as well as add in the number of days in each month in case the days roll over
        public int CalculateDays()
        {
            int days = 0;
            int count = 0;

            while (count < startDate.Count())
            {
                if (startDate[count].Day < endDate[count].Day)
                {
                    days = endDate[count].Day - startDate[count].Day;
                }

                count++;
            }

            return days;
        }

        public int CalculateHours()
        {
            int hours = 0;
            int count = 0;

            while (count < startTime.Count())
            {
                if (startTime[count].Hours < endTime[count].Hours)
                {
                    hours = endDate[count].Day - startDate[count].Day;
                }

                count++;
            }

            return hours;
        }
    }
}
