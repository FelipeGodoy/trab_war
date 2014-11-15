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
			checkTerritories = new List<Territory>(player.Territories);
			checkTerritories.RemoveAll(t => territories.Contains(t));
			return checkTerritories.Count >= this.territoriesCount;
		}
		return false;
	}

	public override string Description{
		get{
			string desc = "Conquiste:";
			int n =1;
			foreach(Territory t in this.territories){
				desc += " "+ t.name;
				if(n < this.territories.Count)desc+= ",";
				n++;
			}
			if(territoriesCount > 0){
				if(this.territories.Count > 0)desc += " e mais";
				desc += " "+territoriesCount+" territorio"+(territoriesCount > 1 ? "s" : "")+" a sua escolha";	
			}
			return desc;
		}
	}

}
