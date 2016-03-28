using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Request {

	public string url;
	public bool isPost;
	private Dictionary<string, string> formParams;

	public delegate void Delegate(WWW www);

	public static Request Create(string url){
		Request request = new Request ();
		request.url = url;
		request.formParams = new Dictionary<string, string>();
		request.isPost = false;
		return request;
	}

	public static Request Create(string path, params string[] formParams){
		Request request = new Request();
		request.url = RequestController.Instance.url + path;
		request.SetFields(formParams);
		request.isPost = false;
		return request;
	}

	public bool RemoveParam(string key){
		return formParams.Remove(key);
	}

	public void AddParam(string key, string value){
		formParams.Add(key,value);
	}

	public void ChangeParam(string key, string value){
		formParams[key] = value;
	}

	public void SetFields(params string[] formParams){
		if(formParams.Length % 2 == 1){
			Debug.LogError("Fields should have pair params!");
		}
		this.formParams = new Dictionary<string, string>();
		for(int i =0; i < formParams.Length; i+=2){
			this.formParams.Add(formParams[i], formParams[i+1]);
		}
	}

	public void Get(Delegate callback){
		string url = this.url;
		bool first = true;
		foreach(KeyValuePair<string, string> pair in formParams){
			if(first){ 
				url += "?";
				first = false;
			}
			else url += "&";
			url += pair.Key + "="+pair.Value;
		}
		WWW www = new WWW(url);
		RequestController.Instance.StartRequest(www, callback);
	}

	public void Post(Delegate callback){
		WWWForm form = new WWWForm();
		foreach(KeyValuePair<string, string> pair in formParams){
			form.AddField(pair.Key, pair.Value);
		}
		WWW www = new WWW(url,form);
		RequestController.Instance.StartRequest(www, callback);
	}

	public static List<PlayerHold> JSONToPlayersHolds(JSONObject json){
		List<PlayerHold> players = new List<PlayerHold>();
		if(json.IsArray){
			foreach(JSONObject jsonPlayer in json.list){
				players.Add(JSONToPlayerHold(jsonPlayer));
			}
		}
		return players;
	}

	public static PlayerHold JSONToPlayerHold(JSONObject json){
		int playerId = (int)json.GetField("id").n;
		int colorId = (int)json.GetField("color").n;
		string name = json.GetField("name").str;
		Player.PlayerType type = (Player.PlayerType)json.GetField("type_id").n;
		PlayerHold p = new PlayerHold(name,playerId,colorId,type);
		if(!json.GetField("order").IsNull){
			p.order = (int)json.GetField("order").n;
		}
		if(!json.GetField("goal_id").IsNull){
			p.goalId = (int)json.GetField("goal_id").n;
		}
		if(!json.GetField("init_territories").IsNull){
			List<JSONObject> jsonTerritories = json.GetField("init_territories").list;
			int[] territories = new int[jsonTerritories.Count];
			for(int i=0; i < territories.Length; i++){
				territories[i] = (int)jsonTerritories[i].n;
			}
			p.initTerritories = territories;
		}
		return p;
	}

}
