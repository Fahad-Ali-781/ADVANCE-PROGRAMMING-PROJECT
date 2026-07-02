# Create-Models-Fix.ps1
# Run this after the main setup script to create model files

$modelsPath = Join-Path (Get-Location) "SalatTrack.Models"

# Check if folder exists
if (-not (Test-Path $modelsPath)) {
    Write-Host "ERROR: SalatTrack.Models folder not found!" -ForegroundColor Red
    Write-Host "Make sure you ran the main setup script first." -ForegroundColor Yellow
    exit 1
}

Write-Host "Creating model files in: $modelsPath" -ForegroundColor Green

# Helper function to write file (compatible with all PowerShell versions)
function Write-ModelFile {
    param($fileName, $content)
    $filePath = Join-Path $modelsPath $fileName
    Set-Content -Path $filePath -Value $content -Encoding UTF8
    Write-Host "  Created: $fileName" -ForegroundColor Green
}

# User.cs
Write-ModelFile "User.cs" @"
namespace SalatTrack.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
"@

# City.cs
Write-ModelFile "City.cs" @"
namespace SalatTrack.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeZone { get; set; } = string.Empty;
    }
}
"@

# PrayerTime.cs
Write-ModelFile "PrayerTime.cs" @"
namespace SalatTrack.Models
{
    public class PrayerTime
    {
        public int PrayerTimeID { get; set; }
        public int CityID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Fajr { get; set; }
        public TimeSpan Dhuhr { get; set; }
        public TimeSpan Asr { get; set; }
        public TimeSpan Maghrib { get; set; }
        public TimeSpan Isha { get; set; }
    }
}
"@

# NotificationSetting.cs
Write-ModelFile "NotificationSetting.cs" @"
namespace SalatTrack.Models
{
    public class NotificationSetting
    {
        public int SettingID { get; set; }
        public int UserID { get; set; }
        public string PrayerName { get; set; } = string.Empty;
        public string AlertType { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
    }
}
"@

# Log.cs
Write-ModelFile "Log.cs" @"
namespace SalatTrack.Models
{
    public class Log
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public string PrayerName { get; set; } = string.Empty;
        public DateTime LoggedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
"@

Write-Host ""
Write-Host "All model files created successfully!" -ForegroundColor Green
Write-Host "Now you can open the solution in Visual Studio." -ForegroundColor Yellow