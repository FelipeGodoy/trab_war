using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class AIAllockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;
	protected int indice;
	protected Territory temp;

	public override void OnGUI(){
		GUI.Label(new Rect(200,30,150,20),"Alocando");
	}

	public override void OnStageStart(){
		this.freeTroops = this.Player.TroopsToEarn();
		this.usedTroops = 0;
		this.indice = 0;
	}

	public override void Update(){
		if(usedTroops < freeTroops){
			if(this.Player.TerritoriesCount == indice)
				indice = 0;
			temp = this.Player.Territories[indice++];
			ComputeShot(new AllockTroopShot(this.Player,temp,1));
			usedTroops++;
			}

		else{
			EndStage();
		}
	}
}
