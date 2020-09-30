
dotnet test --collect:"XPlat Code Coverage"
mkdir coveragereport
reportgenerator "-reports:**\*\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html