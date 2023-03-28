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
    public partial class FormRegistr : Form
    {
        private bool exReg;
        private void FormRegistr_Load(object sender, EventArgs e)
        {
            loginReg.Text = "Введите Логин";
            loginReg.ForeColor = Color.Gray;
            passReg.Text = "Введите Пароль";
            passReg.ForeColor = Color.Gray;
            emailREg.Text = "Введите Email";
            emailREg.ForeColor = Color.Gray;
            NameReg.Text = "Имя";
            NameReg.ForeColor = Color.Gray;
            passReg.PasswordChar= '*';
            pictureBox1.Visible = false;
            LastNameReg.Text = "Фамилия";
            LastNameReg.ForeColor= Color.Gray;
            unicIDREg.Text = "Уникальный код!";
            unicIDREg.ForeColor = Color.Gray;
            SurnameReg.Text = "Отчество(если имеется)";
            SurnameReg.ForeColor = Color.Gray;

            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);
        }
        public FormRegistr()
        {
            InitializeComponent();
        }

        public FormRegistr(bool bb)
        {
            InitializeComponent();
            this.exReg= bb;
        }

        private void buttonRegistr_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            if (NameReg.Text == "Имя" || emailREg.Text == "Введите Email" || loginReg.Text == "Введите Логин" || passReg.Text == "Введите пароль" || LastNameReg.Text == "Фамилия" || unicIDREg.Text == "Уникальный код!")
            {
                MessageBox.Show("Введите все данные");
            }
            
            if (isUserExist()) { return;}
            if (NameReg.Text != "Имя" && emailREg.Text != "Введите Email" && loginReg.Text != "Введите Логин" && passReg.Text != "Введите пароль" && LastNameReg.Text != "Фамилия" && unicIDREg.Text == "121212")
            {
                var login = loginReg.Text;
                var pass = passReg.Text;
                var Name = NameReg.Text;
                var email = emailREg.Text;
                var lastName = LastNameReg.Text;
                var Surname = SurnameReg.Text;
                if (SurnameReg.Text == "Отчество(если имеется)")
                {
                    Surname = "";
                }
                var unic = unicIDREg.Text;
                var un = new SqlCommand("select max(user_id) from userss;", db.getConnection());
                var userid = (Int32)un.ExecuteScalar() + 1;
                try
                {
                    ClassMailPassword messageCode = new ClassMailPassword(email);
                    messageCode.MailMessag();
                    InputDialog inputDialog = new InputDialog();
                    inputDialog.ShowDialog();
                    if (inputDialog.Flag == true)
                    {
                        SqlCommand command = new SqlCommand($"INSERT INTO userss (user_id, login, Name, LastName, Surname, email, password, unic_id) VALUES ('{userid}', '{login}', '{Name}','{lastName}', '{Surname}', '{email}', '{pass}', '{unic}')", db.getConnection());
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Регистрация успешно завершена");
                            exReg = true;
                        }
                        else { MessageBox.Show("Что-то пошло не так.."); }

                    }
                    else
                    {
                        if (NameReg.Text == "" || emailREg.Text == "" || loginReg.Text == "" || passReg.Text == "")
                        {
                            MessageBox.Show("Проверьте введенные данные еще раз");
                        }
                        if (isUserExist()) { return; }
                    }
                }
                catch { MessageBox.Show("Данные о почте неверны"); }
            }
            if (NameReg.Text != "Имя" && emailREg.Text != "Введите Email" && loginReg.Text != "Введите Логин" && passReg.Text != "Введите пароль" && LastNameReg.Text != "Фамилия" && unicIDREg.Text == "140408")
            {
                var login = loginReg.Text;
                var pass = passReg.Text;
                var Name = NameReg.Text;
                var email = emailREg.Text;
                var lastName = LastNameReg.Text;
                var Surname = SurnameReg.Text;
                if (SurnameReg.Text == "Отчество(если имеется)")
                {
                    Surname = "";
                }
                var unic = unicIDREg.Text;
                var Un = new SqlCommand("select max(user_id) from userss;", db.getConnection());
                var Userid = (Int32)Un.ExecuteScalar() + 1;
                var un = new SqlCommand("select max(engineer_id) from engineeeers;", db.getConnection());
                var userid = (Int32)un.ExecuteScalar() + 1;

                ClassMailPassword messageCode = new ClassMailPassword(email);
                messageCode.MailMessag();
                InputDialog inputDialog = new InputDialog();
                inputDialog.ShowDialog();
                if (inputDialog.Flag == true)
                {
                    SqlCommand command = new SqlCommand($"INSERT INTO users.dbo.engineeeers (engineer_id, login, Name, LastName, Surname, email, password, unic_id) VALUES ('{userid}', '{login}', '{Name}','{lastName}', '{Surname}', '{email}', '{pass}', '{unic}')", db.getConnection());
                    SqlCommand commandd = new SqlCommand($"INSERT INTO users.dbo.userss (user_id, login, Name, LastName, Surname, email, password, unic_id) VALUES ('{Userid}', '{login}', '{Name}','{lastName}', '{Surname}', '{email}', '{pass}', '{unic}')", db.getConnection());
                    if (command.ExecuteNonQuery() == 1 && commandd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Регистрация успешно завершена");
                        exReg = true;
                    }
                    else { MessageBox.Show("Что-то пошло не так.."); }

                }
                else
                {
                    if (NameReg.Text == "" || emailREg.Text == "" || loginReg.Text == "" || passReg.Text == "")
                    {
                        MessageBox.Show("Проверьте введенные данные еще раз");
                    }
                    if (isUserExist()) { return; }
                }

            }
            db.closeConnection();
        }

        public Boolean isUserExist()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            //Команда которая должна выполнится для базы данных
            var login = loginReg.Text;
            SqlCommand command = new SqlCommand($"select * from userss where login = '{login}'", db.getConnection());
            

            //Выбрали и выполнили нужную команду
            adapter.SelectCommand = command;
            //Помещаем в table
            adapter.Fill(table);

            //Проверка на существование пользователя
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Придумайте новый");
                return true;
            }
            else { return false; }
        }

        
        private void loginReg_Enter(object sender, EventArgs e)
        {
            if (loginReg.Text == "Введите Логин")
            {
                loginReg.Text = "";
                loginReg.ForeColor = Color.Black;
            }
        }
        private void loginReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginReg.Text))
            {
                loginReg.Text = "Введите Логин";
                loginReg.ForeColor = Color.Gray;
            }
        }
        

        private void passReg_Enter(object sender, EventArgs e)
        {
            if (passReg.Text == "Введите Пароль")
            {
                passReg.Text = "";
                passReg.ForeColor = Color.Black;
            }
        }
        private void passReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passReg.Text))
            {
                passReg.Text = "Введите Пароль";
                passReg.ForeColor = Color.Gray;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            passReg.UseSystemPasswordChar = true;
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            passReg.UseSystemPasswordChar = false;
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }

        private void emailReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailREg.Text))
            {
                emailREg.Text = "Введите Email";
                emailREg.ForeColor = Color.Gray;
            }
        }

        private void emailReg_Enter(object sender, EventArgs e)
        {
            if (emailREg.Text == "Введите Email")
            {
                emailREg.Text = "";
                emailREg.ForeColor = Color.Black;
            }
        }

        private void NameReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameReg.Text))
            {
                NameReg.Text = "Имя";
                NameReg.ForeColor = Color.Gray;
            }
        }

        private void NameReg_Enter(object sender, EventArgs e)
        {
            if (NameReg.Text == "Имя")
            {
                NameReg.Text = "";
                NameReg.ForeColor = Color.Black;
            }
        }

        private void LastNameReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameReg.Text))
            {
                LastNameReg.Text = "Фамилия";
                LastNameReg.ForeColor = Color.Gray;
            }
        }

        private void LastNameReg_Enter(object sender, EventArgs e)
        {
            if (LastNameReg.Text == "Фамилия")
            {
                LastNameReg.Text = "";
                LastNameReg.ForeColor = Color.Black;
            }
        }

        private void SurnameReg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameReg.Text))
            {
                SurnameReg.Text = "Отчество(если имеется)";
                SurnameReg.ForeColor = Color.Gray;
            }
        }

        private void SurnameReg_Enter(object sender, EventArgs e)
        {
            if (SurnameReg.Text == "Отчество(если имеется)")
            {
                SurnameReg.Text = "";
                SurnameReg.ForeColor = Color.Black;
            }
        }

        private void unicIDREg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(unicIDREg.Text))
            {
                unicIDREg.Text = "Уникальный код!";
                unicIDREg.ForeColor = Color.Gray;
            }
        }
        
        private void unicIDREg_Enter(object sender, EventArgs e)
        {
            if (unicIDREg.Text == "Уникальный код!")
            {
                unicIDREg.Text = "";
                unicIDREg.ForeColor = Color.Black;
            }
        }

        
    }
}
