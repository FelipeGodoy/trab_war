using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour{

	public enum PlayerType{PLAYER_CHARACTER, NON_PLAYER_CHARACTER, ONLINE_PLAYER};
	public PlayerType Type{get; set;}

	private HashSet<Territory> _territories;
	private HashSet<Territory> territories{
		get{
			if(_territories == null){
				_territories = new HashSet<Territory>();
			}
			return _territories;
		}
		set{
			_territories = value;
		}
	}

	private List<Symbol> symbols;

	public string armyName;
	public string name;
	public Material troopMaterial;

	public int tradesCount;

	public Goal Goal{get; set;}
	public List<Territory> Territories{
		get{
			return new List<Territory>(this.territories);
		}
	}

	public TurnController TurnController{
		get{
			switch(this.Type){
			case PlayerType.PLAYER_CHARACTER :{
				return null;
			}
			case PlayerType.NON_PLAYER_CHARACTER :{
				return null;
			}
			case PlayerType.ONLINE_PLAYER:{
				return null;
			}
			default : return null;
			}
		}
	}

	void Awake(){
		if(this.territories == null){
			this.territories = new HashSet<Territory>();
		}
		if(this.symbols == null){
			this.symbols = new List<Symbol>();
		}
	}

	public void CleanUp(){
		this.territories.Clear();
		this.symbols.Clear();
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

	public bool HaveTerritory(Territory territory){
		return territories.Contains(territory);
	}

	public bool CheckGoal(){
		return this.Goal.Check(Game.Instance,this);
	}

	public int TroopsToEarn(){
		int troops = this.TerritoriesCount;
		List<Territory> territories = this.Territories;
		foreach(Continent continent in Game.Instance.currentMap.continents){
			troops += continent.CheckBonus(Territories);
		}
		return troops;
	}

}
