using UnityEngine;
using System.Collections;

public class TestTroops : MonoBehaviour {

	public Player player1;
	public Player player2;

	void Start () {
		Territory[] territories = Game.Instance.currentMap.territories;
		for(int i = 0; i < territories.Length /2; i++){
			Territory territory = territories[i];
			territory.CurrentPlayer = player1;
			territory.AddTroop();
		}
		for(int i = territories.Length/2; i < territories.Length; i++){
			Territory territory = territories[i];
			territory.CurrentPlayer = player2;
			territory.AddTroop();
		}
	}
}
