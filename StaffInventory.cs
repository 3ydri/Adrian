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
    public partial class StaffInventory : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;
        

        public StaffInventory()
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
            string query = "SELECT * FROM Inventory";
            adapter = new OleDbDataAdapter(query, conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dgvInventory.DataSource = dt;
        }


        private void btnOrders_Click(object sender, EventArgs e)
        {
            StaffOrders newForm = new StaffOrders();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
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

        private void StaffInventory_Load(object sender, EventArgs e)
        {
            dgvInventory.Columns["INVENTORYID"].Visible = false;
        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM Inventory WHERE DrinkName LIKE @DrinkName";
            adapter = new OleDbDataAdapter(searchQuery, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@DrinkName", tbSearch.Text + "%");

            dt = new DataTable();

            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            dgvInventory.DataSource = dt;
        }
    }
    }
