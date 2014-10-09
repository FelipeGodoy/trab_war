using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour{

	private HashSet<Territory> territories;
	private List<Symbol> symbols;

	public string name;
	public Material troopMaterial;

	public int tradesCount;

	void Awake(){
		if(this.territories == null){
			this.territories = new HashSet<Territory>();
		}
		if(this.symbols == null){
			this.symbols = new List<Symbol>();
		}
	}

	public int TerritoriesCount{
		get{
			return territories.Count;
		}
	}

	public bool AddTerritory(Territory territory){
		return territories.Add(territory);
	}

	public bool RemoveTerritory(Territory territory){
		return territories.Remove(territory);
	}


}
