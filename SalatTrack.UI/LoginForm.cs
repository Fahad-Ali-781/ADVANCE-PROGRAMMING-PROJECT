using System;
using System.Windows.Forms;
using System.Diagnostics;
using SalatTrack.DAL.Repositories;
using SalatTrack.Models;

namespace SalatTrack.UI
{
    internal partial class LoginForm : Form
    {
        private readonly UserRepository _userRepo = new UserRepository();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username and password.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Fetch user from DB
                User? user = _userRepo.GetByUsername(username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set session
                Session.CurrentUserID = user.UserID;
                Session.CurrentCityID = user.CityID ?? 0;
                Session.CurrentUsername = user.Username;

                // Open main form
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        // Ensures the application process is terminated when user closes the login form
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Try graceful shutdown
                Application.Exit();
            }
            catch { }

            try
            {
                // ForceTerminate if still running
                Process.GetCurrentProcess().Kill();
            }
            catch
            {
                Environment.Exit(0);
            }
        }
    }
}