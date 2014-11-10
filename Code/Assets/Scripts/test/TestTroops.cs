using UnityEngine;
using System.Collections;

public class TestTroops : MonoBehaviour {

	public Player player1;
	public Player player2;

	void Start () {
		int[] playersOrder = {0,1};
		player1.CleanUp();
		player2.CleanUp();
//		GameController.Instance.Setup(playersOrder);
		Territory[] territories = GameController.Instance.currentMap.territories;
		for(int i = 0; i < territories.Length /2; i++){
			Territory territory = territories[i];
			Shot s = new AllockTroopShot(player1,territory,1);
			s.sendRequest = false;
			GameController.Instance.ComputeShot(s);
		}
		for(int i = territories.Length/2; i < territories.Length; i++){
			Territory territory = territories[i];
			Shot s = new AllockTroopShot(player2,territory,1);
			s.sendRequest = false;
			GameController.Instance.ComputeShot(s);
		}
		player1.Goal = GoalFactory.Create(6);
		player2.Goal = GoalFactory.Create(6);
		GameController.Instance.PlayersOrder.Add(player1);
		GameController.Instance.PlayersOrder.Add(player2);
		GameController.Instance.Begin();
//		print(player1.Territories.Count);
//		foreach(Territory ter in player1.Territories){
//			Debug.Log(ter.gameObject.name);
//		}
	}
}
