using UnityEngine;
using System.Collections.Generic;

public class Territory : MonoBehaviour {

	private Player _currentPlayer;
	private List<Troop> troops;
	public List<Territory> neighbors;
	public Continent continent;
	public string name;
	public Transform troopSpawn;
	public int index;

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
			if(troops == null){
				troops = new List<Troop>();
			}
			if(neighbors == null){
				neighbors = new List<Territory>();
			}
			if(_currentPlayer != null)_currentPlayer.RemoveTerritory(this);
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

	public void Awake(){
		if(troops == null){
			troops = new List<Troop>();
		}
		if(neighbors == null){
			neighbors = new List<Territory>();
		}
	}

	public bool HaveNeighborEnemy(){
		foreach(Territory neighbor in this.neighbors){
			if(neighbor.CurrentPlayer != this.CurrentPlayer){
				return true;
			}
		}
		return false;
	}

	public bool RemoveTroops(int troopsCount){
		if(troopsCount <= 0)return false;
		if(troopsCount == 1)return RemoveTroop();
		return RemoveTroop() && RemoveTroops(troopsCount - 1);
	}

	public bool RemoveTroop(){
		if(troops.Count <= 0) return false;
		return RemoveTroop(troops[0]);
	}

	public bool RemoveTroop(Troop troop){
		if(troops.Remove(troop)){
			Destroy(troop.gameObject);
			return true;
		}
		return false;
	}

	public void AddTroop(Troop troop){
		troops.Add(troop);
	}

	public void AddTroop(){
		Troop troop = Troop.Create(this);
		troops.Add(troop);
	}

	public void AddTroops(int troopsCount){
		for(int i =0; i < troopsCount; i++){
			AddTroop();
		}
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
