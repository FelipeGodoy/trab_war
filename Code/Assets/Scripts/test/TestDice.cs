using UnityEngine;
using System.Collections;

public class TestDice : MonoBehaviour {
	public int[] attackNumbers;
	public int[] defenseNumbers;

	// Use this for initialization
	void Start () {
		Dice.Instance.CreateDices(this.transform.position,3,3,Callback);
	}
	
	void Callback(int[] attackNumbers, int[] defenseNumber){
		this.attackNumbers = attackNumbers;
		this.defenseNumbers = defenseNumber;
	}
}
