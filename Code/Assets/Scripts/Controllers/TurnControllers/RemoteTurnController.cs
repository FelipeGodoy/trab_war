using UnityEngine;
using System.Collections;

public class RemoteTurnController : TurnController {

	public override StageController StageToController(Stage stage){
		return new WaitShotStageController();
	}

	public override void OnTurnStart(){
		this.sendShotOnEndTurn = false;
	}
}
