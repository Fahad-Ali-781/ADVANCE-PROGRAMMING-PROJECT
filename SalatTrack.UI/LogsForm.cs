using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SalatTrack.DAL.Repositories;
using SalatTrack.Models;

namespace SalatTrack.UI
{
    internal partial class LogsForm : Form
    {
        private readonly LogRepository _logRepo = new LogRepository();

        public LogsForm()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                DateTime from = dtFrom.Value.Date;
                DateTime to = dtTo.Value.Date;

                var logs = _logRepo.GetByUserAndDateRange(Session.CurrentUserID, from, to);

                dgvLogs.Rows.Clear();

                foreach (var log in logs)
                {
                    dgvLogs.Rows.Add(
                        log.PrayerName,
                        log.LoggedAt.ToString("MMM dd, yyyy  hh:mm tt"),
                        log.Status
                    );

                    // Color the status cell
                    var row = dgvLogs.Rows[dgvLogs.Rows.Count - 1];
                    var statusCell = row.Cells[2];

                    statusCell.Style.ForeColor = log.Status switch
                    {
                        "Prayed" => ThemeColors.StatusPrayed,
                        "Missed" => ThemeColors.StatusMissed,
                        "Late" => ThemeColors.StatusLate,
                        _ => ThemeColors.TextPrimary
                    };

                    statusCell.Style.BackColor = log.Status switch
                    {
                        "Prayed" => ThemeColors.StatusPrayedBg,
                        "Missed" => ThemeColors.StatusMissedBg,
                        "Late" => ThemeColors.StatusLateBg,
                        _ => ThemeColors.CardBg
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnLogPrayer_Click(object sender, EventArgs e)
        {
            if (cmbPrayerName.SelectedItem == null || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Select prayer and status.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Log newLog = new Log
                {
                    UserID = Session.CurrentUserID,
                    PrayerName = cmbPrayerName.SelectedItem.ToString()!,
                    Status = cmbStatus.SelectedItem.ToString()!,
                    LoggedAt = DateTime.Now
                };

                _logRepo.Add(newLog);

                MessageBox.Show("Prayer logged.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not log prayer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}