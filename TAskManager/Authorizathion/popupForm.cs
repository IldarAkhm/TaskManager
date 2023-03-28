using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorizathion
{
    public partial class popupForm : Form
    {
        private string name;
        private string login;
        private string email;
        private string lastName;

        private void popupForm_Load(object sender, EventArgs e)
        {
            labelLogin.Text = login;
            labelEmail.Text = email;
            labelName.Text = name;
            labelLastName.Text = lastName;
        }
        public popupForm()
        {
            InitializeComponent();
        }
        public popupForm(string name, string login, string email, string lastName)
        {
            this.name = name;
            this.login = login;
            this.email = email;
            this.lastName = lastName;
            InitializeComponent();
        }

    }
}
