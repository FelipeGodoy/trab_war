using UnityEngine;
using System.Collections;

public class MoveShot : Shot {

	protected int troopsCount;
	protected Territory sourceTerritory, destinationTerritory;

	public Territory SourceTerritory{get{return sourceTerritory;}}
	public Territory TargetTerritory{get{return destinationTerritory;}}
	public int TroopsCount{get{return troopsCount;}}

	public override Type type{
		get{
			return Type.MOVE;
		}
	}

	public MoveShot(Player player, Territory source, Territory destination, int troopsCount){
		this.player = player;
		this.sourceTerritory = source;
		this.destinationTerritory = destination;
		this.troopsCount = troopsCount;
	}

	public override bool Do(){
		if(sourceTerritory == null ||
		   destinationTerritory == null ||
		   sourceTerritory.CurrentPlayer != this.player ||
		   destinationTerritory.CurrentPlayer != this.player ||
		   sourceTerritory == destinationTerritory ||
		   !sourceTerritory.neighbors.Contains(destinationTerritory) ||
		   sourceTerritory.TroopsCount <= troopsCount){
			return false;
		}
		gui = GameObject.Find("GUIFacade").GetComponent<GUIFacade>();
		gui.left.setActive (true);
		gui.left.setTexts (sourceTerritory.CurrentPlayer.name, sourceTerritory.gameObject.name, "" + sourceTerritory.TroopsCount);
		gui.left.changeColor (sourceTerritory.CurrentPlayer.troopMaterial.color);
		
		gui.right.setActive (true);
		gui.right.setTexts (destinationTerritory.CurrentPlayer.name, destinationTerritory.gameObject.name, "" + destinationTerritory.TroopsCount);
		gui.right.changeColor (destinationTerritory.CurrentPlayer.troopMaterial.color);
		sourceTerritory.RemoveTroops(troopsCount);
		destinationTerritory.AddTroops(troopsCount);
		GameController.Instance.OnShotEnd(this);
		return true;
	}

}
