echo Press ENTER to Execute Unit Tests...
set /p key=


@echo off
set "BAT_PATH=%~dp0"



@echo off


::echo %BAT_PATH%

echo.
echo.
echo.
echo ***********Running Simian to detect duplicacy**************
echo.

simian-2.5.10.exe "%BAT_PATH%\**\*.cs"   

echo.
echo.

echo **************Executing batch file for Web Service**************
echo.
devenv "%BAT_PATH%\chatbotrepo.sln" /build Debug 
START "" http://localhost:52413/
echo.


echo.
pause