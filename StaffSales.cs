using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class StaffSales : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;
        private int currentPrintRowIndex = 0;
        private PrintPreviewDialog printPreviewDialogSales;
        private PrintDocument printDocumentSales;


        public StaffSales()
        {
            InitializeDatabase();
            InitializeComponent();
            InitializePrinting();
            LoadData();

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

        private void InitializeDatabase()
        {
            // Initialize the connection
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
            conn = new OleDbConnection(connString);
        }
        private void LoadData()
        {
            // Load data from the database into the DataGridView
            string query = "SELECT * FROM Orders";
            adapter = new OleDbDataAdapter(query, conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dgvSales.DataSource = dt;
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

        private void StaffSales_Load(object sender, EventArgs e)
        {
            dgvSales.Columns["ProductID"].Visible = false;
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

       
