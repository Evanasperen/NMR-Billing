using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Logic;

namespace NMRBillingApp
{
    public partial class AdminForm : Form
    {
        List<DateTime> startDate = new List<DateTime>(); // List to store all the start dates parsed from the files
        List<DateTime> endDate = new List<DateTime>(); // List to store all the end dates parsed from the files

        List<TimeSpan> startTime = new List<TimeSpan>(); // List to store all the start times parsed from the files
        List<TimeSpan> endTime = new List<TimeSpan>(); // List to store all the end dates parsed from the files

        List<string> device = new List<string>(); // List to store all the device names parsed from the files
        List<string> username = new List<string>(); // List to store all the usernames parsed from the files
        List<string> group = new List<string>(); // List to store all the departments parsed from the files

        public AdminForm()
        {
            InitializeComponent();
        }

        public AdminForm(List<DateTime> startD, List<DateTime> endD, List<TimeSpan> startT, List<TimeSpan> endT, List<string> deviceName, List<string> user, List<string> groupName)
        {
            InitializeComponent();

            startDate = startD;
            endDate = endD;
            startTime = startT;
            endTime = endT;
            device = deviceName;
            username = user;
            group = groupName;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LogicClass logic = new LogicClass(startDate, endDate, startTime, endTime, device, username, group);

            List<List<string>> usersByGroup = logic.GetUsersByGroup();

            listBox1.DataSource = logic.CalculateTimeDifference();
            listBox2.DataSource = logic.CalculateIndividualCharges();
            listBox3.DataSource = logic.GetUniqueUsers();
            listBox4.DataSource = logic.CalculateUserTotalCharge();
            listBox5.DataSource = logic.GetUniqueGroups();
            listBox6.DataSource = logic.CalculateGroupTotalCharge();

            //listBox7.DataSource = usersByGroup[0];
            //listBox8.DataSource = usersByGroup[1];
            //listBox9.DataSource = usersByGroup[2];
            //listBox10.DataSource = usersByGroup[3];
            //listBox11.DataSource = usersByGroup[4];
            //listBox12.DataSource = usersByGroup[5];

            //listBox13.DataSource = usersByGroup[0];
            //listBox14.DataSource = usersByGroup[6];
            //listBox15.DataSource = usersByGroup[7];
            //listBox16.DataSource = usersByGroup[8];
            //listBox17.DataSource = usersByGroup[9];
            //listBox18.DataSource = usersByGroup[10];
            
            listBox13.DataSource = logic.GetMasterUsers();
            listBox14.DataSource = logic.GetMasterFullNames();
            listBox15.DataSource = logic.GetMasterGroups();
            listBox16.DataSource = logic.GetMasterDepartments();
            listBox17.DataSource = logic.GetUserDepartments();
            listBox18.DataSource = logic.GetTotalDepartmentTimes();
        }

        private void SetConfigData()
        {
            LogicClass logic = new LogicClass(startDate, endDate, startTime, endTime, device, username, group);

            List<string> input = new List<string>();
            
            // Clear the contents of the file before entering fresh data to avoid possible duplicates
            File.WriteAllText(@"C:\Users\Owner\Source\NMR-Billing\NMRBillingApp\NMRBillingApp\Program Data\Config.txt", string.Empty);

            using (StreamWriter configFile = new StreamWriter(@"C:\Users\Owner\Source\NMR-Billing\NMRBillingApp\NMRBillingApp\Program Data\Config.txt"))
            {
                if(standardHourlyRateTextBox.Text == "" && overcharge1HourlyRateTextBox.Text == "" && overcharge2HourlyRateTextBox.Text == "" && noChargeMinutesTextBox.Text == "" && 
                    standardChargeMinutesTextBox.Text == "" && overcharge1MinutesTextBox.Text == "" && overcharge2MinutesTextBox.Text == "")
                {
                    MessageBox.Show("No valid information. Configuration file unchanged.");
                }
                else
                {
                    if (standardHourlyRateTextBox.Text != "")
                    {
                        input.Add("STANDARDCHARGE=" + standardHourlyRateTextBox.Text);
                    }
                    else
                    {
                        input.Add("STANDARDCHARGE=" + logic.GetStandardHourlyRate());
                    }

                    if (overcharge1HourlyRateTextBox.Text != "")
                    {
                        input.Add("OVERCHARGE1=" + overcharge1HourlyRateTextBox.Text);
                    }
                    else
                    {
                        input.Add("OVERCHARGE1=" + logic.GetOvercharge1HourlyRate());
                    }

                    if (overcharge2HourlyRateTextBox.Text != "")
                    {
                        input.Add("OVERCHARGE2=" + overcharge2HourlyRateTextBox.Text);
                    }
                    else
                    {
                        input.Add("OVERCHARGE2=" + logic.GetOvercharge2HourlyRate());
                    }

                    if (noChargeMinutesTextBox.Text != "")
                    {
                        input.Add("NOCHARGEMINUTES=" + noChargeMinutesTextBox.Text);
                    }
                    else
                    {
                        input.Add("NOCHARGEMINUTES=" + logic.GetNoChargeMinutes());
                    }

                    if (standardChargeMinutesTextBox.Text != "")
                    {
                        input.Add("STANDARDMINUTES=" + standardChargeMinutesTextBox.Text);
                    }
                    else
                    {
                        input.Add("STANDARDMINUTES=" + logic.GetStandardChargeMinutes());
                    }

                    if (overcharge1MinutesTextBox.Text != "")
                    {
                        input.Add("OVERMINUTES1=" + overcharge1MinutesTextBox.Text);
                    }
                    else
                    {
                        input.Add("OVERMINUTES1=" + logic.GetOvercharge1Minutes());
                    }

                    if (overcharge2MinutesTextBox.Text != "")
                    {
                        input.Add("OVERMINUTES2=" + overcharge2MinutesTextBox.Text);
                    }
                    else
                    {
                        input.Add("OVERMINUTES2=" + logic.GetOvercharge2Minutes());
                    }

                    foreach (var line in input)
                    {
                        configFile.WriteLine(line);
                    }

                    MessageBox.Show("Configuration file successfully updated. Changes will become active immediately.");
                }
                configFile.Close();
            }
                
            standardHourlyRateTextBox.Text = "";
            overcharge1HourlyRateTextBox.Text = "";
            overcharge2HourlyRateTextBox.Text = "";
            noChargeMinutesTextBox.Text = "";
            standardChargeMinutesTextBox.Text = "";
            overcharge1MinutesTextBox.Text = "";
            overcharge2MinutesTextBox.Text = "";
        }

        private void abandonChangesButton_Click(object sender, EventArgs e)
        {
            closeButton.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();

            Main main = new Main();

            main.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SetConfigData();
        }
    }
}
