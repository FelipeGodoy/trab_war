using UnityEngine;
using System.Collections;

public class RemoteTurnController : TurnController {

	public override StageController StageToController(Stage stage){
		return new WaitShotStageController();
	}

	public override void OnTurnStart(){
		stageController.gui.hidePassar ();
		this.sendShotOnEndTurn = false;
	}
}
