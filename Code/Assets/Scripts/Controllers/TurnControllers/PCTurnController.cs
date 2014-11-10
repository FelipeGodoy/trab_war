using UnityEngine;
using System.Collections;

public class PCTurnController : TurnController{
	public override StageController StageToController(Stage stage){
		StageController stageController;
		switch(stage){
			case Stage.ALLOCK :{
				stageController = new PCAlockStageController();
				break;
			}
			case Stage.ATTACK:{
				stageController = new PCAttackStageController();
				break;
			}
			case Stage.MOVE:{
				stageController = new PCMoveStageController();
				break;
			}
			default : return null;
		}
		stageController.turnController = this;
		return stageController;
	}

	public override void OnTurnGUI(){

		//GUI.Label(new Rect(200,10,150,20),"Jogador: "+this.Player.name);
	}

	public override void OnTurnStart(){
		stageController.gui.showPassar ();
		this.sendShotOnEndTurn = false;
	}
}
