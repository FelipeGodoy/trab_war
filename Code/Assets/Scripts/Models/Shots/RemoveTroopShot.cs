using UnityEngine;
using System.Collections;

public class RemoveTroopShot : Shot {
	
	protected Territory territory;
	protected int troopsToRemove; 
	
	public RemoveTroopShot(Territory territory, int troopsToRemove){
		this.territory = territory;
		this.troopsToRemove = troopsToRemove;
	}
	
	public override bool Do(){
		if(territory.TroopsCount <= this.troopsToRemove){
			return false;
		}
		territory.RemoveTroops(this.troopsToRemove);
		return true;
	}
	
	public override void Callback(){
		
	}
	
}
