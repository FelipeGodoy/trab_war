using UnityEngine;
using System.Collections.Generic;

public class ContinentsGoal : Goal {

	private List<Continent> continents;
	private int continentsCount;
	
	public ContinentsGoal(int continentsCount){
		this.continentsCount = continentsCount;
		this.continents = new List<Continent>();
	}
	
	public ContinentsGoal(Continent[] continents){
		this.continentsCount = 0;
		this.continents = new List<Continent>(continents);
	}
	
	public ContinentsGoal(Continent[] continents, int adictionalContinents){
		this.continents = new List<Continent>(continents);
		this.continentsCount = adictionalContinents;
	}
	
	public override bool Check(GameController game, Player player){
		List<Continent> checkContinents = new List<Continent>(this.continents);
		checkContinents.RemoveAll(c => player.HaveContinent(c));
		if(checkContinents.Count == 0){
			checkContinents = new List<Continent>(game.currentMap.continents);
			checkContinents.RemoveAll(c => this.continents.Contains(c) || !player.HaveContinent(c));
			return checkContinents.Count >= continentsCount;
		}
		return false;
	}

	public override string Description{
		get{
			string desc = "Conquiste:";
			int n =1;
			foreach(Continent c in this.continents){
				desc += " "+ c.name;
				if(n < this.continents.Count)desc+= ",";
				n++;
			}
			if(continentsCount > 0){
				if(this.continents.Count > 0)desc += " e mais";
				desc += " "+continentsCount+" continente"+(continentsCount > 1 ? "s" : "")+" a sua escolha";	
			}
			return desc;
		}
	}
}
