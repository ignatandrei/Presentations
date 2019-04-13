$watcher = New-Object System.IO.FileSystemWatcher
$watcher.Path = "NetCore"
$watcher.IncludeSubdirectories = $true
$watcher.EnableRaisingEvents = $false
$watcher.NotifyFilter = [System.IO.NotifyFilters]::LastWrite -bor [System.IO.NotifyFilters]::FileName

while($TRUE){
	$result = $watcher.WaitForChanged([System.IO.WatcherChangeTypes]::Changed -bor [System.IO.WatcherChangeTypes]::Renamed -bOr [System.IO.WatcherChangeTypes]::Created, 1000);
	if($result.TimedOut){
		continue;
	}
    switch($result.ChangeType){
        Changed{
        #TODO: see if folder or file
			#Write-Host " modified " $result.Name
			
            $fullName = [System.IO.Path]::Combine($watcher.Path ,$result.Name)          
			
			$doNotCopy = $result.Name.Contains(".vs") -Or $result.Name.Contains("/obj/")
			$doNotCopy = $doNotCopy -Or $result.Name.Contains("/bin/")
			if($doNotCopy){
				#Write-Host " except " +  $result.Name
				continue;
			}
			$isFolder= (Get-Item  $fullName ) -is [System.IO.DirectoryInfo]
			if($isFolder){
				$fullName= $fullName + "/." #https://docs.docker.com/engine/reference/commandline/cp/
			}
            $cmd= "docker cp $fullName testNetCoreContainer:/usr/app/"+$result.Name.Replace("\","/")
            Write-Host $cmd
            Invoke-Expression -Command $cmd
            

        }
        default{
        	write-host "not handled " $result.ChangeType + " Change in "  + $result.Name
        }
    }

    
	
}