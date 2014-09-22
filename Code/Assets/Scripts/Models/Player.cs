using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Player {

	private HashSet<Territory> territories;
	private List<Symbol> symbols;

	public string name;
	public int tradesCount;

	public int TerritoriesCount{
		get{
			return territories.Count;
		}
	}

	void Awake(){
		if(territories == null){
			territories = new HashSet<Territory>();
		}
		if(symbols == null){
			symbols = new List<Symbol>();
		}
	}

	public bool AddTerritory(Territory territory){
		return territories.Add(territory);
	}

	public bool RemoveTerritory(Territory territory){
		return territories.Remove(territory);
	}


}
