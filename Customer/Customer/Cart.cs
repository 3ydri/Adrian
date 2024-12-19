using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Customer
{
    public partial class Cart : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PreDefense.accdb";

        public Cart()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ProductName", "Product Name");
            dgvCart.Columns.Add("Quantity", "Quantity");
            dgvCart.Columns.Add("Price", "Price");
            dgvCart.Columns.Add("TotalAmount", "Total Amount");
            dgvCart.Columns.Add("SugarLevel", "Sugar Level"); // New column
            dgvCart.Columns.Add("AddOns", "Add-Ons");         // New column

            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void LoadCart()
        {
            dgvCart.Rows.Clear(); // Clear previous data to avoid duplication

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ProductName, Quantity, Price, TotalAmount, SugarLevel, AddOns FROM Cart"; // Updated query
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader["ProductName"].ToString();
                            string quantity = reader["Quantity"].ToString();
                            string price = reader["Price"].ToString();
                            string totalAmount = reader["TotalAmount"].ToString();
                            string sugarLevel = reader["SugarLevel"].ToString();
                            string addOns = reader["AddOns"].ToString();

                            // Ensure SugarLevel is not null, and handle it gracefully
                            sugarLevel = string.IsNullOrEmpty(sugarLevel) ? "Not Specified" : sugarLevel;

                            dgvCart.Rows.Add(
                                productName,
                                quantity,
                                price,
                                totalAmount,
                                sugarLevel,  // Display Sugar Level (if null, show "Not Specified")
                                addOns
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cart data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvCart.SelectedCells[0].RowIndex;
            string productName = dgvCart.Rows[selectedRowIndex].Cells[0].Value.ToString();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Cart WHERE ProductName = ?";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("?", productName);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Item removed from cart.", "Success");
                LoadCart();  // Refresh the cart
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item from cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            CheckOut checkoutForm = new CheckOut();  // Create an instance of the Checkout form
            this.Hide();  // Hide the current Customization form
            checkoutForm.Show();
        }
    }
}
