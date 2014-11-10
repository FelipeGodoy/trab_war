using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatesHandler : MonoBehaviour {
	public string alocar;
	public string atacar;
	public string mover;
	public string none;
	Text txt;
	ChangeColor[] lights;

	void Start(){
		txt = this.GetComponentInChildren<Text> ();
		lights = GetComponentsInChildren<ChangeColor> ();
		}

	public void setAlocar(int qtd){
		ChangeColor[] t;
		foreach (ChangeColor c in lights) {
			if(c.gameObject.name == "Light1"){
				c.activate();
				}
			else{
				c.deactivate();
			}
		}
		if (qtd == 1)
			txt.text = alocar + " - " +qtd + " peça";
		else
			txt.text = alocar + " - " +qtd + " peças";
	}

	public void setAtacar(){
		ChangeColor[] t;
		foreach (ChangeColor c in lights) {
			if(c.gameObject.name == "Light2"){
				c.activate();
			}
			else{
				c.deactivate();
			}
		}
		txt.text = atacar;
	}

	public void setMover(){
		ChangeColor[] t;
		foreach (ChangeColor c in lights) {
			if(c.gameObject.name == "Light3"){
				c.activate();
			}
			else{
				c.deactivate();
			}
		}
		txt.text = mover;
	}

	public void setNone(){
		ChangeColor[] t;
		foreach (ChangeColor c in lights) {
			c.deactivate();
		}
		txt.text = none;
	}
}
