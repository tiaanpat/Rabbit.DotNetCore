ECHO OFF
SET pathReceive=%CD%\Rabbit.DotNetCore.Receive
SET pathSend=%CD%\Rabbit.DotNetCore.Send
SET pathTest=%CD%\Rabbit.DotNetCore.Test

dotnet clean
dotnet restore
dotnet build

cd %pathTest%
start cmd /k dotnet test --logger:trx

TIMEOUT 5

cd %pathReceive%
start cmd /k dotnet run

TIMEOUT 5

cd %pathSend%
start cmd /k dotnet run Tiaan
