using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SalatTrack.UI
{
    // ─────────────────────────────────────────────
    // 1. ROUNDED PANEL — base container for all cards
    // ─────────────────────────────────────────────
    internal class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 12;
        public Color BorderColor { get; set; } = ThemeColors.BorderSubtle;
        public bool ShowBorder { get; set; } = true;

        public RoundedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Paint parent background color over full rectangle first — kills white corners
            using SolidBrush parentBg = new SolidBrush(Parent?.BackColor ?? ThemeColors.DeepNavy);
            g.FillRectangle(parentBg, ClientRectangle);

            Rectangle rect = new Rectangle(1, 1, Width - 2, Height - 2);
            using GraphicsPath path = GetRoundedPath(rect, CornerRadius);

            // Fill card background inside rounded path only
            using SolidBrush bg = new SolidBrush(BackColor);
            g.FillPath(bg, path);

            if (ShowBorder)
            {
                using Pen border = new Pen(BorderColor, 1f);
                g.DrawPath(border, path);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Block default — fully handled in OnPaint
        }

        public static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
    // ─────────────────────────────────────────────
    // 2. GOLD BUTTON — primary action button
    // ─────────────────────────────────────────────
    internal class GoldButton : Button
    {
        private bool _isHovered = false;
        private bool _isPressed = false;

        public GoldButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = ThemeColors.DeepNavy;
            Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _isPressed = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _isPressed = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = RoundedPanel.GetRoundedPath(rect, 8);

            Color fill = _isPressed ? ThemeColors.GoldDark
                       : _isHovered ? Color.FromArgb(225, 185, 95)
                       : ThemeColors.GoldPrimary;

            using LinearGradientBrush brush = new LinearGradientBrush(
                rect, fill, ThemeColors.GoldDark, LinearGradientMode.Vertical);
            g.FillPath(brush, path);

            // Button text
            TextRenderer.DrawText(g, Text, Font, rect, ThemeColors.DeepNavy,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // ─────────────────────────────────────────────
    // 3. OUTLINE BUTTON — secondary action button
    // ─────────────────────────────────────────────
    internal class OutlineButton : Button
    {
        private bool _isHovered = false;

        public OutlineButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = ThemeColors.GoldPrimary;
            Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
        }

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = RoundedPanel.GetRoundedPath(rect, 8);

            // Use solid dark color — Color.Transparent bleeds parent content through
            Color bgColor = _isHovered
                ? Color.FromArgb(30, 212, 169, 74)
                : ThemeColors.DeepNavy;

            using SolidBrush bg = new SolidBrush(bgColor);
            g.FillPath(bg, path);

            // Border
            using Pen border = new Pen(Color.FromArgb(120, ThemeColors.GoldDark), 1f);
            g.DrawPath(border, path);

            // Text
            TextRenderer.DrawText(g, Text, Font, rect, ThemeColors.GoldPrimary,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // ─────────────────────────────────────────────
    // 4. TOGGLE SWITCH — for notification on/off
    // ─────────────────────────────────────────────
    internal class ToggleSwitch : Control
    {
        private bool _checked = true;
        public bool Checked
        {
            get => _checked;
            set { _checked = value; Invalidate(); }
        }

        public event EventHandler? CheckedChanged;

        public ToggleSwitch()
        {
            Size = new Size(44, 24);
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer, true);
        }

        protected override void OnClick(EventArgs e)
        {
            _checked = !_checked;
            Invalidate();
            CheckedChanged?.Invoke(this, EventArgs.Empty);
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = new Rectangle(0, 2, Width - 1, Height - 5);
            using GraphicsPath trackPath = RoundedPanel.GetRoundedPath(track, 10);

            Color trackColor = _checked ? ThemeColors.ToggleOn : ThemeColors.ToggleOff;
            using SolidBrush trackBrush = new SolidBrush(trackColor);
            g.FillPath(trackBrush, trackPath);

            // Thumb
            int thumbX = _checked ? Width - 22 : 2;
            Rectangle thumb = new Rectangle(thumbX, 4, 18, 18);
            using SolidBrush thumbBrush = new SolidBrush(ThemeColors.ToggleThumb);
            g.FillEllipse(thumbBrush, thumb);
        }
    }

    // ─────────────────────────────────────────────
    // 5. DARK TEXTBOX — styled input field
    // ─────────────────────────────────────────────
    internal class DarkTextBox : TextBox
    {
        public DarkTextBox()
        {
            BackColor = ThemeColors.InputBg;
            ForeColor = ThemeColors.TextPrimary;
            BorderStyle = BorderStyle.None;
            Font = new Font("Segoe UI", 10f);
        }
    }

    // ─────────────────────────────────────────────
    // 6. DARK COMBOBOX — styled dropdown
    // ─────────────────────────────────────────────
    internal class DarkComboBox : ComboBox
    {
        public DarkComboBox()
        {
            BackColor = ThemeColors.CardBg;
            ForeColor = ThemeColors.TextGold;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Segoe UI", 9f);
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 24;
            DrawItem += OnDrawItem;
        }

        private void OnDrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.Graphics.FillRectangle(new SolidBrush(ThemeColors.CardBg), e.Bounds);

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            if (selected)
                e.Graphics.FillRectangle(new SolidBrush(ThemeColors.CardBgActive), e.Bounds);

            // Respect DisplayMember instead of raw ToString()
            object item = Items[e.Index];
            string? text = null;

            if (!string.IsNullOrEmpty(DisplayMember))
            {
                var prop = item?.GetType().GetProperty(DisplayMember);
                text = prop?.GetValue(item)?.ToString();
            }

            text ??= item?.ToString();

            if (text != null)
                TextRenderer.DrawText(e.Graphics, text, Font, e.Bounds,
                    ThemeColors.TextGold, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }
    }
}