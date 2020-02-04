cls
REM todo modify nameapp parameter
REM build from 0 = add --no-cache
docker build --build-arg nameapp=newAng --tag newang_image --file createAng.txt .
REM TODO: exists newang image
docker run -d -it  -p 4200:4200 -p 49153:49153 --rm --name newang_container newang_image 
REM TODO: verify localhost 4200
docker exec newang_container ls -l -a -m -1
mkdir newAng
docker cp newang_container:/usr/app/README.md newAng
docker cp newang_container:/usr/app/angular.json newAng
docker cp newang_container:/usr/app/e2e newAng
docker cp newang_container:/usr/app/package-lock.json newAng
docker cp newang_container:/usr/app/package.json newAng
docker cp newang_container:/usr/app/src newAng
docker cp newang_container:/usr/app/tsconfig.json newAng
docker cp newang_container:/usr/app/tslint.json newAng

