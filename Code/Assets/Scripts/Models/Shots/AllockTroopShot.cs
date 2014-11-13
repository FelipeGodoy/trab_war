using UnityEngine;
using System.Collections;

public class AllockTroopShot : Shot {

	protected Territory territory;
	protected int troopsCount; 

	public Territory Territory{
		get{
			return territory;
		}
	}

	public int TroopsCount{
		get{
			return troopsCount;
		}
	}

	public override Type type{
		get{
			return Type.ALLOCK;
		}
	}

	public AllockTroopShot(Player player, Territory territory, int troopsCount){
		this.player = player;
		this.territory = territory;
		this.troopsCount = troopsCount;
	}

	public override bool Do(){
		if(this.player != territory.CurrentPlayer) territory.CurrentPlayer = this.player;
//		gui = (GameObject.Find ("GUIFacade")).GetComponent<GUIFacade>();
		gui.left.setActive(true);
		gui.left.setTerritory(territory.gameObject.name, ""+troopsCount);
		territory.AddTroops(troopsCount);
		this.player.AddTerritory(territory);
		GameController.Instance.OnShotEnd(this);
		return true;
	}

}
