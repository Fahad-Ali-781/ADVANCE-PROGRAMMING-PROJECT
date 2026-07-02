using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SalatTrack.BLL.Calculators;
using SalatTrack.BLL;
using SalatTrack.BLL.Notifications;
using SalatTrack.DAL.Repositories;
using SalatTrack.Models;

namespace SalatTrack.UI
{
    internal partial class MainForm : Form
    {
        private readonly CityRepository _cityRepo = new CityRepository();
        private readonly PrayerTimeRepository _prayerRepo = new PrayerTimeRepository();
        private readonly NotificationSettingsRepository _notifRepo = new NotificationSettingsRepository();

        private NotificationScheduler? _scheduler;
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Welcome + Hijri date
            lblWelcome.Text = $"Assalamu Alaikum, {Session.CurrentUsername}";
            lblHijriDate.Text = SalatTrack.BLL.Helpers.HijriCalendarHelper.GetTodayHijriDate();

            // Load cities into dropdown
            LoadCities();

            // Setup notification scheduler
            SetupScheduler();

            // Setup timer — ticks every 60 seconds
            _timer.Interval = 60000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void LoadCities()
        {
            try
            {
                var cities = _cityRepo.GetAll();

                // Unhook event to prevent LoadPrayerTimes() firing mid-load
                cmbCity.SelectedIndexChanged -= cmbCity_SelectedIndexChanged;

                cmbCity.DataSource = null;
                cmbCity.DataSource = cities;
                cmbCity.DisplayMember = "CityName";
                cmbCity.ValueMember = "CityID";

                // Select user's saved city
                foreach (City c in cmbCity.Items)
                {
                    if (c.CityID == Session.CurrentCityID)
                    {
                        cmbCity.SelectedItem = c;
                        break;
                    }
                }

                // Re-hook event AFTER selection is set
                cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;

                // Load prayer times once manually
                LoadPrayerTimes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load cities: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupScheduler()
        {
            try
            {
                var settings = _notifRepo.GetByUserId(Session.CurrentUserID);
                INotifiable notifier = new ToastAlert();

                if (settings.Count > 0 && settings[0].AlertType == "Audio")
                    notifier = new AudioAlert();

                _scheduler = new NotificationScheduler(notifier);
            }
            catch
            {
                // Default to toast if settings fail to load
                _scheduler = new NotificationScheduler(new ToastAlert());
            }
        }

        private void LoadPrayerTimes()
        {
            if (cmbCity.SelectedItem == null) return;

            City selectedCity = (City)cmbCity.SelectedItem;
            int cityId = selectedCity.CityID;

            DateTime today = DateTime.Today;

            try
            {
                PrayerTime? times = _prayerRepo.GetByCityAndDate(cityId, today);

                if (times == null)
                {
                    City? dbCity = _cityRepo.GetById(cityId);
                    if (dbCity == null) return;

                    MWLCalculator calc = new MWLCalculator();
                    times = calc.Calculate(dbCity, today);
                    times.CityID = cityId;
                    _prayerRepo.Add(times);
                }

                DisplayPrayerTimes(times);
                _scheduler?.CheckAndNotify(times);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load prayer times: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPrayerTimes(PrayerTime times)
        {
            lblFajr.Text = times.Fajr.ToString(@"hh\:mm");
            lblDhuhr.Text = times.Dhuhr.ToString(@"hh\:mm");
            lblAsr.Text = times.Asr.ToString(@"hh\:mm");
            lblMaghrib.Text = times.Maghrib.ToString(@"hh\:mm");
            lblIsha.Text = times.Isha.ToString(@"hh\:mm");

            HighlightCurrentPrayer(times);
        }

        private void HighlightCurrentPrayer(PrayerTime times)
        {
            // Reset all cards to default
            pnlFajr.BackColor = ThemeColors.CardBg;
            pnlDhuhr.BackColor = ThemeColors.CardBg;
            pnlAsr.BackColor = ThemeColors.CardBg;
            pnlMaghrib.BackColor = ThemeColors.CardBg;
            pnlIsha.BackColor = ThemeColors.CardBg;

            pnlFajr.BorderColor = ThemeColors.BorderSubtle;
            pnlDhuhr.BorderColor = ThemeColors.BorderSubtle;
            pnlAsr.BorderColor = ThemeColors.BorderSubtle;
            pnlMaghrib.BorderColor = ThemeColors.BorderSubtle;
            pnlIsha.BorderColor = ThemeColors.BorderSubtle;

            // Reset name label colors too
            lblFajrName.ForeColor = ThemeColors.TextMuted;
            lblDhuhrName.ForeColor = ThemeColors.TextMuted;
            lblAsrName.ForeColor = ThemeColors.TextMuted;
            lblMaghribName.ForeColor = ThemeColors.TextMuted;
            lblIshaName.ForeColor = ThemeColors.TextMuted;

            TimeSpan now = DateTime.Now.TimeOfDay;

            // Highlight the current prayer window
            if (now >= times.Fajr && now < times.Dhuhr)
                SetActiveCard(pnlFajr, lblFajrName);
            else if (now >= times.Dhuhr && now < times.Asr)
                SetActiveCard(pnlDhuhr, lblDhuhrName);
            else if (now >= times.Asr && now < times.Maghrib)
                SetActiveCard(pnlAsr, lblAsrName);
            else if (now >= times.Maghrib && now < times.Isha)
                SetActiveCard(pnlMaghrib, lblMaghribName);
            else
                SetActiveCard(pnlIsha, lblIshaName);
        }

        private void SetActiveCard(RoundedPanel panel, System.Windows.Forms.Label nameLabel)
        {
            panel.BackColor = ThemeColors.CardBgActive;
            panel.BorderColor = ThemeColors.BorderActive;
            nameLabel.ForeColor = ThemeColors.GoldPrimary;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            LoadPrayerTimes();
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCity.SelectedItem is City chosenCity)
            {
                Session.CurrentCityID = chosenCity.CityID;
                LoadPrayerTimes();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            LogsForm logsForm = new LogsForm();
            logsForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Session.Logout();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer.Stop();
            base.OnFormClosing(e);
        }
    }
}