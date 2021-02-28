# World Traveller Project
A unity project to learn how shaders work to build to the final goal of creating multiple worlds in one level. The idea is to have one level that the character can move in, and at the press of a button create a  bubble where everything outside the bubble is the original world, but inside the bubble is a piece of another.


The project makes use of Unity's Universal Render Pipeline / Lightweight Render Pipeline package for the shaders, and Cinemachine for camera movement.


# Shaders
I wanted to make two shaders, one that reveals terrain in an area and one that removes terrain from the same area, resulting in a "seamless" transition between areas

##Cut Shader
This shader clips any pixels that are less than radius away from an point, both can be changed in code or manually. This essentially makes a sphere cut
![Cut Shader](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/RegularSliceShader.PNG)

##Reveal Shader
The opposite of the cut shader, works the same way but clips pixels that are greater than a radius away from a point
![Reveal Shader](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/RevealRegular.PNG)

##Smooth Cuts
The cuts are currently too sharp and not really fun to look at, so I added a colored edge effect using noise to soften the transition and make it look like the objects are dissolving rather than being cut.
![Cut Smoothed](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/CutShaderNoiseOutline.PNG)

![Reveal Smoothed](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/RevealNoiseOutline.PNG)

##In practice
I used both of these shaders in combination with a simple Fersnel effect shader to create an openable portal that switches the world wherever the player is standing by pressing *Space*
![Regular World](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/WorldNoBubble.PNG)

![Multi World](https://github.com/tobymcguire0/images/blob/main/WorldTravellerImg/WorldBubble.PNG)
