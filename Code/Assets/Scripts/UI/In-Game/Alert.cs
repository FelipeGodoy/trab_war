using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Alert : MonoBehaviour {
	public Text title;
	public Text message;
	// Use this for initialization
	public void close(){
		Destroy (this.gameObject);
	}

	public void set(string Title, string Message){
		title.text = Title;
		message.text = Message;
	}

}
