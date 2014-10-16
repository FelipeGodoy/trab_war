using UnityEngine;
using System.Collections;

public abstract class TurnController{

	protected Stage stage;
	protected StageController stageController;

	public void NextStage(){
		stage = StageUtils.NextStage(stage);
		stageController.OnStageEnd();
		stageController = StageToController(stage);
		if(stageController != null){
			stageController.OnStageStart();
		}
		else{
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
		OnTurnEnd();
		Game.Instance.EndTurn();
	}

	public void Update(){
		if(stageController == null){
			stageController.Update();
		}
	}

	public virtual void OnTurnStart(){}
	public abstract StageController StageToController(Stage stage);
	public virtual void OnTurnEnd(){}

}
