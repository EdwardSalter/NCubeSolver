@echo off
SET file=bin\Release\Rubiks.exe

if not exist "%file%" (
	SET file=Rubiks.scr
)
if not exist "%file%" (
	SET file=Rubiks.exe
)
if not exist "%file%" (
	SET file=bin\Debug\Rubiks.exe
)
if not exist "%file%" (
	echo Error: Couldn't find screensaver file to install 1>&2
	exit /B 1
)


SET config=bin\Release\Rubiks.exe.config

if not exist "%config%" (
	SET config=Rubiks.exe.config
)
if not exist "%config%" (
	SET config=bin\Debug\Rubiks.exe.config
)
if not exist "%config%" (
	echo Warning: Couldn't find screensaver config file
)


SET targetFolder=%SystemRoot%\SysWOW64

if not exist "%targetFolder%" (
	SET targetFolder=%SystemRoot%\System32
)
if not exist "%targetFolder%" (
	echo Error: Couldn't find system directory to install too 1>&2
	exit /B 1
)

SET target=%targetFolder%\Rubiks.scr
SET configTarget=%targetFolder%\Rubiks.exe.config

copy "%file%"  "%target%" 1> nul
echo Copied %file% to %target%
copy "%config%"  "%configTarget%" 1> nul
echo Copied %config% to %configTarget%
