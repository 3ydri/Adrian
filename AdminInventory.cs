using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class AdminInventory : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;

        public AdminInventory()
        {
            InitializeComponent();
            InitializeDatabase();
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

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbCategory.Text) ||
                string.IsNullOrWhiteSpace(tbDrinkName.Text) ||
                string.IsNullOrWhiteSpace(tbFlavors.Text) ||
                string.IsNullOrWhiteSpace(tbStraws.Text) ||
                string.IsNullOrWhiteSpace(tbCups.Text) ||
                string.IsNullOrWhiteSpace(tbPearl.Text) ||
                string.IsNullOrWhiteSpace(tbCrystals.Text) ||
                string.IsNullOrWhiteSpace(tbCreampuff.Text) ||
                string.IsNullOrWhiteSpace(tbCheesecake.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM Inventory WHERE InventoryID = @InventoryID";
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb"))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InventoryID", Convert.ToInt32(dgvInventory.CurrentRow.Cells["InventoryID"].Value));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inventory item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting inventory item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            string query = "UPDATE Inventory SET Category = @Category, DrinkName = @DrinkName, Flavors = @Flavors, Straws = @Straws, " +
                           "Cups = @Cups, Pearl = @Pearl, Crystals = @Crystals, Creampuff = @Creampuff, Cheesecake = @Cheesecake " +
                           "WHERE InventoryID = @InventoryID";

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb"))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Category", tbCategory.Text);
                cmd.Parameters.AddWithValue("@DrinkName", tbDrinkName.Text);
                cmd.Parameters.AddWithValue("@Flavors", tbFlavors.Text);
                cmd.Parameters.AddWithValue("@Straws", tbStraws.Text);
                cmd.Parameters.AddWithValue("@Cups", tbCups.Text);
                cmd.Parameters.AddWithValue("@Pearl", tbPearl.Text);
                cmd.Parameters.AddWithValue("@Crystals", tbCrystals.Text);
                cmd.Parameters.AddWithValue("@Creampuff", tbCreampuff.Text);
                cmd.Parameters.AddWithValue("@Cheesecake", tbCheesecake.Text);
                cmd.Parameters.AddWithValue("@InventoryID", Convert.ToInt32(dgvInventory.CurrentRow.Cells["InventoryID"].Value));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inventory item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating inventory item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbCategory.Clear();
            tbDrinkName.Clear();
            tbFlavors.Clear();
            tbStraws.Clear();
            tbCups.Clear();
            tbPearl.Clear();
            tbCrystals.Clear();
            tbCreampuff.Clear();
            tbCheesecake.Clear();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            AdminHome newForm = new AdminHome();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }
        private void btnSales_Click(object sender, EventArgs e)
        {
            AdminSales newForm = new AdminSales();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
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

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AdminUserManagement newForm = new AdminUserManagement();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();

        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvInventory.Rows[e.RowIndex];

                tbCategory.Text = row.Cells["Category"].Value.ToString();
                tbDrinkName.Text = row.Cells["DrinkName"].Value.ToString();
                tbFlavors.Text = row.Cells["Flavors"].Value.ToString();
                tbStraws.Text = row.Cells["Straws"].Value.ToString();
                tbCups.Text = row.Cells["Cups"].Value.ToString();
                tbPearl.Text = row.Cells["Pearl"].Value.ToString();
                tbCrystals.Text = row.Cells["Crystals"].Value.ToString();
                tbCreampuff.Text = row.Cells["Creampuff"].Value.ToString();
                tbCheesecake.Text = row.Cells["Cheesecake"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string query = "INSERT INTO Inventory (Category, DrinkName, Flavors, Straws, Cups, Pearl, Crystals, Creampuff, Cheesecake) " +
                           "VALUES (@Category, @DrinkName, @Flavors, @Straws, @Cups, @Pearl, @Crystals, @Creampuff, @Cheesecake)";

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb"))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Category", tbCategory.Text);
                cmd.Parameters.AddWithValue("@DrinkName", tbDrinkName.Text);
                cmd.Parameters.AddWithValue("@Flavors", tbFlavors.Text);
                cmd.Parameters.AddWithValue("@Straws", tbStraws.Text);
                cmd.Parameters.AddWithValue("@Cups", tbCups.Text);
                cmd.Parameters.AddWithValue("@Pearl", tbPearl.Text);
                cmd.Parameters.AddWithValue("@Crystals", tbCrystals.Text);
                cmd.Parameters.AddWithValue("@Creampuff", tbCreampuff.Text);
                cmd.Parameters.AddWithValue("@Cheesecake", tbCheesecake.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inventory item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding inventory item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadData();
        }

        private void AdminInventory_Load(object sender, EventArgs e)
        {
            dgvInventory.Columns["INVENTORYID"].Visible = false;
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

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
    }
