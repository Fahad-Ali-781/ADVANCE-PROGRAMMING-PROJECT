namespace SalatTrack.UI
{
    partial class LogsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private OutlineButton btnFilter;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Label lblManual;
        private DarkComboBox cmbPrayerName;
        private DarkComboBox cmbStatus;
        private GoldButton btnLogPrayer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new OutlineButton();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.lblManual = new System.Windows.Forms.Label();
            this.cmbPrayerName = new DarkComboBox();
            this.cmbStatus = new DarkComboBox();
            this.btnLogPrayer = new GoldButton();

            // ── Form ──
            this.Text = "SalatTrack — Logs";
            this.Size = new System.Drawing.Size(480, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = ThemeColors.DeepNavy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ── lblTitle ──
            this.lblTitle.Text = "PRAYER HISTORY";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = ThemeColors.GoldPrimary;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(420, 28);

            // ── lblFrom ──
            this.lblFrom.Text = "From";
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblFrom.ForeColor = ThemeColors.TextMuted;
            this.lblFrom.Location = new System.Drawing.Point(20, 62);
            this.lblFrom.Size = new System.Drawing.Size(40, 20);

            // ── dtFrom ──
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Value = System.DateTime.Today.AddDays(-7);
            this.dtFrom.Location = new System.Drawing.Point(64, 58);
            this.dtFrom.Size = new System.Drawing.Size(130, 26);
            this.dtFrom.CalendarForeColor = ThemeColors.TextPrimary;
            this.dtFrom.CalendarMonthBackground = ThemeColors.CardBg;

            // ── lblTo ──
            this.lblTo.Text = "To";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblTo.ForeColor = ThemeColors.TextMuted;
            this.lblTo.Location = new System.Drawing.Point(206, 62);
            this.lblTo.Size = new System.Drawing.Size(24, 20);

            // ── dtTo ──
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Value = System.DateTime.Today;
            this.dtTo.Location = new System.Drawing.Point(234, 58);
            this.dtTo.Size = new System.Drawing.Size(130, 26);
            this.dtTo.CalendarForeColor = ThemeColors.TextPrimary;
            this.dtTo.CalendarMonthBackground = ThemeColors.CardBg;

            // ── btnFilter ──
            this.btnFilter.Text = "Filter";
            this.btnFilter.Size = new System.Drawing.Size(70, 30);
            this.btnFilter.Location = new System.Drawing.Point(376, 56);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            // ── dgvLogs ──
            this.dgvLogs.Location = new System.Drawing.Point(20, 100);
            this.dgvLogs.Size = new System.Drawing.Size(426, 340);
            this.dgvLogs.BackgroundColor = ThemeColors.SurfaceBg;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Grid colors
            this.dgvLogs.DefaultCellStyle.BackColor = ThemeColors.CardBg;
            this.dgvLogs.DefaultCellStyle.ForeColor = ThemeColors.TextPrimary;
            this.dgvLogs.DefaultCellStyle.SelectionBackColor = ThemeColors.CardBgActive;
            this.dgvLogs.DefaultCellStyle.SelectionForeColor = ThemeColors.GoldPrimary;
            this.dgvLogs.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f);

            this.dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = ThemeColors.SurfaceBg;
            this.dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = ThemeColors.TextMuted;
            this.dgvLogs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.dgvLogs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLogs.EnableHeadersVisualStyles = false;
            this.dgvLogs.GridColor = ThemeColors.BorderSubtle;

            // Columns
            this.dgvLogs.Columns.Add("Prayer", "Prayer");
            this.dgvLogs.Columns.Add("LoggedAt", "Date & Time");
            this.dgvLogs.Columns.Add("Status", "Status");

            // ── lblManual ──
            this.lblManual.Text = "Log a Prayer Manually";
            this.lblManual.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblManual.ForeColor = ThemeColors.TextMuted;
            this.lblManual.Location = new System.Drawing.Point(20, 456);
            this.lblManual.Size = new System.Drawing.Size(200, 20);

            // ── cmbPrayerName ──
            this.cmbPrayerName.Items.AddRange(new object[] { "Fajr", "Dhuhr", "Asr", "Maghrib", "Isha" });
            this.cmbPrayerName.SelectedIndex = 0;
            this.cmbPrayerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrayerName.Location = new System.Drawing.Point(20, 482);
            this.cmbPrayerName.Size = new System.Drawing.Size(120, 30);

            // ── cmbStatus ──
            this.cmbStatus.Items.AddRange(new object[] { "Prayed", "Missed", "Late" });
            this.cmbStatus.SelectedIndex = 0;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(154, 482);
            this.cmbStatus.Size = new System.Drawing.Size(120, 30);

            // ── btnLogPrayer ──
            this.btnLogPrayer.Text = "Log Prayer";
            this.btnLogPrayer.Size = new System.Drawing.Size(120, 34);
            this.btnLogPrayer.Location = new System.Drawing.Point(286, 480);
            this.btnLogPrayer.Click += new System.EventHandler(this.btnLogPrayer_Click);

            // ── Add to form ──
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.lblManual);
            this.Controls.Add(this.cmbPrayerName);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnLogPrayer);
        }
    }
}