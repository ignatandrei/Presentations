Stop-Service *docker*
taskkill /IM "dockerd.exe" /F
taskkill /IM "Docker for Windows.exe" /F
Stop-VM MobyLinuxVM
Optimize-VHD -Path ":\Users\Public\Documents\Hyper-V\Virtual hard disks\MobyLinuxVM.vhdx" -Mode Full
Start-Service *docker*
& "c:\program files\docker\docker\Docker for Windows.exe"
