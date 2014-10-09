using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public const int MAX_PLAYERS = 6;

	public List<Player> players;
	public static Game Instance{get; set;}
	public Map currentMap;

	void Awake(){
		Instance = this;
		if(players == null){
			players = new List<Player>();
		}
		if(currentMap == null){
			currentMap = GetComponent<Map>();
		}
	}

	public bool AddPlayer(Player player){
		if(players.Count >= MAX_PLAYERS){
			return false;
		}
		return true;
	}

}
