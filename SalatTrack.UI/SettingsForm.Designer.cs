namespace SalatTrack.UI
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFajr, lblDhuhr, lblAsr, lblMaghrib, lblIsha;
        private DarkComboBox cmbFajr, cmbDhuhr, cmbAsr, cmbMaghrib, cmbIsha;
        private ToggleSwitch tglFajr, tglDhuhr, tglAsr, tglMaghrib, tglIsha;
        private GoldButton btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle   = new System.Windows.Forms.Label();
            lblFajr    = new System.Windows.Forms.Label();
            lblDhuhr   = new System.Windows.Forms.Label();
            lblAsr     = new System.Windows.Forms.Label();
            lblMaghrib = new System.Windows.Forms.Label();
            lblIsha    = new System.Windows.Forms.Label();
            cmbFajr    = new DarkComboBox();
            cmbDhuhr   = new DarkComboBox();
            cmbAsr     = new DarkComboBox();
            cmbMaghrib = new DarkComboBox();
            cmbIsha    = new DarkComboBox();
            tglFajr    = new ToggleSwitch();
            tglDhuhr   = new ToggleSwitch();
            tglAsr     = new ToggleSwitch();
            tglMaghrib = new ToggleSwitch();
            tglIsha    = new ToggleSwitch();
            btnSave    = new GoldButton();

            SuspendLayout();

            this.Text            = "SalatTrack — Settings";
            this.BackColor       = ThemeColors.DeepNavy;
            this.ClientSize      = new System.Drawing.Size(404, 530);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load           += new System.EventHandler(this.SettingsForm_Load);

            lblTitle.Text      = "NOTIFICATION SETTINGS";
            lblTitle.Font      = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = ThemeColors.GoldPrimary;
            lblTitle.Location  = new System.Drawing.Point(20, 20);
            lblTitle.Size      = new System.Drawing.Size(360, 28);

            BuildRow(lblFajr,    cmbFajr,    tglFajr,    "Fajr",    70);
            BuildRow(lblDhuhr,   cmbDhuhr,   tglDhuhr,   "Dhuhr",   150);
            BuildRow(lblAsr,     cmbAsr,     tglAsr,     "Asr",     230);
            BuildRow(lblMaghrib, cmbMaghrib, tglMaghrib, "Maghrib", 310);
            BuildRow(lblIsha,    cmbIsha,    tglIsha,    "Isha",    390);

            btnSave.Text     = "Save Settings";
            btnSave.Size     = new System.Drawing.Size(360, 42);
            btnSave.Location = new System.Drawing.Point(20, 468);
            btnSave.Click   += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnSave);

            ResumeLayout(false);
        }

        private void BuildRow(
            System.Windows.Forms.Label nameLabel,
            DarkComboBox combo,
            ToggleSwitch toggle,
            string prayerName,
            int yPos)
        {
            var card = new RoundedPanel();
            card.BackColor    = ThemeColors.CardBg;
            card.BorderColor  = ThemeColors.BorderSubtle;
            card.CornerRadius = 10;
            card.Size         = new System.Drawing.Size(360, 58);
            card.Location     = new System.Drawing.Point(20, yPos);
            this.Controls.Add(card);

            nameLabel.Text      = prayerName;
            nameLabel.Font      = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            nameLabel.ForeColor = ThemeColors.TextPrimary;
            nameLabel.BackColor = ThemeColors.CardBg;
            nameLabel.Location  = new System.Drawing.Point(14, 18);
            nameLabel.Size      = new System.Drawing.Size(90, 24);
            card.Controls.Add(nameLabel);

            combo.Items.AddRange(new object[] { "Toast", "Audio", "Silent" });
            combo.SelectedIndex = 0;
            combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            combo.Location      = new System.Drawing.Point(160, 16);
            combo.Size          = new System.Drawing.Size(100, 26);
            card.Controls.Add(combo);

            toggle.Checked  = true;
            toggle.Location = new System.Drawing.Point(300, 17);
            card.Controls.Add(toggle);
        }
    }
}