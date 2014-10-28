using UnityEngine;
using System.Collections;

public class PCAttackStageController : StageController {

	public override void OnDragNDropTerritory(Territory source, Territory target){
		ComputeShot(new AttackShot(this.Player,source,target,DiceResult));
	}

	public override void OnGUI(){
		GUI.Label(new Rect(200,30,150,20),"Atacando");
		if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}
	}

	private void DiceResult(bool conquested){
		Dice.Instance.ClearDices();
	}

}
