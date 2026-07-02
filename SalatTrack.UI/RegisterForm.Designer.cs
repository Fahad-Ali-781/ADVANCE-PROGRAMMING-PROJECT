namespace SalatTrack.UI
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedPanel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private DarkTextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private DarkTextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private DarkTextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblCity;
        private DarkComboBox cmbCity;
        private GoldButton btnRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new RoundedPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new DarkTextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new DarkTextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new DarkTextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.cmbCity = new DarkComboBox();
            this.btnRegister = new GoldButton();

            // ── Form ──
            this.Text = "SalatTrack — Register";
            this.Size = new System.Drawing.Size(400, 540);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = ThemeColors.DeepNavy;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ── pnlMain ──
            this.pnlMain.BackColor = ThemeColors.CardBg;
            this.pnlMain.CornerRadius = 16;
            this.pnlMain.BorderColor = ThemeColors.BorderSubtle;
            this.pnlMain.Size = new System.Drawing.Size(320, 450);
            this.pnlMain.Location = new System.Drawing.Point(40, 30);

            // ── lblTitle ──
            this.lblTitle.Text = "CREATE ACCOUNT";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = ThemeColors.GoldPrimary;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Size = new System.Drawing.Size(280, 36);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);

            // ── lblUsername ──
            this.lblUsername.Text = "Username";
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblUsername.ForeColor = ThemeColors.TextMuted;
            this.lblUsername.Size = new System.Drawing.Size(260, 20);
            this.lblUsername.Location = new System.Drawing.Point(20, 72);

            // ── txtUsername ──
            this.txtUsername.Size = new System.Drawing.Size(260, 30);
            this.txtUsername.Location = new System.Drawing.Point(20, 94);

            // ── lblPassword ──
            this.lblPassword.Text = "Password";
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblPassword.ForeColor = ThemeColors.TextMuted;
            this.lblPassword.Size = new System.Drawing.Size(260, 20);
            this.lblPassword.Location = new System.Drawing.Point(20, 138);

            // ── txtPassword ──
            this.txtPassword.Size = new System.Drawing.Size(260, 30);
            this.txtPassword.Location = new System.Drawing.Point(20, 160);
            this.txtPassword.PasswordChar = '*';

            // ── lblConfirmPassword ──
            this.lblConfirmPassword.Text = "Confirm Password";
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblConfirmPassword.ForeColor = ThemeColors.TextMuted;
            this.lblConfirmPassword.Size = new System.Drawing.Size(260, 20);
            this.lblConfirmPassword.Location = new System.Drawing.Point(20, 204);

            // ── txtConfirmPassword ──
            this.txtConfirmPassword.Size = new System.Drawing.Size(260, 30);
            this.txtConfirmPassword.Location = new System.Drawing.Point(20, 226);
            this.txtConfirmPassword.PasswordChar = '*';

            // ── lblCity ──
            this.lblCity.Text = "City";
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblCity.ForeColor = ThemeColors.TextMuted;
            this.lblCity.Size = new System.Drawing.Size(260, 20);
            this.lblCity.Location = new System.Drawing.Point(20, 270);

            // ── cmbCity ──
            this.cmbCity.Size = new System.Drawing.Size(260, 30);
            this.cmbCity.Location = new System.Drawing.Point(20, 292);
            this.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ── btnRegister ──
            this.btnRegister.Text = "Create Account";
            this.btnRegister.Size = new System.Drawing.Size(260, 42);
            this.btnRegister.Location = new System.Drawing.Point(20, 370);
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // ── Add to panel ──
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblUsername);
            this.pnlMain.Controls.Add(this.txtUsername);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.lblConfirmPassword);
            this.pnlMain.Controls.Add(this.txtConfirmPassword);
            this.pnlMain.Controls.Add(this.lblCity);
            this.pnlMain.Controls.Add(this.cmbCity);
            this.pnlMain.Controls.Add(this.btnRegister);

            this.Controls.Add(this.pnlMain);
        }
    }
}