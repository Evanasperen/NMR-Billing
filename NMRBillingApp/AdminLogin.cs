using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMRBillingApp
{
    public partial class AdminLogin : Form
    {
        List<DateTime> startDate = new List<DateTime>(); // List to store all the start dates parsed from the files
        List<DateTime> endDate = new List<DateTime>(); // List to store all the end dates parsed from the files

        List<TimeSpan> startTime = new List<TimeSpan>(); // List to store all the start times parsed from the files
        List<TimeSpan> endTime = new List<TimeSpan>(); // List to store all the end dates parsed from the files

        List<string> device = new List<string>(); // List to store all the device names parsed from the files
        List<string> username = new List<string>(); // List to store all the usernames parsed from the files
        List<string> department = new List<string>(); // List to store all the departments parsed from the files

        public AdminLogin()
        {
            InitializeComponent();
        }

        public AdminLogin(List<DateTime> startD, List<DateTime> endD, List<TimeSpan> startT, List<TimeSpan> endT, List<string> deviceName, List<string> user, List<string> departmentName)
        {
            InitializeComponent();

            startDate = startD;
            endDate = endD;
            startTime = startT;
            endTime = endT;
            device = deviceName;
            username = user;
            department = departmentName;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Main form1 = new Main();
            form1.Show();

            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if(usernameTextBox.Text == "admin" && passwordTextBox.Text == "admin")
            {
                AdminForm adminForm = new AdminForm(startDate, endDate, startTime, endTime, device, username, department);

                adminForm.Show();

                this.Close();
            }
            else if(usernameTextBox.Text == "admin" && passwordTextBox.Text != "admin")
            {
                validationFailureLabel.Text = "Invalid password, please try again.";
            }
            else
            {
                validationFailureLabel.Text = "Invalid login credentials, please try again.";
            }
        }
    }
}
