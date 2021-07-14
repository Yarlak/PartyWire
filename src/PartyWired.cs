using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PartyWired : Attribute {

	public string name { get; private set; }

	public PartyWired()
	{
		name = null;
	}

	public PartyWired(string name)
	{
		this.name = name;
	}
}