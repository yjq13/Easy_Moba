@echo off

setlocal enabledelayedexpansion

set PROTOCOL_ROOT=..\..\server\libs\proto\msg
set CSHARP_ROOT=..\Assets\Script\NetWork\ProtoMessage
echo generated started!!!
set /p input=
set finalPath=%PROTOCOL_ROOT%
echo %finalPath%
call :gen_proto !finalPath! CSHARP_ROOT=..



echo generated finished!!
set /p input=
goto :end

:gen_proto
for /f "delims=\" %%f in ('dir /b /a-d /o-d "%1\*.proto"') do (
	echo generated started!!!!
	protogen.exe "%%f" --proto_path="%1" --csharp_out="%1" +langver=2 +names=original
)

:end