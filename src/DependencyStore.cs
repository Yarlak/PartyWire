using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/*
	The DependencyStore holds 'PartyWireable' objects that have been instantiated in the scene

	The all objects of a given type are stored in a Dictionary where the key is the tag for that specific object and the value is the object itself
	All of these Dictionaries are then stored as values in another Dictionary (the key of the larger Dictionary is the type of the objects in that specific smaller Dictionary)
*/
public class DependencyStore
{
	public Dictionary<Type, Dictionary<string, object>> store = new Dictionary<Type, Dictionary<string, object>>();

	public void AddToDependencyStore(object obj, string name = null)
	{
		if (name == null)
		{
			name = obj.ToString();
		}

		Type type = obj.GetType();

		if (!store.ContainsKey(obj.GetType()))
		{
			store[type] = new Dictionary<string, object>();
		}

		Dictionary<string, object> tempObjects = store[type];

		if (tempObjects.ContainsKey(name))
		{
			throw new Exception("name of " + name + " already exists for object of type " + type);
		}

		tempObjects.Add(name, obj);
	}

	public void RemoveFromDependencyStore<T>(string name)
	{
		if (store.ContainsKey(typeof(T)))
		{
			Dictionary<string, object> tempObjects = store[typeof(T)];
			tempObjects.Remove(name);
		}
	}

	// This method is used to retrieve items from the store
	// If the store does not contain a Dictionary for that specific type it will check if there are any objects that inherit from 'type'
	public object GetDependencyFromStore(Type type, string name = null)
	{
		Dictionary<string, object> tempObjects = null;

		if (!store.ContainsKey(type))
		{
			foreach(Type tempType in store.Keys)
			{
				if (tempType.IsSubclassOf(type))
				{
					tempObjects = store[tempType];
				}
			}

			if (tempObjects == null)
			{
				throw new Exception("no object of type " + type + " found");
			}
		}

		tempObjects = store[type];

		if (name != null)
		{
			if (tempObjects.ContainsKey(name))
			{
				return tempObjects[name];
			}
			else
			{
				throw new Exception("no object of name " + name + " found for type " + type);
			}
		}

		if (tempObjects.Keys.Count == 1)
		{
			return tempObjects.First().Value;
		}

		throw new Exception("nothing found");
		
	}
}
