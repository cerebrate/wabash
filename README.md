# wabash

A utility to hold a WSL session-set open continuously. Works in conjunction with wabashd.

So, you want to keep daemons or disowned processes running under Windows Services for Linux even without a bash console open? Well, look no further, friends and users, because wabash is here to help, a simple utility which keeps the WSL session going in the background all the time.

## DISCLAIMER

At present this is PRERELEASE, ALPHA-GRADE SOFTWARE. It still has rough edges. Be prepared for those. Don't complain, just send issues to this github. Or pull requests, if you prefer.

## Installation

The installation comes in two stages:

First, download the release file, and unzip it into a permanent home somewhere on your hard drive, accessible from WSL.

Then, go to that directory from a WSL shell, and run:

    sudo dpkg -i wabashd_1_wsl.deb
    
to install the wabashd pseudo-daemon. This works together with wabash.exe for Windows to keep your WSL session going. If that's not installed, wabash plain won't work. Once the package is installed, though, that's it.

## Usage

Run wabash.exe, which runs as a little orange icon down in your notification area. As long as it's there, there's a WSL session open. Hovering over it gives you a count in the form:

    Wabash: 0 sessions / 0 daemons
    
"sessions" is the number of WSL consoles you currently have open (technically, the number of "init" processes which are children of the initial pid 1 init, which should be everything you started with bash.exe); "daemons" is the number of children of pid 1 that _aren't_ more inits, which should be all the daemons and any disowned processes.

You can open wabash if you like, from its right-click menu or by double-clicking the notification icon, but all its window contains at the moment is the stream of raw update messages sent by wabashd, which are very likely of little interest. You can also ping the daemon from the right-click menu if you just want to double-check that the communications are going on correctly.

Exiting wabash from the right-click menu ends the WSL session (and all the daemons) ***if and only if*** there are no WSL consoles open: if there are, those will sustain the session until they are closed also. i.e., if you have a WSL shell going, you can exit and restart wabash and all the daemons, etc., will be preserved from one run to the next by the open shell.

If you need to kill wabash from Task Manager or otherwise terminate it without going through its exit menu, there will probably be a copy of wabashd left running. Before you can run wabash again successfully, you need to kill that:

    kill -TERM <pid>

...where pid is the pid of the /usr/bin/mono running mono-service, *not* that of start-wabashd.

Also, while this pre-release will let you, you cannot run more than one wabash at once; all but the first will fail to start properly and can't be quit from the menu. You'll have to kill them in Task Manager, kill the wabashd if it's still running, then restart one and only one.

Enjoy!
