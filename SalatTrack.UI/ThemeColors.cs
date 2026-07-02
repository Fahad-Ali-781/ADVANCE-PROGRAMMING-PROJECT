using System.Drawing;

namespace SalatTrack.UI
{
    /// <summary>
    /// Central design system for SalatTrack.
    /// All forms and custom controls pull colors from here — change once, updates everywhere.
    /// </summary>
    internal static class ThemeColors
    {
        // --- Backgrounds ---
        public static readonly Color DeepNavy = Color.FromArgb(13, 17, 23);   // Main window bg
        public static readonly Color CardBg = Color.FromArgb(19, 27, 42);   // Prayer cards
        public static readonly Color CardBgActive = Color.FromArgb(26, 46, 74);   // Highlighted card
        public static readonly Color SurfaceBg = Color.FromArgb(10, 15, 26);   // Inner panels
        public static readonly Color InputBg = Color.FromArgb(13, 17, 23);   // Text fields

        // --- Gold Accents ---
        public static readonly Color GoldPrimary = Color.FromArgb(212, 169, 74);  // Main gold
        public static readonly Color GoldDark = Color.FromArgb(184, 148, 63);  // Darker gold
        public static readonly Color GoldGlow = Color.FromArgb(40, 184, 148, 63); // Semi-transparent gold

        // --- Text ---
        public static readonly Color TextPrimary = Color.FromArgb(232, 220, 200);  // Main text
        public static readonly Color TextMuted = Color.FromArgb(122, 132, 153);  // Muted/secondary
        public static readonly Color TextGold = Color.FromArgb(212, 169, 74);  // Gold text

        // --- Borders ---
        public static readonly Color BorderSubtle = Color.FromArgb(30, 42, 58);   // Default border
        public static readonly Color BorderGold = Color.FromArgb(80, 184, 148, 63); // Gold border
        public static readonly Color BorderActive = Color.FromArgb(184, 148, 63);  // Active border

        // --- Status Colors ---
        public static readonly Color StatusPrayed = Color.FromArgb(76, 175, 130);  // Green
        public static readonly Color StatusMissed = Color.FromArgb(207, 102, 121);  // Red
        public static readonly Color StatusLate = Color.FromArgb(207, 170, 70);  // Amber

        public static readonly Color StatusPrayedBg = Color.FromArgb(10, 46, 26);
        public static readonly Color StatusMissedBg = Color.FromArgb(46, 10, 10);
        public static readonly Color StatusLateBg = Color.FromArgb(46, 32, 10);

        // --- Toggle ---
        public static readonly Color ToggleOn = Color.FromArgb(184, 148, 63);
        public static readonly Color ToggleOff = Color.FromArgb(42, 58, 80);
        public static readonly Color ToggleThumb = Color.FromArgb(255, 255, 255);
    }
}