C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe WebApiSeed.Packages.Azure.csproj /p:Configuration=Release /p:Publish=True

if ($LastExitCode -ne 0)
{
  Write-Output "Error building WebApiSeed.Packages.Azure library."
}
else
{
  .\NuGet.exe pack WebApiSeed.Packages.Azure.csproj
}

read-Host "Pausing..."
