using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour {
	static Order last;
	static Player[] players;
	public Text[] playersName;
	// Use this for initialization

	void Start () {
		playersName = new Text[RequestController.Instance.playersInfos.Count];
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
		Order.Refresh (0);
	}
	
	public static void Refresh(int index){
		List<PlayerHold> l = RequestController.Instance.playersInfos;
		int count = 0;
		index += last.playersName.Length;
		foreach (PlayerHold p in l) {
			last.playersName[(index - p.order)%last.playersName.Length].text = p.name;
			(last.playersName[(index - p.order)%last.playersName.Length].transform.parent.GetComponent<Image>() as Image).color = players[p.color].displayColor;
		}
	}
}
