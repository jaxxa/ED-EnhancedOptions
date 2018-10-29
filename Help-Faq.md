# General Instructions

If you are having an issue with one of the options, please try the Following:
	1. Check that the Mod is Enabled in the Mod menu
	2. Go into the Options Menu, Select Mod Settings and then Select the Enhanced Option Mod
	3. Enable the Features that you want to Use
	4. After this Quit and Restart Rimworld, most of these setting are only applied when the game starts.
	5. Start / Load your game, hopefully it is working now.


# Testing without other mods

If it still does not work you can try the above steps with no other mods loaded.
If you are in the middle of a game or have an extensive mod list that you do not want to mess up, you can follow the following instructions taken from the Rimworld Readme file to point the save location to a different folder:

-----------------------------

OVERRIDING:
You can override the save data folder. This is useful, for example, if you want to install the game on a USB stick so you can plug and play it from anywhere.
To do this, add this to the end of the command line used to launch the game:

	-savedatafolder=C:/Path/To/The/Folder

So it'll look something like this:

	C:/RimWorld/RimWorld.exe -savedatafolder=C:/Path/To/The/Folder

If you don't start the path with anything, it'll be relative to the game's root folder. So you could do this, to have the game save data in a folder called SaveData in its own root folder:

	-savedatafolder=SaveData

Be sure the game is running with permission to modify the folder. It may not work properly if, for example, you run the game under default permissions on its own install folder.

----------------------------

# Report and Log File

If you are still having issues and want to provide a log file, please check that you have "Verbose Logging" enabled in the options menu as this is required to show some of the log messages for this mod.