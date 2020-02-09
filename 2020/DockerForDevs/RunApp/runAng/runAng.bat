cls
ECHO please be aware of absolute path here %cd%
docker build --tag angular_modify_image --file runAng.txt .
docker run -d -p 4200:4200 -p 49153:49153 --mount type=bind,source=%cd%\TestAng8App,target=/usr/app --name angular_modify_container angular_modify_image 
docker exec -it angular_modify_container  npm i
docker exec -it angular_modify_container  ng serve  --host 0.0.0.0 --poll 10