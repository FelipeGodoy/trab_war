using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public const int MAX_PLAYERS = 6;

	public List<Player> playersModels;
	public static Game Instance{get; set;}
	public Map currentMap;

	private List<Player> playersOrder;

	public List<Player> PlayersOrder{
		get{
			if(playersOrder == null){
				playersOrder = new List<Player>();
			}
			return playersOrder;
		}

		protected set{
			playersOrder = value;
		}
	}

	public int TurnPlayerIndex{get; protected set;}

	public Player TurnPlayer{
		get{
			return PlayersOrder[TurnPlayerIndex];
		}
	}

	public bool IsRunning{get; protected set;}

	void Awake(){
		
		Instance = this;
		if(playersModels == null){
			playersModels = new List<Player>();
		}
		if(currentMap == null){
			currentMap = GetComponent<Map>();
		}
	}

	public void Begin(){
		Resume();
	}

	public void Resume(){
		IsRunning = true;
	}

	public void Setup(int[] playersOrder){
		foreach(Player player in playersModels){
			player.Setup();
		}
		foreach(int order in playersOrder){
			PlayersOrder.Add(playersModels[order]);
		}
	}

	public void Stop(){
		IsRunning = false;
	}

	public void EndTurn(){
		TurnPlayerIndex = (TurnPlayerIndex + 1) % playersOrder.Count;
	}

	public Player[] ChampionsPlayers(){
		List<Player> championsPlayers = new List<Player>();
		foreach(Player player in playersOrder){
			if(player.CheckGoal()){
				championsPlayers.Add(player);
			}
		}
		return championsPlayers.ToArray();
	}

	public bool AddPlayer(Player player){
		if(playersModels.Count < MAX_PLAYERS){
			playersModels.Add(player);
			return false;
		}
		return true;
	}

}
