using System;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowDrivesFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                driveListBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Fixed;
            else
                driveListBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Fixed;

            if (((CheckBox)sender).Checked)
                driveComboBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Fixed;
            else
                driveComboBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Fixed;
        }

        private void ShowDrivesRemovable_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                driveListBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Removable;
            else
                driveListBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Removable;

            if (((CheckBox)sender).Checked)
                driveComboBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Removable;
            else
                driveComboBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Removable;
        }

        private void ShowDrivesNetwork_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                driveListBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Network;
            else
                driveListBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Network;

            if (((CheckBox)sender).Checked)
                driveComboBox1.DriveTypes |= Manina.Windows.Forms.DriveType.Network;
            else
                driveComboBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.Network;
        }

        private void ShowDrivesCDRom_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
                driveListBox1.DriveTypes |= Manina.Windows.Forms.DriveType.CDRom;
            else
                driveListBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.CDRom;

            if (((CheckBox)sender).Checked)
                driveComboBox1.DriveTypes |= Manina.Windows.Forms.DriveType.CDRom;
            else
                driveComboBox1.DriveTypes &= ~Manina.Windows.Forms.DriveType.CDRom;
        }

        private void driveListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (driveListBox1.SelectedDrives.Length == 0)
                DriveListSelection.Text = "Select drives from the list.";
            else
                DriveListSelection.Text = string.Join(", ", driveListBox1.SelectedDrives);
        }

        private void driveComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(driveComboBox1.SelectedDrive))
                DriveBoxSelection.Text = "Select a drive from the list.";
            else
                DriveBoxSelection.Text = driveComboBox1.SelectedDrive;
        }
    }
}
