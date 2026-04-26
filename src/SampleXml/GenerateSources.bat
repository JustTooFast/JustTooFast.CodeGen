@ECHO OFF
IF %1.==. GOTO No1
IF %2.==. GOTO No2
SETLOCAL EnableDelayedExpansion

SET projectDir=%1
SET projectDir=!projectDir:~1,-1!
SET targetNamespace=%2
SET targetNamespace=!targetNamespace:~1,-1!

@ECHO ON

if not exist "!projectDir!Output" mkdir "!projectDir!Output"

for /r "!projectDir!Output" %%F in (*.gen.cs) do (
    set "keep="
    set "name=%%~nF"
    set "stem="

    if /i "!name:~-4!"==".gen" set "stem=!name:~0,-4!"

    for %%I in ("!projectDir!Input\*") do (
        set "prefix=%%~nI"

        if /i "!stem!"=="!prefix!Emitter" set "keep=1"
        if /i "!stem!"=="!prefix!Builder" set "keep=1"
        if /i "!stem!"=="!prefix!Model" set "keep=1"
    )

    if not defined keep del /f /q "%%F"
)

dotnet run --no-build --project "!projectDir!..\Scaffolder.Cli\Scaffolder.Cli.csproj" -- "!projectDir!Input" "!projectDir!Output" "!targetNamespace!"

@ECHO OFF
GOTO End1

:No1
  ECHO Please include "$(ProjectDir)" and "targetNamespace"
GOTO End1

:No2
  ECHO Please include "$(ProjectDir)" and "targetNamespace"
GOTO End1

:End1
