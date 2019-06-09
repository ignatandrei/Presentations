cls
ECHO please be aware of absolute path here %cd%
docker build --tag netcore_tools_container --file runNetCore.txt .
pause
docker run --rm -d -p 5000:5000 --mount type=bind,source=%cd%\..\NetCore,target=/usr/app --name netcore_tools_image netcore_tools_container
pause
echo dotnet property
docker exec -it netcore_tools_image dotnet-property NetCoreTestProject/NetCoreTestProject.csproj AssemblyVersion:"3.0.0.0"
pause
echo watch
echo http://localhost:5000/api/values
docker exec -it  netcore_tools_image  dotnet watch -p newNetCoreWebApi/newNetCoreWebApi.csproj run --urls=http://+:5000
