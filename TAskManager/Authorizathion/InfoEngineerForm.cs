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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Authorizathion
{
    public partial class InfoEngineerForm : Form
    {
        private bool NameisTextChanged;
        private int index;
        private bool DistisTextChanged;
        private DataGridView dgw;
        private string email;
        private int uID;
        public InfoEngineerForm()
        {
            InitializeComponent();
        }
        public InfoEngineerForm(DataGridView e, int index, int uID)
        {
            InitializeComponent();
            this.dgw = e;
            this.index = index;
            this.uID = uID;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            label3.Text = selectedValue;
        }
        public void SetRowData(DataGridViewRow row)
        {
            try
            {
                string name = row.Cells["login"].Value.ToString();
                string group = row.Cells["Grooup"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                this.email = email;


                label3.Text = group;
                nameLabel.Text = name;
                
            }
            catch { MessageBox.Show("Что-то пошло не так, проверьте есть ли в таблице данные"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> type;
                switch (label3.Text)
                {
                    case "A":
                        type = new List<string>() {"Проблема", "нет"};
                        break;
                    case "B":
                        type = new List<string>() { "Инцидент", "нет"};
                        break;
                    case "A и B":
                        type = new List<string>() { "Проблема", "Инцидент" };
                        break;
                    default:
                        type = new List<string>() { };
                        break;

                }
                DB db = new DB();
                List<string> list = new List<string>();
                db.openConnection();

                var notif = new SqlCommand($"select notification from users.dbo.users where login = '{nameLabel.Text}'", db.getConnection());
                var not = (Boolean)notif.ExecuteScalar();
                bool notification = Convert.ToBoolean(not);
                if (not) { notification = true; }
                else { notification = false; }

                string query = $"UPDATE users.dbo.engineers SET Grooup = '{label3.Text}' WHERE login = '{nameLabel.Text}'";
                var command = new SqlCommand(query, db.getConnection());
                command.ExecuteNonQuery();
                var cmd = new SqlCommand($"select Name_of_problem from users.dbo.UsersProblems where type in ('{type[0]}', '{type[1]}')", db.getConnection());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
                if (notification)
                {
                    ClassMailPassword messageCode = new ClassMailPassword(email, label3.Text, list);
                    messageCode.MailMessageee();
                }

                db.closeConnection();
                MessageBox.Show("Изменения были сохранены");
            }
            catch { MessageBox.Show("Что-то пошло не так.."); }
        }
    }
}
