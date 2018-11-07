namespace TestApp
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ShowDrivesRemovable = new System.Windows.Forms.CheckBox();
            this.ShowDrivesCDRom = new System.Windows.Forms.CheckBox();
            this.ShowDrivesNetwork = new System.Windows.Forms.CheckBox();
            this.ShowDrivesFixed = new System.Windows.Forms.CheckBox();
            this.DriveListSelection = new System.Windows.Forms.Label();
            this.driveComboBox1 = new Manina.Windows.Forms.DriveComboBox();
            this.driveListBox1 = new Manina.Windows.Forms.DriveListBox();
            this.fileSystemButton1 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemButton2 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemButton3 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemLabel1 = new Manina.Windows.Forms.FileSystemLabel();
            this.fileSystemLabel2 = new Manina.Windows.Forms.FileSystemLabel();
            this.fileSystemLabel6 = new Manina.Windows.Forms.FileSystemLabel();
            this.DriveBoxSelection = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fileSystemLabel1);
            this.groupBox1.Controls.Add(this.fileSystemLabel2);
            this.groupBox1.Controls.Add(this.fileSystemLabel6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 299);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FileSystemLabel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fileSystemButton1);
            this.groupBox2.Controls.Add(this.fileSystemButton2);
            this.groupBox2.Controls.Add(this.fileSystemButton3);
            this.groupBox2.Location = new System.Drawing.Point(12, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 299);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FileSystemButton";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DriveBoxSelection);
            this.groupBox3.Controls.Add(this.DriveListSelection);
            this.groupBox3.Controls.Add(this.driveComboBox1);
            this.groupBox3.Controls.Add(this.ShowDrivesRemovable);
            this.groupBox3.Controls.Add(this.ShowDrivesCDRom);
            this.groupBox3.Controls.Add(this.ShowDrivesNetwork);
            this.groupBox3.Controls.Add(this.ShowDrivesFixed);
            this.groupBox3.Controls.Add(this.driveListBox1);
            this.groupBox3.Location = new System.Drawing.Point(361, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 462);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DriveListBox, DriveComboBox";
            // 
            // ShowDrivesRemovable
            // 
            this.ShowDrivesRemovable.AutoSize = true;
            this.ShowDrivesRemovable.Checked = true;
            this.ShowDrivesRemovable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDrivesRemovable.Location = new System.Drawing.Point(15, 430);
            this.ShowDrivesRemovable.Name = "ShowDrivesRemovable";
            this.ShowDrivesRemovable.Size = new System.Drawing.Size(80, 17);
            this.ShowDrivesRemovable.TabIndex = 4;
            this.ShowDrivesRemovable.Text = "Removable";
            this.ShowDrivesRemovable.UseVisualStyleBackColor = true;
            this.ShowDrivesRemovable.CheckedChanged += new System.EventHandler(this.ShowDrivesRemovable_CheckedChanged);
            // 
            // ShowDrivesCDRom
            // 
            this.ShowDrivesCDRom.AutoSize = true;
            this.ShowDrivesCDRom.Checked = true;
            this.ShowDrivesCDRom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDrivesCDRom.Location = new System.Drawing.Point(137, 430);
            this.ShowDrivesCDRom.Name = "ShowDrivesCDRom";
            this.ShowDrivesCDRom.Size = new System.Drawing.Size(66, 17);
            this.ShowDrivesCDRom.TabIndex = 4;
            this.ShowDrivesCDRom.Text = "CD Rom";
            this.ShowDrivesCDRom.UseVisualStyleBackColor = true;
            this.ShowDrivesCDRom.CheckedChanged += new System.EventHandler(this.ShowDrivesCDRom_CheckedChanged);
            // 
            // ShowDrivesNetwork
            // 
            this.ShowDrivesNetwork.AutoSize = true;
            this.ShowDrivesNetwork.Checked = true;
            this.ShowDrivesNetwork.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDrivesNetwork.Location = new System.Drawing.Point(137, 407);
            this.ShowDrivesNetwork.Name = "ShowDrivesNetwork";
            this.ShowDrivesNetwork.Size = new System.Drawing.Size(66, 17);
            this.ShowDrivesNetwork.TabIndex = 4;
            this.ShowDrivesNetwork.Text = "Network";
            this.ShowDrivesNetwork.UseVisualStyleBackColor = true;
            this.ShowDrivesNetwork.CheckedChanged += new System.EventHandler(this.ShowDrivesNetwork_CheckedChanged);
            // 
            // ShowDrivesFixed
            // 
            this.ShowDrivesFixed.AutoSize = true;
            this.ShowDrivesFixed.Checked = true;
            this.ShowDrivesFixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDrivesFixed.Location = new System.Drawing.Point(15, 407);
            this.ShowDrivesFixed.Name = "ShowDrivesFixed";
            this.ShowDrivesFixed.Size = new System.Drawing.Size(51, 17);
            this.ShowDrivesFixed.TabIndex = 4;
            this.ShowDrivesFixed.Text = "Fixed";
            this.ShowDrivesFixed.UseVisualStyleBackColor = true;
            this.ShowDrivesFixed.CheckedChanged += new System.EventHandler(this.ShowDrivesFixed_CheckedChanged);
            // 
            // DriveListSelection
            // 
            this.DriveListSelection.AutoSize = true;
            this.DriveListSelection.Location = new System.Drawing.Point(15, 286);
            this.DriveListSelection.Name = "DriveListSelection";
            this.DriveListSelection.Size = new System.Drawing.Size(127, 13);
            this.DriveListSelection.TabIndex = 6;
            this.DriveListSelection.Text = "Select drives from the list.";
            // 
            // driveComboBox1
            // 
            this.driveComboBox1.ErrorTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.driveComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.driveComboBox1.ItemHeight = 52;
            this.driveComboBox1.Location = new System.Drawing.Point(15, 305);
            this.driveComboBox1.Name = "driveComboBox1";
            this.driveComboBox1.Size = new System.Drawing.Size(305, 58);
            this.driveComboBox1.TabIndex = 5;
            this.driveComboBox1.SelectedIndexChanged += new System.EventHandler(this.driveComboBox1_SelectedIndexChanged);
            // 
            // driveListBox1
            // 
            this.driveListBox1.ErrorTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.driveListBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.driveListBox1.FormattingEnabled = true;
            this.driveListBox1.IntegralHeight = false;
            this.driveListBox1.ItemHeight = 36;
            this.driveListBox1.Location = new System.Drawing.Point(15, 28);
            this.driveListBox1.Name = "driveListBox1";
            this.driveListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.driveListBox1.Size = new System.Drawing.Size(305, 251);
            this.driveListBox1.TabIndex = 3;
            this.driveListBox1.SelectedIndexChanged += new System.EventHandler(this.driveListBox1_SelectedIndexChanged);
            // 
            // fileSystemButton1
            // 
            this.fileSystemButton1.Location = new System.Drawing.Point(18, 28);
            this.fileSystemButton1.Name = "fileSystemButton1";
            this.fileSystemButton1.Path = "c:\\";
            this.fileSystemButton1.Size = new System.Drawing.Size(305, 80);
            this.fileSystemButton1.TabIndex = 1;
            // 
            // fileSystemButton2
            // 
            this.fileSystemButton2.Location = new System.Drawing.Point(18, 114);
            this.fileSystemButton2.Name = "fileSystemButton2";
            this.fileSystemButton2.Path = "c:\\windows";
            this.fileSystemButton2.Size = new System.Drawing.Size(305, 80);
            this.fileSystemButton2.TabIndex = 1;
            // 
            // fileSystemButton3
            // 
            this.fileSystemButton3.Location = new System.Drawing.Point(18, 200);
            this.fileSystemButton3.Name = "fileSystemButton3";
            this.fileSystemButton3.Path = "c:\\windows\\explorer.exe";
            this.fileSystemButton3.Size = new System.Drawing.Size(305, 80);
            this.fileSystemButton3.TabIndex = 1;
            // 
            // fileSystemLabel1
            // 
            this.fileSystemLabel1.Location = new System.Drawing.Point(15, 28);
            this.fileSystemLabel1.Name = "fileSystemLabel1";
            this.fileSystemLabel1.Path = "c:\\";
            this.fileSystemLabel1.Size = new System.Drawing.Size(305, 68);
            this.fileSystemLabel1.TabIndex = 0;
            // 
            // fileSystemLabel2
            // 
            this.fileSystemLabel2.Location = new System.Drawing.Point(15, 114);
            this.fileSystemLabel2.Name = "fileSystemLabel2";
            this.fileSystemLabel2.Path = "c:\\windows";
            this.fileSystemLabel2.Size = new System.Drawing.Size(305, 68);
            this.fileSystemLabel2.TabIndex = 0;
            // 
            // fileSystemLabel6
            // 
            this.fileSystemLabel6.Location = new System.Drawing.Point(15, 200);
            this.fileSystemLabel6.Name = "fileSystemLabel6";
            this.fileSystemLabel6.Path = "c:\\windows\\explorer.exe";
            this.fileSystemLabel6.Size = new System.Drawing.Size(305, 68);
            this.fileSystemLabel6.TabIndex = 0;
            // 
            // DriveBoxSelection
            // 
            this.DriveBoxSelection.AutoSize = true;
            this.DriveBoxSelection.Location = new System.Drawing.Point(15, 375);
            this.DriveBoxSelection.Name = "DriveBoxSelection";
            this.DriveBoxSelection.Size = new System.Drawing.Size(131, 13);
            this.DriveBoxSelection.TabIndex = 6;
            this.DriveBoxSelection.Text = "Select a drive from the list.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 634);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Test Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Manina.Windows.Forms.FileSystemLabel fileSystemLabel1;
        private Manina.Windows.Forms.FileSystemLabel fileSystemLabel2;
        private Manina.Windows.Forms.FileSystemLabel fileSystemLabel6;
        private Manina.Windows.Forms.FileSystemButton fileSystemButton1;
        private Manina.Windows.Forms.FileSystemButton fileSystemButton2;
        private Manina.Windows.Forms.FileSystemButton fileSystemButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Manina.Windows.Forms.DriveListBox driveListBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ShowDrivesRemovable;
        private System.Windows.Forms.CheckBox ShowDrivesFixed;
        private System.Windows.Forms.CheckBox ShowDrivesCDRom;
        private System.Windows.Forms.CheckBox ShowDrivesNetwork;
        private Manina.Windows.Forms.DriveComboBox driveComboBox1;
        private System.Windows.Forms.Label DriveListSelection;
        private System.Windows.Forms.Label DriveBoxSelection;
    }
}

