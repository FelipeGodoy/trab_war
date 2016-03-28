using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DiceTestStartButton : MonoBehaviour {

	public DiceSideInfo yellowDicePrefab;
	public DiceSideInfo redDicePrefab;
	public DiceInputField yellowInput;
	public DiceInputField redInput;

	private List<DiceSideInfo> dices = new List<DiceSideInfo>();

	public void OnClickButton(){
		DiceSideInfo yellowDice = Instantiate<DiceSideInfo> (yellowDicePrefab);
		DiceSideInfo redDice = Instantiate<DiceSideInfo> (redDicePrefab);
		yellowInput.dice = yellowDice;
		redInput.dice = redDice;
		yellowInput.ChangeDiceCount (yellowInput.GetComponent<InputField> ().text);
		redInput.ChangeDiceCount (redInput.GetComponent<InputField> ().text);
//		foreach (DiceSideInfo dice in dices) {
//			Destroy (dice.gameObject);
//		}
		dices.Clear ();
		dices.Add (yellowDice);
		dices.Add (redDice);
	}

}
