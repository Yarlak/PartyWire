using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Notice that the 'Restaurant' class never calls 'DependencyMapper.ResolveAllDependencies(this)' but is still able to resolve it's 'foodCourt' dependency

	This is because our 'Restaurant' objects are created in 'SetUpDependencySystem()' method in 'PartyWireConfigurer' (which extends 'DependencyMapper')
	After an object is added to the 'DependencyStore' in the 'SetUpDependencySystem()' method - it will automatically call 'DependencyMapper.ResolveAllDependencies()' to resolve it's dependencies

	Notice that 'Restaurant' also implements 'Initializable' which allows us to perform some initialization after all 'Partywired' dependencies are resolved

*/
public class Restaurant : Initializable
{

	public string name;
	public string foodType;

	[PartyWired]
	public FoodCourt foodCourt { get; set; }

    public Restaurant(string name, string foodType)
	{
		this.name = name;
		this.foodType = foodType;
	}

	public void Initialize()
	{
		MonoBehaviour.print("Restaurant: " + name + " is a part of " + foodCourt.name);
	}
}
