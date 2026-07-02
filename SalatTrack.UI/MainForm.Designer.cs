namespace SalatTrack.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Top bar
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblHijriDate;

        // City dropdown
        private DarkComboBox cmbCity;

        // Prayer cards (panels)
        private RoundedPanel pnlFajr;
        private RoundedPanel pnlDhuhr;
        private RoundedPanel pnlAsr;
        private RoundedPanel pnlMaghrib;
        private RoundedPanel pnlIsha;

        // Prayer name labels (inside cards)
        private System.Windows.Forms.Label lblFajrName;
        private System.Windows.Forms.Label lblDhuhrName;
        private System.Windows.Forms.Label lblAsrName;
        private System.Windows.Forms.Label lblMaghribName;
        private System.Windows.Forms.Label lblIshaName;

        // Prayer time labels (inside cards)
        private System.Windows.Forms.Label lblFajr;
        private System.Windows.Forms.Label lblDhuhr;
        private System.Windows.Forms.Label lblAsr;
        private System.Windows.Forms.Label lblMaghrib;
        private System.Windows.Forms.Label lblIsha;

        // Bottom nav buttons
        private OutlineButton btnSettings;
        private OutlineButton btnLogs;
        private OutlineButton btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Instantiate ALL controls ──────────────────────────────────
            lblWelcome = new System.Windows.Forms.Label();
            lblHijriDate = new System.Windows.Forms.Label();
            cmbCity = new DarkComboBox();

            pnlFajr = new RoundedPanel();
            pnlDhuhr = new RoundedPanel();
            pnlAsr = new RoundedPanel();
            pnlMaghrib = new RoundedPanel();
            pnlIsha = new RoundedPanel();

            lblFajrName = new System.Windows.Forms.Label();
            lblDhuhrName = new System.Windows.Forms.Label();
            lblAsrName = new System.Windows.Forms.Label();
            lblMaghribName = new System.Windows.Forms.Label();
            lblIshaName = new System.Windows.Forms.Label();

            lblFajr = new System.Windows.Forms.Label();
            lblDhuhr = new System.Windows.Forms.Label();
            lblAsr = new System.Windows.Forms.Label();
            lblMaghrib = new System.Windows.Forms.Label();
            lblIsha = new System.Windows.Forms.Label();

            btnSettings = new OutlineButton();
            btnLogs = new OutlineButton();
            btnLogout = new OutlineButton();

            SuspendLayout();

            // ── lblWelcome ────────────────────────────────────────────────
            lblWelcome.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblWelcome.ForeColor = System.Drawing.Color.FromArgb(122, 132, 153);
            lblWelcome.Location = new System.Drawing.Point(20, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new System.Drawing.Size(360, 20);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Assalamu Alaikum,";

            // ── lblHijriDate ──────────────────────────────────────────────
            lblHijriDate.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            lblHijriDate.ForeColor = System.Drawing.Color.FromArgb(212, 169, 74);
            lblHijriDate.Location = new System.Drawing.Point(20, 42);
            lblHijriDate.Name = "lblHijriDate";
            lblHijriDate.Size = new System.Drawing.Size(364, 22);
            lblHijriDate.TabIndex = 1;
            lblHijriDate.Text = "...";

            // ── cmbCity ───────────────────────────────────────────────────
            cmbCity.BackColor = System.Drawing.Color.FromArgb(19, 27, 42);
            cmbCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmbCity.Font = new System.Drawing.Font("Segoe UI", 9f);
            cmbCity.ForeColor = System.Drawing.Color.FromArgb(212, 169, 74);
            cmbCity.ItemHeight = 24;
            cmbCity.Location = new System.Drawing.Point(20, 75);
            cmbCity.Name = "cmbCity";
            cmbCity.Size = new System.Drawing.Size(360, 30);
            cmbCity.TabIndex = 2;
            cmbCity.SelectedIndexChanged += new System.EventHandler(cmbCity_SelectedIndexChanged);

            // ── Prayer cards — BuildPrayerCard positions them correctly ───
            BuildPrayerCard(pnlFajr, lblFajrName, lblFajr, "Fajr", 115);
            BuildPrayerCard(pnlDhuhr, lblDhuhrName, lblDhuhr, "Dhuhr", 191);
            BuildPrayerCard(pnlAsr, lblAsrName, lblAsr, "Asr", 267);
            BuildPrayerCard(pnlMaghrib, lblMaghribName, lblMaghrib, "Maghrib", 343);
            BuildPrayerCard(pnlIsha, lblIshaName, lblIsha, "Isha", 419);

            // ── btnSettings ───────────────────────────────────────────────
            btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSettings.Font = new System.Drawing.Font("Segoe UI", 9f);
            btnSettings.ForeColor = System.Drawing.Color.FromArgb(212, 169, 74);
            btnSettings.Location = new System.Drawing.Point(20, 510);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new System.Drawing.Size(108, 36);
            btnSettings.TabIndex = 10;
            btnSettings.Text = "⚙ Settings";
            btnSettings.Click += new System.EventHandler(btnSettings_Click);

            // ── btnLogs ───────────────────────────────────────────────────
            btnLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogs.Font = new System.Drawing.Font("Segoe UI", 9f);
            btnLogs.ForeColor = System.Drawing.Color.FromArgb(212, 169, 74);
            btnLogs.Location = new System.Drawing.Point(148, 510);
            btnLogs.Name = "btnLogs";
            btnLogs.Size = new System.Drawing.Size(108, 36);
            btnLogs.TabIndex = 11;
            btnLogs.Text = "📋 Logs";
            btnLogs.Click += new System.EventHandler(btnLogs_Click);

            // ── btnLogout ─────────────────────────────────────────────────
            btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogout.Font = new System.Drawing.Font("Segoe UI", 9f);
            btnLogout.ForeColor = System.Drawing.Color.FromArgb(212, 169, 74);
            btnLogout.Location = new System.Drawing.Point(276, 510);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new System.Drawing.Size(104, 36);
            btnLogout.TabIndex = 12;
            btnLogout.Text = "🚪 Logout";
            btnLogout.Click += new System.EventHandler(btnLogout_Click);

            // ── MainForm ──────────────────────────────────────────────────
            BackColor = System.Drawing.Color.FromArgb(13, 17, 23);
            ClientSize = new System.Drawing.Size(404, 570);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SalatTrack";
            Load += new System.EventHandler(MainForm_Load);

            // ── Add controls to form ──────────────────────────────────────
            Controls.Add(lblWelcome);
            Controls.Add(lblHijriDate);
            Controls.Add(cmbCity);
            Controls.Add(pnlFajr);
            Controls.Add(pnlDhuhr);
            Controls.Add(pnlAsr);
            Controls.Add(pnlMaghrib);
            Controls.Add(pnlIsha);
            Controls.Add(btnSettings);
            Controls.Add(btnLogs);
            Controls.Add(btnLogout);

            ResumeLayout(false);
        }

        /// <summary>
        /// Builds a prayer card panel with name label on left, time label on right.
        /// </summary>
        private void BuildPrayerCard(
            RoundedPanel panel,
            System.Windows.Forms.Label nameLabel,
            System.Windows.Forms.Label timeLabel,
            string prayerName,
            int yPos)
        {
            panel.BackColor = ThemeColors.CardBg;
            panel.BorderColor = ThemeColors.BorderSubtle;
            panel.CornerRadius = 12;
            panel.Size = new System.Drawing.Size(360, 66);
            panel.Location = new System.Drawing.Point(20, yPos);

            nameLabel.Text = prayerName;
            nameLabel.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            nameLabel.ForeColor = ThemeColors.TextMuted;
            nameLabel.Location = new System.Drawing.Point(16, 22);
            nameLabel.Size = new System.Drawing.Size(120, 24);

            timeLabel.Text = "--:--";
            timeLabel.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            timeLabel.ForeColor = ThemeColors.GoldPrimary;
            timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            timeLabel.Location = new System.Drawing.Point(200, 18);
            timeLabel.Size = new System.Drawing.Size(140, 30);

            panel.Controls.Add(nameLabel);
            panel.Controls.Add(timeLabel);
        }
    }
}