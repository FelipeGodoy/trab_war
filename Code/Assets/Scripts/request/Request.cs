using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Request : ScriptableObject {

	public string url;
	private Dictionary<string, string> formParams;

	public delegate void Delegate(string message);

	public static Request Create(string url, params string[] formParams){
		Request request = ScriptableObject.CreateInstance<Request>();
		request.url = url;
		request.SetFields(formParams);
		return request;
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
		foreach(KeyValuePair<string, string> pair in formParams){
			url += "?" + pair.Key + "="+pair.Value;
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

}
