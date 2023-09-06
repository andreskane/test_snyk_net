rem Blog del que sabe lo que hizo
rem https://gunnarpeipman.com/aspnet-core-code-coverage/#targetText=Getting%20code%20coverage%20data&targetText=Cobertura%20is%20popular%20code%20coverage,xUnit%20formats%20to%20UnitTests%20folder.

cd %0\..\

dotnet test  --collect:"XPlat Code Coverage"   --logger "trx;LogFileName=TestResults.trx" --results-directory  ./BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]* 

dotnet "%USERPROFILE%\.nuget\packages\reportgenerator\4.8.9\tools\net5.0\ReportGenerator.dll" "-reports:%cd%\ABI.API.Structure.Unit.Tests\BuildReports\Coverage\coverage.cobertura.xml;%cd%\ABI.API.Structure.ACL.Truck.Unit.Tests\BuildReports\Coverage\coverage.cobertura.xml" "-targetdir:%cd%\BuildReports\Coverage" -reporttypes:HTML;HTMLSummary

start "%cd%\BuildReports\Coverage\index.htm"


