namespace allping
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.importIPtextBox1 = new System.Windows.Forms.TextBox();
            this.importIP = new System.Windows.Forms.Button();
            this.Allping = new System.Windows.Forms.Button();
            this.AllpingtextBox1 = new System.Windows.Forms.TextBox();
            this.Pinglog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.save = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // importIPtextBox1
            // 
            this.importIPtextBox1.Location = new System.Drawing.Point(10, 20);
            this.importIPtextBox1.Multiline = true;
            this.importIPtextBox1.Name = "importIPtextBox1";
            this.importIPtextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.importIPtextBox1.Size = new System.Drawing.Size(397, 179);
            this.importIPtextBox1.TabIndex = 0;
            // 
            // importIP
            // 
            this.importIP.Location = new System.Drawing.Point(33, 206);
            this.importIP.Name = "importIP";
            this.importIP.Size = new System.Drawing.Size(75, 23);
            this.importIP.TabIndex = 1;
            this.importIP.Text = "导入IP";
            this.importIP.UseVisualStyleBackColor = true;
            this.importIP.Click += new System.EventHandler(this.importIP_Click);
            // 
            // Allping
            // 
            this.Allping.Location = new System.Drawing.Point(178, 207);
            this.Allping.Name = "Allping";
            this.Allping.Size = new System.Drawing.Size(75, 23);
            this.Allping.TabIndex = 2;
            this.Allping.Text = "批量IP";
            this.Allping.UseVisualStyleBackColor = true;
            this.Allping.Click += new System.EventHandler(this.Allping_Click);
            // 
            // AllpingtextBox1
            // 
            this.AllpingtextBox1.Location = new System.Drawing.Point(5, 20);
            this.AllpingtextBox1.Multiline = true;
            this.AllpingtextBox1.Name = "AllpingtextBox1";
            this.AllpingtextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllpingtextBox1.Size = new System.Drawing.Size(397, 155);
            this.AllpingtextBox1.TabIndex = 3;
            // 
            // Pinglog
            // 
            this.Pinglog.Location = new System.Drawing.Point(9, 20);
            this.Pinglog.Multiline = true;
            this.Pinglog.Name = "Pinglog";
            this.Pinglog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Pinglog.Size = new System.Drawing.Size(402, 129);
            this.Pinglog.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Pinglog);
            this.groupBox1.Location = new System.Drawing.Point(-2, 425);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 162);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前ping日志：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.importIPtextBox1);
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 205);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入IP";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(304, 207);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 8;
            this.save.Text = "保存结果";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AllpingtextBox1);
            this.groupBox3.Location = new System.Drawing.Point(7, 235);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(408, 190);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 581);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Allping);
            this.Controls.Add(this.importIP);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "批量ping检测工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox importIPtextBox1;
        private System.Windows.Forms.Button importIP;
        private System.Windows.Forms.Button Allping;
        private System.Windows.Forms.TextBox AllpingtextBox1;
        private System.Windows.Forms.TextBox Pinglog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

