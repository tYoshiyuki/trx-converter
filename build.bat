@echo off
dotnet clean .\TrxConverter.ConsoleApp\TrxConverter.ConsoleApp.csproj
dotnet build .\TrxConverter.ConsoleApp\TrxConverter.ConsoleApp.csproj -c Release
cd ./TrxConverter.Package
nuget pack