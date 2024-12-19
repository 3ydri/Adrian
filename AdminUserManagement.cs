using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;


namespace PreDefense
{
    public partial class AdminUserManagement : Form
    {
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;
        private DataTable dt;

        public AdminUserManagement()
        {         
            InitializeComponent();
            InitializeDatabase();
            GetUsers();

        }
        private void InitializeDatabase()
        {
            // Initialize the connection
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
            conn = new OleDbConnection(connString);
        }

        // Method to retrieve and display users from the database
        private void GetUsers()
        {
            // Load data from the database into the DataGridView
            string query = "SELECT * FROM UserProfile";
            adapter = new OleDbDataAdapter(query, conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dgvEmployee.DataSource = dt;

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbEmployeeID.Text) ||
                string.IsNullOrWhiteSpace(tbFirstname.Text) ||
                string.IsNullOrWhiteSpace(cbRole.Text) ||
                string.IsNullOrWhiteSpace(cbGender.Text) ||
                string.IsNullOrWhiteSpace(tbRePassword.Text) ||
                string.IsNullOrWhiteSpace(tbLastname.Text) ||
                string.IsNullOrWhiteSpace(tbEmail.Text) ||
                string.IsNullOrWhiteSpace(tbContactNo.Text) ||
                string.IsNullOrWhiteSpace(tbUsername.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text) ||
                string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void AdminUserManagement_Load(object sender, EventArgs e)
        {
            dgvEmployee.Columns["ID"].Visible = false;
            GetUsers();


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

        private void btnInventory_Click(object sender, EventArgs e)
        {
            AdminInventory newForm = new AdminInventory();
            newForm.FormClosed += (s, args) => this.Show();
            newForm.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEmployee.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            string query = "UPDATE UserProfile SET EmployeeID = @EmployeeID, Username = @Username, Firstname = @Firstname, Lastname = @Lastname, " +
                           "Email = @Email, Address = @Address, Role = @Role, Gender = @Gender, ContactNo = @ContactNo, [Password] = @Password " +
                           "WHERE ID = @ID";

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb"))
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", tbEmployeeID.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Firstname", tbFirstname.Text.Trim());
                cmd.Parameters.AddWithValue("@Lastname", tbLastname.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", tbAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Role", cbRole.SelectedItem?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@Gender", cbGender.SelectedItem?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@ContactNo", tbContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", tbPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@ID", dgvEmployee.CurrentRow.Cells["ID"].Value); // Replace "ID" with the correct column name if needed.

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            GetUsers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvEmployee.CurrentRow == null)
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM UserProfile WHERE EmployeeID = @EmployeeID";
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", dgvEmployee.CurrentRow.Cells["EmployeeID"].Value);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            GetUsers();
        }


        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pbUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string query = "INSERT INTO UserProfile (EmployeeID, Username, Firstname, Lastname, Email, Address, Role, Gender, ContactNo, [Password]) " +
                           "VALUES (@EmployeeID, @Username, @Firstname, @Lastname, @Email, @Address, @Role, @Gender, @ContactNo, @Password)";

            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", tbEmployeeID.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Firstname", tbFirstname.Text.Trim());
                cmd.Parameters.AddWithValue("@Lastname", tbLastname.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", tbAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Role", cbRole.SelectedItem?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@Gender", cbGender.SelectedItem?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@ContactNo", tbContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", tbPassword.Text.Trim());

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inserting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

            GetUsers();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbEmployeeID.Clear();
            tbFirstname.Clear();
            tbLastname.Clear();
            tbEmail.Clear();
            tbAddress.Clear();
            cbRole.SelectedIndex = -1; // Deselect combo box item
            cbGender.SelectedIndex = -1; // Deselect combo box item
            tbContactNo.Clear();
            tbUsername.Clear();
            tbPassword.Clear();
            tbRePassword.Clear();

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];

            tbEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
            tbFirstname.Text = row.Cells["Firstname"].Value.ToString();
            tbLastname.Text = row.Cells["Lastname"].Value.ToString();
            tbEmail.Text = row.Cells["Email"].Value.ToString();
            tbAddress.Text = row.Cells["Address"].Value.ToString();
            cbRole.Text = row.Cells["Role"].Value.ToString();
            cbGender.Text = row.Cells["Gender"].Value.ToString();
            tbContactNo.Text = row.Cells["ContactNo"].Value.ToString();
            tbUsername.Text = row.Cells["Username"].Value.ToString();
            tbPassword.Text = row.Cells["Password"].Value.ToString();



        }


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM UserProfile WHERE EmployeeID LIKE @EmployeeID";
            adapter = new OleDbDataAdapter(searchQuery, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@EmployeeID", tbSearch.Text + "%");

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

            dgvEmployee.DataSource = dt;
        }


        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];

            tbEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
            tbFirstname.Text = row.Cells["Firstname"].Value.ToString();
            tbLastname.Text = row.Cells["Lastname"].Value.ToString();
            tbEmail.Text = row.Cells["Email"].Value.ToString();
            tbAddress.Text = row.Cells["Address"].Value.ToString();
            cbRole.Text = row.Cells["Role"].Value.ToString();
            cbGender.Text = row.Cells["Gender"].Value.ToString();
            tbContactNo.Text = row.Cells["ContactNo"].Value.ToString();
            tbUsername.Text = row.Cells["Username"].Value.ToString();
            tbPassword.Text = row.Cells["Password"].Value.ToString();

        }



        private void label14_Click(object sender, EventArgs e)
        {
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void CTtb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;  
            }
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            
        }
    }
}