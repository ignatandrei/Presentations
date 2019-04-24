cls
ECHO please be aware of absolute path here %cd%
docker build -t testcinetcore -f copyFilesToDocker.txt .
docker run -d --name citest  testcinetcore 
docker cp citest:/usr/app/coveragereport .
docker container kill citest
docker container rm citest
