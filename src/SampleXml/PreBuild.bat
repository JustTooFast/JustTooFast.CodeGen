@ECHO OFF
IF %1.==. GOTO No1
IF %2.==. GOTO No2
IF %3.==. GOTO No3
SETLOCAL enabledelayedexpansion
SET projectDir=%1
SET projectDir=!projectDir:~1,-1!
SET outputPath=%2
SET outputPath=!outputPath:~1,-1!
SET targetNamespace=%3
SET targetNamespace=!targetNamespace:~1,-1!
@ECHO ON
if not exist "!projectDir!Output" mkdir "!projectDir!Output"
del /f /q "!projectDir!Output\*.gen.cs"
dotnet run --project "!projectDir!..\Scaffolder.Cli\Scaffolder.Cli.csproj" -- "!projectDir!Input" "!projectDir!Output" "!targetNamespace!"
@ECHO OFF
GOTO End1

:No1
  ECHO Please include "$(ProjectDir)", "$(OutputPath)", and "targetNamespace"
GOTO End1
:No2
  ECHO Please include "$(ProjectDir)", "$(OutputPath)", and "targetNamespace"
GOTO End1
:No3
  ECHO Please include "$(ProjectDir)", "$(OutputPath)", and "targetNamespace"
GOTO End1

:End1
