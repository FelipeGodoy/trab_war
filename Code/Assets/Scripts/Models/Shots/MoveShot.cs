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
		sourceTerritory.RemoveTroops(troopsCount);
		destinationTerritory.AddTroops(troopsCount);
		GameController.Instance.OnShotEnd(this);
		return true;
	}

}
