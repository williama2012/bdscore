namespace SqlSmoSearcher {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbDbList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblObjectsInDb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDbList
            // 
            this.cbDbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbList.Enabled = false;
            this.cbDbList.FormattingEnabled = true;
            this.cbDbList.Location = new System.Drawing.Point(71, 39);
            this.cbDbList.Name = "cbDbList";
            this.cbDbList.Size = new System.Drawing.Size(166, 21);
            this.cbDbList.TabIndex = 0;
            this.cbDbList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database:";
            // 
            // tbKeyword
            // 
            this.tbKeyword.Enabled = false;
            this.tbKeyword.Location = new System.Drawing.Point(71, 66);
            this.tbKeyword.Name = "tbKeyword";
            this.tbKeyword.Size = new System.Drawing.Size(169, 20);
            this.tbKeyword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Keyword:";
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(243, 64);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 113);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(574, 228);
            this.dataGridView1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server:";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(71, 10);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(166, 20);
            this.tbServer.TabIndex = 8;
            this.tbServer.Text = "(local)";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(243, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblServer
            // 
            this.lblServer.Location = new System.Drawing.Point(357, 13);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(200, 13);
            this.lblServer.TabIndex = 10;
            this.lblServer.Text = "Server: Not Connected";
            // 
            // lblDatabase
            // 
            this.lblDatabase.Location = new System.Drawing.Point(341, 28);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(200, 13);
            this.lblDatabase.TabIndex = 11;
            this.lblDatabase.Text = "Database: Not Connected";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(27, 93);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 17);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Status: ";
            // 
            // lblObjectsInDb
            // 
            this.lblObjectsInDb.Location = new System.Drawing.Point(323, 43);
            this.lblObjectsInDb.Name = "lblObjectsInDb";
            this.lblObjectsInDb.Size = new System.Drawing.Size(200, 13);
            this.lblObjectsInDb.TabIndex = 6;
            this.lblObjectsInDb.Text = "Objects In Db: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 353);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblObjectsInDb);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbKeyword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDbList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "SQL Structure Search";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDbList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblObjectsInDb;

    }
}

