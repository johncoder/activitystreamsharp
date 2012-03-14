function Get-File-Exists-On-Path
{
	param(
		[string]$file
	)
	$results = ($Env:Path).Split(";") | Get-ChildItem -filter $file -erroraction silentlycontinue
	return $results.Length > 0
}

function Get-Git-Commit
{
	if ((Get-File-Exists-On-Path git.exe )){
		$gitLog = git log --oneline -1
		return $gitLog.Split(' ')[0]
	}
	else {
		return ""
	}
}


function Delete-Sample-Data-For-Release
{
  param([string]$sample_dir)
  rd "$sample_dir\bin" -force -recurse -ErrorAction SilentlyContinue
  rd "$sample_dir\obj" -force -recurse -ErrorAction SilentlyContinue
  
  rd "$sample_dir\Servers\Shard1\Data" -force -recurse -ErrorAction SilentlyContinue
  rd "$sample_dir\Servers\Shard2\Data" -force -recurse -ErrorAction SilentlyContinue
  
  rd "$sample_dir\Servers\Shard1\Plugins" -force -recurse -ErrorAction SilentlyContinue
  rd "$sample_dir\Servers\Shard2\Plugins" -force -recurse -ErrorAction SilentlyContinue
  
  
  del "$sample_dir\Servers\Shard1\RavenDB.exe" -force -recurse -ErrorAction SilentlyContinue
  del "$sample_dir\Servers\Shard2\RavenDB.exe" -force -recurse -ErrorAction SilentlyContinue
}

function Generate-Assembly-Info
{
param(
	[string]$clsCompliant = "true",
	[string]$title, 
	[string]$description, 
	[string]$company, 
	[string]$product, 
	[string]$copyright, 
	[string]$version,
	[string]$file = $(throw "file is a required parameter.")
)
  $regex = [regex] 'InternalsVisibleTo\("(?<assembly>.*)"\)'
  $match = $regex.Match("")
  $contents = (get-content $file)
  foreach($line in $contents) {
	$match = $regex.Match($line)
  }
  write-host $match.Value
$asmInfo = "using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliantAttribute($clsCompliant )]
[assembly: ComVisibleAttribute(false)]
"
if($title)
{
$asmInfo += "
[assembly: AssemblyTitleAttribute(""$title"")]
"
}

$asmInfo += "
[assembly: AssemblyDescriptionAttribute(""$description"")]
[assembly: AssemblyCompanyAttribute(""$company"")]
[assembly: AssemblyProductAttribute(""$product"")]
[assembly: AssemblyCopyrightAttribute(""$copyright"")]
[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyInformationalVersionAttribute(""$version"")]
[assembly: AssemblyFileVersionAttribute(""$version"")]
[assembly: AssemblyDelaySignAttribute(false)]
"
if ([string]::IsNullOrEmpty($match.Value) -ne [bool]1) {
	$assemblyName = $match.Groups[1].Value
	Write-Host "Adding $product InternalsVisibleTo $assemblyName"
	$asmInfo += "
[assembly: InternalsVisibleTo(""$assemblyName"")]
"
}
	$dir = [System.IO.Path]::GetDirectoryName($file)
	if ([System.IO.Directory]::Exists($dir) -eq $false)
	{
		Write-Host "Creating directory $dir"
		[System.IO.Directory]::CreateDirectory($dir)
	}
	Write-Host "Generating assembly info file: $file"
	Write-Output $asmInfo > $file
}