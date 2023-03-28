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
    public partial class Form6 : Form
    {
        private bool NameisTextChanged;
        private int index;
        private bool DistisTextChanged;
        private DataGridView dgw;
        private string email;
        private int uID;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(DataGridView e, int index, int uID)
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
            DB db = new DB();

            db.openConnection();

            var cmd = new SqlCommand($"select max(email) from users.dbo.userss where user_id = '{uID}'", db.getConnection());
            string email = (string)cmd.ExecuteScalar();
            if (StatusLabel.Text == "Выполнено")
            {
                var date = DateTime.Now;
                string query3 = $"UPDATE users.dbo.UsersProblems SET expiriation_date = '{date}' WHERE Name_of_problem = '{nameLabel.Text}'";
                var command3 = new SqlCommand(query3, db.getConnection());
                command3.ExecuteNonQuery();
            }

            string query = $"UPDATE users.dbo.UsersProblems SET status = '{StatusLabel.Text}' WHERE Name_of_problem = '{nameLabel.Text}'";
            var command = new SqlCommand(query, db.getConnection());
            command.ExecuteNonQuery();
            string query2 = $"UPDATE users.dbo.UsersProblems SET solution = '{ExpBox.Text}' where Name_of_problem = '{nameLabel.Text}'";
            var command2 = new SqlCommand(query2, db.getConnection());
            command2.ExecuteNonQuery();

            if (checkBox1.Checked)
            {
                ClassMailPassword messageCode = new ClassMailPassword(email, ExpBox.Text, nameLabel.Text, StatusLabel.Text);
                messageCode.MailMessagee();
            }

            db.closeConnection();
        }

        
    }
}
