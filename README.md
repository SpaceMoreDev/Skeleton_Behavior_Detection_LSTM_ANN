# Skeleton_Behavior_Detection_LSTM_ANN
3D action behavior is a system presented to detect 3D poses/actions using different methods and techniques.

A basic classification model of LSTM ANN architecture was constructed using Keras APIs to verify that the model can distinguish between the six types of animations.

The 6 different classes were represented by the following numbers which start from 0 and ends with 5:

![image](https://user-images.githubusercontent.com/60396165/183023635-4f116b0e-eb38-4d28-87d3-1ff85022e46c.png)

to import the model to unity, a WebSocket solution was used in this project to send data from unity to a python script that accesses the model (.h5) and input data into the model and outputs a prediction which is then sent back to unity by the WebSocket. 


data preparing and preprocessing happens when the animation data is collected from free Mixamo animation packs, where we first save the xyz positions of each bone in the rig as float numbers in a CSV excel file.
![image](https://user-images.githubusercontent.com/60396165/183023994-386233eb-5982-4de0-a67d-420b759ea89d.png)

This data is considered as an input for our model where we have animation frames, head and tail positions, and action class.

![image](https://user-images.githubusercontent.com/60396165/183023945-c69f9989-47c8-4214-b5fb-a13144ef73e4.png)

## Confusion matrix for the model:

![image](https://user-images.githubusercontent.com/60396165/183024042-be9a4ca8-ff93-4a40-98a4-3bf11e5b2b42.png)

## Tools 
− Anaconda enviroments: is a platform for efficient developing and applying AI and machine learning models.

− VS Code: An open-source notebook environment that enables developers to work and integrate their systems quickly.

− Blender: is a 3d modeling and animation program that allows developers to write python addons in-program.

− Adobe Mixamo: is an open-source animation/poses database available online by Adobe. 


### To run the project: 

Run the Python scripts\Websocket\Websocket.ipynb first to get data from the model to unity.
you can test animation sequences by adding animations to the character's animation controller
