namespace NMRBillingApp
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.adminLoginPageButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.importBNMRLogButton = new System.Windows.Forms.Button();
            this.importedBNMRFilesListBox = new System.Windows.Forms.ListBox();
            this.importInfoLabel = new System.Windows.Forms.Label();
            this.generateMonthlyReportButton = new System.Windows.Forms.Button();
            this.startDateListBox = new System.Windows.Forms.ListBox();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.endDateListBox = new System.Windows.Forms.ListBox();
            this.startTimeLabel = new System.Windows.Forms.Label();
            this.startTimeListBox = new System.Windows.Forms.ListBox();
            this.endTimeLabel = new System.Windows.Forms.Label();
            this.endTimeListBox = new System.Windows.Forms.ListBox();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceNameListBox = new System.Windows.Forms.ListBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.userNameListBox = new System.Windows.Forms.ListBox();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.groupNameListBox = new System.Windows.Forms.ListBox();
            this.importVNMRLogButton = new System.Windows.Forms.Button();
            this.importedVNMRFilesListBox = new System.Windows.Forms.ListBox();
            this.generateGroupsByDepartmentReportButton = new System.Windows.Forms.Button();
            this.generateUsersByGroupReportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // adminLoginPageButton
            // 
            this.adminLoginPageButton.Location = new System.Drawing.Point(731, 485);
            this.adminLoginPageButton.Name = "adminLoginPageButton";
            this.adminLoginPageButton.Size = new System.Drawing.Size(75, 23);
            this.adminLoginPageButton.TabIndex = 0;
            this.adminLoginPageButton.Text = "Admin Login";
            this.adminLoginPageButton.UseVisualStyleBackColor = true;
            this.adminLoginPageButton.Click += new System.EventHandler(this.adminLoginPageButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(813, 485);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // importBNMRLogButton
            // 
            this.importBNMRLogButton.Location = new System.Drawing.Point(12, 29);
            this.importBNMRLogButton.Name = "importBNMRLogButton";
            this.importBNMRLogButton.Size = new System.Drawing.Size(107, 23);
            this.importBNMRLogButton.TabIndex = 3;
            this.importBNMRLogButton.Text = "Import BNMR Log";
            this.importBNMRLogButton.UseVisualStyleBackColor = true;
            this.importBNMRLogButton.Click += new System.EventHandler(this.importBNMRLogButton_Click);
            // 
            // importedBNMRFilesListBox
            // 
            this.importedBNMRFilesListBox.FormattingEnabled = true;
            this.importedBNMRFilesListBox.Location = new System.Drawing.Point(138, 29);
            this.importedBNMRFilesListBox.Name = "importedBNMRFilesListBox";
            this.importedBNMRFilesListBox.Size = new System.Drawing.Size(208, 69);
            this.importedBNMRFilesListBox.TabIndex = 4;
            // 
            // importInfoLabel
            // 
            this.importInfoLabel.AutoSize = true;
            this.importInfoLabel.Location = new System.Drawing.Point(13, 13);
            this.importInfoLabel.Name = "importInfoLabel";
            this.importInfoLabel.Size = new System.Drawing.Size(371, 13);
            this.importInfoLabel.TabIndex = 5;
            this.importInfoLabel.Text = "Import log files generated by each machine. Multiple log files can be imported.";
            // 
            // generateMonthlyReportButton
            // 
            this.generateMonthlyReportButton.Location = new System.Drawing.Point(138, 121);
            this.generateMonthlyReportButton.Name = "generateMonthlyReportButton";
            this.generateMonthlyReportButton.Size = new System.Drawing.Size(107, 23);
            this.generateMonthlyReportButton.TabIndex = 6;
            this.generateMonthlyReportButton.Text = "Monthly Summary";
            this.generateMonthlyReportButton.UseVisualStyleBackColor = true;
            this.generateMonthlyReportButton.Click += new System.EventHandler(this.generateMonthlyReportButton_Click);
            // 
            // startDateListBox
            // 
            this.startDateListBox.FormattingEnabled = true;
            this.startDateListBox.Location = new System.Drawing.Point(12, 176);
            this.startDateListBox.Name = "startDateListBox";
            this.startDateListBox.Size = new System.Drawing.Size(120, 303);
            this.startDateListBox.TabIndex = 7;
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(13, 160);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(58, 13);
            this.startDateLabel.TabIndex = 8;
            this.startDateLabel.Text = "Start Date:";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(138, 160);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(55, 13);
            this.endDateLabel.TabIndex = 10;
            this.endDateLabel.Text = "End Date:";
            // 
            // endDateListBox
            // 
            this.endDateListBox.FormattingEnabled = true;
            this.endDateListBox.Location = new System.Drawing.Point(138, 176);
            this.endDateListBox.Name = "endDateListBox";
            this.endDateListBox.Size = new System.Drawing.Size(120, 303);
            this.endDateListBox.TabIndex = 9;
            // 
            // startTimeLabel
            // 
            this.startTimeLabel.AutoSize = true;
            this.startTimeLabel.Location = new System.Drawing.Point(264, 160);
            this.startTimeLabel.Name = "startTimeLabel";
            this.startTimeLabel.Size = new System.Drawing.Size(58, 13);
            this.startTimeLabel.TabIndex = 12;
            this.startTimeLabel.Text = "Start Time:";
            // 
            // startTimeListBox
            // 
            this.startTimeListBox.FormattingEnabled = true;
            this.startTimeListBox.Location = new System.Drawing.Point(264, 176);
            this.startTimeListBox.Name = "startTimeListBox";
            this.startTimeListBox.Size = new System.Drawing.Size(120, 303);
            this.startTimeListBox.TabIndex = 11;
            // 
            // endTimeLabel
            // 
            this.endTimeLabel.AutoSize = true;
            this.endTimeLabel.Location = new System.Drawing.Point(390, 160);
            this.endTimeLabel.Name = "endTimeLabel";
            this.endTimeLabel.Size = new System.Drawing.Size(55, 13);
            this.endTimeLabel.TabIndex = 14;
            this.endTimeLabel.Text = "End Time:";
            // 
            // endTimeListBox
            // 
            this.endTimeListBox.FormattingEnabled = true;
            this.endTimeListBox.Location = new System.Drawing.Point(390, 176);
            this.endTimeListBox.Name = "endTimeListBox";
            this.endTimeListBox.Size = new System.Drawing.Size(120, 303);
            this.endTimeListBox.TabIndex = 13;
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(516, 160);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(59, 13);
            this.deviceNameLabel.TabIndex = 16;
            this.deviceNameLabel.Text = "Instrument:";
            // 
            // deviceNameListBox
            // 
            this.deviceNameListBox.FormattingEnabled = true;
            this.deviceNameListBox.Location = new System.Drawing.Point(516, 176);
            this.deviceNameListBox.Name = "deviceNameListBox";
            this.deviceNameListBox.Size = new System.Drawing.Size(120, 303);
            this.deviceNameListBox.TabIndex = 15;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(642, 160);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(32, 13);
            this.usernameLabel.TabIndex = 18;
            this.usernameLabel.Text = "User:";
            // 
            // userNameListBox
            // 
            this.userNameListBox.FormattingEnabled = true;
            this.userNameListBox.Location = new System.Drawing.Point(642, 176);
            this.userNameListBox.Name = "userNameListBox";
            this.userNameListBox.Size = new System.Drawing.Size(120, 303);
            this.userNameListBox.TabIndex = 17;
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(768, 160);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(39, 13);
            this.groupNameLabel.TabIndex = 20;
            this.groupNameLabel.Text = "Group:";
            // 
            // groupNameListBox
            // 
            this.groupNameListBox.FormattingEnabled = true;
            this.groupNameListBox.Location = new System.Drawing.Point(768, 176);
            this.groupNameListBox.Name = "groupNameListBox";
            this.groupNameListBox.Size = new System.Drawing.Size(120, 303);
            this.groupNameListBox.TabIndex = 19;
            // 
            // importVNMRLogButton
            // 
            this.importVNMRLogButton.Location = new System.Drawing.Point(390, 29);
            this.importVNMRLogButton.Name = "importVNMRLogButton";
            this.importVNMRLogButton.Size = new System.Drawing.Size(107, 23);
            this.importVNMRLogButton.TabIndex = 21;
            this.importVNMRLogButton.Text = "Import VNMR Log";
            this.importVNMRLogButton.UseVisualStyleBackColor = true;
            this.importVNMRLogButton.Click += new System.EventHandler(this.importVNMRLogButton_Click);
            // 
            // importedVNMRFilesListBox
            // 
            this.importedVNMRFilesListBox.FormattingEnabled = true;
            this.importedVNMRFilesListBox.Location = new System.Drawing.Point(516, 29);
            this.importedVNMRFilesListBox.Name = "importedVNMRFilesListBox";
            this.importedVNMRFilesListBox.Size = new System.Drawing.Size(208, 69);
            this.importedVNMRFilesListBox.TabIndex = 22;
            // 
            // generateGroupsByDepartmentReportButton
            // 
            this.generateGroupsByDepartmentReportButton.Location = new System.Drawing.Point(264, 121);
            this.generateGroupsByDepartmentReportButton.Name = "generateGroupsByDepartmentReportButton";
            this.generateGroupsByDepartmentReportButton.Size = new System.Drawing.Size(107, 23);
            this.generateGroupsByDepartmentReportButton.TabIndex = 23;
            this.generateGroupsByDepartmentReportButton.Text = "Groups By Dept.";
            this.generateGroupsByDepartmentReportButton.UseVisualStyleBackColor = true;
            this.generateGroupsByDepartmentReportButton.Click += new System.EventHandler(this.generateGroupsByDepartmentReportButton_Click);
            // 
            // generateUsersByGroupReportButton
            // 
            this.generateUsersByGroupReportButton.Location = new System.Drawing.Point(390, 121);
            this.generateUsersByGroupReportButton.Name = "generateUsersByGroupReportButton";
            this.generateUsersByGroupReportButton.Size = new System.Drawing.Size(107, 23);
            this.generateUsersByGroupReportButton.TabIndex = 24;
            this.generateUsersByGroupReportButton.Text = "Users By Group";
            this.generateUsersByGroupReportButton.UseVisualStyleBackColor = true;
            this.generateUsersByGroupReportButton.Click += new System.EventHandler(this.generateUsersByGroupReportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Generate Reports:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 515);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateUsersByGroupReportButton);
            this.Controls.Add(this.generateGroupsByDepartmentReportButton);
            this.Controls.Add(this.importedVNMRFilesListBox);
            this.Controls.Add(this.importVNMRLogButton);
            this.Controls.Add(this.groupNameLabel);
            this.Controls.Add(this.groupNameListBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.userNameListBox);
            this.Controls.Add(this.deviceNameLabel);
            this.Controls.Add(this.deviceNameListBox);
            this.Controls.Add(this.endTimeLabel);
            this.Controls.Add(this.endTimeListBox);
            this.Controls.Add(this.startTimeLabel);
            this.Controls.Add(this.startTimeListBox);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.endDateListBox);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.startDateListBox);
            this.Controls.Add(this.generateMonthlyReportButton);
            this.Controls.Add(this.importInfoLabel);
            this.Controls.Add(this.importedBNMRFilesListBox);
            this.Controls.Add(this.importBNMRLogButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.adminLoginPageButton);
            this.Name = "Main";
            this.Text = "NMR Billing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button adminLoginPageButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button importBNMRLogButton;
        private System.Windows.Forms.Label importInfoLabel;
        private System.Windows.Forms.ListBox importedBNMRFilesListBox;
        private System.Windows.Forms.Button generateMonthlyReportButton;
        private System.Windows.Forms.ListBox startDateListBox;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.ListBox endDateListBox;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.ListBox startTimeListBox;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.ListBox endTimeListBox;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.ListBox deviceNameListBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.ListBox userNameListBox;
        private System.Windows.Forms.Label groupNameLabel;
        private System.Windows.Forms.ListBox groupNameListBox;
        private System.Windows.Forms.Button importVNMRLogButton;
        private System.Windows.Forms.ListBox importedVNMRFilesListBox;
        private System.Windows.Forms.Button generateGroupsByDepartmentReportButton;
        private System.Windows.Forms.Button generateUsersByGroupReportButton;
        private System.Windows.Forms.Label label1;
    }
}

