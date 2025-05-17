echo "Run tests with coverage"
dotnet test  Ambev.DeveloperEvaluation.sln --no-restore --verbosity normal \
/p:CollectCoverage=true \
/p:CoverletOutputFormat=cobertura \
/p:CoverletOutput=./TestResults/coverage.cobertura.xml \
/p:Exclude="[*]*.Program [*]*.Startup [*]*.Migrations.*"

echo "Generate coverage report"
reportgenerator \
-reports:"./tests/**/TestResults/coverage.cobertura.xml" \
-targetdir:"./TestResults/CoverageReport" \
-reporttypes:Html

