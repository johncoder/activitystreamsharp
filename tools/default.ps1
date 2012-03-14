Properties {
  $base_dir  			= resolve-path ..
  $lib_dir 				= "$base_dir\lib"
  $build_dir 			= "$base_dir\build"
  $tools_dir			= "$base_dir\tools"
  $buildartifacts_dir 	= "$build_dir\"
  $nuget_dir 			= "$build_dir\nuget"
  $sln_file 			= "$base_dir\src\ActivityStreamSharp.sln"
  $release_dir 			= "$base_dir\Release"
  $build_config 		= if ("$buildConfig".length -gt 0) { "$buildConfig" } else { "Release" }
  $publish_dir 			= "M:\Staging\Releases\$build_config\ActivityStreamSharp\ActivityStreamSharp-"
  $build_number_default = if ("$assembly_version".length -gt 0) { "$assembly_version" } else { "1.0.0.0" }
  $build_number = if ("$env:BUILD_NUMBER".length -gt 0) { "$env:BUILD_NUMBER" } else { "$build_number_default" } 
  $build_vcs_number = if ("$env:BUILD_VCS_NUMBER".length -gt 0) { "$env:BUILD_VCS_NUMBER" } else { "0" } 
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

include .\psake_ext.ps1

task default -depends Release

task Verify40 {
	if( (ls "$env:windir\Microsoft.NET\Framework\v4.0*") -eq $null ) {
		throw "Building ActivityStreamSharp requires .NET 4.0, which doesn't appear to be installed on this machine"
	}
}

task Clean {
  remove-item -force -recurse $buildartifacts_dir -ErrorAction SilentlyContinue
  remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue
}

task Init -depends Verify40, Clean {
	
	if($env:buildlabel -eq $null) {
		$env:buildlabel = "13"
	}
	
	$asmInfos = ls -path $base_dir -include AssemblyInfo.cs -recurse | 
					Where { $_ -notmatch [regex]::Escape($lib_dir) }
	
	foreach($asmInfo in $asmInfos) {
		
		$propertiesDir = [System.IO.Path]::GetDirectoryName($asmInfo.FullName)
		$projectDir = [System.IO.Path]::GetDirectoryName($propertiesDir)
		$projectName = [System.IO.Path]::GetFileName($projectDir)
		
		Generate-Assembly-Info `
			-file $asmInfo.FullName `
			-description "ActivityStreamSharp" `
			-company "" `
			-product "ActivityStreamSharp v$build_number" `
			-clsCompliant "false" `
			-version $build_number `

	}
		
	#new-item $release_dir -itemType directory
	#new-item $buildartifacts_dir -itemType directory
}

task Compile -depends Init {
	$v4_net_version = (ls "$env:windir\Microsoft.NET\Framework\v4.0*").Name
	
	$ErrorActionPreference = 'Stop'

	Write-Host "$build_config"

	try {
    	exec { msbuild "$sln_file" /p:OutDir="$buildartifacts_dir" /p:Configuration=$build_config }
    } catch {
    	Write-Host "Build Failed"
    	exit 1;
    }

	if ($LastExitCode -ne 0)
	{
		Write-Host "Error with MSBuild"
		throw "An error occured during invocation of MSbuild"
		Send-Error
		Exit 1
	}	$v4_net_version = (ls "$env:windir\Microsoft.NET\Framework\v4.0*").Name
}

task Publish -depends Compile {
}

task NuGet -depends Publish {

}

task Release -depends NuGet {
    mkdir $nuget_dir
    cd "$tools_dir"
    .\nuget.exe pack "$base_dir\src\ActivityStreamSharp\nuget\ActivityStreamSharp.nuspec" /o "$nuget_dir" /version $build_number
    cd "$base_dir"
}

task Finish -depends Release {

	

    if ($Error -ne '') {write-host "ERROR: $error" -fore RED; exit $error.Count} 
}
