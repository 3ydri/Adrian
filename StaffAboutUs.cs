using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class StaffAboutUs : Form
    {
        

        public StaffAboutUs()
        {
            InitializeComponent();
            
        }
       

        private void btnHome_Click(object sender, EventArgs e)
        {
            StaffHome newForm = new StaffHome();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            StaffOrders newForm = new StaffOrders();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            StaffTransaction newForm = new StaffTransaction();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            StaffSales newForm = new StaffSales();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            StaffInventory newForm = new StaffInventory();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            StaffProfile newForm = new StaffProfile();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void StaffAboutUs_Load(object sender, EventArgs e)
        {

        }
    }
}
