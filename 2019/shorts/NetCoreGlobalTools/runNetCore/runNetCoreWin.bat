cls
ECHO please be aware of absolute path here %cd%
docker build --tag netcore_tools_container --file runNetCore.txt .
pause
docker run --rm -d -p 5000:5000 --mount type=bind,source=%cd%\..\NetCore,target=/usr/app --name netcore_tools_image netcore_tools_container
pause
echo dotnet property
docker exec -it netcore_tools_image dotnet-property NetCoreTestProject/NetCoreTestProject.csproj AssemblyVersion:"3.0.0.0"
pause

echo test - step 1 dotnet test
docker exec -it netcore_tools_image  dotnet test NetCoreTestProject/NetCoreTestProject.csproj --logger trx  --logger "console;verbosity=normal" --collect "Code coverage"
pause
echo test - step 2 coverlet
docker exec -it netcore_tools_image  coverlet NetCoreTestProject/bin/Debug/netcoreapp2.1/NetCoreTestProject.dll --target "dotnet" --targetargs "test NetCoreTestProject/NetCoreTestProject.csproj --configuration Debug --no-build"  --format opencover --exclude "[xunit*]*" --exclude "[*]NetCoreTestProject*"
pause
echo test - step 3 report generator
docker exec -it netcore_tools_image  reportgenerator "-reports:coverage.opencover.xml" "-targetdir:coveragereport" "-reporttypes:HTMLInline;HTMLSummary;Badges"
pause
echo test - step 4 copy results
docker cp netcore_tools_image:/usr/app/coveragereport .
pause
echo watch
echo http://localhost:5000/api/values
docker exec -it  netcore_tools_image  dotnet watch -p NetCoreSimple/NetCoreSimple.csproj run --urls=http://+:5000
