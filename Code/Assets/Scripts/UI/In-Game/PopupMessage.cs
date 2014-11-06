using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupMessage : MonoBehaviour {
	public GameObject alert;

	public void createAlert(string title, string message){
		GameObject o = (GameObject)Instantiate (alert);
		o.transform.position = this.transform.position;
		o.transform.parent = this.transform;
		RectTransform r = o.GetComponent<RectTransform>();
		RectTransform model = this.GetComponent<RectTransform>();
		r.sizeDelta = model.sizeDelta;
		o.GetComponent<Alert>().set(title, message);
	}

	public void test(){
		string t = "HUEHUEHUE";
		string m = "HUEHUHEUHEUHEUHEUHEUHEUHEUH";
		createAlert (t,m);
	}
}
