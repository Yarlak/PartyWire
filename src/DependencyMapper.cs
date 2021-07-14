using System;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*
	The 'DependencyMapper' contains all the methods needed for an object to resolve its 'PartyWired' dependencies

	All game scenes should contain a single instance of a class that extends 'DependencyMapper'

	The 'SetUpDependencySystem()' method is used by extending classes to define the objects that can be 'PartyWired'
	(See 'PartyWireConfigurer' in the Scripts/Demo for an example)

	All fields marked as 'PartyWired' from within a class must have public 'get' and 'set' methods

-------------------------------------------------------------------------------------------------------------

	Order of operations on startup:
	1. objects are created and added to the DependencyStore in 'SetUpDependencySystem()'
	2. resolve all 'PartyWired' dependencies of objects added to the DependencyStore
	3. iterate through all objects in the DependencyStore and if the object implements 'Initializable' call 'Initialize()'
*/
public abstract class DependencyMapper : MonoBehaviour
{
	public static DependencyMapper _instance;

	// The 'DependencyStore' contains all the objects that can be 'PartyWired'
	protected DependencyStore store;

	// Called before start functions
	void Awake()
	{
		_instance = this;
		store = new DependencyStore();
		SetUpDependencySystem();
		WireStoredDependencies();
	}

	// Start is called before first frame update in scene
	void Start()
	{
		InitializeWiredDependencies();
	}

	protected abstract void SetUpDependencySystem();

	// After 'SetUpDependencySystem()' is called - all 'PartyWired' dependencies of objects currently in the 'DependencyStore' are resolved
	// This way we don't need to worry about the order in which original objects are added to the 'DependencyStore' in case of circular references
	protected void WireStoredDependencies()
	{
		foreach(Dictionary<string, object> tempObjects in store.store.Values)
		{
			foreach(object temp in tempObjects.Values)
			{
				ResolveAllDependencies(temp);
			}			
		}
	}

	// Every object that is instantiated outside of the 'SetUpDependencySystem()' method must call 'ResolveAllDependencies()' on itself in order to resolve all 'PartyWired' fields
	public static void ResolveAllDependencies(object obj)
	{
		if (obj != null)
		{
			foreach (PropertyInfo props in obj.GetType().GetProperties())
			{
				if (Attribute.IsDefined(props, typeof(PartyWired)))
				{
					Type type = props.PropertyType;

					PartyWired partyWired = props.GetCustomAttribute(typeof(PartyWired)) as PartyWired;
					MethodInfo method = _instance.GetType().GetMethod("ResolveDependency").MakeGenericMethod(new Type[] { type });

					object tempObj = method.Invoke(_instance, new object[] { partyWired.name });

					props.SetValue(obj, tempObj);
				}
			}
		}		
	}

	// In case objects created in 'SetUpDependencySystem()' require some initialization after their dependencies are resolved
	// Iterate through all objects in the 'DependencyStore' - if the object implements the 'Initializable' interface run the 'Initialize()' method
	protected void InitializeWiredDependencies()
	{
		foreach(Dictionary<string, object> tempObjects in store.store.Values)
		{
			foreach (object obj in tempObjects.Values)
			{
				if (obj.GetType().GetInterfaces().Contains(typeof(Initializable)))
				{
					((Initializable)obj).Initialize();
				}
			}
		}
	}

	// Resolve a dependency of type 'T' and tag 'name'
	// Tags exist in case there are more than one object of a specific type in the 'DependencyStore'
	// If there is only one object of type 'T' in the 'DependencyStore' then no tag is necessary to retrieve it
	public T ResolveDependency<T>(string name)
	{
		return (T) store.GetDependencyFromStore(typeof(T), name);
	}
}
