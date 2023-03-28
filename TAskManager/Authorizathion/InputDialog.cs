using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Authorizathion
{
    public partial class InputDialog : Form
    {
        public bool Flag = false;
        private Label label1;
        private Button button1;
        private Button button2;
        private Panel panel2;
        private Panel panel9;
        private Panel panel6;
        private Panel panel4;
        private Panel panel1;
        private Panel panel3;
        private Panel panel5;
        private Panel panel7;
        private Panel panel8;
        private Panel panel10;
        private TextBox textBox1;

        public string InputText { get; private set; }
        public bool IsOkClicked { get; private set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            InputText = textBox1.Text;
            if (InputText == "ILDHALADE")
            {
                IsOkClicked = true;
                Flag = true;
            }
            else 
            {
                Flag = false;
                IsOkClicked = false;
                MessageBox.Show("Код введен неправильно, возможно вы неверно указали почту");
            }
            
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            InputText = "";
            IsOkClicked = false;
            Close();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(158, 100);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 26);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Image = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.label1.Location = new System.Drawing.Point(129, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Укажите код отправленный вам на почту";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(363, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ок";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.okButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(133, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(15, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 5);
            this.panel2.TabIndex = 21;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel9.Location = new System.Drawing.Point(574, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(5, 567);
            this.panel9.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(569, 6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 573);
            this.panel6.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 295);
            this.panel4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Location = new System.Drawing.Point(15, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 5);
            this.panel1.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(574, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 567);
            this.panel3.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(569, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 573);
            this.panel5.TabIndex = 9;
            // 
            // panel7
            // 
            this.panel7.Location = new System.Drawing.Point(0, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(10, 295);
            this.panel7.TabIndex = 8;
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel8.Location = new System.Drawing.Point(15, 15);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(5, 195);
            this.panel8.TabIndex = 10;
            // 
            // panel10
            // 
            this.panel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel10.Location = new System.Drawing.Point(560, 17);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(5, 195);
            this.panel10.TabIndex = 23;
            // 
            // InputDialog
            // 
            this.BackgroundImage = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.ClientSize = new System.Drawing.Size(578, 222);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "InputDialog";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}
