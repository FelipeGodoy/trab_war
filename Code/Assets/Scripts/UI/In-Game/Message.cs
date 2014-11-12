using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Message : MonoBehaviour {
	public GameObject alert;
	static Message example;

	void Start(){
		example = this;
	}

	public void position(Alert obj){
		obj.transform.position = this.transform.position;
		obj.transform.parent = this.transform;
		RectTransform r = obj.GetComponent<RectTransform>();
		RectTransform model = this.GetComponent<RectTransform>();
		r.sizeDelta = model.sizeDelta;
	}

	public static void New(string title, string message, Alert.Delegate onClose){
		GameObject g = (GameObject)Resources.Load("Alert");
		//Alert a = g.GetComponent<Alert>();
		Alert a = ((GameObject)Instantiate (g)).GetComponent<Alert>();
		a.set(title,message);
		a.onClose = onClose;
		example.position (a);
	}

	public void test(){
		Message.New ("HUEHU", "heuheuheuheuheuheu", null);
	}
}
