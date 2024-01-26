[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True)]
    [string]$solutionDirectory,

    [Parameter(Mandatory=$True)]
    [string]$copyTo
)

$absolutePath = "${copyTo}/assets/"
$assetsDirectory = "${solutionDirectory}TacminMusavir/assets/"

Write-Host "Clear Deploy"
Get-ChildItem "${copyTo}" -Recurse | Remove-Item -Recurse

Write-Host "Starting Copying Assets Files"

$exclude = "tacmin"
$folders = Get-ChildItem -Path $assetsDirectory | Where {($_.PSIsContainer) -and ($exclude -notcontains $_.Name)}

foreach ($f in $folders){ 
      Copy-Item -Path $assetsDirectory\$f -Destination $absolutePath\$f -Recurse -Force
}