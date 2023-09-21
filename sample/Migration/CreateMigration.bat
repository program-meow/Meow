@echo off
cd /d "%~dp0"
echo ===================================================================================
echo Please enter the appsettings.json file directory relative to the migration project.
echo default: ..\Meow.Sample.Api
echo ===================================================================================
set /p basePath=
if "%basePath%" EQU "" set basePath="..\Meow.Sample.Api"

:enterMigrationName
echo ============================
echo Please enter migration name.
echo ============================
set /p migrationName=
if "%migrationName%" EQU "" goto :enterMigrationName

echo.
echo ===========================================
echo Add Migration for Meow.Sample.Data.MySql.
echo ===========================================
dotnet ef migrations add %migrationName% --project ../Meow.Sample.Data.MySql -- --basePath %basePath% --environment Development

echo.
echo ============================================
echo Add Migration for Meow.Sample.Data.Oracle.
echo ============================================
dotnet ef migrations add %migrationName% --project ../Meow.Sample.Data.Oracle -- --basePath %basePath% --environment Development

echo.
echo ===============================================
echo Add Migration for Meow.Sample.Data.PgSql.
echo ===============================================
dotnet ef migrations add %migrationName% --project ../Meow.Sample.Data.PgSql -- --basePath %basePath% --environment Development
pause

echo.
echo ============================================
echo Add Migration for Meow.Sample.Data.SqlServer.
echo ============================================
dotnet ef migrations add %migrationName% --project ../Meow.Sample.Data.SqlServer -- --basePath %basePath% --environment Development
