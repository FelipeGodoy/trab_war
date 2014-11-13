using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyHandler : MonoBehaviour {
	public GameObject[] slots;
	public PlayerHold myself;
	public int id;
	public string[] names;
	protected const float tickToRequest = 1f;
	private float tickElapsed;

	private bool sendingRequest;

	void Start () {
		names = new string[]{"Leonardo Murta", "Igor", "Jefferson", "Daniel", "Gabriel", "Alessandro", "Marcelo", "Alexandre",
		                   "Felipe", "Matheus", "Carlos", "Bruno", "Gabriel", "Gisele", "Fabiane", "Ian", "Roberto",
		                   "Igor", "Paulo", "Karina", "Filipe", "Fernando", "Julius", "Pedro", "Maximiano", "Joao",
		                   "Jorge", "Matheus", "Alexandre", "Luiz","Victor", "Rafael", "Marina", "Paulo",
		                   "Thadeu", "Leonardo", "Luana", "Victor", "Marcelo", "Bernardo", "Leonardo", "Matheus",
		                   "Raphael", "Vinicius", "Outro Gustavo", "Tito", "Ana", "Claudio","Hugo", "Gustavo T.",
						   "Arthur", "Tomas", "Felipe", "Leandro", "Felipe", "Thiago"};

		sendingRequest = false;
		UpdateView();
		tickElapsed = 0f;
	}
	//Criar animaçao de loading
	//RequestController.Instance.gameObject.GetComponent<LoadingAnimation>().StartLoading(transform.parent);
	void Update(){
		if(tickElapsed >= tickToRequest && !sendingRequest){
//			print (RequestController.Instance.url+"/games/"+RequestController.Instance.gameId+".json");
			Request r = Request.Create(RequestController.Instance.url+"/games/"+RequestController.Instance.gameId+".json");
			r.Get(OnGameRequestResponse);
			sendingRequest = true;
			tickElapsed = tickElapsed % tickToRequest;
		}
		tickElapsed += Time.deltaTime;
	}

	public void OnGameRequestResponse(WWW www){
		sendingRequest = false;
		if(www.error != null)return;
		JSONObject json = new JSONObject(www.text);
		List<PlayerHold> playersInfo = Request.JSONToPlayersHolds(json.GetField("players"));
		PlayerHold[] playersArray = playersInfo.ToArray();
		RequestController.Instance.remotePlayersInfos = playersInfo;
		UpdateView ();
		if(json.GetField("active").b){
			Application.LoadLevel("Tabuleiro");
		}
	}

	public void StartGame(){
		Request r = Request.Create(RequestController.Instance.url + "/games/start.json");
		r.AddParam("game_id",""+RequestController.Instance.gameId);
		r.Post(null);
	}

	void UpdateView(){
		GameObject g;
		Panel p;
		bool[] hasPlayer = new bool[slots.Length];
		List<PlayerHold> players = RequestController.Instance.AllPlayersInfo;
		foreach (PlayerHold ph in players) {
			g = slots[ph.color];
			hasPlayer[ph.color] = true;
			p = g.GetComponentInChildren<Panel>();
//			if(ph.type == Player.PlayerType.NON_PLAYER_CHARACTER){
//				p.setIA(ph.name);
//			}
//			else{
				p.setPlayer(ph.name);
//			}
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
		string name = null;
		while(name == null){
			int idx = Random.Range (0, names.Length);
			if(names[idx] != null){
				name = names[idx];
				names[idx] = null;
			}
		}
		int gameId = RequestController.Instance.gameId;
		int playerType = (int)Player.PlayerType.NON_PLAYER_CHARACTER;
		Request r = Request.Create(RequestController.Instance.url + "/rooms/connect.json");
		r.AddParam("player[name]",name);
		r.AddParam("player[type_id]",""+playerType);
		r.AddParam("game_id",""+gameId);
		r.Post(OnConnectRoomReponse);
	}

	public void OnConnectRoomReponse(WWW www){
		if(www.error != null)return;
		JSONObject json = new JSONObject(www.text);
		string username = json.GetField("new_player").GetField("name").str;
		int playerId = (int)json.GetField("new_player").GetField("id").n;
		int colorId = (int)json.GetField("new_player").GetField("color").n;
		Player.PlayerType type = (Player.PlayerType)json.GetField("new_player").GetField("type_id").n;
		PlayerHold ph = new PlayerHold(username,playerId,colorId,type);
		RequestController.Instance.playersInfos.Add(ph);
	}
}
