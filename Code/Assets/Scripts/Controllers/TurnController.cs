using UnityEngine;
using System.Collections;

public abstract class TurnController{

	protected Stage stage;
	protected StageController stageController;
	protected bool sendShotOnEndTurn = true;
	GUIFacade gui;
	private Player _player;

	public static TurnController Create(Player.PlayerType playerType){
		switch(playerType){
		case Player.PlayerType.PLAYER_CHARACTER :{
			TurnController t = new PCTurnController();
			t.sendShotOnEndTurn = true;
			return t;
		}
		case Player.PlayerType.NON_PLAYER_CHARACTER:{
			TurnController t = new AITurnController();
			t.sendShotOnEndTurn = true;
			return t;
		}
		case Player.PlayerType.REMOTE_PLAYER_CHARACTER:{
			TurnController t = new RemoteTurnController();
			t.sendShotOnEndTurn = false;
			return t;
		}
		}
		return null;
	}

	public StageController StageController{
		get{
			return stageController;
		}
	}
	
	protected Player Player{
		get{
			if(_player == null){
				_player = GameController.Instance.CurrentPlayer;
			}
			return _player;
		}
	}

	public void NextStage(){
		stage = StageUtils.NextStage(stage);
		stageController = StageToController(stage);
		if(stageController != null && !this.Player.firstPlay){
			stageController.OnStageStart();
		}
		else{
			gui.left.setActive(false);
			this.Player.firstPlay = false;
			EndTurn();
		}
	}

	public void Start(){
		stage = Stage.ALLOCK;
		stageController = StageToController(stage);
		stageController.OnStageStart();
		OnTurnStart();
		gui = GameObject.Find ("GUIFacade").GetComponent<GUIFacade> ();
	}


	public void EndTurn(){
		Shot passTurnShot = new PassTurnShot(Player); 
		OnTurnEnd();
		GameController.Instance.EndTurn();
		if(this.sendShotOnEndTurn && passTurnShot.sendRequest){
			RequestController.Instance.SendShot(passTurnShot);
		}
	}

	public void Update(){
		if(stageController != null){
			stageController.Update();
		}
	}

	public void ForcedUpdate(){
		if(stageController != null){
			stageController.ForcedUpdate();
		}
	}

	public void OnGUI(){
		if(stageController != null){
			stageController.OnGUI();
		}
		gui.left.setName (this.Player.name);
		gui.left.setNameActive ();
		Color c = Player.troopMaterial.color;
		gui.left.changeColor (c);
		OnTurnGUI();
	}

	public virtual void OnTurnGUI(){}
	public virtual void OnTurnStart(){}
	public abstract StageController StageToController(Stage stage);
	public virtual void OnTurnEnd(){}

}
