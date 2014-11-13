using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingGamesLoader : MonoBehaviour {
	//public Request r;
	public LinkedList<ListOption> l;
	public GameObject loading;
	GameObject load;
	public ListOption option;
	float minY = 0.9077111f;
	float maxY = 0.9868443f;
	const float MIN_Y = 0.9077111f;
	const float MAX_Y = 0.9868443f;
	public GameObject pos;
	Vector3 next;

	// Use this for initialization
	void Start () {
		l = new LinkedList<ListOption>();
		next = pos.transform.position;
		OnUpdateGames();
	}

	public void OnClickConnect(){
		int roomId = ListOption.sala_id_selected;
		if(roomId != -1){
			RequestController.Instance.gameObject.GetComponent<LoadingAnimation>().StartLoading(transform.parent);
			string name = transform.parent.GetComponentInChildren<InputField>().text.text;
			RequestController.Instance.gameId = roomId;
			int tipo = (int)Player.PlayerType.PLAYER_CHARACTER;
			Request r = Request.Create(RequestController.Instance.url+"/rooms/connect.json");
			r.AddParam("player[name]",name);
			r.AddParam("player[type_id]",""+tipo);
			r.AddParam("game_id",""+roomId);
			RequestController.Instance.playersInfos.Clear();
			r.Post(OnRequestConnectResponse);
		}
	}

	public void OnRequestConnectResponse(WWW www){
		RequestController.Instance.gameObject.GetComponent<LoadingAnimation>().EndLoading();
		load = null;
		if(www.error == null){
			JSONObject json = new JSONObject(www.text);
			int playerId = (int)json.GetField("new_player").GetField("id").n;
			int colorId = (int)json.GetField("new_player").GetField("color").n;
			string name = json.GetField("new_player").GetField("name").str;
			Player.PlayerType type = (Player.PlayerType)json.GetField("new_player").GetField("type_id").n;
			PlayerHold ph = new PlayerHold(name,playerId,colorId,type);
			RequestController.Instance.playersInfos.Add(ph);
			Application.LoadLevel("Lobby");
		}
	}
	
	// Update is called once per frame

	public void OnUpdateGames(){
		RequestController.Instance.gameObject.GetComponent<LoadingAnimation>().StartLoading(transform.parent);
		Request r = Request.Create(RequestController.Instance.url+"/rooms.json");
		r.Get(OnRequestUpdateRoomsResponse);
	}

	private void OnRequestUpdateRoomsResponse(WWW www){
		RequestController.Instance.gameObject.GetComponent<LoadingAnimation> ().EndLoading();
		if(www.error != null) return;
		JSONObject json = new JSONObject(www.text);
		minY = MIN_Y;
		maxY = MAX_Y;
		foreach(ListOption li in l){
			Destroy(li.gameObject);
		}
		this.l.Clear();
		foreach(JSONObject jsonGame in json.list){
			int id = (int)jsonGame.GetField("id").n;
			string name = jsonGame.GetField("name").str;
			ListOption l = addOptions();
			if(l != null){
				l.sala_id = id;
				Text[] texts = l.GetComponentsInChildren<Text>();
				foreach(Text t in texts){
					if(t.gameObject.name.Equals("NomeSala")){
						t.text = name;
					}
					else if(t.gameObject.name.Equals("NomeCriador")){
						t.text = ""+id;
					}
				}
			}
		}
	}

	public ListOption addOptions () {
		//Separa em grupos
		int tamanhoGrupo = 12;
		int qtd = l.Count;
		if (qtd >= 12) {
			return null;
		}
		float diff = (maxY - minY);
		//for(int i = 0; i < tamanhoGrupo; i++){
		ListOption o = (ListOption)Instantiate (option);
		o.transform.position = pos.transform.position;
		next.y += (maxY - minY);
		o.transform.parent = this.transform;
		l.AddLast(o);
		RectTransform r = o.GetComponent<RectTransform>();
		RectTransform model = option.GetComponent<RectTransform>();
		print(r.pivot);
		r.sizeDelta = model.sizeDelta;
		
		Vector2 min = new Vector2 ();
		min = model.anchorMin;
		min.y = minY- qtd*diff;
		r.anchorMin = min;
		
		Vector2 max = new Vector2 ();
		max = model.anchorMax;
		max.y = maxY - qtd*diff;
		r.anchorMax = max;
		return o;
	}
	
}
