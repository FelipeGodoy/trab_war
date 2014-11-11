using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Alert : MonoBehaviour {
	public delegate void Delegate();
	public Delegate onClose;
	public Text title;
	public Text message;

	public void close(){
		if(onClose != null)onClose();
		Destroy (this.gameObject);
	}

	public void set(string Title, string Message){
		title.text = Title;
		message.text = Message;
	}

	public static void Message(string title, string message, Delegate onClose){
		GameObject g = (GameObject)Resources.Load("Alert");
		Alert a = g.GetComponent<Alert>();
		a.set(title,message);
		a.onClose = onClose;
	}

}
