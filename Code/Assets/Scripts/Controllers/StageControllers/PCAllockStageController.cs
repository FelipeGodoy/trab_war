﻿using UnityEngine;
using System.Collections.Generic;

public class PCAlockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;

	public override void OnStageStart(){
		freeTroops = this.Player.TroopsToEarn();
		troopsAllocked = new Dictionary<Territory, int>();
		foreach(Territory territory in this.Player.Territories){
			troopsAllocked[territory] = 0;
		}
	}

	public override void OnClickTerritory(Territory territory){
		if(Player.HaveTerritory(territory) && usedTroops < freeTroops){
			usedTroops ++;
			ComputeShot(new AllockTroopShot(this.Player,territory,1));
		}
	}

}
