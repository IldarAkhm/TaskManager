using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorizathion
{
    public partial class InfoProbAndType : Form
    {
        private DataGridView dgw;
        private int uID;
        private int index;
        public InfoProbAndType()
        {
            InitializeComponent();
        }
        public InfoProbAndType(DataGridView e, int index, int uID)
        {
            this.dgw= e;
            this.index = index;
            this.uID = uID;
            InitializeComponent();
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
                string type = row.Cells["Type"].Value.ToString();

                typeLabel.Text = type;
                ExpBox.Text = Exp;
                StatusLabel.Text = status;
                NameUsLabel.Text = nameUs;
                nameLabel.Text = name;
                dateLabel.Text = date;
                distLabel.Text = dist;
            }
            catch { MessageBox.Show("Что-то пошло не так, проверьте есть ли в таблице данные"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            {
                DB db = new DB();
                db.openConnection();

                var notif = new SqlCommand($"select notification from users.dbo.users where login = '{NameUsLabel.Text}'", db.getConnection());
                var not = (Boolean)notif.ExecuteScalar();
                bool notification = Convert.ToBoolean(not);
                if (not) { notification = true; }
                else { notification = false; }

                var cmd = new SqlCommand($"select max(email) from users.dbo.users where login = '{NameUsLabel.Text}'", db.getConnection());
                string email = (string)cmd.ExecuteScalar();
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
                string query4 = $"UPDATE users.dbo.UsersProblems SET type = '{typeLabel.Text}' WHERE Name_of_user = '{NameUsLabel.Text}' and Name_of_problem = '{nameLabel.Text}'";
                var commanddd = new SqlCommand(query4, db.getConnection());
                commanddd.ExecuteNonQuery();

                if (checkBox1.Checked && notification)
                {
                    ClassMailPassword messageCode = new ClassMailPassword(email, ExpBox.Text, nameLabel.Text, StatusLabel.Text);
                    messageCode.MailMessagee();
                }

                switch (typeLabel.Text)
                {
                    case "Проблема":
                        List<string> list = new List<string>();
                        var cmdd = new SqlCommand($"select email from users.dbo.engineers where Grooup in ('{"A"}', '{"A и B"}')", db.getConnection());
                        var reader = cmdd.ExecuteReader();
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                        foreach (var item in list)
                        {
                            ClassMailPassword messageCode = new ClassMailPassword(item, nameLabel.Text);
                            messageCode.MailMessages();
                        }
                        break;
                    case "Инцидент":
                        List<string> list1 = new List<string>();
                        var cmddd = new SqlCommand($"select email from users.dbo.engineers where Grooup in ('{"B"}', '{"A и B"}')", db.getConnection());
                        var readerr = cmddd.ExecuteReader();
                        while (readerr.Read())
                        {
                            list1.Add(readerr.GetString(0));
                        }
                        foreach (var item in list1)
                        {
                            ClassMailPassword messageCode = new ClassMailPassword(item, nameLabel.Text);
                            messageCode.MailMessages();
                        }
                        break;
                    default: break;
                }

                db.closeConnection();
                MessageBox.Show("Изменения были сохранены");
            }
            //catch { MessageBox.Show("Что-то пошло не так.."); }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedValue = comboBox2.SelectedItem.ToString();
            typeLabel.Text = selectedValue;
        }

    }
}
