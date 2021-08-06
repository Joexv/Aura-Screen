### Air Screen

This repo is setup to keep track of code, 
ideas and the like during the initial development.

This program's goal is to provide aid to visual impairments
through color filters, and screen adjustments. 
It will idealy provide aid to those who have eye strain from computer use or eye floaters.
Theoretically continued use of the application will lead to long term eye strain relief.


Current Features
- Colored section that follows the mouse
- Color overlays on applications and windows
- Screen filtering
- Overlays on select parts of the screen
- A toolbox to control common toggles and settings
- Change the cursor on idle
- And probably other things! Yay!


### Setup

## Windows
- Goto the (Releases Tab)[https://github.com/Joexv/Air-Screen/releases] and download the latest release of Air Screen. 
You will only need the first ZIP file.
- Once downloaded, extract the ZIP anywhere on your computer. It's recommended to extract it somewhere easy to access, like your Desktop.
- Inside the newly created folder, simply open the AirScreen.exe.
- That's it! setting up Air Screen is nice and simple. No bulky installation required!


- Want to have AirScreen open when you start your computer? Right click the AirScreen.exe and click Copy.
- On your keyboard press the Windows Key + R to open the Run dialog.
- Type in "shell:startup" then hit run.
- In the newly opened folder, right click any empty space, and hit 'Paste Shortcut'.
- To remove the program from start up simply delete this same shortcut!

If you experiance any issues or simply want to backup your configuration for any reason, the program's configuration file is located in %localappdata%\AirScreen\


## MacOS
Since Air Screen is developed in C# .Net, it only works natively in Windows. This is nice for Windows users as they
don't have to worry about compatability at all. They run it and it will jsut work. Because of this the MacOS setup is a bit tricker. 
There are a few options, some cost money, some are free. Choose the method you are comfortable with.

# MacOS Parallels - The most robust, but costs $$
Using Prallels gives you some added functionality via the Comprehensive mode which will overlay a Windows Program ontop of your MacOSX desktop.
Parallels, does cost money. If you already have it great! Setup is easy!
- Download Parallels from https://www.parallels.com/
- Follow the Prallels setup instructions (HERE)[https://download.parallels.com/desktop/v16/docs/en_US/Parallels%20Desktop%20User's%20Guide.pdf]
Once Parallels is installed and setup, simply follow the same instructions for the Windows Section!

# MacOS Bootcamp - Identical to the Windows Use case as you just boot windows. Technically free
- Follow the Bootcamp setup from Apple https://support.apple.com/en-us/HT201468
- Boot into the new Windows Partition, and then follow the Windows instructions
You can migrate from Bootcamp to Paralles in the future for ease of use without having to reinstall Windows!

# MacOS VMWare - Identical to running in Windows - free for personal use
Using VMWare is not recommended as it will require your computer to run a full version of Windows alongside your already running MacOS.
- Install your Windows VM following the instructions form VMWare https://kb.vmware.com/s/article/2128765
- Setup AirScreen in the Guest OS as you would normally in Windows.