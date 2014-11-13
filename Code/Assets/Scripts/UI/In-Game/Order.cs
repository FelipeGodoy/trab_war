using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour {
	static Order last;
	static Player[] players;
	public Text[] playersName;
	// Use this for initialization

	void Awake(){
		playersName = new Text[RequestController.Instance.AllPlayersInfo.Count];
		Text[] txt = GetComponentsInChildren<Text> ();
		foreach (Text t in txt) {
			int pos = int.Parse(t.gameObject.name)-1;
			if(pos < playersName.Length)
				playersName[pos] = t;
			else
				Destroy(t.transform.parent.gameObject);
		}	
		last = this;
		players = new Player [6];
		for (int i = 0; i < 6; i++) {
			players[i] = GameObject.Find ("Player"+i).GetComponent<Player>();
		}
	}

	void Start () {
		Order.Refresh (0);
	}
	
	public static void Refresh(int index){
		//int index = GameController.Instance.TurnPlayerIndex;
		List<PlayerHold> l = RequestController.Instance.AllPlayersInfo;
		index -= last.playersName.Length;
		foreach (PlayerHold p in l) {
			 last.playersName[(p.order - index)%last.playersName.Length].text = p.name;
			(last.playersName[(p.order - index)%last.playersName.Length].transform.parent.GetComponent<Image>() as Image).color = players[p.color].displayColor;
		}
	}
	
}
