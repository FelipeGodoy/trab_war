using UnityEngine;
using System.Collections.Generic;

public class TerritoriesGoal : Goal {

	private List<Territory> territories;
	private int territoriesCount;

	public TerritoriesGoal(int territoriesCount){
		this.territoriesCount = territoriesCount;
		this.territories = new List<Territory>();
	}

	public TerritoriesGoal(Territory[] territories){
		this.territoriesCount = 0;
		this.territories = new List<Territory>(territories);
	}

	public TerritoriesGoal(Territory[] territories, int adictionalTerritories){
		this.territories = new List<Territory>(territories);
		this.territoriesCount = adictionalTerritories;
	}

	public override bool Check(GameController game, Player player){
		List<Territory> checkTerritories = new List<Territory>(this.territories);
		checkTerritories.RemoveAll(t => player.HaveTerritory(t));
		if(checkTerritories.Count == 0){
			checkTerritories = player.Territories;
			checkTerritories.RemoveAll(t => territories.Contains(t));
			return checkTerritories.Count >= this.territoriesCount;
		}
		return false;
	}
}
