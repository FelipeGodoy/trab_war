using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	Image[] panels;
	Text name, territoryName, troopQtd;
	public bool show;
	// Use this for initialization
	void Start () {
		panels = GetComponentsInChildren<Image> ();
		Text[] temp = GetComponentsInChildren<Text> ();
		foreach (Text t in temp) {
			if (t.transform.parent.name == "Name") {
				name = t;
			} else if (t.transform.parent.name == "TerritoryName") {
				territoryName = t;
			} else if (t.transform.parent.name == "TroopQtd") {
				troopQtd = t;
			}
		}
	}

	void Update(){
		setActive (show);
	}

	public void changeColor(Color c){
		foreach (Image i in panels) {
			i.color = c;
		}	
	}

	public void setTexts(string Name, string Territory, string Qtd){
		name.text = Name;
		territoryName.text = Territory;
		troopQtd.text = Qtd;
	}

	public void setActive(bool active){
		Behaviour[] g = this.GetComponentsInChildren<Behaviour> ();
		foreach(Behaviour  o in g){
			if(o.transform != this.transform){
				o.enabled = active;
			}
		}
	}
}
