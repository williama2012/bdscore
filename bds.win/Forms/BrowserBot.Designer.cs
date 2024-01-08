namespace bds.win.Forms {
    partial class BrowserBotForm1 {
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
        private void InitializeComponent()
        {
            this.homeBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.showHideBtn = new System.Windows.Forms.Button();
            this.runBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.getListBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // homeBtn
            // 
            this.homeBtn.Location = new System.Drawing.Point(12, 12);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(75, 23);
            this.homeBtn.TabIndex = 0;
            this.homeBtn.Text = "Open Home";
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 20);
            this.textBox1.TabIndex = 2;
            // 
            // showHideBtn
            // 
            this.showHideBtn.Location = new System.Drawing.Point(12, 41);
            this.showHideBtn.Name = "showHideBtn";
            this.showHideBtn.Size = new System.Drawing.Size(75, 23);
            this.showHideBtn.TabIndex = 3;
            this.showHideBtn.Text = "Show";
            this.showHideBtn.UseVisualStyleBackColor = true;
            this.showHideBtn.Click += new System.EventHandler(this.showHideBtn_Click);
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(12, 93);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 4;
            this.runBtn.Text = "Open";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.Run_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(423, 106);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(43, 23);
            this.clearBtn.TabIndex = 5;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 135);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(456, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Parameter string";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "List Empty";
            // 
            // getListBtn
            // 
            this.getListBtn.Location = new System.Drawing.Point(281, 106);
            this.getListBtn.Name = "getListBtn";
            this.getListBtn.Size = new System.Drawing.Size(75, 23);
            this.getListBtn.TabIndex = 12;
            this.getListBtn.Text = "Get List";
            this.getListBtn.UseVisualStyleBackColor = true;
            this.getListBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // BrowserBotForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 168);
            this.Controls.Add(this.getListBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.showHideBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.homeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BrowserBotForm1";
            this.Text = "Browser Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button showHideBtn;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button getListBtn;
        
    }
}

