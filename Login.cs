using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PreDefense
{
    public partial class Login : Form
    {
        private OleDbConnection conn;
        private int loginAttempts = 0; // Track failed login attempts
        private Timer lockoutTimer;
        private int lockoutTimeRemaining = 60; // Lockout duration in seconds

        public Login()
        {
            InitializeComponent();

            // Initialize the lockout timer
            lockoutTimer = new Timer();
            lockoutTimer.Interval = 1000; // Timer ticks every second
            lockoutTimer.Tick += LockoutTimer_Tick;

            lblTimer.Text = ""; // Initially, the lockout timer is not displayed
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (loginAttempts >= 3)
            {
                MessageBox.Show("Too many failed attempts. Please wait for the lockout timer to expire.");
                return;
            }

            // SQL query using positional parameters for security
            string query = "SELECT [Role] FROM UserProfile WHERE Username = ? AND [Password] = ?";
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PreDefense.accdb");

            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("?", tbUsername.Text.Trim());
                cmd.Parameters.AddWithValue("?", tbPassword.Text.Trim());

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Login successful, retrieve user role
                        string userRole = result.ToString();
                        this.Hide();

                        if (userRole == "Admin")
                        {
                            AdminHome adminForm = new AdminHome();
                            adminForm.Show();
                        }
                        else if (userRole == "Staff")
                        {
                            StaffHome staffForm = new StaffHome();
                            staffForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Unrecognized user type.");
                            this.Show();
                        }
                    }
                    else
                    {
                        // Invalid login
                        loginAttempts++;
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (loginAttempts >= 3)
                        {
                            StartLockout();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = cbShowPassword.Checked ? '\0' : '•'; // Toggle password visibility
        }

        private void StartLockout()
        {
            // Disable login controls
            btnLogin.Enabled = false;
            tbUsername.Enabled = false;
            tbPassword.Enabled = false;

            lockoutTimeRemaining = 60; // Reset lockout time
            lblTimer.Text = "Lockout Time: 01:00"; // Initial lockout display
            lblTimer.Visible = true; // Make the timer label visible
            lockoutTimer.Start();

            MessageBox.Show("Too many failed login attempts. Please wait 1 minute before trying again.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LockoutTimer_Tick(object sender, EventArgs e)
        {
            lockoutTimeRemaining--;

            TimeSpan timeLeft = TimeSpan.FromSeconds(lockoutTimeRemaining);
            lblTimer.Text = $"Lockout Time: {timeLeft:mm\\:ss}"; // Update the timer display

            if (lockoutTimeRemaining <= 0)
            {
                lockoutTimer.Stop();

                // Reset login attempts and re-enable controls
                loginAttempts = 0;
                btnLogin.Enabled = true;
                tbUsername.Enabled = true;
                tbPassword.Enabled = true;

                lblTimer.Text = ""; // Clear timer display
                lblTimer.Visible = false; // Hide the timer label
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblTimer.Visible = false; // Hide timer label at startup
        }
    }
}
