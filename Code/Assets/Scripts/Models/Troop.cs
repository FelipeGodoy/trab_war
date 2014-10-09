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
			currentTerritory = value;
		}
	}

	public Player CurrentPlayer{ get{ return CurrentTerritory.CurrentPlayer;}}

	public static Troop Create(Territory territory){
		GameObject prefab = Resources.Load<GameObject>("troop");
		GameObject g = Instantiate(prefab) as GameObject;
		g.AddComponent<AngularSpeedStart>();
		g.GetComponent<TroopInTerritory>().territory = territory;
		g.transform.parent = territory.transform;
		Troop troop = g.GetComponent<Troop>();
		troop.currentTerritory = territory;
		troop.rigidbody.position = territory.SpawnPosition;
		troop.renderer.material = troop.CurrentPlayer.troopMaterial;
		return troop;
	}

}
