using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Customer
{
    public partial class Customization : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PreDefense.accdb";

        private int drinkId;
        private string drinkName;
        private decimal price;

        public Customization()
        {
            InitializeComponent();
        }

        public Customization(int drinkId, string drinkName, decimal price)
        {
            InitializeComponent();

            this.drinkId = drinkId;
            this.drinkName = drinkName;
            this.price = price;
        }

        private void Customization_Load(object sender, EventArgs e)
        {
            lblDrinkName.Text = drinkName; // Display drink name on form
            lblPrice.Text = $"₱{price:F2}"; // Display base price
            LoadAddOns();
        }

        private void LoadAddOns()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT AddOnID, AddOnName, Price FROM AddOns";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string addOnName = reader["AddOnName"].ToString();
                        decimal addOnPrice = Convert.ToDecimal(reader["Price"]);

                        // Display add-on with price
                        clbAddOns.Items.Add($"{addOnName} - ₱{addOnPrice:F2}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading add-ons: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToCart(string productName, int quantity, decimal price, string sugarLevel, string addOns)
        {
            decimal totalAmount = quantity * price;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Cart (ProductName, Quantity, Price, TotalAmount, SugarLevel, AddOns) VALUES (?, ?, ?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", productName);
                        command.Parameters.AddWithValue("?", quantity);
                        command.Parameters.AddWithValue("?", price);
                        command.Parameters.AddWithValue("?", totalAmount);
                        command.Parameters.AddWithValue("?", sugarLevel);
                        command.Parameters.AddWithValue("?", addOns);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Item added to cart.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item to cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetAddOnPrice(string addOnName)
        {
            decimal addOnPrice = 0;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Price FROM AddOns WHERE AddOnName = ?";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("?", addOnName);

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        addOnPrice = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching add-on price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return addOnPrice;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SPECIAL_DRINKS menuForm = new SPECIAL_DRINKS(); // Create an instance of Menu form
            this.Hide(); // Hide the current form
            menuForm.Show();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = (int)nudQuantity.Value;  // Get the quantity selected by the user
                decimal totalAmount = price * quantity;  // Start with base price and multiply by quantity
                string addOns = "";

                // Calculate the total amount based on selected add-ons
                foreach (var item in clbAddOns.CheckedItems)
                {
                    string selectedAddOn = item.ToString().Split('-')[0].Trim();
                    decimal addOnPrice = GetAddOnPrice(selectedAddOn);  // Get the price for each add-on
                    totalAmount += addOnPrice * quantity;  // Add the price of each add-on multiplied by quantity

                    // Concatenate add-ons selected for the cart
                    addOns += $"{selectedAddOn}, ";
                }

                // Remove trailing comma and space from add-ons string
                if (addOns.Length > 2)
                    addOns = addOns.Substring(0, addOns.Length - 2);

                // Add the item to the cart (with the updated total amount including add-ons)
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Cart (ProductID, ProductName, Quantity, Price, TotalAmount, AddOns) VALUES (?, ?, ?, ?, ?, ?)";
                    OleDbCommand cmd = new OleDbCommand(query, connection);
                    cmd.Parameters.AddWithValue("?", drinkId);
                    cmd.Parameters.AddWithValue("?", drinkName);
                    cmd.Parameters.AddWithValue("?", quantity);
                    cmd.Parameters.AddWithValue("?", price);  // Keep the base price
                    cmd.Parameters.AddWithValue("?", totalAmount);  // Total amount with add-ons and quantity
                    cmd.Parameters.AddWithValue("?", addOns);  // Add the selected add-ons
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"{drinkName} has been added to the cart.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding to cart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            try
            {
                // Show a confirmation dialog to the customer
                DialogResult dialogResult = MessageBox.Show(
                    "Do you want to proceed to checkout?",
                    "Confirm Purchase",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // If the customer chooses "No", return and stay on the current form
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                // Proceed if the customer confirms ("Yes")
                int quantity = (int)nudQuantity.Value;
                decimal totalAmount = price * quantity;
                string addOns = "";

                // Gather selected add-ons
                foreach (var item in clbAddOns.CheckedItems)
                {
                    string selectedAddOn = item.ToString().Split('-')[0].Trim();
                    decimal addOnPrice = GetAddOnPrice(selectedAddOn);
                    totalAmount += addOnPrice * quantity;

                    // Concatenate add-ons selected
                    addOns += $"{selectedAddOn}, ";
                }

                // Remove trailing comma and space
                if (addOns.Length > 2)
                    addOns = addOns.Substring(0, addOns.Length - 2);

                // Add the item directly to the cart
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Cart (ProductID, ProductName, Quantity, Price, TotalAmount, AddOns) VALUES (?, ?, ?, ?, ?, ?)";
                    OleDbCommand cmd = new OleDbCommand(query, connection);
                    cmd.Parameters.AddWithValue("?", drinkId);
                    cmd.Parameters.AddWithValue("?", drinkName);
                    cmd.Parameters.AddWithValue("?", quantity);
                    cmd.Parameters.AddWithValue("?", price);
                    cmd.Parameters.AddWithValue("?", totalAmount);
                    cmd.Parameters.AddWithValue("?", addOns);
                    cmd.ExecuteNonQuery();
                }

                // Navigate to the Checkout form after confirming the order
                CheckOut checkoutForm = new CheckOut();  // Create an instance of the Checkout form
                this.Hide();  // Hide the current Customization form
                checkoutForm.Show();  // Show the Checkout form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing the order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
    