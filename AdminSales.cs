using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class AdminSales : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;
        private int currentPrintRowIndex = 0;
        private PrintPreviewDialog printPreviewDialogSales;
        private PrintDocument printDocumentSales;

        public AdminSales()
        {
            InitializeDatabase();
            InitializeComponent();
            InitializePrinting();
            LoadData();
        }

        private void InitializeDatabase()
        {
            // Initialize the database connection
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
            conn = new OleDbConnection(connString);
        }

        private void InitializePrinting()
        {
            // Initialize print document and preview dialog
            printDocumentSales = new PrintDocument();
            printDocumentSales.PrintPage += printDocumentSales_PrintPage;

            printPreviewDialogSales = new PrintPreviewDialog
            {
                Document = printDocumentSales,
                Width = 800,
                Height = 600
            };
        }

        private void LoadData()
        {
            // Load data from the database into the DataGridView
            string query = "SELECT OrderID, OrderDate, ProductName, Quantity, Price FROM Orders";
            adapter = new OleDbDataAdapter(query, conn);
            dt = new DataTable();
            adapter.Fill(dt);

            // Add a calculated 'Total' column
            dt.Columns.Add("Total", typeof(decimal), "Quantity * Price");

            dgvSales.DataSource = dt;
        }


        private void btnSales_Click(object sender, EventArgs e)
        {
            AdminHome newForm = new AdminHome();
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

        private void AdminSales_Load(object sender, EventArgs e)
        {
            // Hide specific columns if necessary
            if (dgvSales.Columns.Contains("ProductID"))
            {
                dgvSales.Columns["ProductID"].Visible = false;
            }
        }

        private void btnStaff_Click_1(object sender, EventArgs e)
        {
            AdminUserManagement newForm = new AdminUserManagement();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "SELECT OrderID, OrderDate, ProductName, Quantity, Price, Quantity * Price AS Total FROM Orders WHERE OrderDate LIKE ?";
            adapter = new OleDbDataAdapter(searchQuery, conn);
            adapter.SelectCommand.Parameters.AddWithValue("?", tbSearch.Text + "%");

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

            dgvSales.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset the print row index before printing
                currentPrintRowIndex = 0;

                // Show the Print Preview dialog
                printPreviewDialogSales.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocumentSales_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print header
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 10);
            float yPos = e.MarginBounds.Top;

            e.Graphics.DrawString("Sales Report", headerFont, Brushes.Black, e.MarginBounds.Left, yPos);
            yPos += headerFont.GetHeight(e.Graphics) + 10;

            // Print column headers
            string headers = "Order ID   Date         Product Name     Quantity    Price    Total";
            e.Graphics.DrawString(headers, bodyFont, Brushes.Black, e.MarginBounds.Left, yPos);
            yPos += bodyFont.GetHeight(e.Graphics) + 5;

            // Print rows
            while (currentPrintRowIndex < dt.Rows.Count)
            {
                DataRow row = dt.Rows[currentPrintRowIndex];

                try
                {
                    string line = $"{row["OrderID"],-10} {row["OrderDate"],-12} {row["ProductName"],-20} {row["Quantity"],-10} {row["Price"],-8:C} {row["Total"],-8:C}";
                    e.Graphics.DrawString(line, bodyFont, Brushes.Black, e.MarginBounds.Left, yPos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error printing row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                yPos += bodyFont.GetHeight(e.Graphics) + 5;

                if (yPos >= e.MarginBounds.Bottom)
                {
                    // If the page is full, break and print the next page
                    e.HasMorePages = true;
                    return;
                }

                currentPrintRowIndex++;
            }

            // If all rows are printed, finish
            e.HasMorePages = false;
        }
    }
}
