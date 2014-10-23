using UnityEngine;
using System.Collections.Generic;

public class Continent : MonoBehaviour {

	public string name;
	public int troopsBonus;
	public Territory[] territories;

	public int CheckBonus(List<Territory> territories){
		foreach(Territory territory in this.territories){
			if(!territories.Contains(territory)){
				return 0;
			}
		}
		return troopsBonus;
	}

}
