@echo off
rem start /wait cmd /c az login

FOR %%G IN (packages\*.nupkg) DO (
	echo %%G
	.\nuget.exe push -Source "teambond-packagemanager" -ApiKey AzureDevOps %%G
	IF NOT ERRORLEVEL 1 del /Q %%G
)


pause