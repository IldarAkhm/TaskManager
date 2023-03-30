using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Authorizathion
{
    public partial class FormEngineer2 : Form
    {
        private int notification = 0;
        private int indexNameProblem;
        private bool isTextChanged;
        private popupForm popup;
        private string name;
        private string login;
        private string email;
        private string lastName;
        private bool popupOpen = false;
        private int uID;
        private string grooup;
        private string unID;
        DB db = new DB();
        enum RowState
        {
            Existed,
            New,
            Mofied,
            ModifiedNew,
            Deleted
        }
        public FormEngineer2()
        {
            InitializeComponent();
        }

        public FormEngineer2(string name, string login, string email, string lastName, int uID, string unID)
        {
            this.name = name;
            this.login = login;
            this.email = email;
            this.lastName = lastName;
            this.uID = uID;
            this.unID = unID;
            InitializeComponent();
            panel1.Paint += panel1_Paint;
        }
        public void Form5_Load(object sender, EventArgs e)
        {
            
            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);

            db.openConnection();
            var group = new SqlCommand($"select isnull(Grooup, '') from users.dbo.engineers where login = '{login}'", db.getConnection());
            var gr = (string)group.ExecuteScalar();
            grooup = gr;

            var not = new SqlCommand($"select isnull(notification, '1') from users.dbo.users where login = '{login}'", db.getConnection());
            var notif = (Boolean)not.ExecuteScalar();
            if (notif) { checkBox1.Checked = true; }
            else { checkBox1.Checked = false; }
            db.closeConnection();
            GroupLabel.Text = gr;

            ToolTip tooltip = new ToolTip();
            ToolTip tooltip2 = new ToolTip();
            ToolTip tooltip3 = new ToolTip();
            ToolTip tooltip4 = new ToolTip();
            ToolTip tooltip5 = new ToolTip();
            ToolTip tooltip6 = new ToolTip();
            ToolTip tooltip7 = new ToolTip();

            tooltip7.SetToolTip(pictureBox1, "Обновление списка проблем.");
            tooltip7.InitialDelay = 200;
            tooltip6.SetToolTip(buttonEx, "Выход.");
            tooltip6.InitialDelay = 200;
            tooltip5.SetToolTip(button3, "Фильтр.");
            tooltip5.InitialDelay = 200;
            tooltip4.SetToolTip(button2, "Выгрузка таблицы в Excel.");
            tooltip4.InitialDelay = 200;
            tooltip3.SetToolTip(pictureBox3, "Щелкните два раза по полю, которое хотите редактировать.");
            tooltip3.InitialDelay = 200;
            tooltip2.SetToolTip(label2, "Проблемы пользователей.");
            tooltip2.InitialDelay = 200;
            tooltip.SetToolTip(dataGridView1, "Щелкните два раза по полю, которое хотите редактировать.");
            tooltip.InitialDelay = 200;



            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
        void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(panel1.Parent.BackColor);
            Control control = panel1;
            int radius = 30;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                using (SolidBrush brush = new SolidBrush(control.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            if (popupOpen)
            {
                popup.Close();
                popupOpen = false;
            }
            else
            {
                popup = new popupForm(name, login, email, lastName);
                popup.FormBorderStyle = FormBorderStyle.None;
                popup.BackColor = Color.White;
                popup.StartPosition = FormStartPosition.Manual;
                popup.Location = new Point(this.Location.X + pictureBox3.Location.X, this.Location.Y + pictureBox3.Location.Y + pictureBox3.Height + 35);
                popup.Size = new Size(550, 300);
                popup.Show();
                popupOpen = true;
            }
            
        }

        private void buttonEx_Click(object sender, EventArgs e)
        {
            var val = new Form1();
            this.Hide();
            val.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView1.Visible == true) { dataGridView1.Visible = false; }
            else
            {
                dataGridView1.Visible = true;
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Name_of_problem", "Название проблемы");
            dataGridView1.Columns.Add("Date_of_problem", "Дата");
            dataGridView1.Columns.Add("Distribution", "Описание");
            dataGridView1.Columns.Add("expiretion_date", "Дата решения");
            dataGridView1.Columns.Add("Type", "Тип");
            dataGridView1.Columns.Add("solution", "Решение");
            dataGridView1.Columns.Add("Status", "Статус");
            dataGridView1.Columns.Add("Name_of_user", "Имя пользователя");
            dataGridView1.Columns.Add("user_id", "ID Пользователя");

        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetDateTime(1), record.GetString(2), record.GetDateTime(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetInt32(8), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            string gr = grooup;
            List<string> types;
            switch (gr)
            {
                case "A":
                    types = new List<string>() { "Проблема", "Все" };
                    break;
                case "B":
                    types = new List<string>() { "Инцидент", "Все" };
                    break;
                case "A и B":
                    types = new List<string>(){"Проблема", "Инцидент"};
                    break;
                default:
                    types = new List<string>() { "Нет", "Нет" };
                    break;
            }


            db.openConnection();
            dgw.Rows.Clear();

            string queryString = $"select Name_of_problem, date_of_problem, distibution, isnull(expiriation_date, ''), type, isnull(solution, ''), status, Name_of_user, user_id from users.dbo.UsersProblems where unic_id = '121212' and type in ('{types[0]}', '{types[1]}')";

            SqlCommand command = new SqlCommand(queryString, db.getConnection());


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            db.closeConnection();
        }
        private void dataGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получаем данные о выбранной строке
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    // Создаем экземпляр формы Form2
                    InfoProbEngineer form6 = new InfoProbEngineer(dataGridView1, indexNameProblem, uID);

                    // Передаем данные о выбранной строке в форму Form2
                    form6.SetRowData(row);

                    // Открываем форму Form2
                    form6.Show();
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так.. Убедитесь, есть ли в строке данные");
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.. Убедитесь, есть ли в строке данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            int i, j;
            for (i = 0; i <= dataGridView1.RowCount - 2; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    wsh.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }
            exApp.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            for (int i = 0; i <= dataGridView1.Rows.Count - 2; i++)
            {
                if ((string)dataGridView1.Rows[i].Cells[6].Value == selectedValue)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    dataGridView1.Rows[i].Visible = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true) { comboBox1.Visible = false; }
            else { comboBox1.Visible = true; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) 
            {
                notification = 1;
                db.openConnection();

                var query = new SqlCommand($"UPDATE users.dbo.users SET notification = '{notification}' where login = '{login}'", db.getConnection());
                query.ExecuteNonQuery();


                db.closeConnection();
            }
            else 
            {
                notification = 0;
                db.openConnection();

                var query = new SqlCommand($"UPDATE users.dbo.users SET notification = '{notification}'where login = '{login}'", db.getConnection());
                query.ExecuteNonQuery();


                db.closeConnection();
            }
        }
    }
}
