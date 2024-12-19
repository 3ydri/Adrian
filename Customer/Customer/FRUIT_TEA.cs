using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Customer
{
    public partial class FRUIT_TEA : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PreDefense.accdb";

        public FRUIT_TEA()
        {
            InitializeComponent();
        }

        private void btnSpecialDrinks_Click(object sender, EventArgs e)
        {
            SPECIAL_DRINKS newForm = new SPECIAL_DRINKS();
            this.Close();
            newForm.Show();
        }

        private void btnMilkTea_Click(object sender, EventArgs e)
        {
            MILK_TEA newForm = new MILK_TEA();
            this.Close();
            newForm.Show();
        }

        private void btnFruitTea_Click(object sender, EventArgs e)
        {
            // This method is not implemented, you can add functionality if needed
        }

        private void FRUIT_TEA_Load(object sender, EventArgs e)
        {
            LoadFruitTea();
        }

        private void LoadFruitTea()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ID, DrinkName, Price, Image, [Stock Status] AS StockStatus FROM FruitTea ";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    flowLayoutPanel11.Controls.Clear();
                    flowLayoutPanel11.FlowDirection = FlowDirection.LeftToRight;
                    flowLayoutPanel11.WrapContents = true;
                    flowLayoutPanel11.AutoScroll = true;

                    while (reader.Read())
                    {
                        int drinkId = reader.GetInt32(0);
                        string drinkName = reader["DrinkName"]?.ToString() ?? "Unnamed Product";
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        string imagePath = reader["Image"]?.ToString();
                        string stockStatus = reader["StockStatus"]?.ToString() ?? "In Stock";

                        Panel productPanel = new Panel
                        {
                            Width = 205,
                            Height = 300,
                            Margin = new Padding(10),
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        PictureBox pictureBox = new PictureBox
                        {
                            Width = productPanel.Width,
                            Height = 200,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Cursor = Cursors.Hand,
                            Dock = DockStyle.Top
                        };

                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                        {
                            pictureBox.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            pictureBox.Image = Properties.Resources.bg; // Default image
                        }

                        pictureBox.Click += (s, e) =>
                        {
                            // Show confirmation dialog asking if the user wants to buy the product
                            DialogResult dialogResult = MessageBox.Show(
                                "Do you want to buy this product?",
                                "Proceed to Customization",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {
                                // If the user clicks "Yes", proceed to the Customization form
                                Customization customizationForm = new Customization(drinkId, drinkName, price);
                                this.Hide(); // Optionally hide the current form
                                customizationForm.ShowDialog();
                                this.Show(); // Optionally show the current form again after customization
                            }
                            // If the user clicks "No", do nothing and stay on the current form
                        };

                        Label nameLabel = new Label
                        {
                            Text = drinkName,
                            AutoSize = false,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Height = 40,
                            Dock = DockStyle.Top,
                            Font = new Font("Tahoma", 11, FontStyle.Regular),
                        };

                        Label priceLabel = new Label
                        {
                            Text = $"₱{price:F2}",
                            AutoSize = false,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Top,
                            Font = new Font("Tahoma", 10, FontStyle.Bold)
                        };

                        productPanel.Controls.Add(priceLabel);
                        productPanel.Controls.Add(pictureBox);
                        productPanel.Controls.Add(nameLabel);

                        flowLayoutPanel11.Controls.Add(productPanel);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PurchaseProduct(int ID, string drinkName, decimal price)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string queryGetCount = "SELECT Stocks FROM FruitTea WHERE ID = ?";
                    OleDbCommand getCountCommand = new OleDbCommand(queryGetCount, connection);
                    getCountCommand.Parameters.AddWithValue("@ID", ID);
                    int currentCount = Convert.ToInt32(getCountCommand.ExecuteScalar());

                    int newCount = currentCount + 1;

                    if (newCount >= 100)
                    {
                        string queryOutOfStock = "UPDATE FruitTea SET [Stock Status] = 'Out of Stock', Stocks = ? WHERE ID = ?";
                        OleDbCommand outOfStockCommand = new OleDbCommand(queryOutOfStock, connection);
                        outOfStockCommand.Parameters.AddWithValue("@NewCount", newCount);
                        outOfStockCommand.Parameters.AddWithValue("@ID", ID);
                        outOfStockCommand.ExecuteNonQuery();

                        MessageBox.Show(
                            $"The product {drinkName} is now out of stock as it has been purchased 100 times.",
                            "Out of Stock",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        string queryUpdateCount = "UPDATE FruitTea SET Stocks = ? WHERE ID = ?";
                        OleDbCommand updateCountCommand = new OleDbCommand(queryUpdateCount, connection);
                        updateCountCommand.Parameters.AddWithValue("@NewCount", newCount);
                        updateCountCommand.Parameters.AddWithValue("@ID", ID);
                        updateCountCommand.ExecuteNonQuery();

                        MessageBox.Show(
                            $"You have successfully purchased {drinkName} for ₱{price:F2}. Total purchases: {newCount}/100.",
                            "Purchase Completed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    }

                    LoadFruitTea();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pbCart_Click(object sender, EventArgs e)
        {
            Cart newForm = new Cart();
            this.Close();
            newForm.Show();
        }
    }
}
