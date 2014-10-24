using UnityEngine;
using System.Collections.Generic;

public class PCAlockStageController : StageController {

	protected int freeTroops;
	protected int usedTroops;
	protected Dictionary<Territory,int> troopsAllocked;

	public override void OnGUI(){
		GUI.Label(new Rect(10,10,100,20),"tropas livres: "+(freeTroops-usedTroops));
		if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}
	}

	public override void OnStageStart(){
		this.freeTroops = this.Player.TroopsToEarn();
		troopsAllocked = new Dictionary<Territory, int>();
		foreach(Territory territory in this.Player.Territories){
			troopsAllocked[territory] = 0;
		}
	}

	public override void OnClickTerritory(Territory territory){
//		Debug.Log(Player.TerritoriesCount);
//		Debug.Log("have territory? "+territory.gameObject.name+" "+Player.HaveTerritory(territory));
//		Debug.Log("free troops: "+freeTroops);
//		Debug.Log("usedTroops: "+usedTroops);
//		Debug.Log(Player.Territories);
		if(Player.HaveTerritory(territory) && usedTroops < freeTroops){
			usedTroops ++;
			ComputeShot(new AllockTroopShot(this.Player,territory,1));
		}
	}

}
