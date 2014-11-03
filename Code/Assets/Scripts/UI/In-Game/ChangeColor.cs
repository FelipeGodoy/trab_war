using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public Color active;
	public Color inactive;

	public void Start(){
		active = GetComponent<Image> ().color;
	}

	public void changeColor(){
		Image i = GetComponent<Image> ();
		if (i.color == active) {
			i.color = inactive;
		} else {
			i.color = active;
		}	
	}
	public void activate(){
		Image i = GetComponent<Image> ();
		i.color = active;
	}

	public void deactivate(){
		Image i = GetComponent<Image> ();
		i.color = inactive;
	}
}
