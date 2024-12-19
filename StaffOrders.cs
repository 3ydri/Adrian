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
    public partial class StaffOrders : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;

        public StaffOrders()
        {
            InitializeDatabase();
            InitializeComponent();
            LoadData();          
            
        }
        private void InitializeDatabase()
        {
            // Initialize the connection
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
            conn = new OleDbConnection(connString);
        }
        private void LoadData()
        {
            // Load data from the database into the DataGridView
            string query = "SELECT * FROM Cart";
            adapter = new OleDbDataAdapter(query, conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dgvSales.DataSource = dt;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            StaffHome newForm = new StaffHome();
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

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            StaffAboutUs newForm = new StaffAboutUs();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
        }

        private void StaffOrders_Load(object sender, EventArgs e)
        {
            dgvSales.Columns["CartID"].Visible = false;
        }
    }
}
