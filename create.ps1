# Create-SalatTrack-Full.ps1
# Run this script from the folder where you want the SalatTrack solution.
# It will create solution, projects, install packages, and create all model classes.

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  SalatTrack - Complete Setup (Stage 1+2)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$currentDir = Get-Location
Write-Host "[INFO] Working directory: $currentDir" -ForegroundColor Yellow
Write-Host ""

# ------------------------------------------------------------
# 1. Create blank solution
# ------------------------------------------------------------
Write-Host "[1/6] Creating solution..." -ForegroundColor Green
$slnPath = Join-Path $currentDir "SalatTrack.sln"
if (Test-Path $slnPath) {
    Write-Host "[WARN] Solution already exists. Overwriting..." -ForegroundColor Red
    Remove-Item $slnPath -Force
}
dotnet new sln -n "SalatTrack" --force
if (-not (Test-Path $slnPath)) {
    Write-Host "[ERROR] Failed to create solution." -ForegroundColor Red
    exit 1
}
Write-Host "[OK] Solution created." -ForegroundColor Green

# ------------------------------------------------------------
# 2. Create projects
# ------------------------------------------------------------
Write-Host ""
Write-Host "[2/6] Creating projects..." -ForegroundColor Green

$projects = @(
    @{Name="SalatTrack.Models"; Type="classlib"},
    @{Name="SalatTrack.DAL"; Type="classlib"},
    @{Name="SalatTrack.BLL"; Type="classlib"},
    @{Name="SalatTrack.UI"; Type="winforms"}
)

foreach ($proj in $projects) {
    $projDir = Join-Path $currentDir $proj.Name
    if (Test-Path $projDir) {
        Write-Host "[WARN] Project folder $($proj.Name) already exists. Removing..." -ForegroundColor Red
        Remove-Item $projDir -Recurse -Force
    }
    Write-Host "  Creating $($proj.Name)..."
    dotnet new $($proj.Type) -n $($proj.Name) -f net8.0 --force
    if (-not (Test-Path $projDir)) {
        Write-Host "[ERROR] Failed to create project $($proj.Name)." -ForegroundColor Red
        exit 1
    }
    Write-Host "  [OK] $($proj.Name) created." -ForegroundColor Green
}

# ------------------------------------------------------------
# 3. Add projects to solution
# ------------------------------------------------------------
Write-Host ""
Write-Host "[3/6] Adding projects to solution..." -ForegroundColor Green
dotnet sln add "SalatTrack.Models/SalatTrack.Models.csproj"
dotnet sln add "SalatTrack.DAL/SalatTrack.DAL.csproj"
dotnet sln add "SalatTrack.BLL/SalatTrack.BLL.csproj"
dotnet sln add "SalatTrack.UI/SalatTrack.UI.csproj"

# ------------------------------------------------------------
# 4. Add project references
# ------------------------------------------------------------
Write-Host ""
Write-Host "[4/6] Adding project references..." -ForegroundColor Green
dotnet add "SalatTrack.UI/SalatTrack.UI.csproj" reference "SalatTrack.BLL/SalatTrack.BLL.csproj"
dotnet add "SalatTrack.BLL/SalatTrack.BLL.csproj" reference "SalatTrack.DAL/SalatTrack.DAL.csproj"
dotnet add "SalatTrack.BLL/SalatTrack.BLL.csproj" reference "SalatTrack.Models/SalatTrack.Models.csproj"
dotnet add "SalatTrack.DAL/SalatTrack.DAL.csproj" reference "SalatTrack.Models/SalatTrack.Models.csproj"

# ------------------------------------------------------------
# 5. Install NuGet packages
# ------------------------------------------------------------
Write-Host ""
Write-Host "[5/6] Installing NuGet packages..." -ForegroundColor Green
dotnet add "SalatTrack.DAL/SalatTrack.DAL.csproj" package System.Data.SqlClient
dotnet add "SalatTrack.UI/SalatTrack.UI.csproj" package System.Data.SqlClient
dotnet add "SalatTrack.BLL/SalatTrack.BLL.csproj" package BCrypt.Net-Next
dotnet add "SalatTrack.UI/SalatTrack.UI.csproj" package BCrypt.Net-Next

# Clean up auto-generated Class1.cs files
Remove-Item "SalatTrack.Models/Class1.cs" -ErrorAction SilentlyContinue
Remove-Item "SalatTrack.DAL/Class1.cs" -ErrorAction SilentlyContinue
Remove-Item "SalatTrack.BLL/Class1.cs" -ErrorAction SilentlyContinue

# ------------------------------------------------------------
# 6. Create folder structure (DAL and BLL subfolders)
# ------------------------------------------------------------
Write-Host ""
Write-Host "[6/6] Creating folder structure..." -ForegroundColor Green
New-Item -ItemType Directory -Path "SalatTrack.DAL/Interfaces" -Force | Out-Null
New-Item -ItemType Directory -Path "SalatTrack.DAL/Repositories" -Force | Out-Null
New-Item -ItemType Directory -Path "SalatTrack.BLL/Calculators" -Force | Out-Null
New-Item -ItemType Directory -Path "SalatTrack.BLL/Notifications" -Force | Out-Null
New-Item -ItemType Directory -Path "SalatTrack.BLL/Helpers" -Force | Out-Null

# ------------------------------------------------------------
# 7. Create Model classes (POCOs)
# ------------------------------------------------------------
Write-Host ""
Write-Host "[7/7] Creating model classes..." -ForegroundColor Green

# Helper to write file
function Write-ModelFile {
    param($fileName, $content)
    $filePath = Join-Path $currentDir "SalatTrack.Models" $fileName
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

# ------------------------------------------------------------
# Completion message
# ------------------------------------------------------------
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "[SUCCESS] Setup complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Solution and projects created in: $currentDir"
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "  1. Open the solution in Visual Studio:"
Write-Host "       start SalatTrack.sln"
Write-Host "  2. Build the solution (Ctrl+Shift+B) to ensure no errors."
Write-Host "  3. Ready for Stage 3 (DAL Interfaces and Repositories)."
Write-Host ""