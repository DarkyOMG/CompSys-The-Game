# CompSys-The-Game
CompSys - the Game is a Computer game for computer science students, trying to learn the aspects of computersystems.
A fully build version can be found on https://darkyomg.itch.io/compsys-the-game.

# Implementation
The implementation uses the Unity scene-management to distinguish between minigames read on, to understand how this works.
## Folder structure
The asset-folder by itself is only structured into the games aspects, like Audio or Animation. These folders hold all Assets for all scenes. Since the number of assets is rather small, a deeper hierarchy doesn't seem to be needed, but could be added in the future. Only exceptions are the Scripts and the ressources folder. 
### Ressources
Ressources are the assets, that can or will be loaded into the game, such as ScriptableObjects, prefabs or certain sprites.
* Container
  
ScriptableObjectsVariables, which are being used to save variables class-independently. These can then be accessed by all classes in the scene, without the need for a designated monobehaviour. These hold information, such as the currently configured mouse-sensetivity or Audio-volumes. Also loaded here are the ScriptableObjectSingletons, which are needed to manage global aspects of the game. See more in the chapter **Global Management**.
* LevelSetups
  
In this folder are pre-defined Level-Setups for different game modes. These Level-Setup files hold information about the difficulty, game-mode and other information, which are needed to load a level for a specific minigame.
* Prefabs
  
Here are all prefabs stored. These can be loaded on runtime. The folder "Schaltenetze" holds special pre-defined elements for the minigame "Powercity". This folder is needed, to load all unlocked prefabs into the side-bar (left) on the powercity stage and is currently hard-coded into the unlocking-mechanism. Chaning the name of the folder right now, would break the powercity-stage. This can be changed in the future.
* Sprites
  
Sprites to be loaded into the scenes, when needed.
### Scripts
This folder holds all scripts of the game. The top-level of the folder holds all Scripts, that are used for the general-game-managment, like Scene- and inputmanagement.
The Gamemodes folder holds scripts specific for certain minigames and scenes, like local managers or special UI management for the Tutorial scene or powercity-minigame. See chapter **Managers** for more details.
## Managers
Managers are used to handle certain aspects of the game. For example the Audiomanager manages all audio-related behaviour, while the inputhandler handles all inputs.
In this implementation, the managers are divided into global managers and local managers. Global managers are used in every scene of the game. They handle aspects of the game that are needed in every game mode. Local managers are what define the rules of a specific scene and therefore the current game mode.
### Global Management
Global management of the game is mostly done through ScriptableObjects-Singletons. ScriptableObject-Singletons are ScriptableObjects, which can be saved as an asset, that hold gamelogic. As the name suggests, they are designed with the singleton-pattern. These objects can be found in Ressources/Container/Global Managers. They inherit from the SingletonScriptableObject-class in scripts/ScriptableObjectScripts. Their specific scripts then hold logic to manage the game over multiple scenes. 
#### Inputhandling
Input is handled through the InputHandler class, which is a ScriptableObject-Singleton. Local Managers can call the ChangeGIT-methode to switch to a specific input-behaviour indicated by the enum, given with the method-call. Right now, all Inputbehaviour is implemented into the InputHandler and is switched through the ChangeGIT-Method. This could and should be changed in the future, so the InputHandler doesn't hold all possible handling-methods in it's class.
To change or add Input-handling at the current state, implement the handling functions in the InputHandler class, if needed, add a new value to the GameInputType-enum at the top of the class, which represents your Input-behaviour. At last, change the InputMasterCompSys-Asset in Scripts/Inputhandling by either adding a new ActionMap or use an existing Actionmap, that already has all interaction-capabilities you need. Add the Corresponding Functions in the "SetFunctions" and "ResetFunctions" to your actionmap.
#### Scene Management
To Switch between Game-modes (which is switching between scenes), use the SceneTransitionManager. This global manager is also implemented as a singleton. You can Transtition to scenes directly, whilst providing the corresponding scene-ID, or you can transition to other scenes using LevelSetup-Assets. This is particularly useful, when using buttons to initiate the transition. 
To Add your own scene, add a new value to the GameType enum in the SceneTransitionManager class. Also add declarations to the if-statements (on higher number of values this needs to be changed to a dictionary or switch-statement). The nextScene variable holds the scene-ID, to which the transition will lead. Scenes can not be loaded directly. This is due the fact, that tutorials-scenes might be loaded before the actual scene is loaded. This is indicated by the parameters of the LevelSetup-ScriptableObject (See Scripts/ScriptableObjectScripts/LevelSetupSO for more information). The nextScene variable will also be available to the tutorial scene, to indicate which scene needs to be reached after the tutorial.
The _gameMode.value declaration sets the ScriptableObjectVariable GameMode to the desired GameMode. This is only used on the tutorial scene, to load the correct tutorial-panel (See the TutorialHandler-Class in Scripts/Gamemodes/Tutorial for more information).
#### Game State Management
Game states are managed through encoding of the game state to an int, which is then saved to the playerprefs. Right now the game state works with just one int.
For each level of the game, one bit is used to indicate if it is unlocked(1) or not(0). Bits 1-4 are used for Number push, 5-8 for trackymania, 9-12 for Harvest Bool and 13-16 for Powercity. Bits 17-21 are currently used for elements of powercity. 
On game-start only the first and the 17th bit is set to 1 to unlock the first level of Number push and basic powercity elements, which are needed later (This should be changed soon). 
This is implemented in the GameStateHandler class (located at Scripts/Gamemodes/Menu). For the menu, it locks and unlocks all buttons in the scene, to their corresponding state. The class can also be called by it's static functions GetGameState and SetGameState, which allow to retrieve the currently set gamestate-int or to add to the gamestate int.
To issue a reward, a local game manager can use SetGameState, to bitwise add unlocked bits to the gamestate. This is done in the Solve-Methods of the **Local Manager** classes, but could be called from any class. Rewards can also be given with the challenge, by directly writing them in the corresponding field in the LevelSetup-Asset.
### Local Managers
Right now, local management is not handled through a pre-defined class, but is done by game-logic classes in the corresponding Game-Modes. These need to be generalized in the future.
Namely, these classes are DecimalManager for Number push, KVHandler for Harvest bool, PlotterHandler for Powercity and CMOSHandler for Trackymania.
Their use is to set the correct Inputbehaviour using the ChangeGIT function on the InputHandler-Singleton, building a gameboard or populating the scene with objects and implementing a solving-algorithm.
#### Local Variables
To keep track of certain objects in the scene, without searching for them using GameObject.find() or similiar methods, all scenes use Object-Reference-ScriptableObjects, which can be found in Ressources/Container/Object-References. Most of this is done by the SceneInitializer-script, which needs to be placed on the main-Camera of every scene. It fills the Objects-Reference-ScriptableObjects with the Objects in the scene, making them available to every script, without the need of looking them up. The SceneInitializer also needs to be present in the scene, to reference the global manager assets. This is a bit of a trick, since unity doesn't load ressources, that are not referenced, the singletons are not loaded, therefore there are no instances to get. To work around this problem, the SceneInitializer references all global managers, to indicate unity that they need to be loaded (Source: https://www.youtube.com/watch?v=cH-QQoNNpaI).


## How to build your own Minigame
To extend the game with more minigames, you would first need to create a scene for it. In the scene, add the Scene-Initializer as a component to the main camera and populate it's fields in the inspector. You can find the Object-Reference-Assets in Ressources/Container/Object-References. The Objects in your scene are meant to be set as the corresponding "GO" Objects, like "Pause Menu GO" is the Pause-Menu-Panel GameObject that is in your scene.
Then your scene is ready to be populated. 
If you need to implement Input-behaviour, checkout the **Inputhandling**-chapter. Any class in the scene then needs to call the ChangeGIT-function to activate the correct Input behaviour. Preferably the local Game-manager, like those described in **Local Managers**-section. 
Too make your scene available through a menu, add scene-transitioning capabilities. See **Scene Management**-Chapter to understand how this is done.
To add a reward for completing your game, checkout the **Game State Management**-chapter.
