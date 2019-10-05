docker build --tag netcorewhatsnew_image --file netcorewhatsnew.txt .
docker run -p 5000:5000 --mount type=bind,source=%cd%,target=/usr/app --name netcorewhatsnew_container netcorewhatsnew_image
