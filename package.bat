REM *********************************************************************************
REM ****  This script aims to zip package the code to be upload via AWS Console  ****
REM *********************************************************************************

cd AWS.Lambda.Worker
dotnet lambda package -c release -o ../AWS.Lambda.Worker.zip --framework netcoreapp2.1