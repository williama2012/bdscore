namespace bds.win.Forms
{
    partial class DatasetRetreiver
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
            this.serverTxt = new System.Windows.Forms.TextBox();
            this.databaseTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.executeBtn = new System.Windows.Forms.Button();
            this.queryTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serverTxt
            // 
            this.serverTxt.Location = new System.Drawing.Point(63, 12);
            this.serverTxt.Name = "serverTxt";
            this.serverTxt.Size = new System.Drawing.Size(100, 20);
            this.serverTxt.TabIndex = 0;
            // 
            // databaseTxt
            // 
            this.databaseTxt.Location = new System.Drawing.Point(63, 37);
            this.databaseTxt.Name = "databaseTxt";
            this.databaseTxt.Size = new System.Drawing.Size(100, 20);
            this.databaseTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // executeBtn
            // 
            this.executeBtn.Location = new System.Drawing.Point(169, 10);
            this.executeBtn.Name = "executeBtn";
            this.executeBtn.Size = new System.Drawing.Size(57, 23);
            this.executeBtn.TabIndex = 4;
            this.executeBtn.Text = "Execute";
            this.executeBtn.UseVisualStyleBackColor = true;
            this.executeBtn.Click += new System.EventHandler(this.executeBtn_Click);
            // 
            // queryTxt
            // 
            this.queryTxt.Location = new System.Drawing.Point(7, 63);
            this.queryTxt.Multiline = true;
            this.queryTxt.Name = "queryTxt";
            this.queryTxt.Size = new System.Drawing.Size(219, 187);
            this.queryTxt.TabIndex = 5;
            // 
            // DatasetRetreiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 259);
            this.Controls.Add(this.queryTxt);
            this.Controls.Add(this.executeBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.databaseTxt);
            this.Controls.Add(this.serverTxt);
            this.Name = "DatasetRetreiver";
            this.Text = "DatasetRetreiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverTxt;
        private System.Windows.Forms.TextBox databaseTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button executeBtn;
        private System.Windows.Forms.TextBox queryTxt;
    }
}