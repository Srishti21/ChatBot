echo Press ENTER to Execute Unit Tests...
set /p key=


@echo off
set "BAT_PATH=%~dp0"



@echo off


echo %BAT_PATH%

echo Executing batch file for Web Service

:: param1 =     certificate hash for cash microservice : default no certificate

:: param2 =     Build Type: default Debug, Range: Debug or Release
::Win32
::              
:: To Make it to work open BranchConfiguration.json then set the value of "name": "BranchProfile" 

::              to as per Build Type like, "value": "Release" or "value": "Debug", By default the vlaue is debug
::X64

::              To Make it to work open BranchConfiguration64.json then set the value of "name": "BranchProfile" 

::              to as per Build Type like, "value": "Release" or "value": "Debug", By default the vlaue is debug

:: param3 =     Infra Compiling: default: build, Range: nobuild OR build
:: param4 =     Platform: default: x64 or Win32, Range: Win32 OR x64

devenv "%BAT_PATH%\chatbotrepo.sln" /build Debug 
start "%BAT_PATH%\chatbotrepo.sln" 
echo.
echo.
echo.
echo.
echo Executing batch file for Client
devenv "%BAT_PATH%\../HttpConsoleApp/HttpConsoleApp.sln" /build Debug 
start "%BAT_PATH%\../HttpConsoleApp/HttpConsoleApp.sln" 


echo.
pause