cls
echo "https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0&preserve-view=true"
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" `
   -p 1433:1433 --name sql1 --hostname sql1 `
   -d `
   mcr.microsoft.com/mssql/server:2022-latest

docker ps -a
dotnet tool restore
echo '!! create database DistCache !!'
pause
dotnet sql-cache create "Data Source=.;Initial Catalog=DistCache;Integrated Security=False;UID=sa;PWD=<YourStrong@Passw0rd>;TrustServerCertificate=true" dbo TestCache
echo 'after press key, delete all'
pause
docker ps -q | % { docker stop $_ }
docker system prune -f