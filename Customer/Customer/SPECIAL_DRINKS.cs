using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Customer
{
    public partial class SPECIAL_DRINKS : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
        private List<int> cartItems = new List<int>(); // List to store added-to-cart DrinkIDs

        public SPECIAL_DRINKS()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            LoadSpecial();
        }

        private void LoadSpecial()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DrinkID, DrinkName, Price, Image, [Stock Status] AS StockStatus FROM SPECIALDRINKS WHERE Category = 'SpecialDrinks'";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                    flowLayoutPanel1.WrapContents = true;
                    flowLayoutPanel1.AutoScroll = true;

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

                        flowLayoutPanel1.Controls.Add(productPanel);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PurchaseProduct(int drinkId, string drinkName, decimal price)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string queryGetCount = "SELECT Stocks FROM SPECIALDRINKS WHERE DrinkID = ?";
                    OleDbCommand getCountCommand = new OleDbCommand(queryGetCount, connection);
                    getCountCommand.Parameters.AddWithValue("@DrinkID", drinkId);
                    int currentCount = Convert.ToInt32(getCountCommand.ExecuteScalar());

                    int newCount = currentCount + 1;

                    if (newCount >= 100)
                    {
                        string queryOutOfStock = "UPDATE SPECIALDRINKS SET [Stock Status] = 'Out of Stock', Stocks = ? WHERE DrinkID = ?";
                        OleDbCommand outOfStockCommand = new OleDbCommand(queryOutOfStock, connection);
                        outOfStockCommand.Parameters.AddWithValue("@NewCount", newCount);
                        outOfStockCommand.Parameters.AddWithValue("@DrinkID", drinkId);
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
                        string queryUpdateCount = "UPDATE SPECIALDRINKS SET Stocks = ? WHERE DrinkID = ?";
                        OleDbCommand updateCountCommand = new OleDbCommand(queryUpdateCount, connection);
                        updateCountCommand.Parameters.AddWithValue("@NewCount", newCount);
                        updateCountCommand.Parameters.AddWithValue("@DrinkID", drinkId);
                        updateCountCommand.ExecuteNonQuery();

                        MessageBox.Show(
                            $"You have successfully purchased {drinkName} for ₱{price:F2}. Total purchases: {newCount}/100.",
                            "Purchase Completed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    }

                    LoadSpecial();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 



private void btnMilkTea_Click(object sender, EventArgs e)
        {
            MILK_TEA newForm = new MILK_TEA();
            this.Close();
            newForm.Show();
        }

        private void btnFruitTea_Click(object sender, EventArgs e)
        {
            FRUIT_TEA newForm = new FRUIT_TEA();
            this.Close();
            newForm.Show();
        }

        private void pbCart_Click(object sender, EventArgs e)
        {
            Cart newForm = new Cart();
            this.Close();
            newForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
