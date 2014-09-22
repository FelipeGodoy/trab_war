using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public List<Player> players;
	public List<Color> playersColors;
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

}
