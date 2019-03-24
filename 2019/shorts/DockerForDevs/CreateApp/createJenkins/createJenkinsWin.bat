cls
rem docker build --tag newjenkins_image --file createJenkins.txt .

docker run -d -it -p 8080:8080 --rm --name newjenkins_container -e JENKINS_OPTS="--argumentsRealm.passwd.admin=andrei --argumentsRealm.roles.user=admin --argumentsRealm.roles.admin=admin" -e JAVA_OPTS="-Xmx4096m -Djenkins.install.runSetupWizard=false"  jenkins/jenkins:lts
      
echo wait for jenkins to run : http://localhost:8080/
pause
docker cp ./jenkinsjob.xml newjenkins_container:/var/jenkins_home/jenkinsjob.xml
pause 
docker exec newjenkins_container bash -c "cat /var/jenkins_home/jenkinsjob.xml | java -jar /var/jenkins_home/war/WEB-INF/jenkins-cli.jar -s http://localhost:8080 create-job newmyjob"
REM TODO: verify localhost 8080
