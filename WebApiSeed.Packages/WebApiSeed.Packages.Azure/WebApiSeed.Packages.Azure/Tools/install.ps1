param($installPath, $toolsPath, $package, $project)
$projectFullName = $project.FullName
$debugString = "install.ps1 executing for " + $projectFullName
Write-Output $debugString

foreach ($proj in get-project) {
    $basePath = Split-Path $proj.FileName;
    $webConfigPath = $basePath + "\web.config"

    AddKeyToConfig $webConfigPath "UserPicturesBlobContainer" "userpictures"
    AddKeyToConfig $webConfigPath "AzureStorageConnectionString" "DefaultEndpointsProtocol=https;AccountName=myAccountName;AccountKey=myAccountKey;"
    
}