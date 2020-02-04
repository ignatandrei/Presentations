#!/bin/bash
sudo Xvfb +extension RANDR :0 -ac -screen 0 1280x720x16 > /dev/null &

sleep 5

sudo x11vnc -forever -create -display :0 >/dev/null 2>&1 &
sudo -u vscode code "$@"

sleep 5

WID=`xwininfo -display :0 -root -children | grep "Visual Studio" | grep -Eo '0x[a-z0-9]+'`
xdotool windowmove $WID 0 0
xdotool windowsize $WID 1280 720

sudo /websockify/run --web /web 80 localhost:5900 >/dev/null 2>&1 