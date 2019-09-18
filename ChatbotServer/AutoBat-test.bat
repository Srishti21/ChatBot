echo Press ENTER to Execute Unit Tests...
set /p key=
echo.
echo.
echo Unit Tests Execution Started...
echo.
echo.
echo --------------------------------------------------------

set TestPath=.\ChatbotUnitTest\bin\Debug\ChatbotUnitTest.dll
vstest.console.exe %TestPath% /ResultsDirectory:./TestResults /InIsolation /logger:trx

echo --------------------------------------------------------
echo.
echo.
echo Unit Tests Execution Ended...
echo.
echo.
pause 