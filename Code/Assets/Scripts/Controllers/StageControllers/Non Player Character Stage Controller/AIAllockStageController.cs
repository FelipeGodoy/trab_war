using UnityEngine;
using System.Collections.Generic;


public class AIAllockStageController : StageController {

	protected int freeTroops = 0;
	protected int usedTroops = 0;
	protected Dictionary<Territory,int> troopsAllocked;
	protected int indice = 0;
	protected Territory temp;

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
		/*if(usedTroops < freeTroops){
			this.indice++;
			temp = this.Player.Territories[1];
			ComputeShot(new AllockTroopShot(this.Player,temp,1));
			this.usedTroops++;
		}
		else{
			EndStage();
		}
	}
}*/
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
