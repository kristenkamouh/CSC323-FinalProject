namespace CSC323FinalProject
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
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label2 = new Label();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            label3 = new Label();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.ForeColor = SystemColors.ActiveCaption;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(26, 9);
            label1.Name = "label1";
            label1.Size = new Size(184, 22);
            label1.TabIndex = 0;
            label1.Text = "Gym Management System";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(26, 57);
            button1.Name = "button1";
            button1.Size = new Size(134, 29);
            button1.TabIndex = 1;
            button1.Text = "Add Member";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(26, 92);
            button2.Name = "button2";
            button2.Size = new Size(134, 29);
            button2.TabIndex = 2;
            button2.Text = "Remove Member";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(26, 196);
            button3.Name = "button3";
            button3.Size = new Size(134, 29);
            button3.TabIndex = 3;
            button3.Text = "Add Coach";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(26, 231);
            button4.Name = "button4";
            button4.Size = new Size(134, 29);
            button4.TabIndex = 4;
            button4.Text = "Remove Coach";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(26, 292);
            button5.Name = "button5";
            button5.Size = new Size(134, 29);
            button5.TabIndex = 5;
            button5.Text = "Add Dietician";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(26, 327);
            button6.Name = "button6";
            button6.Size = new Size(134, 29);
            button6.TabIndex = 6;
            button6.Text = "Remove Dietician";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(294, 57);
            button7.Name = "button7";
            button7.Size = new Size(215, 29);
            button7.TabIndex = 7;
            button7.Text = "Book Training Session";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(294, 11);
            label2.Name = "label2";
            label2.Size = new Size(176, 20);
            label2.TabIndex = 8;
            label2.Text = "Premium Member Access";
            label2.Click += label2_Click;
            // 
            // button8
            // 
            button8.Location = new Point(294, 92);
            button8.Name = "button8";
            button8.Size = new Size(215, 29);
            button8.TabIndex = 9;
            button8.Text = "Cancel Training Session";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(294, 149);
            button9.Name = "button9";
            button9.Size = new Size(215, 29);
            button9.TabIndex = 10;
            button9.Text = "Book Dietician Consultation";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Location = new Point(294, 184);
            button10.Name = "button10";
            button10.Size = new Size(215, 29);
            button10.TabIndex = 11;
            button10.Text = "Cancel Dietician Consultation";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(591, 11);
            label3.Name = "label3";
            label3.Size = new Size(168, 20);
            label3.TabIndex = 12;
            label3.Text = "Regular Member Access";
            label3.Click += label3_Click;
            // 
            // button11
            // 
            button11.Location = new Point(591, 57);
            button11.Name = "button11";
            button11.Size = new Size(168, 29);
            button11.TabIndex = 13;
            button11.Text = "Pay Additional Fees";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.Location = new Point(591, 92);
            button12.Name = "button12";
            button12.Size = new Size(168, 29);
            button12.TabIndex = 14;
            button12.Text = "Premium Upgrade";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button13
            // 
            button13.Location = new Point(26, 127);
            button13.Name = "button13";
            button13.Size = new Size(134, 29);
            button13.TabIndex = 15;
            button13.Text = "Display Members";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(label3);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(label2);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label2;
        private Button button8;
        private Button button9;
        private Button button10;
        private Label label3;
        private Button button11;
        private Button button12;
        private Button button13;
    }
}
