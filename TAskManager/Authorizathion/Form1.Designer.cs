namespace Authorizathion
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.HideBox2 = new System.Windows.Forms.PictureBox();
            this.ViewBox1 = new System.Windows.Forms.PictureBox();
            this.registrbutton = new System.Windows.Forms.Button();
            this.entrancebutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.passField = new System.Windows.Forms.TextBox();
            this.loginField = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HideBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BackgroundImage = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.HideBox2);
            this.panel1.Controls.Add(this.ViewBox1);
            this.panel1.Controls.Add(this.registrbutton);
            this.panel1.Controls.Add(this.entrancebutton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.passField);
            this.panel1.Controls.Add(this.loginField);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 604);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // HideBox2
            // 
            this.HideBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HideBox2.BackgroundImage = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.HideBox2.Image = global::Authorizathion.Properties.Resources.hide;
            this.HideBox2.Location = new System.Drawing.Point(464, 317);
            this.HideBox2.Name = "HideBox2";
            this.HideBox2.Size = new System.Drawing.Size(45, 44);
            this.HideBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HideBox2.TabIndex = 5;
            this.HideBox2.TabStop = false;
            this.HideBox2.Click += new System.EventHandler(this.HideBox2_Click);
            // 
            // ViewBox1
            // 
            this.ViewBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ViewBox1.BackgroundImage = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.ViewBox1.Image = global::Authorizathion.Properties.Resources.view;
            this.ViewBox1.Location = new System.Drawing.Point(465, 317);
            this.ViewBox1.Name = "ViewBox1";
            this.ViewBox1.Size = new System.Drawing.Size(45, 44);
            this.ViewBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ViewBox1.TabIndex = 4;
            this.ViewBox1.TabStop = false;
            this.ViewBox1.Click += new System.EventHandler(this.ViewBox1_Click);
            // 
            // registrbutton
            // 
            this.registrbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.registrbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.registrbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registrbutton.Location = new System.Drawing.Point(196, 493);
            this.registrbutton.Name = "registrbutton";
            this.registrbutton.Size = new System.Drawing.Size(210, 56);
            this.registrbutton.TabIndex = 1;
            this.registrbutton.Text = "Зарегистрироваться";
            this.registrbutton.UseVisualStyleBackColor = true;
            this.registrbutton.Click += new System.EventHandler(this.registrbutton_Click);
            // 
            // entrancebutton
            // 
            this.entrancebutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.entrancebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.entrancebutton.Location = new System.Drawing.Point(196, 417);
            this.entrancebutton.Name = "entrancebutton";
            this.entrancebutton.Size = new System.Drawing.Size(210, 56);
            this.entrancebutton.TabIndex = 0;
            this.entrancebutton.Text = "Вход";
            this.entrancebutton.UseVisualStyleBackColor = true;
            this.entrancebutton.Click += new System.EventHandler(this.entrancebutton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Image = global::Authorizathion.Properties.Resources._1648513457_2_gamerwall_pro_p_belii_kholodnii_fon_krasivie_2;
            this.label1.Location = new System.Drawing.Point(132, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 72);
            this.label1.TabIndex = 3;
            this.label1.Text = "Авторизация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // passField
            // 
            this.passField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passField.Location = new System.Drawing.Point(153, 317);
            this.passField.Multiline = true;
            this.passField.Name = "passField";
            this.passField.Size = new System.Drawing.Size(305, 44);
            this.passField.TabIndex = 1;
            this.passField.Enter += new System.EventHandler(this.passField_Enter);
            this.passField.Leave += new System.EventHandler(this.passField_Leave);
            // 
            // loginField
            // 
            this.loginField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginField.Location = new System.Drawing.Point(153, 209);
            this.loginField.Multiline = true;
            this.loginField.Name = "loginField";
            this.loginField.Size = new System.Drawing.Size(305, 44);
            this.loginField.TabIndex = 0;
            this.loginField.Enter += new System.EventHandler(this.loginField_Enter);
            this.loginField.Leave += new System.EventHandler(this.loginField_Leave);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(13, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(579, 5);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(13, 582);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(579, 5);
            this.panel3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 295);
            this.panel4.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Location = new System.Drawing.Point(13, 15);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 570);
            this.panel5.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(569, 6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 573);
            this.panel6.TabIndex = 9;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel9.Location = new System.Drawing.Point(574, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(5, 567);
            this.panel9.TabIndex = 9;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Location = new System.Drawing.Point(590, 12);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(5, 575);
            this.panel7.TabIndex = 10;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel8.Location = new System.Drawing.Point(574, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(5, 567);
            this.panel8.TabIndex = 9;
            // 
            // panel10
            // 
            this.panel10.Location = new System.Drawing.Point(569, 6);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(10, 573);
            this.panel10.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 604);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HideBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button entrancebutton;
        private System.Windows.Forms.Button registrbutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox passField;
        private System.Windows.Forms.TextBox loginField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox HideBox2;
        private System.Windows.Forms.PictureBox ViewBox1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
    }
}

