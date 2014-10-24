using UnityEngine;
using System.Collections;

public class TestTroops : MonoBehaviour {

	public Player player1;
	public Player player2;

	void Start () {
		int[] playersOrder = {0,1};
		GameController.Instance.Setup(playersOrder);
		Territory[] territories = GameController.Instance.currentMap.territories;
		for(int i = 0; i < territories.Length /2; i++){
			Territory territory = territories[i];
			GameController.Instance.ComputeShot(new AllockTroopShot(player1,territory,1));
		}
		for(int i = territories.Length/2; i < territories.Length; i++){
			Territory territory = territories[i];
			GameController.Instance.ComputeShot(new AllockTroopShot(player2,territory,1));
		}
		GameController.Instance.Begin();
//		print(player1.Territories.Count);
//		foreach(Territory ter in player1.Territories){
//			Debug.Log(ter.gameObject.name);
//		}
	}
}
