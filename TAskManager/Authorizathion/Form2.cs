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
using System.IO;
using System.Xml;
using CsvHelper;
using OfficeOpenXml;
using CsvHelper.Configuration;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;


namespace Authorizathion
{
    
    public partial class Form2 : Form
    {
        private void Form2_Load(object sender, EventArgs e)
        {
            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);

            CreateColumns();
            RefreshDataGrid(dataGridView1);

            ToolTip tooltip1 = new ToolTip();
            ToolTip tooltip2 = new ToolTip();
            ToolTip tooltip3 = new ToolTip();
            ToolTip tooltip4 = new ToolTip();
            ToolTip tooltip5 = new ToolTip();
            ToolTip tooltip6 = new ToolTip();
            ToolTip tooltip7 = new ToolTip();
            ToolTip tooltip8 = new ToolTip();

            tooltip8.SetToolTip(dataGridView1, "Щелкните два раза по полю, которое хотите редактировать.");
            tooltip8.InitialDelay = 200;
            tooltip7.SetToolTip(button3, "Фильтр сортировки.");
            tooltip7.InitialDelay = 400;
            tooltip6.SetToolTip(button2, "Выгрузка таблицы в Excel.");
            tooltip6.InitialDelay = 400;
            tooltip5.SetToolTip(buttonEx, "Выход.");
            tooltip5.InitialDelay = 400;
            tooltip4.SetToolTip(pictureBox3, "Информация о профиле.");
            tooltip4.InitialDelay = 400;
            tooltip2.SetToolTip(pictureBox1, "Обновление списка проблем.");
            tooltip2.InitialDelay = 400;
            tooltip3.SetToolTip(pictureBox2, "Добавить проблему.");
            tooltip3.InitialDelay = 400;
            tooltip1.SetToolTip(label2, "Нажмите для переключения главного экрана и списка проблем.");
            tooltip1.InitialDelay = 400;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private int indexNameProblem;
        private bool isTextChanged;
        private popupForm popup;
        private string name;
        private string login;
        private string email;
        private string lastName;
        private int uID;
        private string unID;
        DB db = new DB();
        private bool popupOpen = false;
        enum RowState
        {
            Existed,
            New,
            Mofied,
            ModifiedNew,
            Deleted
        }
        public Form2()
        {
            InitializeComponent();
            panel1.Paint += panel1_Paint;
        }

        public Form2(string name, string login, string email, string lastName, int uID, string unID) 
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

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Name_of_problem", "Название проблемы");
            dataGridView1.Columns.Add("Date_of_problem", "Дата");
            dataGridView1.Columns.Add("Distribution", "Описание");
            dataGridView1.Columns.Add("expiretion_date", "Дата решения");
            dataGridView1.Columns.Add("Type", "Тип");
            dataGridView1.Columns.Add("solution", "Решение");
            dataGridView1.Columns.Add("Status","Статус");
            
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetDateTime(1), record.GetString(2), record.GetDateTime(3), record.GetString(4), record.GetString(5), record.GetString(6), RowState.ModifiedNew);      
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select Name_of_problem, date_of_problem, distibution, isnull(expiriation_date, ''), type, isnull(solution, ''), status from users.dbo.UsersProblems where Name_of_user = '{name}'";

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

        
        private void label2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible == true) { dataGridView1.Visible = false; }
            else { 
                dataGridView1.Visible = true;
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor= Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.ForeColor = Color.White;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.ForeColor = Color.Black;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 add = new Form3(name, uID, unID);
            add.Show();
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
        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Проверяем, что была выбрана ячейка с текстом
        //    if (e.ColumnIndex == 2)
        //    {
        //        if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].Value != null)
        //        {
        //            if (textBox.Visible == true)
        //            {
        //                indexNameProblem = e.RowIndex;
        //                textBox.Visible= false;
        //                // Создаем текстовое поле для редактирования текста
        //                textBox.Multiline = true;
        //                textBox.Text = dataGridView1.SelectedCells[0].Value.ToString();

        //                // Определяем положение текстового поля на форме
        //                Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
        //                textBox.Bounds = rect;
                        
        //                // Добавляем текстовое поле на форму и устанавливаем фокус на нем
        //                this.Controls.Add(textBox);
        //                textBox.Focus();

        //            }
        //            else
        //            {

        //                indexNameProblem = e.RowIndex;
        //                textBox.Visible = true;
        //                // Создаем текстовое поле для редактирования текста
        //                textBox.Multiline = true;
        //                textBox.Text = dataGridView1.SelectedCells[0].Value.ToString();

        //                // Определяем положение текстового поля на форме
        //                Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
        //                textBox.Bounds = rect;

        //                // Добавляем текстовое поле на форму и устанавливаем фокус на нем
        //                this.Controls.Add(textBox);
        //                textBox.Focus();
        //            }
        //        }

        //    }
        //}
        private void dataGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Получаем данные о выбранной строке
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Создаем экземпляр формы Form2
                Form4 form4 = new Form4(dataGridView1, indexNameProblem);

                // Передаем данные о выбранной строке в форму Form2
                form4.SetRowData(row);

                // Открываем форму Form2
                form4.Show();
            }
            catch { MessageBox.Show("Что-то пошло не так.. Убедитесь, есть ли в строке данные"); }
        }

        //private void textBox_TextChanged(object sender, EventArgs e)
        //{
        //    isTextChanged = true;
        //}
        //private void textBox_leave(object sender, EventArgs e)
        //{
        //    // Если текст был изменен, открыть диалоговое окно для подтверждения сохранения изменений
        //    if (isTextChanged)
        //    {
        //        var result = MessageBox.Show("Хотите сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
        //        if (result == DialogResult.Yes)
        //        {
        //            SaveChanges();
        //        }
        //    }
        //}

        //private void SaveChanges()
        //{
        //    isTextChanged= false;
        //    DB db = new DB();
        //    var newDist = textBox.Text;
        //    //var nameProblems = new SqlCommand($"select max(LastName) from userss where login = '{loginUser}' ;", db.getConnection());
        //    string query = $"UPDATE users.dbo.UsersProblems SET distibution = '{newDist}' where Name_of_problem = '{dataGridView1.Rows[indexNameProblem].Cells[0].Value}'";
        //    db.openConnection();
        //    var com = new SqlCommand(query, db.getConnection());
        //    com.ExecuteNonQuery();
        //    db.closeConnection();
        //}

        //private void textBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        textBox_leave(sender, e);
        //    }     
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            int i, j;
            for (i = 0; i <= dataGridView1.RowCount - 2; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount -1;j++)
                {
                    wsh.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }
            exApp.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true) { comboBox1.Visible= false; }
            else { comboBox1.Visible = true; }
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
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = "Щелкните два раза, чтобы отредактировать описание";
            
        }

    }
}
