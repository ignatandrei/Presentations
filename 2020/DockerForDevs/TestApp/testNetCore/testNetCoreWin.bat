cls
ECHO please be aware of absolute path here %cd%
docker build -t testnetcore -f testNetCore.txt .
echo start powershell
Start powershell ./all.ps1
docker run -it --name testNetCoreContainer --rm testnetcore  dotnet watch -p NetCoreTestProject/NetCoreTestProject.csproj test
