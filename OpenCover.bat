@echo off
echo Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"
 
echo Remove any previous test execution files to prevent issues overwriting
IF EXIST "%~dp0ModelOC.trx" del "%~dp0ModelOC.trx%"
 
echo Remove any previously created test output directories
CD %~dp0
FOR /D /R %%X IN (%USERNAME%*) DO RD /S /Q "%%X"
 
echo Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics
 
echo Generate the report output based on the test results
if %errorlevel% equ 0 (
 call :RunReportGeneratorOutput
)
 
echo Launch the report
if %errorlevel% equ 0 (
 call :RunLaunchReport
)

echo return back to the working directory
cd %~dp0

exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics
"%~dp0packages\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
-register:user ^
-target:"%VS140COMNTOOLS%\..\IDE\mstest.exe" ^
-targetargs:"/testcontainer:\"%~dp0Model\ModelTest\bin\Debug\ModelTest.dll\" /resultsfile:\"%~dp0ModelOC.trx\"" ^
-targetargs:"/testcontainer:\"%~dp0ViewModels\ViewModelsTest\bin\Debug\ViewModelsTest.dll\"" ^
-filter:"+[*]* -[ModelTest]* -[ViewModelsTest]*" ^
-mergebyhash ^
-skipautoprops ^
-output:"%~dp0\GeneratedReports\ModelOCReport.xml"

:RunReportGeneratorOutput
echo on
"%~dp0packages\ReportGenerator.2.5.6\tools\ReportGenerator.exe" ^
-reports:"%~dp0\GeneratedReports\ModelOCReport.xml" ^
-targetdir:"%~dp0\GeneratedReports"
@echo off
exit /b %errorlevel%
 
:RunLaunchReport
start "report" "%~dp0\GeneratedReports\index.htm"
exit /b %errorlevel%