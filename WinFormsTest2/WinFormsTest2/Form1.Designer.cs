namespace WinFormsTest2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.buttonExec = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxSeperator = new System.Windows.Forms.TextBox();
            this.textBoxNum1 = new System.Windows.Forms.TextBox();
            this.textBoxNum2 = new System.Windows.Forms.TextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(12, 19);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(312, 15);
            this.labelDesc.TabIndex = 0;
            this.labelDesc.Text = " Exec1 : A 부터 B 개의 숫자를 C 구분자로 숫자 출력하기";
            this.labelDesc.Click += new System.EventHandler(this.labelDesc_Click);
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(29, 49);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(15, 15);
            this.labelA.TabIndex = 7;
            this.labelA.Text = "A";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(29, 77);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(14, 15);
            this.labelB.TabIndex = 8;
            this.labelB.Text = "B";
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Location = new System.Drawing.Point(29, 106);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(15, 15);
            this.labelC.TabIndex = 9;
            this.labelC.Text = "C";
            // 
            // buttonExec
            // 
            this.buttonExec.Location = new System.Drawing.Point(330, 15);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(75, 23);
            this.buttonExec.TabIndex = 17;
            this.buttonExec.Text = "실행";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBoxOutput);
            this.panel1.Location = new System.Drawing.Point(146, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 78);
            this.panel1.TabIndex = 1;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.Color.White;
            this.textBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxOutput.Location = new System.Drawing.Point(4, 6);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(258, 68);
            this.textBoxOutput.TabIndex = 18;
            this.textBoxOutput.Text = "0";
            this.textBoxOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSeperator
            // 
            this.textBoxSeperator.Location = new System.Drawing.Point(59, 104);
            this.textBoxSeperator.Name = "textBoxSeperator";
            this.textBoxSeperator.Size = new System.Drawing.Size(72, 23);
            this.textBoxSeperator.TabIndex = 16;
            // 
            // textBoxNum1
            // 
            this.textBoxNum1.Location = new System.Drawing.Point(59, 49);
            this.textBoxNum1.Name = "textBoxNum1";
            this.textBoxNum1.Size = new System.Drawing.Size(72, 23);
            this.textBoxNum1.TabIndex = 14;
            // 
            // textBoxNum2
            // 
            this.textBoxNum2.Location = new System.Drawing.Point(59, 78);
            this.textBoxNum2.Name = "textBoxNum2";
            this.textBoxNum2.Size = new System.Drawing.Size(72, 23);
            this.textBoxNum2.TabIndex = 15;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(204, 162);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(80, 17);
            this.hScrollBar1.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(464, 287);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.textBoxNum2);
            this.Controls.Add(this.textBoxNum1);
            this.Controls.Add(this.labelC);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.textBoxSeperator);
            this.Controls.Add(this.buttonExec);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelDesc);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel panel1;
        private TextBox textBoxOutput;

        private Button button1;

        private TextBox txtTestBox;
        private Label label1;

        private Button button2;
        private Label labelDesc;
        
        private Button buttonExec;
        private Label labelA;
        private Label labelB;
        private Label labelC;
        private TextBox textBoxSeperator;
        private TextBox textBoxNum1;
        private TextBox textBoxNum2;
        private HScrollBar hScrollBar1;
    }
}