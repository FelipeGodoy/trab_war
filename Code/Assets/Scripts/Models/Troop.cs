using UnityEngine;
using System.Collections.Generic;

public class Troop : MonoBehaviour {

	private Territory currentTerritory;

	public int Weight{get;set;}

	public Territory CurrentTerritory{
		get{ 
			return currentTerritory;
		}
		set{
			currentTerritory.RemoveTroop(this);
			currentTerritory = value;
			currentTerritory.AddTroop(this);
		}
	}

	public Player CurrentPlayer{ get{ return CurrentTerritory.CurrentPlayer;}}

}
