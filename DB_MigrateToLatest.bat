@echo off
rem  ---------------------------------
rem  --
rem  -- In order to use: replace naje sure the connectionstring matches with your connectionstring
rem  -- (it appears twice in the script)
rem  --
rem  ----------------------------------


set version=-1
set /p version="migrate to Which version? (just press enter for latest version): "

if "-1" == "%version%" goto latest
   rem if we are still here that means user entered version number	
   .\Lib\Migrator.Console.exe SqlServer "Data Source=.;Initial Catalog=ts;uid=dbuser;pwd=dbpass" .\Timesheet.Micro.Migrations\bin\Timesheet.Micro.Migrations.dll -version %version%  
   goto endif
:latest
   .\Lib\Migrator.Console.exe SqlServer "Data Source=.;Initial Catalog=ts;uid=dbuser;pwd=dbpass" .\Timesheet.Micro.Migrations\bin\Timesheet.Micro.Migrations.dll 
:endif

echo "migration done"
pause

