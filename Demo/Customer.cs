using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Any instance of the 'Customer' class in will immediately use the 'DependencyMapper' to resolve their 'pizzaHut', 'burgerKing', and 'foodCourt' dependencies

	Notice that because we have added two instances of 'Restaurant' to our 'DependencyStore' we must specify the object with their tag
	We have only added one instance of 'FoodCourt' so no tag is necessary in order to resolve it
*/
public class Customer
{
	[PartyWired("Pizza Hut")]
	public Restaurant pizzaHut { get; set; }

	[PartyWired("Burger King")]
	public Restaurant burgerKing { get; set; }

	[PartyWired]
	public FoodCourt foodCourt { get; set; }

	private string name;

	public Customer(string name)
	{
		DependencyMapper.ResolveAllDependencies(this);

		this.name = name;

		MonoBehaviour.print("Customer: " + name + " must decide between " + pizzaHut.name + " and " + burgerKing.name + " at the " + foodCourt.name);
	}

}
