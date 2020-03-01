cls
REM todo modify nameapp parameter
REM build from 0 = add --no-cache
call docker build --build-arg nameapp=newAng9 --tag newang9_image --file createAng9.txt .
REM TODO: exists newang image
call docker run -d -it  -p 4200:4200 -p 49153:49153 --rm --name newang9_container newang9_image 
REM TODO: verify localhost 4200
call docker exec newang9_container ls -l -a -m -1
mkdir newAng9
call docker cp newang9_container:/usr/app/README.md newAng9
call docker cp newang9_container:/usr/app/angular.json newAng9
call docker cp newang9_container:/usr/app/e2e newAng9
call docker cp newang9_container:/usr/app/package-lock.json newAng9
call docker cp newang9_container:/usr/app/package.json newAng9
call docker cp newang9_container:/usr/app/src newAng9
call docker cp newang9_container:/usr/app/tsconfig.json newAng9
call docker cp newang9_container:/usr/app/tsconfig.app.json newAng9
call docker cp newang9_container:/usr/app/tslint.json newAng9

