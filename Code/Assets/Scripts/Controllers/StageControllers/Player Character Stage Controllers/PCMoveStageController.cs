using UnityEngine;
using System.Collections;

public class PCMoveStageController : StageController {

	public override void OnDragNDropTerritory(Territory source, Territory target){
		ComputeShot(new MoveShot(this.Player,source,target,1));
	}
	
	public override void OnGUI(){
		GUI.Label(new Rect(200,30,150,20),"Movendo");
		if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}
	}

}
