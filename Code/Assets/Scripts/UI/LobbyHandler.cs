using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyHandler : MonoBehaviour {
	public GameObject[] slots;
	public PlayerHold myself;
	public int id;

	private bool sendingRequest;

	void Start () {
		sendingRequest = false;
	}
	//Criar animaçao de loading
	//RequestController.Instance.gameObject.GetComponent<LoadingAnimation>().StartLoading(transform.parent);
	void Update(){
		if(!sendingRequest){
			print (RequestController.Instance.url+"/games/"+RequestController.Instance.gameId+".json");
			Request r = Request.Create(RequestController.Instance.url+"/games/"+RequestController.Instance.gameId+".json");
			r.Get(OnGameRequestResponse);
			sendingRequest = true;
		}
	}

	public void OnGameRequestResponse(string s){
		sendingRequest = false;
		JSONObject json = new JSONObject(s);
		//DEVE ESTAR ERRADO
		int playerId = (int)json.GetField("new_player").GetField("id").n;
		int colorId = (int)json.GetField("new_player").GetField("color").n;
		string name = json.GetField("new_player").GetField("name").str;
		Player.PlayerType type = (Player.PlayerType)json.GetField("new_player").GetField("type_id").n;
		foreach (PlayerHold ph in RequestController.Instance.playersInfos) {
			if (ph.backend_id == playerId) {
				ph.color = colorId;
				ph.name = name;
				ph.type = type;
			}
		}
		PlayerHold phold = new PlayerHold(name,playerId,colorId,type);
		RequestController.Instance.playersInfos.Add(phold);
		UpdateView ();
	}

	void UpdateView(){
		GameObject g;
		Panel p;
		bool[] hasPlayer = new bool[slots.Length];
		foreach (PlayerHold ph in RequestController.Instance.playersInfos) {
			g = slots[ph.order];
			hasPlayer[ph.order] = true;
			p = g.GetComponentInChildren<Panel>();
			if(ph.type == Player.PlayerType.NON_PLAYER_CHARACTER){
				p.setIA(ph.name);
			}
			else{
				p.setPlayer(ph.name);
			}
		}
		for(int i = 0; i < hasPlayer.Length; i++){
			if(hasPlayer[i] == false)
				slots[i].GetComponentInChildren<Panel>().setEmpty();
		}
	}

	public void ExitRequest(){
		//Requisitar saida
		OnExitRequestResponse ("HUE");
	}

	void OnExitRequestResponse(string s){
		Application.LoadLevel ("Menu");
	}


	public void AddIA(){
		string name = "BOT "; //Arrumar lista de nomes aleatorios
		int gameId = this.id;
		int playerType = (int)Player.PlayerType.NON_PLAYER_CHARACTER;
		RequestController.Instance.gameId = gameId;
		Request r = Request.Create(RequestController.Instance.url + "/rooms/connect.json");
		r.AddParam("player[name]",name);
		r.AddParam("player[type_id]",""+playerType);
		r.AddParam("game_id",""+gameId);
		r.Post(OnConnectRoomReponse);
	}

	public void OnConnectRoomReponse(string s){
		//DEVE ESTAR ERRADO
		JSONObject json = new JSONObject(s);
		string username = json.GetField("new_player").GetField("name").str;
		int playerId = (int)json.GetField("new_player").GetField("id").n;
		int colorId = (int)json.GetField("new_player").GetField("color").n;
		Player.PlayerType type = (Player.PlayerType)json.GetField("new_player").GetField("type_id").n;
		PlayerHold ph = new PlayerHold(username,playerId,colorId,type);
		RequestController.Instance.playersInfos.Add(ph);
	}
}
