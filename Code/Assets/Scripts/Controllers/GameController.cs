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

	void Start(){
#if LOCAL_MODE
		TestTroops test = gameObject.AddComponent<TestTroops>();
		test.player1 = playersModels[0];
		test.player2 = playersModels[1];
#else
		Setup(RequestController.Instance.AllPlayersInfo);
		Begin();
#endif
	}

	public bool ComputeShot(Shot shot){
		return shot.Do();
	}

	public void OnShotEnd(Shot shot){
		if(shot.sendRequest){
			RequestController.Instance.SendShot(shot);
		}
		shot.gui.left.setActive (false);
		shot.gui.right.setActive (false);
//		JSONObject json = ShotEncoder.ToJSON(shot);
//		Shot other = ShotDecoder.FromJSON(json);
//		json = ShotEncoder.ToJSON(other);
//		Debug.Log(json.ToString());
	}

	public void Begin(){
		IsRunning = true;
		this.TurnPlayerIndex = 0;
		this.CurrentTurnController = TurnController.Create(this.CurrentPlayer.type);
		this.CurrentTurnController.Start();
	}

	public void nextStage(){
		this.CurrentTurnController.NextStage ();
	}

	public void Resume(){
		IsRunning = true;
	}

	public void Setup(List<PlayerHold> playersHold){
		playersHold.Sort( delegate(PlayerHold a, PlayerHold b) {
			return a.order.CompareTo(b.order);
		});
		PlayersOrder.Clear();
		foreach(PlayerHold ph in playersHold){
			Player player = this.playersModels[ph.color];
			player.CleanUp();
			playersOrder.Add(player);
			player.Goal = GoalFactory.Create(ph.goalId);
			player.type = ph.type;
			player.name = ph.name;
			player.backendId = ph.backendId;
			foreach(int i in ph.initTerritories){
				Shot allockShot = new AllockTroopShot(player,currentMap.territories[i],1);
				allockShot.sendRequest = false;
				ComputeShot(allockShot);
			}
		}
		IsRunning = false;
	}

	public void Pause(){
		IsRunning = false;
	}

	public void Update(){
		if(CurrentTurnController != null){
			CurrentTurnController.ForcedUpdate();
			if(IsRunning){
				CurrentTurnController.Update();
			}
		}
	}

	void OnGUI(){
		if(CurrentTurnController != null){
			CurrentTurnController.OnGUI();
		}
//		int i =0;
//		foreach(Player p in playersOrder){
//			GUI.Label(new Rect(50,50 + (20 * i),50,50),p.name);
//			i++;
//		}
		GUI.Label(new Rect(50,50,50,50),""+RequestController.Instance.shotCount);
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

	public void OnConfirm(){
		if(IsRunning){
			nextStage();
		}
	}

	public void OnClickTerritory(Territory territory){
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnClickTerritory(territory);
		}
	}
	public void OnPressTerritory(Territory territory){
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnPressTerritory(territory);
		}
	}
	public void OnKeepPressedTerritory(Territory territory){

		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnKeepPressedTerritory(territory);
		}
	}
	public void OnStopPressTerritory(Territory territory){
		//CurrentTurnController.StageController.gui.right.setActive (false);
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnStopPressTerritory(territory);
		}
	}
	public void OnReleaseTerritory(Territory territory){
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnReleaseTerritory(territory);
		}
	}
	public void OnDragTerritory(Territory source, Territory target){
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnDragTerritory(source, target);
		}
	}

	public void OnDragNDropTerritory(Territory source, Territory target){
		if(CurrentStageController != null && IsRunning){
			CurrentStageController.OnDragNDropTerritory(source, target);
		}
	}
	
}
