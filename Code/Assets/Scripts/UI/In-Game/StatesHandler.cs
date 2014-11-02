using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatesHandler : MonoBehaviour {
	public string alocar;
	public string atacar;
	public string mover;

	public void setAlocar(){
		ChangeColor[] t;
		t = GetComponentsInChildren<ChangeColor> ();
		foreach (ChangeColor c in t) {
			if(c.gameObject.name == "Light1"){
				c.activate();
				}
			else{
				c.deactivate();
			}
		}
		Text txt = this.GetComponentInChildren<Text> ();
		txt.text = alocar;
	}

	public void setAtacar(){
		ChangeColor[] t;
		t = GetComponentsInChildren<ChangeColor> ();
		foreach (ChangeColor c in t) {
			if(c.gameObject.name == "Light2"){
				c.activate();
			}
			else{
				c.deactivate();
			}
		}
		Text txt = this.GetComponentInChildren<Text> ();
		txt.text = atacar;
	}

	public void setMover(){
		ChangeColor[] t;
		t = GetComponentsInChildren<ChangeColor> ();
		foreach (ChangeColor c in t) {
			if(c.gameObject.name == "Light3"){
				c.activate();
			}
			else{
				c.deactivate();
			}
		}
		Text txt = this.GetComponentInChildren<Text> ();
		txt.text = mover;
	}
}
