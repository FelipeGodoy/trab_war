using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerHold{
	public string name;
	public int backendId;
	public int color;
	public Player.PlayerType type;
	public int order;
	public int[] initTerritories;
	public int goalId;

	public PlayerHold(string name, int backendId, int color, Player.PlayerType type){
		this.name = name;
		this.backendId = backendId;
		this.color = color;
		this.type = type;
	}

}
