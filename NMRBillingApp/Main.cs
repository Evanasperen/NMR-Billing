using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // Enable use of StreamReader and StreamWriter
using Logic;

namespace NMRBillingApp
{
    public partial class Main : Form
    {
        DateTime currentDateTime = DateTime.Now;

        List<DateTime> startDate = new List<DateTime>(); // List to store all the start dates parsed from the files
        List<DateTime> endDate = new List<DateTime>(); // List to store all the end dates parsed from the files

        List<TimeSpan> startTime = new List<TimeSpan>(); // List to store all the start times parsed from the files
        List<TimeSpan> endTime = new List<TimeSpan>(); // List to store all the end dates parsed from the files

        List<string> device = new List<string>(); // List to store all the device names parsed from the files
        List<string> username = new List<string>(); // List to store all the usernames parsed from the files
        List<string> group = new List<string>(); // List to store all the departments parsed from the files

        List<StreamReader> inputFile = new List<StreamReader>(); // List to store the imported files for use later

        bool alreadyImportedBNMRLog = false;
        bool alreadyImportedVNMRLog = false;

        public Main()
        {
            InitializeComponent();
        }

        // Create the AdminLogin page, display it, and hide the Main page
        private void adminLoginPageButton_Click(object sender, EventArgs e)
        {
            //AdminLogin adminLogin = new AdminLogin(startDate, endDate, startTime, endTime, device, username, department);

            //adminLogin.Show();

            //this.Hide();

            AdminForm adminForm = new AdminForm(startDate, endDate, startTime, endTime, device, username, group);

            adminForm.Show();

            this.Hide();
        }

        // Exit the application entirely
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Allow user to select a file they wish to import into the program
        private void importBNMRLogButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) // Creating a new instance of OpenFileDialog()
            {
                if (ofd.ShowDialog() == DialogResult.OK) // If the "Open" button is clicked, perform the following code
                {
                    importedBNMRFilesListBox.Items.Add(ofd.FileName); // Add the file path to importedFilesListBox for future use
                }
            }
        }
        
        private void importVNMRLogButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) // Creating a new instance of OpenFileDialog()
            {
                if (ofd.ShowDialog() == DialogResult.OK) // If the "Open" button is clicked, perform the following code
                {
                    importedVNMRFilesListBox.Items.Add(ofd.FileName); // Add the file path to importedFilesListBox for future use
                }
            }
        }

        private void generateMonthlyReportButton_Click(object sender, EventArgs e)
        {
            if (importedBNMRFilesListBox.Items.Count > 0 && alreadyImportedBNMRLog == false) // If there is a file path specified in importedBNMRFilesListBox, parse the logs
            {
                ImportBNMRLogs();

                alreadyImportedBNMRLog = true;
            }

            if (importedVNMRFilesListBox.Items.Count > 0 && alreadyImportedVNMRLog == false) // If there is a file path specified in importedVNMRFilesListBox, parse the logs
            {
                ImportVNMRLogs();

                alreadyImportedVNMRLog = true;
            }

            ClearListBoxes();
            BindLogDataToListBox();

            CreateMonthlyReportFile();

            // Close all open StreamReaders
            foreach (var item in inputFile)
            {
                item.Close();
            }
        }

        private void generateGroupsByDepartmentReportButton_Click(object sender, EventArgs e)
        {
            if (importedBNMRFilesListBox.Items.Count > 0 && alreadyImportedBNMRLog == false) // If there is a file path specified in importedBNMRFilesListBox, parse the logs
            {
                ImportBNMRLogs();

                alreadyImportedBNMRLog = true;
            }

            if (importedVNMRFilesListBox.Items.Count > 0 && alreadyImportedVNMRLog == false) // If there is a file path specified in importedVNMRFilesListBox, parse the logs
            {
                ImportVNMRLogs();

                alreadyImportedVNMRLog = true;
            }

            ClearListBoxes();
            BindLogDataToListBox();

            CreateGroupsByDepartmentReportFile();

            // Close all open StreamReaders
            foreach (var item in inputFile)
            {
                item.Close();
            }
        }

        private void generateUsersByGroupReportButton_Click(object sender, EventArgs e)
        {
            if (importedBNMRFilesListBox.Items.Count > 0 && alreadyImportedBNMRLog == false) // If there is a file path specified in importedBNMRFilesListBox, parse the logs
            {
                ImportBNMRLogs();

                alreadyImportedBNMRLog = true;
            }

            if (importedVNMRFilesListBox.Items.Count > 0 && alreadyImportedVNMRLog == false) // If there is a file path specified in importedVNMRFilesListBox, parse the logs
            {
                ImportVNMRLogs();

                alreadyImportedVNMRLog = true;
            }

            ClearListBoxes();
            BindLogDataToListBox();

            CreateUsersByGroupReportFile();

            // Close all open StreamReaders
            foreach (var item in inputFile)
            {
                item.Close();
            }
        }

        private void CreateMonthlyReportFile()
        {
            // The path to the output file, change this to get it working on your machine
            string outputFilePath = @"C:\billing";
            //Directory.CreateDirectory(outputFilePath); // DO NOT UNCOMMENT THIS LINE UNTIL FINAL RELEASE // Creates the folder at the file path specified in outputFilePath

            // Output a file with with a name structure of Monthly Report MM-DD-YYYY HH.MM.SS.txt
            string outputFileName = "Monthly Report" + " " + currentDateTime.Month + "-" + currentDateTime.Day + "-" + currentDateTime.Year +
                    " " + currentDateTime.Hour + "." + currentDateTime.Minute + "." + currentDateTime.Second + ".txt";

            //Use Combine again to add the file name to the path.
            outputFilePath = Path.Combine(outputFilePath, outputFileName);

            // Check that the file doesn't already exist.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!File.Exists(outputFilePath))
            {
                FileStream fs = File.Create(outputFilePath);

                fs.Close();

                MessageBox.Show("Report successfully generated and saved to " + outputFilePath);
            }
            else
            {
                MessageBox.Show("File " + outputFileName + " already exists.");
                return;
            }

            LogicClass logic = new LogicClass(startDate, endDate, startTime, endTime, device, username, group);

            List<string> input = new List<string>(); // Holds the strings that will be placed into the file

            using (StreamWriter monthlyReportFile = new StreamWriter(outputFilePath))
            {
                int year = 0;
                string month = getMonth();

                if (month == "December")
                {
                    year = currentDateTime.Year - 1;
                }
                else
                {
                    year = currentDateTime.Year;
                }

                input.Add("NDSU - Chemistry & Biochemistry NMR Facility");
                input.Add("");
                input.Add(month + " " + year + " Billing Summary");
                input.Add("");
                input.Add("");
                input.Add("\t\t" + "BNMR400" + "\t\t\t" + "VNMR400" + "\t\t\t" + "VNMR500");
                input.Add("Department:" + "\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Total");
                input.Add("-------------------------------------------------------------------------------------------------");

                List <string> departments = logic.GetUniqueMasterDepartments();

                int index = 0;

                foreach (var item in departments)
                {
                    if (item.Length <= 5)
                    {
                        input.Add(item + "\t\t" + logic.GetNMRTimesByDepartment("bnmr400")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("bnmr400")[index].ToString("C") + "\t\t" + logic.GetNMRTimesByDepartment("vnmr400")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("vnmr400")[index].ToString("C") + "\t\t" + logic.GetNMRTimesByDepartment("vnmr500")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("vnmr500")[index].ToString("C") + "\t\t" + logic.GetTotalDepartmentTimes()[index].ToString("F"));
                    }
                    else
                    {
                        input.Add(item + "\t" + logic.GetNMRTimesByDepartment("bnmr400")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("bnmr400")[index].ToString("C") + "\t\t" + logic.GetNMRTimesByDepartment("vnmr400")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("vnmr400")[index].ToString("C") + "\t\t" + logic.GetNMRTimesByDepartment("vnmr500")[index].ToString("F") + "\t" + logic.CalculateNMRChargeByDepartment("vnmr500")[index].ToString("C") + "\t\t" + logic.GetTotalDepartmentTimes()[index].ToString("F"));
                    }
                    index++;
                }

                input.Add("-------------------------------------------------------------------------------------------------");
                input.Add("Total:" + "\t\t" + logic.GetNMRTotalTimeByDepartment("bnmr400").ToString("F") + "\t" + logic.CalculateTotalNMRChargeByDepartment("bnmr400").ToString("C") + "\t\t" + logic.GetNMRTotalTimeByDepartment("vnmr400").ToString("F") + "\t" + logic.CalculateTotalNMRChargeByDepartment("vnmr400").ToString("C") + "\t\t" + logic.GetNMRTotalTimeByDepartment("vnmr500").ToString("F") + "\t" + logic.CalculateTotalNMRChargeByDepartment("vnmr500").ToString("C"));
                input.Add("");
                input.Add("");
                input.Add("Total Monthly Usage: " + logic.CalculateTotalMonthlyTimeUsage().ToString("F") + " Hours");
                input.Add("");
                input.Add("Total " + month + " Billing: " + logic.CalculateTotalMonthlyCharge().ToString("C"));
                input.Add("");
                input.Add("Total Fiscal Year To Date Billing: " + "*FYTDTOTAL*");

                foreach (var line in input)
                {
                    monthlyReportFile.WriteLine(line);
                }

                monthlyReportFile.Close();
            }
        }

        private void CreateGroupsByDepartmentReportFile()
        {
            // The path to the output file, change this to get it working on your machine
            string outputFilePath = @"C:\billing";
            //Directory.CreateDirectory(outputFilePath); // DO NOT UNCOMMENT THIS LINE UNTIL FINAL RELEASE // Creates the folder at the file path specified in outputFilePath

            // Output a file with with a name structure of Monthly Report MM-DD-YYYY HH.MM.SS.txt
            string outputFileName = "Groups By Department Report" + " " + currentDateTime.Month + "-" + currentDateTime.Day + "-" + currentDateTime.Year +
                    " " + currentDateTime.Hour + "." + currentDateTime.Minute + "." + currentDateTime.Second + ".txt";

            //Use Combine again to add the file name to the path.
            outputFilePath = Path.Combine(outputFilePath, outputFileName);

            // Check that the file doesn't already exist.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!File.Exists(outputFilePath))
            {
                FileStream fs = File.Create(outputFilePath);

                fs.Close();

                MessageBox.Show("Report successfully generated and saved to " + outputFilePath);
            }
            else
            {
                MessageBox.Show("File " + outputFileName + " already exists.");
                return;
            }

            LogicClass logic = new LogicClass(startDate, endDate, startTime, endTime, device, username, group);

            List<string> input = new List<string>(); // Holds the strings that will be placed into the file
            
            using (StreamWriter groupsByDepartmentReportFile = new StreamWriter(outputFilePath))
            {
                foreach (var dept in logic.GetUniqueMasterDepartments())
                {
                    int year = 0;
                    string month = getMonth();

                    if (month == "December")
                    {
                        year = currentDateTime.Year - 1;
                    }
                    else
                    {
                        year = currentDateTime.Year;
                    }

                    input.Add("NDSU - Chemistry & Biochemistry NMR Facility");
                    input.Add("");
                    input.Add(month + " " + year + " " + dept + " Department Billing");
                    input.Add("");
                    input.Add("");
                    input.Add("\t\t" + "BNMR400" + "\t\t\t" + "VNMR400" + "\t\t\t" + "VNMR500");
                    input.Add("Group:" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Total");
                    input.Add("-------------------------------------------------------------------------------------------------");

                    List<string> groupName = logic.GetUniqueGroups();
                    List<double> groupCharge = logic.CalculateGroupTotalCharge();
                    List<TimeSpan> groupTime = logic.CalculateGroupTotalTime();
                    //TimeSpan totalTime = new TimeSpan();

                    int index = 0;

                    foreach (var item in logic.GetUniqueGroupsByDepartment(dept))
                    {
                        if (item.Length >= 8)
                        {
                            input.Add(item + "\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TOTAL*");
                        }
                        else
                        {
                            input.Add(item + "\t\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "." + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TOTAL*");
                        }

                        index++;
                    }

                    index = 0;

                    input.Add("-------------------------------------------------------------------------------------------------");
                    input.Add("Total:" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*");

                    input.Add("\n\n\n\n");
                }

                foreach (var line in input)
                {
                    groupsByDepartmentReportFile.WriteLine(line);
                }

                groupsByDepartmentReportFile.Close();
            }
        }

        private void CreateUsersByGroupReportFile()
        {
            // The path to the output file, change this to get it working on your machine
            string outputFilePath = @"C:\billing";
            //Directory.CreateDirectory(outputFilePath); // DO NOT UNCOMMENT THIS LINE UNTIL FINAL RELEASE // Creates the folder at the file path specified in outputFilePath

            // Output a file with with a name structure of Monthly Report MM-DD-YYYY HH.MM.SS.txt
            string outputFileName = "Users By Group Report" + " " + currentDateTime.Month + "-" + currentDateTime.Day + "-" + currentDateTime.Year +
                    " " + currentDateTime.Hour + "." + currentDateTime.Minute + "." + currentDateTime.Second + ".txt";

            //Use Combine again to add the file name to the path.
            outputFilePath = Path.Combine(outputFilePath, outputFileName);

            // Check that the file doesn't already exist.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!File.Exists(outputFilePath))
            {
                FileStream fs = File.Create(outputFilePath);

                fs.Close();

                MessageBox.Show("Report successfully generated and saved to " + outputFilePath);
            }
            else
            {
                MessageBox.Show("File " + outputFileName + " already exists.");
                return;
            }

            LogicClass logic = new LogicClass(startDate, endDate, startTime, endTime, device, username, group);

            List<string> input = new List<string>(); // Holds the strings that will be placed into the file

            using (StreamWriter usersByGroupReportFile = new StreamWriter(outputFilePath))
            {
                int year = 0;
                string month = getMonth();

                if (month == "December")
                {
                    year = currentDateTime.Year - 1;
                }
                else
                {
                    year = currentDateTime.Year;
                }

                input.Add("NDSU - Chemistry & Biochemistry NMR Facility");
                input.Add("");
                input.Add(month + " " + year + " Billing for " + "*GROUPNAME*" + " - " + "*DEPARTMENTNAME*");
                input.Add("");
                input.Add("");
                input.Add("\t\t" + "BNMR400" + "\t\t\t" + "VNMR400" + "\t\t\t" + "VNMR500");
                input.Add("User:" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Time" + "\t" + "Fee" + "\t\t" + "Total");
                input.Add("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                List<string> fullName = new List<string>();
                fullName.Add("Kristine Konkol");
                fullName.Add("Trent Anderson");
                fullName.Add("Eric Uzelac");

                //int index = 0;

                foreach (var item in fullName)
                {
                    input.Add(item + "\t" + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TIME*" + "\t" + "*FEE*" + "\t\t" + "*TOTAL*");
                }

                input.Add("----------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                input.Add("Total:" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*" + "\t\t" + "*TTIME*" + "\t" + "*TFEE*");
                input.Add("");
                input.Add("");
                input.Add("Total " + month + "Billing: " + "*MTOTAL*");
                input.Add("");
                input.Add("Total Fiscal Year To Date Billing: " + "*FYTDTOTAL*");

                foreach (var line in input)
                {
                    usersByGroupReportFile.WriteLine(line);
                }

                usersByGroupReportFile.Close();
            }
        }

        private string getMonth()
        {
            int numberOfMonth = currentDateTime.Month;
            string month = "";

            switch (numberOfMonth)
            {
                case 1:
                    month = "December";
                    //month = "January";
                    break;
                case 2:
                    month = "January";
                    //month = "February";
                    break;
                case 3:
                    month = "February";
                    //month = "March";
                    break;
                case 4:
                    month = "March";
                    //month = "April";
                    break;
                case 5:
                    month = "April";
                    //month = "May";
                    break;
                case 6:
                    month = "May";
                    //month = "June";
                    break;
                case 7:
                    month = "June";
                    //month = "July";
                    break;
                case 8:
                    month = "July";
                    //month = "August";
                    break;
                case 9:
                    month = "August";
                    //month = "September";
                    break;
                case 10:
                    month = "September";
                    //month = "October";
                    break;
                case 11:
                    month = "October";
                    //month = "November";
                    break;
                case 12:
                    month = "November";
                    //month = "December";
                    break;
                default:
                    MessageBox.Show("Invalid month given during monthly report generation.");
                    break;
            }

            return month;
        }

        private void ClearListBoxes()
        {
            startDateListBox.Text = string.Empty;
            endDateListBox.Text = string.Empty;
            startTimeListBox.Text = string.Empty;
            endTimeListBox.Text = string.Empty;
            deviceNameListBox.Text = string.Empty;
            userNameListBox.Text = string.Empty;
            groupNameListBox.Text = string.Empty;
        }

        private void BindLogDataToListBox()
        {
            startDateListBox.DataSource = startDate; // Bind the contents of the startDate List to startDateListBox
            endDateListBox.DataSource = endDate; // Bind the contents of the endDate List to endDateListBox
            startTimeListBox.DataSource = startTime; // Bind the contents of the startTime List to startTimeListBox
            endTimeListBox.DataSource = endTime; // Bind the contents of the endTime List to endTimeListBox
            deviceNameListBox.DataSource = device; // Bind the contents of the device List to deviceNameListBox
            userNameListBox.DataSource = username; // Bind the contents of the username List to userNameListBox
            groupNameListBox.DataSource = group; // Bind the contents of the department List to departmentNameListBox
        }

        // Load and parse BNMR logs
        private void ImportBNMRLogs()
        {
            string line = ""; // Variable that will hold a single line parsed from an imported file

            char[] delimiterChar = { ' ' }; // Array of characters used to parse the file, currently using only a space character to split up the file, but can be changed to any character(s)

            try
            {
                foreach (var item in importedBNMRFilesListBox.Items) // Iterate through the file paths stored in importedFilesListBox
                {
                    inputFile.Add(new StreamReader(item.ToString())); // Add file to inputFile and open it for editing 
                }
            }
            catch (Exception exception) // If a file isn't found, throw this error
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exception.Message);
            }

            int counter = 0; // ***Counter that will be used in the following switch statement. IF THE NUMBER IF ITEMS PER LINE THAT NEED TO BE PARSED CHANGES, THE SWITCH STATEMENTS WILL NEED TO BE MODIFIED***

            foreach (var item in inputFile) // Iterate through each StreamReader object
            {
                while ((line = item.ReadLine()) != null) // Perform until there are no more lines in the file to read
                {
                    string[] parsedLine = line.Split(delimiterChar); // Create an array to store each parsed item. Items will be parsed based on the delimiter character(s) stored in delimiterChar

                    while (counter < parsedLine.Count())
                    {
                        if(parsedLine.Count() != 7)
                        {
                            MessageBox.Show("An invalid file was placed in the BNMR file selection. Please verify the proper file type and try again.");
                            importedBNMRFilesListBox.Items.Clear();
                            return;
                        }
                        else
                        {
                            switch (counter) // ***IF THE NUMBER IF ITEMS PER LINE THAT NEED TO BE PARSED CHANGES, THIS SWITCH STATEMENTS WILL NEED TO BE MODIFIED***
                            {
                                case 0: // If counter == 0
                                    startDate.Add(Convert.ToDateTime(parsedLine[counter])); // Add the first date to the startDate List
                                    break;
                                case 1: // If counter == 1
                                    endDate.Add(Convert.ToDateTime(parsedLine[counter])); // Add the second date to the endDate List
                                    break;
                                case 2: // If counter == 2
                                    startTime.Add(TimeSpan.Parse(parsedLine[counter])); // Add the first time to the startTime List
                                    break;
                                case 3: // If counter == 3
                                    endTime.Add(TimeSpan.Parse(parsedLine[counter])); // Add the second time to the endTime List
                                    break;
                                case 4: // If counter == 4
                                    device.Add(parsedLine[counter]); // Add the device name to the device List
                                    break;
                                case 5: // If counter == 5
                                    username.Add(parsedLine[counter]); // Add the username to the username List
                                    break;
                                case 6: // If counter == 6
                                    group.Add(parsedLine[counter]); // Add the department name to the department List
                                    break;
                                default: // If none of the above is true, perform this code
                                         // Enter default code
                                    break;
                            }
                            counter++; // Incrememnt counter every time a word is parsed from the line
                        }
                    }
                    counter = 0; // Reset the counter when the line has been completely parsed
                }
            }
        }

        // Load and parse VNMR logs
        private void ImportVNMRLogs()
        {
            string line = ""; // Variable that will hold a single line parsed from an imported file

            char[] delimiterChar = { ' ' }; // Array of characters used to parse the file, currently using only a space character to split up the file, but can be changed to any character(s)
            try
            {
                foreach (var item in importedVNMRFilesListBox.Items) // Iterate through the file paths stored in importedFilesListBox
                {
                    inputFile.Add(new StreamReader(item.ToString())); // Add file to inputFile and open it for editing 
                }
            }
            catch (Exception exception) // If a file isn't found, throw this error
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exception.Message);
            }

            int counter = 0; // ***Counter that will be used in the following switch statement. IF THE NUMBER IF ITEMS PER LINE THAT NEED TO BE PARSED CHANGES, THE SWITCH STATEMENTS WILL NEED TO BE MODIFIED***
            int lineCycle = 1; // This counter is used to determine which line we need to be on. Since we only need to parse 2 lines (Start line and end line) this value only needs to be 1 or 2

            foreach (var item in inputFile) // Iterate through each StreamReader object
            {
                while ((line = item.ReadLine()) != null) // Perform until there are no more lines in the file to read
                {
                    string[] parsedLine = line.Split(delimiterChar); // Create an array to store each parsed item. Items will be parsed based on the delimiter character(s) stored in delimiterChar

                    while (counter < parsedLine.Count())
                    {
                        if (parsedLine.Count() != 10)
                        {
                            MessageBox.Show("An invalid file was placed in the BNMR file selection. Please verify the proper file type and try again.");
                            importedBNMRFilesListBox.Items.Clear();
                            return;
                        }
                        else
                        {
                            switch (lineCycle) // Determines whether we are on the start line, or the end line and parses accordingly
                            {
                                case 1:
                                    switch (counter) // ***IF THE NUMBER IF ITEMS PER LINE THAT NEED TO BE PARSED CHANGES, THIS SWITCH STATEMENTS WILL NEED TO BE MODIFIED***
                                    {
                                        case 3: // If counter == 3
                                            device.Add(parsedLine[counter]); // Add the device name to the device List
                                            break;
                                        case 5: // If counter == 2
                                            startTime.Add(TimeSpan.Parse(parsedLine[counter])); // Add the first time to the startTime List
                                            break;
                                        case 6: // If counter == 6
                                            startDate.Add(Convert.ToDateTime(parsedLine[counter])); // Add the first date to the startDate List
                                            break;
                                        case 8: // If counter == 8
                                            username.Add(parsedLine[counter]); // Add the username to the username List
                                            break;
                                        case 9: // If counter == 9
                                            group.Add(parsedLine[counter]); // Add the department name to the department List
                                            lineCycle = 2;
                                            break;
                                        default: // If none of the above is true, perform this code
                                                 // Enter default code
                                            break;
                                    }
                                    counter++; // Incrememnt counter every time a word is parsed from the line
                                    break;

                                case 2:
                                    switch (counter) // ***IF THE NUMBER IF ITEMS PER LINE THAT NEED TO BE PARSED CHANGES, THIS SWITCH STATEMENTS WILL NEED TO BE MODIFIED***
                                    {
                                        case 5: // If counter == 2
                                            endTime.Add(TimeSpan.Parse(parsedLine[counter])); // Add the second time to the endTime List
                                            break;
                                        case 6: // If counter == 6
                                            endDate.Add(Convert.ToDateTime(parsedLine[counter])); // Add the second date to the endDate List
                                            break;
                                        case 8: // If counter == 8 (IE. This is required to prevent duplicates of the username from showing up and allow the "first" line to be parsed again)
                                            lineCycle = 1;
                                            break;
                                        default: // If none of the above is true, perform this code
                                                 // Enter default code
                                            break;
                                    }
                                    counter++; // Incrememnt counter every time a word is parsed from the line
                                    break;

                                default: // If none of the above is true, perform this code
                                         // Enter default code
                                    break;
                            }
                        }
                    }
                    counter = 0; // Reset the counter when the line has been completely parsed
                }
            }
        }
    }
}
