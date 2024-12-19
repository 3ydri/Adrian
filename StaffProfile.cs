using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class StaffProfile : Form
    {
        private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb";
        private readonly string userId;

        public StaffProfile()
        {
            InitializeComponent();
            userId = GetLoggedInUserId();
        }

       
        private string GetLoggedInUserId()
        {
            return "17"; 
        }

       
        private void AdmProfile_Load(object sender, EventArgs e)
        {
            LoadProfileData();
            ToggleEditing(false); 
        }

       
        private void LoadProfileData()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM UserProfile WHERE ID = @ID";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", userId);

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            
                            tbFirstname.Text = dt.Rows[0]["Firstname"].ToString();
                            tbLastname.Text = dt.Rows[0]["Lastname"].ToString();
                            tbEmail.Text = dt.Rows[0]["Email"].ToString();
                            tbUsername.Text = dt.Rows[0]["Username"].ToString();
                            tbAddress.Text = dt.Rows[0]["Address"].ToString();
                            cbGender.Text = dt.Rows[0]["Gender"].ToString();
                            tbContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Profile data not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Toggle editing mode
        private void ToggleEditing(bool isEditing)
        {
            tbFirstname.ReadOnly = !isEditing;
            tbLastname.ReadOnly = !isEditing;
            tbEmail.ReadOnly = !isEditing;
            tbAddress.ReadOnly = !isEditing;
            cbGender.Enabled = isEditing;
            tbContactNo.ReadOnly = !isEditing;
            tbUsername.ReadOnly = !isEditing;
            btnSave.Visible = isEditing;
            btnCancel.Visible = isEditing;
            btnEditProfile.Visible = !isEditing;
            btnLogout.Visible = !isEditing;
        }

        // Event: Edit Profile button clicked
        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            ToggleEditing(true); // Enable editing mode
        }

        // Event: Save button clicked
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                DialogResult result = MessageBox.Show("Do you want to save the changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveProfileData();
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Save updated profile data to the database
        private void SaveProfileData()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "UPDATE UserProfile SET Firstname = @Firstname, Lastname = @Lastname, Email = @Email, Username = @Username, Address = @Address, Gender = @Gender, ContactNo = @ContactNo WHERE ID = @ID";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@Firstname", tbFirstname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Lastname", tbLastname.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Username", tbUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", tbAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNo", tbContactNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", userId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ToggleEditing(false); // Return to view mode
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving profile data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Cancel button clicked
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to discard the changes?", "Cancel Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                LoadProfileData(); // Reload original data
                ToggleEditing(false); // Return to view mode
            }
        }

        // Input validation
        private bool ValidateInputs()
        {
            return !string.IsNullOrWhiteSpace(tbFirstname.Text) &&
                   !string.IsNullOrWhiteSpace(tbEmail.Text) &&
                   !string.IsNullOrWhiteSpace(tbUsername.Text) &&
                   !string.IsNullOrWhiteSpace(tbAddress.Text) &&
                   !string.IsNullOrWhiteSpace(cbGender.Text) &&
                   !string.IsNullOrWhiteSpace(tbContactNo.Text);
        }

        // Event: Logout button clicked
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("You have logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Redirect to the Login form
                Login loginForm = new Login();
                loginForm.Show();
                this.Close(); // Close the current form
            }
        }

        private void CTtb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AdminUserManagement newForm = new AdminUserManagement();
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
    }
}
