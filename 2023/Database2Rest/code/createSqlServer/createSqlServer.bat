docker build -f sqlserver.txt --tag sqlservercreate .
pause
echo can try -it instead of -d
docker run --rm -d --name sqlservercontainer -p 1433:1433 sqlservercreate