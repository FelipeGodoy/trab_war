using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequestController : MonoBehaviour {

	private static RequestController instance;
	public bool debugMode = false;
	public string remoteUrl = "http://war-uff.herokuapp.com";
	public string localUrl = "http://127.0.0.1:3000";
	public string shotPath = "/games/shots";
	public string connectPath = "/rooms/connect";//post, game_id, player[name], player[type_id]
	public string startGamePath = "/games/start";
	public float delayRequest = 1f;
	public int shotCount;
	private List<Shot> shotsToSend;
	private List<JSONObject> jsonShotsReceived;
	public int gameId;
	public string gameName;
	private bool sending;

	public List<PlayerHold> playersInfos;
	public List<PlayerHold> remotePlayersInfos;

	public List<PlayerHold> AllPlayersInfo{
		get{
			List<PlayerHold> all = new List<PlayerHold>(playersInfos);
			foreach(PlayerHold remote in remotePlayersInfos){
				PlayerHold local = playersInfos.Find(a => a.color == remote.color);
				if(local != null){
					local.initTerritories = remote.initTerritories;
					local.order = remote.order;
					local.goalId = remote.goalId;
				}
				else{
					remote.type = Player.PlayerType.REMOTE_PLAYER_CHARACTER;
					all.Add(remote);
				}
			}
			return all;
		}
	}

	public bool SendingRequest{
		get{
			return sending;
		}
	}

	public string url{
		get{
			if(debugMode) return localUrl;
			else return remoteUrl;
		}
	}

	public void SendShot(Shot shot){
		shotsToSend.Add(shot);
	}

	void Awake(){
		if(instance != null && instance != this){
			Destroy(gameObject);
		}
		else{
			playersInfos = new List<PlayerHold>();
			remotePlayersInfos = new List<PlayerHold>();
			jsonShotsReceived = new List<JSONObject>();
			shotsToSend = new List<Shot>();
			shotCount = 0;
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			sending = false;
		}
	}

	void Update(){
		if(!sending && shotsToSend.Count > 0){
			sending = true;
			Request r = Request.Create(url+shotPath);
			r.AddParam("game_id",""+(gameId));
			r.AddParam("shot_id",""+(shotCount+1));
			int n = 0;
			foreach(Shot shot in shotsToSend){
				r.AddParam("shots["+n+"][content]",ShotEncoder.ToJSON(shot).ToString());
				n++;
			}
			r.Post(OnResponseSendShot);
		}
	}

	public void OnResponseSendShot(string s){
		JSONObject json = JSONObject.Create(s);
		if(!json.HasField("error")){
			int savedShots = (int)json.GetField("shots_saved").n;
			shotCount += savedShots;
			shotsToSend.RemoveRange(0,savedShots);
		}
		else{
			Debug.LogError(json.GetField("error").str);
		}
		sending = false;
		Debug.Log(!json.HasField("error") + s);
	}

	void Start(){
		if(instance != null && instance != this){
			Destroy(gameObject);
		}
		else{
			playersInfos = new List<PlayerHold>();
			remotePlayersInfos = new List<PlayerHold>();
			jsonShotsReceived = new List<JSONObject>();
			shotsToSend = new List<Shot>();
			shotCount = 0;
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			sending = false;
		}
	}

	public static RequestController Instance{
		get{
			if(instance == null){
				GameObject g = new GameObject("RequestController");
				DontDestroyOnLoad(g);
				instance = g.AddComponent<RequestController>();
			}
			return instance;
		}
	}

	public void StartRequest(WWW www, Request.Delegate callback){
		StartCoroutine(_StartRequest(www,callback));
	}

	private IEnumerator _StartRequest(WWW www, Request.Delegate callback){
		yield return www;
		if(www.error != null){
			Debug.LogError("Request Error: "+www.error);
			Message.New("Error",www.error,delegate {
				Application.LoadLevel("Menu");
		});
		}
		else if(callback != null){
			callback(www.text);
		}
	}
}
