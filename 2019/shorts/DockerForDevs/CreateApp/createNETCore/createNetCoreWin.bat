cls
REM todo modify nameapp parameter
REM build from 0 = add --no-cache
docker build --build-arg nameapp=newNetCoreWebApi --tag newcore_image --file createNetCore.txt .
REM TODO: exists newcore_image 
docker run -d -it -p 5000:5000 --rm --name newnetcore_container newcore_image 
REM TODO: verify http://localhost:5000/api/values
docker exec newnetcore_container ls -l -a -m -1
mkdir newNetCore
docker cp newnetcore_container:/usr/app/newNetCoreWebApi newNetCore
REM TODO: verify http://localhost:5000/api/values

