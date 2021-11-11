using DataAccess.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentation.Forms;

namespace Presentation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Employee oEmployee = new Employee();
            bool success = oEmployee.LoginEmployee(txtUser.Text, txtPass.Text);
            if (success)
            {
                Form menu = new FormMenu();
                this.Hide();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Fail to login");
            }
        }
    }
}
