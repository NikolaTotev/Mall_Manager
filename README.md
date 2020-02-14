



# Mall Manager Wiki

- [Installation](#installation)
	- Standard .exe installation
	- Downloading repo & building project in Visual Studio
- [UI Walkthrough](#ui-walkthrough)
	- Dashboard
	- Mall Menu
		- Rental spaces menu
		- Mall activities menu
		- Mall statistics page
	- Adding/Editing/Removing
		- Creating a mall
		- Add/Edit/Remove rental space 
		- Add/Remove activity
			-  For a room
			-  For a mall
	- Statistics
	- Using command mode searching
		
- [Program Data Files](#program-data-files)
	- Location
	- What they are used for
----------------------------------

# Installation
## Standard .exe installation
The easiest way to start Mall Manager is to download this repository. That is done like this:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/Downloading.png?raw=true" width="512" height="396.5">

After downloading the repository locate the .zip file on your computer and extract it.
Once the the extraction is completed locate the ***"Exe Lanucher"*** Folder:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/ExeFolder.png?raw=true">

Open it and run ***"User_Interface.exe"***. Thats it! You are now ready to use Mall Manager 1.0.

## Building in Visual Studio
Building in Visual Studio requires a couple more steps. 
Being by again downloading & extracting the repository file. Once complete locate the ***"C_Sharp_Course_Project"*** folder:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/VsFolder.png?raw=true">

Open it and locate the only ***.sln*** item inside:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/OpenSln.png?raw=true">

Double click it and if you have Visual Studio installed it should start up. If you do not have it you can download it here: https://visualstudio.microsoft.com/vs/
Once Visual Studio has started you may recive the following message:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/SecurityWindow.png?raw=true">

Follow these steps:
**1.** Select the checkbox
**2.** Press Ok.
Once you do it once, you may recieve it 2 more times. If you do execute the previous steps again. 
After that you should be greeted with an empty screen with the ***Solution Explorer*** at the top right of your screen.
You may notice the following warning messages & green underlines:

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/NuGetErrors.png?raw=true">

These are showing because when downloading from Github, the nessecary NuGet packages are not downloaded. This issue is easliy fixed by simply building the solution. This is done by right clicking the solution which will show the following menu, then click ***"Build Solution"*** :

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/BuildingSolution.png?raw=true">

The warnings should have gone away and you are ready to lanunch the program:
***1.*** Right-click on the User_Interface project
***2.*** From the menu select ***"Set StartUp Project"***

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/SettingAsStartupProj.png?raw=true">

***3.*** Press the **"Start"** button.

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/Starting.png?raw=true">

# UI Walkthrough
## Dashboard
This is the dashboard, from here you can create up to 2 malls or open a mall that is already created.

<img src="https://github.com/NikolaTotev/C_Sharp_Course_Project/blob/master/ReadMe_Images/Starting.png?raw=true">

## Mall Menu
Once you open a mall you are directed to this page. This is the mall menu. From here you can view:
- The rental spaces of the mall
- Activities for the mall as a whole
- Statistics for the mall as a whole

INSERT IMAGE

### Rental spaces menu
This is the rental spaces menu. From here you can **Add** and **Remove** rental spaced from the mall. 
You can also search by name or you can use  [CMS (Command Mode Search)](#using-command-mode-search-(CMS)) .

INSERT IMAGE

### Mall activities menu
This is the mall activities menu. From here you can **Add** and **Remove** activities from the current mall. 
You can also view statistics from the ***"Quick Stats"*** panel or open the full statistics from the ***"Stats"*** button.

INSERT IMAGE

### Mall statistics page
 This is the mall statistics page. Here you can view statistics about the mall.
 
INSERT IMAGE

## Adding/Editing/Removing
Below are all of the supported ***Add***, ***Edit*** & ***Remove*** operations that are supported. (This is not the final list of operations, the rest are put as **"future development opportunities"**)

**Note:** If not explicitly mentionted  the ***"Cancel"*** button returns you to the previous page and does not execute any operation
### Creating a mall
Creating a mall is done through this window. The add button is active once all fields have a valid value.

INSERT IMAGE

### Adding/Editing/Removing rental space
- Adding a rental space is done through this menu. As before the ***"Add"*** button becomes active only when the fields have valid values.

INSERT IMAGE

- Removing a rental space is done by selecting the rental space(s) you want to delete from the Rental Spaces menu and clicking the ***"Delete"*** button.

***Notice: Only spaces with 0 associated activities will be deleted.  If you attempt to delete a space that still have active activities you will recieve a warning.*** 

INSERT IMAGE

### Adding/Removing activity
There are two types of activity you can add:
***1. For a mall:***  This associates the created activity only with the mall, not a specific room.
***2. For a room:***  This associates the created activity only with the mall, not a specific room.
Both use the same interface: *(The same rules about the ***"Add"*** button apply as before.)*

INSERT IMAGE

### Statistics
### Using Command Mode Search (CMS)

# Program Data Files
## Location
## What they are used for
