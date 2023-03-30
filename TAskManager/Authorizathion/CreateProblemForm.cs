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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Authorizathion
{
    public partial class CreateProblemForm : Form
    {
        private string namee;
        private int usID;
        private string unID;
        DB db = new DB();
        private void Form3_Load(object sender, EventArgs e)
        {
            int centerX = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int centerY = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;

            this.Location = new Point(centerX, centerY);
        }
        public CreateProblemForm()
        {
            InitializeComponent();

            panel1.Paint += panel1_Paint;
        }
        public CreateProblemForm(string name, int uID, string unID) 
        {
            this.usID = uID;
            this.unID= unID;
            this.namee = name;
            InitializeComponent();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            db.openConnection();



            var name = textBoxName.Text;
            if (name == "") { MessageBox.Show("Нельзя добавить проблему без названия"); }
            else
            {
                var dist = textBoxDist.Text;
                var stat = "Отправлено в отдел распределения";
                var type = "";
                var date = DateTime.Now;

                var addQuery = $"insert into users.dbo.UsersProblems (Name_of_problem, Name_of_user, distibution, type, status, date_of_problem, user_id, unic_id ) values ('{name}', '{namee}', '{dist}', '{type}', '{stat}', '{date}', '{usID}', '{unID}') ";

                var command = new SqlCommand(addQuery, db.getConnection());
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Задача добавлена!");
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так..");
                }
            }
            List<string> list = new List<string>() { };
            var query = new SqlCommand($"select email from users.dbo.users where isnull(notification, '1') = '1' and unic_id = '080414'", db.getConnection());
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            foreach (var item in list)
            {
                ClassMailPassword messageCode = new ClassMailPassword(item, name);
                messageCode.MailMessages();
            }

            db.closeConnection();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

    }
}
