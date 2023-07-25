# Mujoco to Unity Model Conversion Script

This repository contains a script that enables the conversion of MuJoCo models (XML format) to Unity prefabs. The script is designed to work with basic 3D objects initially, and it should be added to an empty GameObject in Unity. The purpose of this script is to provide a starting point for converting MuJoCo models to Unity, and you can further enhance it to handle more complex models in the future.

## Prerequisites

Before using the script, ensure that you have the following:

1. Unity installed on your system (tested with Unity 20xx.x, but it should work with other versions).
2. A Unity project set up with basic prefabs representing the desired objects for conversion. These prefabs will serve as the basis for creating the MuJoCo models in Unity.

## Getting Started

Follow these steps to use the conversion script:

1. Clone or download this repository to your local machine.

2. Open your Unity project where you want to perform the conversion.

3. Create the basic prefabs that correspond to the objects you want to convert from MuJoCo. Ensure that each prefab has a unique name and contains the necessary components for rendering and physics simulation.

4. Create an empty GameObject in the Unity project. This will be the GameObject where you will attach the conversion script.

5. Copy the `mujocoToUnity.cs` script from this repository into your Unity project's Assets folder.

6. Attach the `mujocoToUnity.cs` script to the empty GameObject you created in step 4.

## Using the Script

With the setup completed, follow these steps to convert a MuJoCo model to a Unity prefab:

1. Locate the MuJoCo model in XML format that you want to convert and attach to the project.

2. In the Unity editor, select the empty GameObject where you attached the `mujocoToUnity.cs` script.

3. In the Inspector window for the GameObject, you will see a new section called "MuJoCo to Unity Conversion."

4. Drag and drop the MuJoCo XML file into the designated field in the Inspector.

5. Click on the play button in the Inspector to initiate the conversion process.

6. The script will parse the MuJoCo XML and attempt to create corresponding prefabs in the Unity project based on the information in the XML.

7. After the conversion process is complete, check your Unity project to find the newly created prefabs. They should now represent the objects from the MuJoCo model.

## Improving the Script

As mentioned earlier, this script is a basic version to get you started. You can further improve it to handle more complex MuJoCo models by implementing additional features and functionalities. Some ideas for improvement include:

1. Supporting more advanced 3D features like colliders, rigidbodies, and custom materials during the conversion process.

2. Handling different types of MuJoCo model elements, such as joints, actuators, and sensors.

3. Implementing error handling and validation to ensure the MuJoCo XML is properly formatted before attempting conversion.

4. Providing options for scaling, rotation, and translation of the converted objects to better fit your Unity project's scene.

## Contributing

Contributions to this repository are welcome! If you have any improvements or additional features to suggest, feel free to submit a pull request. Let's work together to make this script a more robust and versatile tool for converting MuJoCo models to Unity prefabs.

## License

This project is licensed under the [MIT License](LICENSE).
