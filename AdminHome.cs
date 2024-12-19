using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            AdminSales newForm = new AdminSales();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            AdminInventory newForm = new AdminInventory();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            AdminProfile newForm = new AdminProfile();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            AdminAboutUs newForm = new AdminAboutUs();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
        }

        private void btnStaff_Click_1(object sender, EventArgs e)
        {
            AdminUserManagement newForm = new AdminUserManagement();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }
    }
}
