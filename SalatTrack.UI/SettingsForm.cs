using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SalatTrack.BLL;
using SalatTrack.Models;

namespace SalatTrack.UI
{
    public partial class SettingsForm : Form
    {
        private readonly NotificationSettingsService _settingsService;

        public SettingsForm()
        {
            InitializeComponent();
            _settingsService = new NotificationSettingsService();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                var settings = _settingsService.GetByUser(Session.CurrentUserID);
                foreach (var s in settings)
                {
                    switch (s.PrayerName)
                    {
                        case "Fajr":
                            cmbFajr.SelectedItem = s.AlertType;
                            tglFajr.Checked = s.IsEnabled;
                            break;
                        case "Dhuhr":
                            cmbDhuhr.SelectedItem = s.AlertType;
                            tglDhuhr.Checked = s.IsEnabled;
                            break;
                        case "Asr":
                            cmbAsr.SelectedItem = s.AlertType;
                            tglAsr.Checked = s.IsEnabled;
                            break;
                        case "Maghrib":
                            cmbMaghrib.SelectedItem = s.AlertType;
                            tglMaghrib.Checked = s.IsEnabled;
                            break;
                        case "Isha":
                            cmbIsha.SelectedItem = s.AlertType;
                            tglIsha.Checked = s.IsEnabled;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load settings: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = new List<NotificationSetting>
                {
                    new NotificationSetting { UserID = Session.CurrentUserID, PrayerName = "Fajr",    AlertType = cmbFajr.SelectedItem?.ToString()    ?? "Toast", IsEnabled = tglFajr.Checked    },
                    new NotificationSetting { UserID = Session.CurrentUserID, PrayerName = "Dhuhr",   AlertType = cmbDhuhr.SelectedItem?.ToString()   ?? "Toast", IsEnabled = tglDhuhr.Checked   },
                    new NotificationSetting { UserID = Session.CurrentUserID, PrayerName = "Asr",     AlertType = cmbAsr.SelectedItem?.ToString()     ?? "Toast", IsEnabled = tglAsr.Checked     },
                    new NotificationSetting { UserID = Session.CurrentUserID, PrayerName = "Maghrib", AlertType = cmbMaghrib.SelectedItem?.ToString() ?? "Toast", IsEnabled = tglMaghrib.Checked },
                    new NotificationSetting { UserID = Session.CurrentUserID, PrayerName = "Isha",    AlertType = cmbIsha.SelectedItem?.ToString()    ?? "Toast", IsEnabled = tglIsha.Checked    },
                };

                _settingsService.SaveAll(Session.CurrentUserID, settings);
                MessageBox.Show("Settings saved!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}