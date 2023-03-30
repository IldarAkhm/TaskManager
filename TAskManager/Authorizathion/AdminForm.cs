using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Authorizathion
{
    public partial class AdminForm : Form
    {
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
        private int notification;
        enum RowState
        {
            Existed,
            New,
            Mofied,
            ModifiedNew,
            Deleted
        }
        public AdminForm(string name, string login, string email, string lastName, int uID, string unID)
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
        private void AdminForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
            CreateColumns2();
            RefreshDataGrid2(dataGridView2);
            CreateColumns3();
            RefreshDataGrid3(dataGridView3);
            ToolTip tooltip = new ToolTip();
            ToolTip tooltip2 = new ToolTip();
            ToolTip tooltip3 = new ToolTip();
            ToolTip tooltip4 = new ToolTip();
            ToolTip tooltip6 = new ToolTip();
            ToolTip tooltip7 = new ToolTip();
            ToolTip tooltip5 = new ToolTip();
            ToolTip tooltip8 = new ToolTip();

            tooltip8.SetToolTip(label3, "Таблица всех пользователей.");
            tooltip8.InitialDelay = 200;
            tooltip5.SetToolTip(label1, "Таблица инженеров.");
            tooltip5.InitialDelay = 200;
            tooltip7.SetToolTip(pictureBox1, "Обновление списка проблем.");
            tooltip7.InitialDelay = 200;
            tooltip6.SetToolTip(buttonEx, "Выход.");
            tooltip6.InitialDelay = 200;
            tooltip4.SetToolTip(button2, "Выгрузка таблицы в Excel.");
            tooltip4.InitialDelay = 200;
            tooltip3.SetToolTip(pictureBox3, "Щелкните два раза по полю, которое хотите редактировать.");
            tooltip3.InitialDelay = 200;
            tooltip2.SetToolTip(label2, "Проблемы пользователей.");
            tooltip2.InitialDelay = 200;
            tooltip.SetToolTip(dataGridView1, "Щелкните два раза по полю, которое хотите редактировать.");
            tooltip.InitialDelay = 200;
            db.openConnection();
            var not = new SqlCommand($"select isnull(notification, '1') from users.dbo.users where login = '{login}'", db.getConnection());
            var notif = (Boolean)not.ExecuteScalar();
            if (notif) { checkBox1.Checked = true; }
            else { checkBox1.Checked = false; }
            db.closeConnection();
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
        public AdminForm()
        {
            InitializeComponent();
        }
        private void buttonEx_Click(object sender, EventArgs e)
        {
            var val = new Form1();
            this.Hide();
            val.Show();
        }
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
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
                popup.Location = new System.Drawing.Point(this.Location.X + pictureBox3.Location.X, this.Location.Y + pictureBox3.Location.Y + pictureBox3.Height + 35);
                popup.Size = new Size(550, 300);
                popup.Show();
                popupOpen = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            DataGridView dgww = dataGridView1;
            if (dataGridView1.Visible == true) { dgww = dataGridView1; }
            else if (dataGridView2.Visible== true) { dgww = dataGridView2; }
            else if (dataGridView3.Visible == true) { dgww = dataGridView3; }
           
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            int i, j;
            for (i = 0; i <= dgww.RowCount - 2; i++)
            {
                for (j = 0; j <= dgww.ColumnCount - 1; j++)
                {
                    wsh.Cells[i + 1, j + 1] = dgww[j, i].Value.ToString();
                }
            }
            exApp.Visible = true;
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
            db.openConnection();
            dgw.Rows.Clear();

            string queryString = $"select Name_of_problem, date_of_problem, distibution, isnull(expiriation_date, ''), type, isnull(solution, ''), status, Name_of_user, user_id from users.dbo.UsersProblems where unic_id = '121212'";

            SqlCommand command = new SqlCommand(queryString, db.getConnection());


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            db.closeConnection();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView1.Visible == true) { 
                dataGridView1.Visible = false;
            }
            else
            {
                dataGridView1.Visible = true;
            }
        }
        private void CreateColumns2()
        {
            dataGridView2.Columns.Add("engineer_id", "ID Инженера");
            dataGridView2.Columns.Add("user_id", "ID Пользователя");
            dataGridView2.Columns.Add("login", "Логин");
            dataGridView2.Columns.Add("Name", "Имя");
            dataGridView2.Columns.Add("LastName", "Фамииля");
            dataGridView2.Columns.Add("email", "Почта");
            dataGridView2.Columns.Add("Grooup", "Группа");

        }

        private void ReadSingleRow2(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), RowState.ModifiedNew);
        }

        private void RefreshDataGrid2(DataGridView dgw)
        {
            db.openConnection();
            dgw.Rows.Clear();

            string queryString = $"select engineer_id, user_id, login, Name, LastName, email, isnull(Grooup, '') from users.dbo.engineers";

            SqlCommand command = new SqlCommand(queryString, db.getConnection());


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow2(dgw, reader);
            }
            reader.Close();
            db.closeConnection();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView2.Visible == true)
            {
                dataGridView2.Visible = false;
            }
            else
            {
                dataGridView2.Visible = true;
            }
        }
        private void CreateColumns3()
        {
            dataGridView3.Columns.Add("user_id", "ID Пользователя");
            dataGridView3.Columns.Add("login", "Логин");
            dataGridView3.Columns.Add("Name", "Имя");
            dataGridView3.Columns.Add("LastName", "Фамииля");
            dataGridView3.Columns.Add("email", "Почта");
            dataGridView3.Columns.Add("password", "Пароль");
            dataGridView3.Columns.Add("unic_id", "Уникальный ID");
        }

        private void ReadSingleRow3(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), RowState.ModifiedNew);
        }

        private void RefreshDataGrid3(DataGridView dgw)
        {
            db.openConnection();
            dgw.Rows.Clear();

            string queryString = $"select user_id, login, Name, LastName, email, password, unic_id from users.dbo.users";

            SqlCommand command = new SqlCommand(queryString, db.getConnection());


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow3(dgw, reader);
            }
            reader.Close();
            db.closeConnection();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView3.Visible == true)
            {
                dataGridView3.Visible = false;
            }
            else
            {
                dataGridView3.Visible = true;
            }
        }
        private void dataGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получаем данные о выбранной строке
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    // Создаем экземпляр формы Form2
                    InfoEngineerForm form6 = new InfoEngineerForm(dataGridView2, indexNameProblem, uID);

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
        private void dataGridView2_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (e.RowIndex >= 0)
                {
                    // Получаем данные о выбранной строке
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    if (row.Cells[0].Value != null)
                    {
                        // Создаем экземпляр формы Form2
                        InfoProbAndType form6 = new InfoProbAndType(dataGridView1, indexNameProblem, uID);

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            RefreshDataGrid2(dataGridView2);
            RefreshDataGrid3(dataGridView3);
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
