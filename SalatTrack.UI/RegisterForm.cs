using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SalatTrack.DAL.Repositories;
using SalatTrack.Models;

namespace SalatTrack.UI
{
    internal partial class RegisterForm : Form
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly CityRepository _cityRepo = new CityRepository();
        private readonly NotificationSettingsRepository _notifRepo = new NotificationSettingsRepository();

        public RegisterForm()
        {
            InitializeComponent();
            this.Shown += RegisterForm_Shown;
        }

        private void RegisterForm_Shown(object? sender, System.EventArgs e)
        {
            // Load cities after the form is shown so custom combo control is fully initialized
            LoadCities();
            this.Shown -= RegisterForm_Shown;
        }

        private void LoadCities()
        {
            try
            {
                var cities = _cityRepo_GetAllSafe();

                if (cmbCity == null)
                {
                    MessageBox.Show("City dropdown control is not initialized.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmbCity.Items.Clear();
                cmbCity.DisplayMember = "CityName";
                foreach (var city in cities)
                    cmbCity.Items.Add(city);

                if (cmbCity.Items.Count > 0)
                    cmbCity.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                // Log full exception for diagnostics
                try
                {
                    string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error_log.txt");
                    System.IO.File.AppendAllText(logPath, System.DateTime.Now + " - LoadCities exception:\r\n" + ex.ToString() + "\r\n\r\n");
                }
                catch { }

                // Show full exception to user to get stack trace
                MessageBox.Show(ex.ToString(), "Could not load cities", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Ensure the combo has a valid (empty) items list to avoid further NREs
                try
                {
                    if (cmbCity != null)
                    {
                        cmbCity.Items.Clear();
                    }
                }
                catch { }
            }
        }

        // Helper to call repository safely and return empty list on error
        private System.Collections.Generic.List<City> _cityRepo_GetAllSafe()
        {
            try
            {
                var cities = _cityRepo.GetAll();
                return cities ?? new System.Collections.Generic.List<City>();
            }
            catch (System.Exception ex)
            {
                // rethrow to be handled by caller
                throw new System.Exception("Failed to retrieve cities from repository.", ex);
            }
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirmPassword.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Fill in all fields.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be 3+ characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be 6+ characters.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCity == null || cmbCity.SelectedItem == null)
            {
                MessageBox.Show("Select a city.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check username taken
                var existing = _userRepo.GetByUsername(username);
                if (existing != null)
                {
                    MessageBox.Show("Username already taken.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hash password with BCrypt
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);

                // Create user
                User newUser = new User
                {
                    Username = username,
                    PasswordHash = hashed,
                    CityID = ((City)cmbCity.SelectedItem).CityID,
                    CreatedAt = System.DateTime.Now
                };

                _userRepo.Add(newUser);

                // Fetch the new user to get their ID
                User? created = _userRepo.GetByUsername(username);
                if (created == null) throw new System.Exception("User creation failed.");

                // Seed default notification settings (5 prayers, Toast, enabled)
                string[] prayers = { "Fajr", "Dhuhr", "Asr", "Maghrib", "Isha" };
                foreach (string prayer in prayers)
                {
                    _notifRepo.Add(new NotificationSetting
                    {
                        UserID = created.UserID,
                        PrayerName = prayer,
                        AlertType = "Toast",
                        IsEnabled = true
                    });
                }

                MessageBox.Show("Account created! Please log in.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Registration error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            try
            {
                Application.Exit();
            }
            catch { }

            try
            {
                Process.GetCurrentProcess().Kill();
            }
            catch
            {
                Environment.Exit(0);
            }
        }
    }
}