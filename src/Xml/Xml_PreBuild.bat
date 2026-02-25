@ECHO OFF
IF %1.==. GOTO No1
IF %2.==. GOTO No2
SETLOCAL enabledelayedexpansion
SET projectDir=%1
SET projectDir=!projectDir:~1,-1!
SET targetNamespace=%2
SET targetNamespace=!targetNamespace:~1,-1!
@ECHO ON
del /f /q "!projectDir!Output\*.gen.cs"
dotnet run --project "!projectDir!..\Scaffolder.Cli\Scaffolder.Cli.csproj" -- "!projectDir!Input" "!projectDir!Output" "!targetNamespace!"
@ECHO OFF
GOTO End1

:No1
  ECHO Please include "$(ProjectDir)" and "targetNamespace"
GOTO End1
:No2
  ECHO Please include "$(ProjectDir)" and "targetNamespace"
GOTO End1

:End1
