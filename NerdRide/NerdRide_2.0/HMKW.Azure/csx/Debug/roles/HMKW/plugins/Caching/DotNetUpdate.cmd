@echo off

set exitCode=0

echo Dotnetupdate.cmd start >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
date /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
time /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log

if "%IsEmulated%"=="true" (
    echo "Emulated environment; skipping installation" >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
    goto :EOF
)

REM Checking install status for KB2600211 (superset of the required fix KB983182)
FOR /F "skip=2 tokens=3" %%i in ('REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Updates\Microsoft .NET Framework 4 Client Profile\KB2600211" /v ThisVersionInstalled') DO (

    if %errorlevel% NEQ 1 (
        if %%i==Y (
            echo "KB2600211 is already installed; skipping installation" >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
            goto skipInstallation
        )
    )
)

REM Checking install status of KB983182 (WCF performance issues for the .NET Framework 4.0)
FOR /F "skip=2 tokens=3" %%i in ('REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Updates\Microsoft .NET Framework 4 Client Profile\KB983182" /v ThisVersionInstalled') DO (

    if %errorlevel% NEQ 1 (
        if %%i==Y (
            echo "KB983182 is already installed; skipping installation" >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
            goto skipInstallation
        )
    )
)

echo "KB983182 is not installed; it will be installed" >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
date /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
time /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
start /w NDP40-KB983182-x64.exe /q /log %CachingLocalStorePath%\KB983182.log

echo errorlevel is %errorlevel% >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
date /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log
time /t >> %CachingLocalStorePath%\Dotnetupdate.cmd.log

set exitCode=%errorlevel%

:skipInstallation

exit /b %exitCode%
