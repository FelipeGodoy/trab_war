using UnityEngine;
using System.Collections;

public class roomData : MonoBehaviour {
	public string roomName, roomPassword, username;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	public void setRoomName ( string n) {
		roomName = n;
	}
	public void setRoomPassword ( string n) {
		roomPassword = n;
	}
	public void setUsername ( string n) {
		username = n;
	}
}
