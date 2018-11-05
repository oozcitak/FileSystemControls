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
            this.fileSystemButton1 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemButton2 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemButton3 = new Manina.Windows.Forms.FileSystemButton();
            this.fileSystemLabel1 = new Manina.Windows.Forms.FileSystemLabel();
            this.fileSystemLabel2 = new Manina.Windows.Forms.FileSystemLabel();
            this.fileSystemLabel6 = new Manina.Windows.Forms.FileSystemLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Location = new System.Drawing.Point(376, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 299);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FileSystemButton";
            // 
            // fileSystemButton1
            // 
            this.fileSystemButton1.Location = new System.Drawing.Point(18, 28);
            this.fileSystemButton1.Name = "fileSystemButton1";
            this.fileSystemButton1.Path = "c:\\";
            this.fileSystemButton1.Size = new System.Drawing.Size(305, 82);
            this.fileSystemButton1.TabIndex = 1;
            // 
            // fileSystemButton2
            // 
            this.fileSystemButton2.Location = new System.Drawing.Point(18, 114);
            this.fileSystemButton2.Name = "fileSystemButton2";
            this.fileSystemButton2.Path = "c:\\windows";
            this.fileSystemButton2.Size = new System.Drawing.Size(305, 82);
            this.fileSystemButton2.TabIndex = 1;
            // 
            // fileSystemButton3
            // 
            this.fileSystemButton3.Location = new System.Drawing.Point(18, 200);
            this.fileSystemButton3.Name = "fileSystemButton3";
            this.fileSystemButton3.Path = "c:\\windows\\explorer.exe";
            this.fileSystemButton3.Size = new System.Drawing.Size(305, 82);
            this.fileSystemButton3.TabIndex = 1;
            // 
            // fileSystemLabel1
            // 
            this.fileSystemLabel1.Location = new System.Drawing.Point(15, 28);
            this.fileSystemLabel1.Name = "fileSystemLabel1";
            this.fileSystemLabel1.Path = "c:\\";
            this.fileSystemLabel1.Size = new System.Drawing.Size(305, 64);
            this.fileSystemLabel1.TabIndex = 0;
            // 
            // fileSystemLabel2
            // 
            this.fileSystemLabel2.Location = new System.Drawing.Point(15, 114);
            this.fileSystemLabel2.Name = "fileSystemLabel2";
            this.fileSystemLabel2.Path = "c:\\windows";
            this.fileSystemLabel2.Size = new System.Drawing.Size(305, 64);
            this.fileSystemLabel2.TabIndex = 0;
            // 
            // fileSystemLabel6
            // 
            this.fileSystemLabel6.Location = new System.Drawing.Point(15, 200);
            this.fileSystemLabel6.Name = "fileSystemLabel6";
            this.fileSystemLabel6.Path = "c:\\windows\\explorer.exe";
            this.fileSystemLabel6.Size = new System.Drawing.Size(305, 64);
            this.fileSystemLabel6.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 576);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Test Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
    }
}

