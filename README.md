![image](https://user-images.githubusercontent.com/67195196/219104625-dd5f889e-24d8-4def-b2f8-74b8f733cac5.png)


The program is designed to conveniently launch different Steam accounts with games that require DRM activation (for example, Denuvo)

Explanation:
Steam folder - the folder where steam.exe is located, for example, if the path to steam.exe is: D:\Programms\Steam\steam.exe, then the folder is D:\Programms\Steam\

1. Buy an acc where there is a game you need
2. Take steam.exe from main folder and copy it to a new folder, name it whatever you like, run it, it will download 250MB at startup and after unpacking the directory size will be ~1GB, so it will be a new folder with another copy of steam
3. Go to the purchased account through the new steam (run steam.exe from new folder) (be sure to check the remember me checkbox), download the game, run it, go to the main menu, close the game, wait until the synchronization with the steam cloud takes place after closing, turn off the Internet connection (through the device manager / wifi even easier), go offline in steam (top left Steam -> Go Offline), right-click on the steam icon in the taskbar and select Exit
4. Run account launcher, click add, enter name (any), account login (no spaces, lowercase), path to new steam folder
5. Test that everything is working correctly, select the account in the list and click Run, if everything is done correctly Steam will start without asking a login and password in offline mode.
6. Play
7. To enter in the main account, it must also be added to the software (any name, account login and the path to the main steam folder) and run through the software.
