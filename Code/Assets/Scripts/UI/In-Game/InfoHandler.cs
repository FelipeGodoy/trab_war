using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoHandler : MonoBehaviour {
	public bool show;
	Text txt;
	void Start(){
		txt = GetComponentInChildren<Text> ();
	}

	void Update(){
		//setActive (show);
	}

	// Update is called once per frame
	public void changeInfo (string territoryName, string qtd, Color c) {
		txt.text = territoryName + " (" + qtd + ")";
		GetComponent<Image> ().color = c;
	}

	public void setActive(bool active){
		Behaviour[] g = this.GetComponentsInChildren<Behaviour> ();
		foreach(Behaviour  o in g){
			if(o != this){
				o.enabled = active;
			}
		}
	}
}
