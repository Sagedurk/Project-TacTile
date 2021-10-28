# Project TacTile
SRPG inspired by *Shining Force: RotDD*, *Golden Sun*, and *Avatar: TLA*. Started development in 2019
</br>
Developed primarily for PC with controller support, in **Unity 2020.1.2f1**

All code can be found in Assets/Resources/Scripts
</br>
The most important scripts can be found in Assets/Resources/Scripts/Combat Scripts
</br>
which is where you'll find *UnitSkillManager*, *UnitBaseClass*, *DummyClass*, *TacticsCombat*, *PlayerMovement*, the former three can be found in the sub-folders of Combat Scripts

### Challenges
* Feeling the need to refactor the whole project due to difficulty understanding why I did things the way I did
* Tried to create a skill system using C# reflection, scrapped it in favor of *UnitSkillManager*, due to the feeling of more problems arising than getting solved

### Future plans
* Fix any problems that impedes the playing of the prototype
* Refactoring the whole project for better readability and easier understanding
* Starting on a status ailment system
* Changing the KBM input to be less clunky
* Including more

## INSTALLATION INSTRUCTIONS
Download Unity **2020.1.2f1** and open the project's root folder in Unity.
</br>
Before running it inside the Unity editor, go to Assets/Resources/Scenes and open the *Prototype PvP* scene.

## USAGE INSTRUCTIONS
You directly control a cursor, which you can then use to interact with your units, telling them where to go and which action to use
</br>
Both teams are currently controlled with the same cursor
</br>
The 4 types of actions a unit can use are **Attack**, **Skills**, **Items**, and **Defend**
</br>
The goal is to defeat all units of the opposing team, no win condition exists as of now
</br> 
The stat balancing between units is completely unbalanced, for development purposes

### Controls
| Action | KBM | Gamepad |
| ------ | --- | ------- |
| Camera Zoom In | Numpad + | Right Trigger |
| Camera Zoom Out | Numpad - | Left Trigger |
| Cursor Movement| WASD | Left Stick |
| Camera Rotation | Arrow Keys | Right Stick |
| Free Camera Toggle | Left CTRL | D-PAD Left |
| Free Camera Movement | WASD | Left Stick |
| Accept Action | Enter | Button South |
| Cancel Action | Escape | Button East |

<img src="/Screenshots/Action%20Menu.png" width="75%" height="75%" />
</br>
</br>

### No license provided
