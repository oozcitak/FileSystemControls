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
    }
}
