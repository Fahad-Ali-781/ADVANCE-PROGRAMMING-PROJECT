namespace SalatTrack.UI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedPanel pnlMain;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUsername;
        private DarkTextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private DarkTextBox txtPassword;
        private GoldButton btnLogin;
        private System.Windows.Forms.Button btnRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMain = new RoundedPanel();
            lblLogo = new Label();
            lblSubtitle = new Label();
            lblUsername = new Label();
            txtUsername = new DarkTextBox();
            lblPassword = new Label();
            txtPassword = new DarkTextBox();
            btnLogin = new GoldButton();
            btnRegister = new Button();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(19, 27, 42);
            pnlMain.BorderColor = Color.FromArgb(30, 42, 58);
            pnlMain.Controls.Add(lblLogo);
            pnlMain.Controls.Add(lblSubtitle);
            pnlMain.Controls.Add(lblUsername);
            pnlMain.Controls.Add(txtUsername);
            pnlMain.Controls.Add(lblPassword);
            pnlMain.Controls.Add(txtPassword);
            pnlMain.Controls.Add(btnLogin);
            pnlMain.Controls.Add(btnRegister);
            pnlMain.CornerRadius = 16;
            pnlMain.Location = new Point(12, 12);
            pnlMain.Name = "pnlMain";
            pnlMain.ShowBorder = true;
            pnlMain.Size = new Size(337, 662);
            pnlMain.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(212, 169, 74);
            lblLogo.Location = new Point(20, 20);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(280, 40);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "SALATTRACK";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Click += lblLogo_Click;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(122, 132, 153);
            lblSubtitle.Location = new Point(30, 60);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(280, 24);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Islamic Prayer Times";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            lblUsername.Font = new Font("Segoe UI", 9F);
            lblUsername.ForeColor = Color.FromArgb(122, 132, 153);
            lblUsername.Location = new Point(20, 110);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(260, 20);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(13, 17, 23);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.FromArgb(232, 220, 200);
            txtUsername.Location = new Point(40, 133);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(260, 18);
            txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 9F);
            lblPassword.ForeColor = Color.FromArgb(122, 132, 153);
            lblPassword.Location = new Point(20, 178);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(260, 20);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(13, 17, 23);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(232, 220, 200);
            txtPassword.Location = new Point(40, 201);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(260, 18);
            txtPassword.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(13, 17, 23);
            btnLogin.Location = new Point(40, 260);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(260, 40);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Sign In";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 9F);
            btnRegister.ForeColor = Color.FromArgb(212, 169, 74);
            btnRegister.Location = new Point(40, 329);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(260, 36);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Create Account";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(13, 17, 23);
            ClientSize = new Size(361, 448);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "S";
            Load += LoginForm_Load;
            FormClosing += LoginForm_FormClosing;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }
    }
}