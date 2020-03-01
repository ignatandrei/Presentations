cls
REM todo modify nameapp parameter
REM build from 0 = add --no-cache
call docker build --build-arg nameapp=newAng8 --tag newang8_image --file createAng8.txt .
REM TODO: exists newang image
call docker run -d -it  -p 4200:4200 -p 49153:49153 --rm --name newang8_container newang8_image 
REM TODO: verify localhost 4200
call docker exec newang8_container ls -l -a -m -1
mkdir newAng8
call docker cp newang8_container:/usr/app/README.md newAng8
call docker cp newang8_container:/usr/app/angular.json newAng8
call docker cp newang8_container:/usr/app/e2e newAng8
call docker cp newang8_container:/usr/app/package-lock.json newAng8
call docker cp newang8_container:/usr/app/package.json newAng8
call docker cp newang8_container:/usr/app/src newAng8
call docker cp newang8_container:/usr/app/tsconfig.json newAng8
call docker cp newang8_container:/usr/app/tsconfig.app.json newAng8
call docker cp newang8_container:/usr/app/tslint.json newAng8

