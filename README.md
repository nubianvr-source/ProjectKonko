# Project Konko
Project Konko is an Open Source Virtual Reality lesson creator built in Unity and designed primarily for the Oculus Go. It seeks to provide the tools that will allow developers, creators and educators to:
- deploy 2D & 360 video lessons in Virtual Reality
- manipulate 3D object in Virtual Reality environments
- build multiple choice lesson assessment in Virtual Reality
- maintain a badge & leaderboard functionality for Virtual Reality lessons.

## Getting Started

##### Prerequisites
-	Latest version of Unity that can be downloaded from here. This game project was created using Unity 2019.2.10f1.
-	The game was tested on Android, but one should be able to port it to other platforms too.

##### Setup

**Using Git Bash**

Click on the clone or download button and copy the https link (or SSH link if you prefer) and clone it into your preferred directory.

**Using GitHub Desktop**

Click on the clone or download button in the GitHub and click on Open in Desktop. Wait till the project is done cloning and then add it to your list of projects by using Unity Hub and add a new project by browsing to the folder where you cloned the project into. 

## A Note about Asset Sources

Assets for this project were design and created by the Lime X Honey (https://www.behance.net/limexhoney) team, and (we state the permission of reusability here)
Project Structure
Project structure is split into various graphical elements, gameplay prefabs, materials and textures, the primary scene, and scripts.
##### Environment
These are various assets required for the overall scene. This is predominantly the skybox, distant classroom objects, and platform. 

###### Graphics
These are more locally used assets, including fonts, cameras, UI text, and player avatar. These assets are linked to various prefabs, and should be more carefully swapped out in individual prefabs before deleting.

##### Prefabs
Contains prefabs for the player, cameras, vr grabber, and circuit components.

##### Scene
For now, just one primary scene, this is the starting point for this sample.

##### Scripts
The simple scripts that get everything working. We tried to keep it minimal, easy to understand, and adjustable, so even those not very comfortable with programming can hopefully get an idea of how it's working.

##### Scene Structure
For now, the main scene is located at Assets/Scene/AlphaBuildScene. The most important elements for understanding the scene here are Oculus VR Camera, Event system responsible for understanding UI interactions made and VRGrabber.

## Contributing
Please read CONTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.(this file would have to be created)

## License
This project is licensed under the MIT License - see the LICENSE file for details.

