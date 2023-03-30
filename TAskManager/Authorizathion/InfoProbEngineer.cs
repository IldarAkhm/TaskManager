using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Authorizathion
{
    public partial class InfoProbEngineer : Form
    {
        private bool NameisTextChanged;
        private int index;
        private bool DistisTextChanged;
        private DataGridView dgw;
        private string email;
        private int uID;
        public InfoProbEngineer()
        {
            InitializeComponent();
        }
        public InfoProbEngineer(DataGridView e, int index, int uID)
        {
            InitializeComponent();
            this.dgw = e;
            this.index = index;
            this.uID = uID;
        }

        public void Form4_Load(object sender, EventArgs e)
        {
            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);
            this.Width = 1450;
            this.Height = 650;
        }
        public void SetRowData(DataGridViewRow row)
        {
            try
            {
                string name = row.Cells["Name_of_problem"].Value.ToString();
                string date = row.Cells["Date_of_problem"].Value.ToString();
                string dist = row.Cells["Distribution"].Value.ToString();
                string nameUs = row.Cells["Name_of_user"].Value.ToString();
                string status = row.Cells["Status"].Value.ToString();
                string Exp = row.Cells["solution"].Value.ToString();

                ExpBox.Text = Exp;
                StatusLabel.Text = status;
                NameUsLabel.Text = nameUs;
                nameLabel.Text = name;
                dateLabel.Text = date;
                distLabel.Text = dist;
            }
            catch { MessageBox.Show("Что-то пошло не так, проверьте есть ли в таблице данные"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            StatusLabel.Text = selectedValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();

                db.openConnection();

                var notif = new SqlCommand($"select notification from users.dbo.users where login = '{NameUsLabel.Text}'", db.getConnection());
                var not = (Boolean)notif.ExecuteScalar();
                bool notification = Convert.ToBoolean(not);
                if (not) { notification = true; }
                else { notification = false; }

                var cmd = new SqlCommand($"select max(email) from users.dbo.users where login = '{NameUsLabel.Text}'", db.getConnection());
                var email = (string)cmd.ExecuteScalar();
                if (StatusLabel.Text == "Выполнено")
                {
                    var date = DateTime.Now;
                    string query3 = $"UPDATE users.dbo.UsersProblems SET expiriation_date = '{date}' WHERE Name_of_user = '{NameUsLabel.Text}' and Name_of_problem = '{nameLabel.Text}'";
                    var command3 = new SqlCommand(query3, db.getConnection());
                    command3.ExecuteNonQuery();
                }
                if (StatusLabel.Text == "В работе")
                {
                    var date = DateTime.Now;
                    string query5 = $"UPDATE users.dbo.UsersProblems SET date_of_commissioning = '{date}' WHERE Name_of_user = '{NameUsLabel.Text}' and Name_of_problem = '{nameLabel.Text}'";
                    var comand5 = new SqlCommand(query5, db.getConnection());
                    comand5.ExecuteNonQuery();
                }

                string query = $"UPDATE users.dbo.UsersProblems SET status = '{StatusLabel.Text}' WHERE Name_of_user = '{NameUsLabel.Text}' and Name_of_problem = '{nameLabel.Text}'";
                var command = new SqlCommand(query, db.getConnection());
                command.ExecuteNonQuery();
                string query2 = $"UPDATE users.dbo.UsersProblems SET solution = '{ExpBox.Text}' where Name_of_user = '{NameUsLabel.Text}' and Name_of_problem = '{nameLabel.Text}'";
                var command2 = new SqlCommand(query2, db.getConnection());
                command2.ExecuteNonQuery();

                if (checkBox1.Checked && notification)
                {
                    ClassMailPassword messageCode = new ClassMailPassword(email.ToString(), ExpBox.Text, nameLabel.Text, StatusLabel.Text);
                    messageCode.MailMessagee();
                }

                db.closeConnection();
                MessageBox.Show("Изменения были сохранены");
            }
            catch { MessageBox.Show("Что-то пошло не так.."); }
        }

    }
}
