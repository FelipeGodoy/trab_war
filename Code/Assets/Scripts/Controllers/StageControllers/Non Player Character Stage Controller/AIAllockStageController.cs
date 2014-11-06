using UnityEngine;
using System.Collections.Generic;

public class AIAllockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;

	public override void OnGUI(){
		GUI.Label(new Rect(200,30,150,20),"Alocando");
	}

	public override void OnStageStart(){
		this.freeTroops = this.Player.TroopsToEarn();
		this.usedTroops = 0;
		troopsAllocked = new Dictionary<Territory, int>();
		foreach(Territory territory in this.Player.Territories){
			troopsAllocked[territory] = 0;
		}
	}

	public override void Update(){
		//int ind =0;
		//ind = (ind+1) % Player.Territories.Count;
		if(usedTroops < freeTroops) {
			foreach(Territory territory in this.Player.Territories){
				if(usedTroops<freeTroops){
					usedTroops++;
					ComputeShot(new AllockTroopShot(this.Player,territory,1));
				}
			}
		}
		else{
			EndStage();
		}
	}	

}
