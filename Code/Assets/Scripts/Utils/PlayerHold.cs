using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerHold{
	public string name;
	public int backend_id;
	public int color;
	public Player.PlayerType type;
	public int order;
	public int[] init_territories;

	public PlayerHold(string name, int backend_id, int color, Player.PlayerType type){
		this.name = name;
		this.backend_id = backend_id;
		this.color = color;
		this.type = type;
	}

}
