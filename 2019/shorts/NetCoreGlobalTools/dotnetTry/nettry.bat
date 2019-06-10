cls
docker build --tag netcore_try_container --file nettry.txt .
pause
docker run --rm -d -p 5000:5000 --name netcore_try_image netcore_try_container
pause
START http://localhost:5000/QuickStart.md
