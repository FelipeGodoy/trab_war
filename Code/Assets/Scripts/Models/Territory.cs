using UnityEngine;
using System.Collections.Generic;

public class Territory : MonoBehaviour {

	private HashSet<Troop> troops;
	public List<Territory> neighbors;
	public Continent continent;
	public string name;
	public Transform troopSpawn;

	public Player CurrentPlayer{get; set;}

	public int TroopsCount{
		get{
			return troops.Count;
		}
	}

	void Start(){
		continent = GetComponentInParent<Continent>();
	}

	void Awake(){
		if(troops == null){
			troops = new HashSet<Troop>();
		}
		if(neighbors == null){
			neighbors = new List<Territory>();
		}
	}

	public bool RemoveTroop(Troop troop){
		return troops.Remove(troop);
	}

	public bool AddTroop(Troop troop){
		return troops.Add(troop);
	}

	void OnDrawGizmos(){
		if(neighbors != null){
			foreach(Territory territory in neighbors){
				if(territory.neighbors.Contains(this)){
					Gizmos.color = Color.cyan;
				}
				else{
					Gizmos.color = Color.blue;
				}
				Gizmos.DrawLine(this.transform.position, territory.transform.position);

			}
		}
	}


}
