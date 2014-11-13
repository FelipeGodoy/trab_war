using UnityEngine;
using System.Collections.Generic;

public class PCAlockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;

	public override void OnGUI(){
		//GUI.Label(new Rect(200,30,150,20),"Alocando");
		//GUI.Label(new Rect(10,10,100,20),"tropas livres: "+(freeTroops-usedTroops));
		gui.setAlocar (freeTroops-usedTroops);
		gui.showPassar ();
		/*if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}*/
	}

	public override void OnStageStart(){

		this.freeTroops = this.Player.TroopsToEarn();
		troopsAllocked = new Dictionary<Territory, int>();
		foreach(Territory territory in this.Player.Territories){
			troopsAllocked[territory] = 0;
		}
	}

	public override void OnClickTerritory(Territory territory){
		if (!Player.HaveTerritory (territory))
						return;
		gui.left.setActive (true);
		gui.left.setTerritory (territory.name, ""+territory.TroopsCount);
		if(usedTroops < freeTroops){
			usedTroops ++;
			ComputeShot(new AllockTroopShot(this.Player,territory,1));
			gui.left.setTerritory (territory.name, ""+territory.TroopsCount);
		}
	}

	public override void OnStageEnd(){
		//GUI.Label(new Rect(200,30,150,20),"Movendo");
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

}
