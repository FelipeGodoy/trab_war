using UnityEngine;
using System.Collections;

public class PassTurnShot : Shot {

	public PassTurnShot(Player player){
		this.player = player;
	}

	public override Type type{
		get{
			return Type.PASS_TURN;
		}
	}

	public override bool Do(){
		GameController.Instance.CurrentTurnController.EndTurn();
		return true;
	}

}
