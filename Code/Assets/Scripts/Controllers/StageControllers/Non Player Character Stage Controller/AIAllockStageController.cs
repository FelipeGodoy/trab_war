using UnityEngine;
using System.Collections.Generic;

public class AIAllockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;


	public override void OnStageStart(){
		this.freeTroops = this.Player.TroopsToEarn();
		this.usedTroops = 0;
		troopsAllocked = new Dictionary<Territory, int>();
		foreach(Territory territory in this.Player.Territories){
			troopsAllocked[territory] = 0;
		}
		EndStage();
	}

}
