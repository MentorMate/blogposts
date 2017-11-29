param(
    [string] $Out="$($MyInvocation.MyCommand.Path | Split-Path)/publish",
    [string] $Configuration="Release",
    [switch] $FeatureBasicCalculator,
    [switch] $FeatureExtendedCalculator,
    [switch] $Run    
)

Write-Host "Publish $Configuration to $Out"
Write-Host
Write-Host ".NET Tools $(& dotnet --version)"
Write-Host
Write-Host "Clean old files and build outputs"
Remove-Item -Recurse -Force $Out
& dotnet clean -nologo
Write-Host

If ($FeatureBasicCalculator.IsPresent) {
    Write-Host "Basic Calculator"
    & dotnet build FeatureToggle.BasicCalculator -c $Configuration -nologo
}

If ($FeatureExtendedCalculator.IsPresent) {
    Write-Host "Extended Calculator"
    & dotnet build FeatureToggle.ExtendedCalculator -c $Configuration -nologo
}

Write-Host "Publishing"
& dotnet publish FeatureToggle.Web -c $Configuration -o $Out -nologo

If ($Run.IsPresent) {
    Write-Host "Run"
    & dotnet $Out/FeatureToggle.Web.dll
}