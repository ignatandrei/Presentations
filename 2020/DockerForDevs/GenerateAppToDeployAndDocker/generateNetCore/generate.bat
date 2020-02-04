cls

docker build -t generatetestcore -f generate.txt .
pause
docker run -p 5000:5000 -d --name generateTestCoreContainer --rm generatetestcore 
