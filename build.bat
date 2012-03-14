@echo off
powershell -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module .\tools\psake.psm1; Invoke-psake .\tools\default.ps1 -framework '4.0' -parameters @{"assembly_version"='%1';"buildConfig"='%2';} }
if "%ERRORLEVEL%" == "1" exit /B 1