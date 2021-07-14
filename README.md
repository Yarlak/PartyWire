# PartyWire
This is a dependency management framework written in C# for scenes created with the Unity Game Engine. Very similar idea to how dependency injection works in the Spring Framework (used for developing web api's in Java).

Dependencies of a class that are decorated with the 'PartyWired' attribute can be easily resolved for newly instantiated objects without having to pass these dependencies through a class's constructor.

The ***src*** folder contains all the scripts needed for the dependency management system. And the ***Demo*** folder shows an example of how you can implement this system with some simple C# classes.


# How to Use
1. Create a class that extends [DependencyMapper](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyMapper.cs) and make sure only a single instance of it exists in the game scene
2. In the class from Step 1. override the [SetUpDependencySystem()](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyMapper.cs#L47) method to create all the objects you want to be 'PartyWireable'


