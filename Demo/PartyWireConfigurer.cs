using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	In order to automatically resolve 'PartyWired' dependencies we must instantiate a single instance of a class that extends 'DependencyMapper'

	We use the 'SetUpDependencySystem()' method to create the objects we want to be 'PartyWireable' and add them to the store
*/
public class PartyWireConfigurer : DependencyMapper
{
	protected override void SetUpDependencySystem()
	{

		Restaurant pizzaHut = new Restaurant("Pizza Hut", "Pizza");
		Restaurant burgerKing = new Restaurant("Burger King", "Burgers");

		store.AddToDependencyStore(pizzaHut, "Pizza Hut");
		store.AddToDependencyStore(burgerKing, "Burger King");

		FoodCourt foodCourt = new FoodCourt("Awesome Food Court");

		store.AddToDependencyStore(foodCourt);

	}
}
