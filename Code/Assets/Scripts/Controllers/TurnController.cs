using UnityEngine;
using System.Collections;

public abstract class TurnController{

	protected Stage stage;
	protected StageController stageController;
	protected bool sendShotOnEndTurn = true;

	private Player _player;

	public static TurnController Create(Player.PlayerType playerType){
		switch(playerType){
		case Player.PlayerType.PLAYER_CHARACTER :{
			return new PCTurnController();
		}
		case Player.PlayerType.NON_PLAYER_CHARACTER:{
			return new AITurnController();
		}
		case Player.PlayerType.REMOTE_PLAYER_CHARACTER:{
			return new RemoteTurnController();
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
			stageController.gui.left.setActive(false);
			this.Player.firstPlay = false;
			EndTurn();
		}
	}

	public void Start(){
		stage = Stage.ALLOCK;
		stageController = StageToController(stage);
		stageController.OnStageStart();
		OnTurnStart();
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
		stageController.gui.left.setName (this.Player.name);
		stageController.gui.left.setNameActive ();
		Color c = Player.troopMaterial.color;
		stageController.gui.left.changeColor (c);
		OnTurnGUI();
	}

	public virtual void OnTurnGUI(){}
	public virtual void OnTurnStart(){}
	public abstract StageController StageToController(Stage stage);
	public virtual void OnTurnEnd(){}

}
