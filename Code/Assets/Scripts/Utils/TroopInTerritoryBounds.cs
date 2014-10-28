using UnityEngine;
using System.Collections;

public class TroopInTerritoryBounds : MonoBehaviour {

	private TroopInTerritory[] troops = null;
	public Color color;

	private TroopInTerritory[] Troops{
		get{
			if(troops == null || troops.Length ==0){
				troops = GetComponentsInChildren<TroopInTerritory>();
			}
			return troops;
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.color = color;
		for(int i = 0; i < Troops.Length ;i++){
			Vector3 p1 = Troops[i].transform.position;
			Vector3 p2 = Troops[(i+1) % Troops.Length].transform.position;
			Gizmos.DrawLine(p1,p2);
		}
	}
}
