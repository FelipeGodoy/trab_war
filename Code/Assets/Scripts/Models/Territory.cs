using UnityEngine;
using System.Collections.Generic;

public class Territory : MonoBehaviour {

	private Player _currentPlayer;
	private HashSet<Troop> troops;
	public List<Territory> neighbors;
	public Continent continent;
	public string name;
	public Transform troopSpawn;

	public Vector3 SpawnPosition{
		get{
			if(troopSpawn == null){
				return this.transform.position;
			}
			return troopSpawn.transform.position;
		}
	}

	public Player CurrentPlayer{
		get{
			return _currentPlayer;
		}
		set{
			_currentPlayer = value;
			_currentPlayer.AddTerritory(this);
		}
	}

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
		if(troops.Remove(troop)){
			Destroy(troop.gameObject);
			return true;
		}
		return false;
	}

	public bool AddTroop(Troop troop){
		return troops.Add(troop);
	}

	public bool AddTroop(){
		Troop troop = Troop.Create(this);
		if(troops.Add(troop)){
			return true;
		}
		else{
			Destroy(troop.gameObject);
		}
		return false;
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
				Gizmos.DrawLine(this.SpawnPosition, territory.SpawnPosition);

			}
		}
	}


}
