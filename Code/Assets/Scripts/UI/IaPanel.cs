using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IaPanel : Panel {

	public void setName(string s){
		Text t = GetComponentInChildren<Text> ();
		t.text = s;
	}

}
