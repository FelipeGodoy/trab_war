using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public const int MAX_PLAYERS = 6;

	public List<Player> playersModels;
	public static GameController Instance{get; set;}
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

	public Player CurrentPlayer{
		get{
			return PlayersOrder[TurnPlayerIndex];
		}
	}

	public bool IsRunning{get; protected set;}

	public TurnController CurrentTurnController{get; protected set;}

	public StageController CurrentStageController{
		get{
			if(CurrentTurnController != null && CurrentTurnController.StageController != null){
				return CurrentTurnController.StageController;
			}
			return null;
		}
	}

	void Awake(){
		Instance = this;
		if(playersModels == null){
			playersModels = new List<Player>();
		}
		if(currentMap == null){
			currentMap = GetComponent<Map>();
		}
	}

	public bool ComputeShot(Shot shot){
		return shot.Do();
	}

	public void Begin(){
		IsRunning = true;
		this.TurnPlayerIndex = 0;
		this.CurrentTurnController = TurnController.Create(this.CurrentPlayer.type);
		this.CurrentTurnController.Start();
	}

	public void Resume(){
		IsRunning = true;
	}

	public void Setup(int[] playersOrder){
		foreach(Player player in playersModels){
			player.CleanUp();
		}
		foreach(int order in playersOrder){
			PlayersOrder.Add(playersModels[order]);
		}
		IsRunning = false;
	}

	public void Stop(){
		IsRunning = false;
	}

	public void Update(){
		if(CurrentTurnController != null){
			CurrentTurnController.Update();
		}
	}

	public void EndTurn(){
		Player[] championsPlayers = this.ChampionsPlayers();
		if(championsPlayers.Length == 0){
			TurnPlayerIndex = (TurnPlayerIndex + 1) % playersOrder.Count;
			this.CurrentTurnController = TurnController.Create(this.CurrentPlayer.type);
			this.CurrentTurnController.Start();
		}
		else{
			Debug.Log("Alguem ganhou");
		}
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

	public void OnClickTerritory(Territory territory){
		if(CurrentStageController != null){
			CurrentStageController.OnClickTerritory(territory);
		}
	}
	public void OnPressTerritory(Territory territory){
		if(CurrentStageController != null){
			CurrentStageController.OnPressTerritory(territory);
		}
	}
	public void OnKeepPressedTerritory(Territory territory){
		if(CurrentStageController != null){
			CurrentStageController.OnKeepPressedTerritory(territory);
		}
	}
	public void OnStopPressTerritory(Territory territory){
		if(CurrentStageController != null){
			CurrentStageController.OnStopPressTerritory(territory);
		}
	}
	public void OnReleaseTerritory(Territory territory){
		if(CurrentStageController != null){
			CurrentStageController.OnReleaseTerritory(territory);
		}
	}
	public void OnDragTerritory(Territory source, Territory target){
		if(CurrentStageController != null){
			CurrentStageController.OnDragTerritory(source, target);
		}
	}
	
}
