using UnityEngine;
using System.Collections;

public abstract class StageController{
	protected TurnController turnController;

	public StageController(TurnController turnController){
		this.turnController = turnController;
	}

	public abstract void OnStageStart();
	public abstract void Update();
	public abstract void OnStageEnd();

	public void EndStage(){
		turnController.NextStage();
	}

}
