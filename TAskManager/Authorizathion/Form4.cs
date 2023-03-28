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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Authorizathion
{
    public partial class Form4 : Form
    {
        private bool NameisTextChanged;
        private int index;
        private bool DistisTextChanged;
        private DataGridView dgw;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(DataGridView e, int index)
        {
            InitializeComponent();
            this.dgw = e;
            this.index = index;
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

                nameLabel.Text = name;
                dateLabel.Text = date;
                distBox.Text = dist;
            }
            catch { MessageBox.Show("Что-то пошло не так, проверьте есть ли в таблице данные"); }
        }

        
        

        private void distBox_TextChanged(object sender, EventArgs e)
        {
            DistisTextChanged = true;
        }
        private void SaveChangesDist()
        {
            DistisTextChanged = false;
            DB db = new DB();
            var newDist = distBox.Text;
            //var nameProblems = new SqlCommand($"select max(LastName) from userss where login = '{loginUser}' ;", db.getConnection());
            string query = $"UPDATE users.dbo.UsersProblems SET distibution = '{newDist}' where Name_of_problem = '{dgw.Rows[index].Cells[0].Value}'";
            db.openConnection();
            var com = new SqlCommand(query, db.getConnection());
            try
            {
                com.ExecuteNonQuery();
                MessageBox.Show("Описание было измененно");
            }
            catch { MessageBox.Show("Что-то пошло не так..."); }
            db.closeConnection();
        }

        //private void distBox_leave(object sender, EventArgs e)
        //{
        //    // Если текст был изменен, открыть диалоговое окно для подтверждения сохранения изменений
        //    if (DistisTextChanged)
        //    {
        //        var result = MessageBox.Show("Хотите сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
        //        if (result == DialogResult.Yes)
        //        {
        //            SaveChangesDist();
        //        }
        //    }
        //}
        //private void distBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        distBox_leave(sender, e);
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Если текст был изменен, открыть диалоговое окно для подтверждения сохранения изменений
            if (DistisTextChanged)
            {
                var result = MessageBox.Show("Хотите сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveChangesDist();
                }
            }
        }
    }
}
