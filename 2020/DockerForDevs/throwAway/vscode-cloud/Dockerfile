FROM ubuntu

RUN echo "tzdata tzdata/Areas select Europe" > /tmp/preseed.txt; \
    echo "tzdata tzdata/Zones/Europe select Brussels" >> /tmp/preseed.txt; \
    debconf-set-selections /tmp/preseed.txt && \
    apt-get update -q && \
    apt-get install -y --no-install-recommends tzdata
	
# Update apt cache
RUN apt-get update

# Install vscode dependencies
RUN apt-get -y --no-install-recommends install libgtk2.0-0 libgtk-3-0 libpango-1.0-0 libcairo2 libfontconfig1 libgconf2-4 libnss3 libasound2 libxtst6 unzip libglib2.0-bin libcanberra-gtk-module libgl1-mesa-glx mono-complete curl build-essential gettext libstdc++6 software-properties-common wget git xterm automake libtool autogen rxvt-unicode-256color nodejs libnotify-bin aspell aspell-en htop

# Install vscode
#RUN wget -O VSCode-linux-x64-stable.zip 'https://go.microsoft.com/fwlink/?LinkID=620884'
#RUN tar xvzf VSCode-linux-x64-stable.zip --directory /usr/local/
RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
RUN mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
RUN  add-apt-repository -y "deb https://packages.microsoft.com/repos/vscode stable main "
#RUN  apt update
#RUN mkdir /usr/local/
RUN  apt -y install code
#RUN cd /usr/local/bin && ln -s /usr/local/VSCode-linux-x64/bin/code code
#RUN cd /usr/bin/code && ln -s /usr/bin/code code
RUN whereis code
# Install xvnc
RUN apt-get -y --no-install-recommends install x11vnc xvfb net-tools python xfonts-base x11-utils xdotool
ENV DISPLAY=:0

# Add user
RUN apt-get -y --no-install-recommends install sudo
RUN useradd -ms /bin/bash vscode
RUN bash -c 'echo "vscode ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers'

# Add init script
ADD init.sh /init.sh
RUN chmod +x /init.sh

# Add html client
ADD web /web
RUN bash -c "git clone https://github.com/kanaka/websockify /websockify"
RUN chown -R vscode /websockify
RUN chown -R vscode /web

USER vscode
EXPOSE 80

ENTRYPOINT ["/init.sh"]
#CMD tail -f /dev/null