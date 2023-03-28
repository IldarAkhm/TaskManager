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
    public partial class Form5 : Form
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
        public Form5()
        {
            InitializeComponent();
        }

        public Form5(string name, string login, string email, string lastName, int uID, string unID)
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
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);

            

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
            dgw.Rows.Clear();

            string queryString = $"select Name_of_problem, date_of_problem, distibution, isnull(expiriation_date, ''), type, isnull(solution, ''), status, Name_of_user, user_id from users.dbo.UsersProblems where unic_id = '121212'";

            SqlCommand command = new SqlCommand(queryString, db.getConnection());

            db.openConnection();

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
            try
            {
                // Получаем данные о выбранной строке
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Создаем экземпляр формы Form2
                Form6 form6 = new Form6(dataGridView1, indexNameProblem, uID);

                // Передаем данные о выбранной строке в форму Form2
                form6.SetRowData(row);

                // Открываем форму Form2
                form6.Show();
            }
            catch { MessageBox.Show("Что-то пошло не так.. Убедитесь, есть ли в строке данные"); }
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
    }
}
