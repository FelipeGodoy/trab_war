using UnityEngine;
using System.Collections;

public class AITurnController : TurnController {

	//TODO: Retornar os controladores de estagio.
	public override StageController StageToController(Stage stage){
		StageController stageController = null;
		switch(stage){
		case Stage.ALLOCK :{
			stageController =  new AIAllockStageController();
			break;
		}
		case Stage.ATTACK:{
			break;
		}
		case Stage.MOVE:{
			break;
		}
		default : return null;
		}
		stageController.turnController = this;
		return stageController;
	}
}
