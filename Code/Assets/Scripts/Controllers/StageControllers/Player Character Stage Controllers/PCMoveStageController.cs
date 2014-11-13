using UnityEngine;
using System.Collections;

public class PCMoveStageController : StageController {

	public override void OnDragNDropTerritory(Territory source, Territory target){
		ComputeShot(new MoveShot(this.Player,source,target,1));
		gui.left.setActive (false);
		gui.right.setActive (false);
	}
	public override void OnPressTerritory(Territory territory){
		if (territory.CurrentPlayer == this.Player) {
			gui.left.setActive (true);
			gui.left.setTexts (territory.CurrentPlayer.name, territory.name, "" + territory.TroopsCount);
			gui.left.changeColor (territory.CurrentPlayer.displayColor);
		}
	}
	
	public override void OnKeepPressedTerritory(Territory territory){
		string n = gui.left.territoryName.text;
		if (territory != null && territory.CurrentPlayer == this.Player && territory.name != n) {
			gui.right.setActive (true);
			gui.right.setTexts (territory.CurrentPlayer.name, territory.name, "" + territory.TroopsCount);
			gui.right.changeColor (territory.CurrentPlayer.displayColor);
		}
	}
	
	public override void OnStopPressTerritory(Territory territory){
		gui.right.setActive (false);
	}

	public override void OnStageStart(){
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

	public override void OnStageEnd(){
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

	public override void OnGUI(){
		//GUI.Label(new Rect(200,30,150,20),"Movendo");
		gui.setMover ();
		/*if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}*/
	}

}
