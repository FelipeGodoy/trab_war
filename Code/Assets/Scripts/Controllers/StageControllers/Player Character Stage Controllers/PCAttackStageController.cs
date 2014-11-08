using UnityEngine;
using System.Collections;

public class PCAttackStageController : StageController {

	public override void OnPressTerritory(Territory territory){
		if (territory.CurrentPlayer == this.Player) {
			gui.left.setActive (true);
			gui.left.setTexts (territory.CurrentPlayer.name, territory.gameObject.name, "" + territory.TroopsCount);
			gui.left.changeColor (territory.CurrentPlayer.troopMaterial.color);
		}
	}

	public override void OnKeepPressedTerritory(Territory territory){
		string n = gui.left.territoryName.text;
		if (territory != null && territory.CurrentPlayer != this.Player) {
			gui.right.setActive (true);
			gui.right.setTexts (territory.CurrentPlayer.name, territory.gameObject.name, "" + territory.TroopsCount);
			gui.right.changeColor (territory.CurrentPlayer.troopMaterial.color);
		}
	}

	public override void OnStopPressTerritory(Territory territory){
		gui.right.setActive (false);
	}

	public override void OnDragNDropTerritory(Territory source, Territory target){
		if (source.neighbors.Contains (target) && target.neighbors.Contains (source) &&
		    source.TroopsCount > 1 && source.CurrentPlayer != target.CurrentPlayer) {

			gui.left.setActive (true);
			gui.left.setTexts (source.CurrentPlayer.name, source.gameObject.name, "" + source.TroopsCount);
			gui.left.changeColor (source.CurrentPlayer.troopMaterial.color);

			gui.right.setActive (true);
			gui.right.setTexts (target.CurrentPlayer.name, target.gameObject.name, "" + target.TroopsCount);
			gui.right.changeColor (target.CurrentPlayer.troopMaterial.color);
		}

		ComputeShot(new AttackShot(this.Player,source,target,DiceResult));
	}

	public override void OnGUI(){
		//GUI.Label(new Rect(200,30,150,20),"Atacando");
		gui.setAtacar ();
		/*if(GUI.Button(new Rect(10,40,100,20),"Passar")){
			turnController.NextStage();
		}*/
	}

	public override void OnStageEnd(){
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

	private void DiceResult(bool conquested){
		Dice.Instance.ClearDices();
		gui.right.setActive (false);
		gui.left.setActive (false);
	}

}
