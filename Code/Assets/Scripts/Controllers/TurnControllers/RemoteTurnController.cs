using UnityEngine;
using System.Collections;

public class RemoteTurnController : TurnController {
	int troopQtd;
	public override StageController StageToController(Stage stage){
		return new WaitShotStageController();
	}

	public override void OnTurnStart(){
		stageController.gui.hidePassar();
		this.sendShotOnEndTurn = false;
		troopQtd = this.Player.TroopsToEarn ();
	}

	void Update(){
			switch (stage) {
				case Stage.ALLOCK:
						{
								stageController.gui.setAlocar (troopQtd);
								break;
						}
				case Stage.ATTACK:
						{
								stageController.gui.setAtacar ();
								break;
						}
				case Stage.MOVE:
						{
								stageController.gui.setMover ();
								break;
						}
				case Stage.END:
						{
								stageController.gui.states.setNone ();
								break;
						}
				}
		}
}
