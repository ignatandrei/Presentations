cd NETcore
cd NetCoreSimple
dotnet publish -c release -r win10-x64
echo run the site 
explorer /select, bin\release\netcoreapp2.1\win10-x64\publish\NetCoreSimple.exe
echo NetCoreSimple.exe 
echo NetCoreSimple.exe urls="http://localhost:5004"

pause