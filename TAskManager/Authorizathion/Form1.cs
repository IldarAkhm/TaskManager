using MySql.Data.MySqlClient;
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
using System.Collections.Specialized;

namespace Authorizathion
{
    public partial class Form1 : Form
    {
        
        public string name_user;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            loginField.Text = "Логин";
            loginField.ForeColor = Color.Gray;
            passField.Text = "Пароль";
            passField.ForeColor = Color.Gray;
            passField.PasswordChar = '*';
            ViewBox1.Visible = false;

            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);
        }
        public Form1()
        {
            InitializeComponent();

            this.loginField.Size = new Size(this.passField.Size.Width, this.passField.Size.Height);
        }

        private void registrbutton_Click(object sender, EventArgs e)
        {
            var bb = false;
            var val = new FormRegistr(bb);
            this.Hide();
            val.ShowDialog();
            this.Show();
        }
        

        private void entrancebutton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            // Получаем данные от пользователя
            String loginUser = loginField.Text;
            String passUser = passField.Text;
            name_user = loginUser;
            db.openConnection();
            //try
            {
                var uncid = "121212"; // Пользовательский код
                var unicEng = "140408";
                var unicAdm = "080414";
                var name = new SqlCommand($"select max(Name) from userss where login = '{loginUser}' ;", db.getConnection());
                var Lastname = new SqlCommand($"select max(LastName) from userss where login = '{loginUser}' ;", db.getConnection());
                var email = new SqlCommand($"select max(email) from userss where login = '{loginUser}' ;", db.getConnection());
                var unic_id = new SqlCommand($"select max(unic_id) from userss where login = '{loginUser}' ;", db.getConnection());
                var user_id = new SqlCommand($"select max(user_id) from userss where login = '{loginUser}' ;", db.getConnection());
                var userr_id = (int)user_id.ExecuteScalar();
                var unic_idd = (string)unic_id.ExecuteScalar();
                var namme = (string)name.ExecuteScalar();
                var LastNamee = (string)Lastname.ExecuteScalar();
                var emaiil = (string)email.ExecuteScalar();
                //Создаем обьект класса нашей базы данных

                DataTable table = new DataTable();


                //Команда которая должна выполнится для базы данных
                var log = loginUser;
                var pas = passUser;

                SqlCommand cmd = new SqlCommand($"select count(*) from userss where login = '{log}' and password = '{pas}'", db.getConnection());
                db.openConnection();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0 && unic_idd == uncid) { 
                    this.Hide();
                    var val = new Form2(namme, loginUser, emaiil, LastNamee, userr_id, unic_idd);
                    val.ShowDialog();
                }
                else if (count > 0 && unic_idd == unicEng)
                {
                    this.Hide();
                    var val = new Form5(namme, loginUser, emaiil, LastNamee, userr_id, unic_idd);
                    val.ShowDialog();
                }
                else if (count > 0 && unic_idd == unicAdm)
                {
                    this.Hide();
                    var val = new Form2(namme, loginUser, emaiil, LastNamee, userr_id, unic_idd);
                    val.ShowDialog();
                }
                else { MessageBox.Show("Введенные данные неверны"); }
            }
            //catch { MessageBox.Show("Возникла ошибка") ; }
            db.closeConnection();
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginField.Text))
            {
                loginField.Text = "Логин";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passField.Text))
            {
                passField.Text = "Пароль";
                passField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Пароль")
            {
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }
        }

        private void HideBox2_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = true;
            HideBox2.Visible = false;
            ViewBox1.Visible = true;
        }
        private void ViewBox1_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = false;
            HideBox2.Visible = true;
            ViewBox1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
