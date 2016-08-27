namespace FirebirdTool
{
    partial class frmFirebirdTool
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
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtStrQuery = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOnline = new System.Windows.Forms.Button();
            this.btnExtractData = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btntran = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkGateway = new System.Windows.Forms.CheckBox();
            this.chkBackPurge = new System.Windows.Forms.CheckBox();
            this.chkDeleteQuery = new System.Windows.Forms.CheckBox();
            this.btncsvExport = new System.Windows.Forms.Button();
            this.btnPurgeData = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.btnBackupFolder = new System.Windows.Forms.Button();
            this.cmbBatch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(85, 40);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(366, 22);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "C:\\SonicPro\\SystemCache\\SYSPRODLL.CHP";
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.Location = new System.Drawing.Point(3, 43);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(62, 14);
            this.lblFilename.TabIndex = 33;
            this.lblFilename.Text = "Select File";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 305);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(845, 231);
            this.dataGridView1.TabIndex = 35;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(6, 268);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStatus.Size = new System.Drawing.Size(59, 23);
            this.lblStatus.TabIndex = 36;
            this.lblStatus.Text = "label1";
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.Location = new System.Drawing.Point(85, 12);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(366, 22);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.Text = "localhost";
            // 
            // txtStrQuery
            // 
            this.txtStrQuery.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStrQuery.Location = new System.Drawing.Point(86, 123);
            this.txtStrQuery.Multiline = true;
            this.txtStrQuery.Name = "txtStrQuery";
            this.txtStrQuery.Size = new System.Drawing.Size(768, 136);
            this.txtStrQuery.TabIndex = 2;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(6, 123);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(65, 55);
            this.button5.TabIndex = 3;
            this.button5.Text = "Run Query";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 47;
            this.label1.Text = "Server Name";
            // 
            // btnOnline
            // 
            this.btnOnline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnline.Location = new System.Drawing.Point(6, 184);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(65, 55);
            this.btnOnline.TabIndex = 48;
            this.btnOnline.Text = "Run Online Query";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // btnExtractData
            // 
            this.btnExtractData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExtractData.Location = new System.Drawing.Point(509, 7);
            this.btnExtractData.Name = "btnExtractData";
            this.btnExtractData.Size = new System.Drawing.Size(65, 55);
            this.btnExtractData.TabIndex = 49;
            this.btnExtractData.Text = "Extract Data";
            this.btnExtractData.UseVisualStyleBackColor = true;
            this.btnExtractData.Click += new System.EventHandler(this.btnExtractData_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(580, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 55);
            this.btnClear.TabIndex = 50;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btntran
            // 
            this.btntran.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntran.Location = new System.Drawing.Point(649, 7);
            this.btntran.Name = "btntran";
            this.btntran.Size = new System.Drawing.Size(65, 55);
            this.btntran.TabIndex = 51;
            this.btntran.Text = "Tran";
            this.btntran.UseVisualStyleBackColor = true;
            this.btntran.Click += new System.EventHandler(this.btntran_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkGateway);
            this.groupBox1.Controls.Add(this.chkBackPurge);
            this.groupBox1.Controls.Add(this.chkDeleteQuery);
            this.groupBox1.Location = new System.Drawing.Point(6, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 40);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extract Data";
            // 
            // chkGateway
            // 
            this.chkGateway.AutoSize = true;
            this.chkGateway.Location = new System.Drawing.Point(318, 16);
            this.chkGateway.Name = "chkGateway";
            this.chkGateway.Size = new System.Drawing.Size(68, 17);
            this.chkGateway.TabIndex = 2;
            this.chkGateway.Text = "Gateway";
            this.chkGateway.UseVisualStyleBackColor = true;
            // 
            // chkBackPurge
            // 
            this.chkBackPurge.AutoSize = true;
            this.chkBackPurge.Location = new System.Drawing.Point(146, 17);
            this.chkBackPurge.Name = "chkBackPurge";
            this.chkBackPurge.Size = new System.Drawing.Size(157, 17);
            this.chkBackPurge.TabIndex = 1;
            this.chkBackPurge.Text = "Take backup on Purgedata";
            this.chkBackPurge.UseVisualStyleBackColor = true;
            // 
            // chkDeleteQuery
            // 
            this.chkDeleteQuery.AutoSize = true;
            this.chkDeleteQuery.Location = new System.Drawing.Point(14, 20);
            this.chkDeleteQuery.Name = "chkDeleteQuery";
            this.chkDeleteQuery.Size = new System.Drawing.Size(124, 17);
            this.chkDeleteQuery.TabIndex = 0;
            this.chkDeleteQuery.Text = "Include Delete query";
            this.chkDeleteQuery.UseVisualStyleBackColor = true;
            // 
            // btncsvExport
            // 
            this.btncsvExport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncsvExport.Location = new System.Drawing.Point(720, 7);
            this.btncsvExport.Name = "btncsvExport";
            this.btncsvExport.Size = new System.Drawing.Size(65, 55);
            this.btncsvExport.TabIndex = 53;
            this.btncsvExport.Text = "Export CSV";
            this.btncsvExport.UseVisualStyleBackColor = true;
            this.btncsvExport.Click += new System.EventHandler(this.btncsvExport_Click);
            // 
            // btnPurgeData
            // 
            this.btnPurgeData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurgeData.Location = new System.Drawing.Point(789, 7);
            this.btnPurgeData.Name = "btnPurgeData";
            this.btnPurgeData.Size = new System.Drawing.Size(65, 55);
            this.btnPurgeData.TabIndex = 54;
            this.btnPurgeData.Text = "Load Batch";
            this.btnPurgeData.UseVisualStyleBackColor = true;
            this.btnPurgeData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(703, 74);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(149, 20);
            this.dateTimePicker1.TabIndex = 55;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(703, 97);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(149, 20);
            this.dateTimePicker2.TabIndex = 56;
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupFolder.Location = new System.Drawing.Point(509, 66);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(109, 22);
            this.txtBackupFolder.TabIndex = 57;
            // 
            // btnBackupFolder
            // 
            this.btnBackupFolder.Location = new System.Drawing.Point(616, 87);
            this.btnBackupFolder.Name = "btnBackupFolder";
            this.btnBackupFolder.Size = new System.Drawing.Size(81, 23);
            this.btnBackupFolder.TabIndex = 58;
            this.btnBackupFolder.Text = "Close Batch";
            this.btnBackupFolder.UseVisualStyleBackColor = true;
            this.btnBackupFolder.Click += new System.EventHandler(this.btnBackupFolder_Click);
            // 
            // cmbBatch
            // 
            this.cmbBatch.FormattingEnabled = true;
            this.cmbBatch.Location = new System.Drawing.Point(509, 93);
            this.cmbBatch.Name = "cmbBatch";
            this.cmbBatch.Size = new System.Drawing.Size(101, 21);
            this.cmbBatch.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(441, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 14);
            this.label2.TabIndex = 60;
            this.label2.Text = "Register";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(441, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 61;
            this.label3.Text = "Batch";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(616, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 62;
            this.button2.Text = "Export";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnPurgeData_Click);
            // 
            // frmFirebirdTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 548);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBatch);
            this.Controls.Add(this.btnBackupFolder);
            this.Controls.Add(this.txtBackupFolder);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnPurgeData);
            this.Controls.Add(this.btncsvExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btntran);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExtractData);
            this.Controls.Add(this.btnOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtStrQuery);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.button1);
            this.Name = "frmFirebirdTool";
            this.Text = "Database Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtStrQuery;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Button btnExtractData;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btntran;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkDeleteQuery;
        private System.Windows.Forms.Button btncsvExport;
        private System.Windows.Forms.Button btnPurgeData;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.CheckBox chkBackPurge;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.Button btnBackupFolder;
        private System.Windows.Forms.ComboBox cmbBatch;
        private System.Windows.Forms.CheckBox chkGateway;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
    }
}

