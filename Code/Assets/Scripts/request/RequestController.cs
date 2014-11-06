using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequestController : MonoBehaviour {

	private static RequestController instance;
	public bool debugMode = false;
	public string defaultUrl = "http://war-uff.herokuapp.com";
	public string testUrl = "http://0.0.0.0:3000";
	public string shotPath = "\\games\\shots";
	public float delayRequest = 1f;
	public int shotCount;
	private List<Shot> shotsToSend;
	private List<JSONObject> jsonShotsReceived;
	public int gameId;
	private bool sending;

	public string url{
		get{
			if(debugMode) return testUrl;
			return defaultUrl;
		}
	}

	public void SendShot(Shot shot){
		shotsToSend.Add(shot);
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
		jsonShotsReceived = new List<JSONObject>();
		shotsToSend = new List<Shot>();
		shotCount = 0;
		instance = this;
		DontDestroyOnLoad(this.gameObject);
		sending = false;
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
		}
		if(callback != null){
			callback(www.text);
		}
	}
}
