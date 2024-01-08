namespace bds.win.Forms
{
    partial class ListRetreiver
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
            this.listFromDatatable1 = new bds.win.Controls.ListFromDatatable();
            this.getDatasetBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listFromDatatable1
            // 
            this.listFromDatatable1.dataTable = null;
            this.listFromDatatable1.Location = new System.Drawing.Point(12, 41);
            this.listFromDatatable1.Name = "listFromDatatable1";
            this.listFromDatatable1.Size = new System.Drawing.Size(511, 256);
            this.listFromDatatable1.TabIndex = 0;
            // 
            // getDatasetBtn
            // 
            this.getDatasetBtn.Location = new System.Drawing.Point(12, 12);
            this.getDatasetBtn.Name = "getDatasetBtn";
            this.getDatasetBtn.Size = new System.Drawing.Size(75, 23);
            this.getDatasetBtn.TabIndex = 1;
            this.getDatasetBtn.Text = "Get Dataset";
            this.getDatasetBtn.UseVisualStyleBackColor = true;
            this.getDatasetBtn.Click += new System.EventHandler(this.getDatasetBtn_Click);
            // 
            // ListRetreiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 309);
            this.Controls.Add(this.getDatasetBtn);
            this.Controls.Add(this.listFromDatatable1);
            this.Name = "ListRetreiver";
            this.Text = "ListRetreiver";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ListFromDatatable listFromDatatable1;
        private System.Windows.Forms.Button getDatasetBtn;
    }
}