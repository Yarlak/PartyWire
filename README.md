# PartyWire
This is a dependency management framework written in C# for scenes created with the Unity Game Engine. Very similar idea to how dependency injection works in the Spring Framework (used for developing web api's in Java). See [Customer](https://github.com/Yarlak/PartyWire/blob/main/Demo/Customer.cs) for an example of the implementation.

Dependencies of a class that are decorated with the ***PartyWired*** attribute can be easily resolved for newly instantiated objects without having to pass these dependencies through a class's constructor.

The [src](https://github.com/Yarlak/PartyWire/tree/main/src) folder contains all the scripts needed for the dependency management system. And the [Demo](https://github.com/Yarlak/PartyWire/tree/main/Demo) folder shows an example of how you can implement this system with some simple C# classes.


# How to Use
1. Create a class that extends [DependencyMapper](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyMapper.cs) and make sure only a single instance of it exists in the game scene
2. In the class from Step 1 override the [SetUpDependencySystem()](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyMapper.cs#L47) method to create all the objects you want to be ***PartyWireable*** and add them to the [DependencyStore](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyStore.cs#L17) - see [PartyWireConfigurer](https://github.com/Yarlak/PartyWire/blob/main/Demo/PartyWireConfigurer.cs#L12-L25) for reference
3. All objects added to the [DependencyStore](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyStore.cs) as part of Step 2 will auto-resolve any ***PartyWired*** dependencies they may have - so the order in which you add objects in Step 2 is irrelevant
4. Now anytime you call the static method [ResolveAllDependencies(object obj)](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyMapper.cs#L63-L82) - any fields marked with the attribute [PartyWired](https://github.com/Yarlak/PartyWire/blob/main/src/PartyWired.cs) in the class definition of ***obj*** will be resolved to whatever object is currently stored in the [DependencyStore](https://github.com/Yarlak/PartyWire/blob/main/src/DependencyStore.cs). That is of course assuming a valid object of the required type has been added to the store. See [Customer](https://github.com/Yarlak/PartyWire/blob/main/Demo/Customer.cs) for an example.


