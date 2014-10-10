using UnityEngine;
using System.Collections;

public class AllockTroopShot : Shot {

	protected Territory territory;
	protected int troopsCount; 

	public AllockTroopShot(Player player, Territory territory, int troopsCount){
		this.player = player;
		this.territory = territory;
		this.troopsCount = troopsCount;
	}

	public override bool Do(){
		territory.CurrentPlayer = this.player;
		territory.AddTroops(troopsCount);
		return true;
	}

	public override void Callback(){
		
	}

}
