echo Press ENTER to Execute Unit Tests...
set /p key=


@echo off
set "BAT_PATH=%~dp0..\HttpConsoleApp"



@echo off


::echo %BAT_PATH%



echo.
echo Executing batch file for Client
devenv "%BAT_PATH%\HttpConsoleApp.sln" /build Debug 


echo.
echo %BAT_PATH%\HttpConsoleApp\bin\Debug\HttpConsoleApp.exe 
start "" %BAT_PATH%\HttpConsoleApp\bin\Debug\HttpConsoleApp.exe 


echo.
pause