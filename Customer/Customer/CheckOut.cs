using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace Customer
{
    public partial class CheckOut : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PreDefense.accdb";

        public CheckOut()
        {
            InitializeComponent();
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {
            // Display total amount from Cart
            lblTotalAmount.Text = $"₱{GetTotalAmount()}";
        }

        // Calculate the total amount from Cart
        private decimal GetTotalAmount()
        {
            decimal totalAmount = 0;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TotalAmount FROM Cart";
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    totalAmount += Convert.ToDecimal(reader["TotalAmount"]);
                }
            }

            return totalAmount;
        }

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmOrder_Click_1(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate a random order ID
            Random random = new Random();
            int orderId = random.Next(100000, 999999); // Generates a 6-digit random number

            string paymentMethod = cmbPaymentMethod.SelectedItem.ToString();
            decimal totalAmount = GetTotalAmount();

            StringBuilder orderSummary = new StringBuilder(); // To store order summary

            // Add the Order ID to the summary
            orderSummary.AppendLine($"Order ID: {orderId}");
            orderSummary.AppendLine("--------------------------------------------------");
            orderSummary.AppendLine("Order Summary:");
            orderSummary.AppendLine("--------------------------------------------------");

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Retrieve cart items for the customer
                string cartQuery = "SELECT ProductName, Quantity, Price FROM Cart";
                using (OleDbCommand cartCommand = new OleDbCommand(cartQuery, connection))
                using (OleDbDataReader reader = cartCommand.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Cart is empty. Please add items to the cart before confirming the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert each cart item into the Orders table and add to summary
                    while (reader.Read())
                    {
                        string productName = reader["ProductName"].ToString();
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        decimal itemTotal = quantity * price;

                        orderSummary.AppendLine($"Product: {productName}");
                        orderSummary.AppendLine($"Quantity: {quantity}");
                        orderSummary.AppendLine($"Price: ₱{price:F2}");
                        orderSummary.AppendLine($"Subtotal: ₱{itemTotal:F2}");
                        orderSummary.AppendLine("--------------------------------------------------");

                        string orderQuery = "INSERT INTO Orders (OrderID, ProductName, Quantity, Price, TotalAmount, PaymentMethod, OrderDate) VALUES (?, ?, ?, ?, ?, ?, ?)";
                        using (OleDbCommand orderCommand = new OleDbCommand(orderQuery, connection))
                        {
                            // Add parameters for each cart item
                            orderCommand.Parameters.Add("OrderID", OleDbType.Integer).Value = orderId; // Unique order ID
                            orderCommand.Parameters.Add("ProductName", OleDbType.VarChar).Value = productName; // Text/Short Text
                            orderCommand.Parameters.Add("Quantity", OleDbType.Integer).Value = quantity; // Number (Integer)
                            orderCommand.Parameters.Add("Price", OleDbType.Currency).Value = price; // Currency/Decimal
                            orderCommand.Parameters.Add("TotalAmount", OleDbType.Currency).Value = itemTotal; // Total for the product
                            orderCommand.Parameters.Add("PaymentMethod", OleDbType.VarChar).Value = paymentMethod; // Text/Short Text
                            orderCommand.Parameters.Add("OrderDate", OleDbType.Date).Value = DateTime.Now; // Date/Time

                            // Execute the order command
                            orderCommand.ExecuteNonQuery();
                        }
                    }
                }

                // Clear the cart after the order is confirmed
                string clearCartQuery = "DELETE FROM Cart";
                using (OleDbCommand clearCartCommand = new OleDbCommand(clearCartQuery, connection))
                {
                    clearCartCommand.ExecuteNonQuery();
                }
            }

            // Append total payment to the summary
            orderSummary.AppendLine($"Total Payment: ₱{totalAmount:F2}");
            orderSummary.AppendLine($"Payment Method: {paymentMethod}");
            orderSummary.AppendLine($"Date: {DateTime.Now:G}");

            // Display the order summary
            MessageBox.Show(orderSummary.ToString(), "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ConfirmOrder()
        {

        }
    }
}