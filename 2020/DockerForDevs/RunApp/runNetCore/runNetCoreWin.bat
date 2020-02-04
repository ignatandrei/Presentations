cls
ECHO please be aware of absolute path here %cd%
docker build --tag netcore_modify_image --file runNetCore.txt .
docker run --rm -d -p 5000:5000 --mount type=bind,source=%cd%\newNetCore,target=/usr/app --name netcore_modify_container netcore_modify_image 
echo http://localhost:5000/api/values
docker exec -it  netcore_modify_container  dotnet watch -p newNetCoreWebApi/newNetCoreWebApi.csproj run --urls=http://+:5000
