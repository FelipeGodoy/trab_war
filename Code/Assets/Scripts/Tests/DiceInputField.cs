using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiceInputField : MonoBehaviour {
	public DiceSideInfo dice;

	public void ChangeDiceCount(string s){
		dice.forcedNumber = int.Parse (s);
	}

}
