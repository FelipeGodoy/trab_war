using UnityEngine;
using System.Collections;

public class RemoveTroopShot : Shot {
	
	protected Territory territory;
	protected int troopsToRemove; 

	public Territory Territory{get{return territory;}}
	public int TroopsCount{get{return troopsToRemove;}}

	public override Type type{
		get{
			return Type.REMOVE;
		}
	}
	
	public RemoveTroopShot(Territory territory, int troopsToRemove){
		this.territory = territory;
		this.troopsToRemove = troopsToRemove;
	}
	
	public override bool Do(){
		if(territory.TroopsCount <= this.troopsToRemove){
			return false;
		}
		territory.RemoveTroops(this.troopsToRemove);
		GameController.Instance.OnShotEnd(this);
		return true;
	}
	
}
