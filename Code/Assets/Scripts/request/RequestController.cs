using UnityEngine;
using System.Collections;

public class RequestController : MonoBehaviour {

	private static RequestController instance;
	public string url = "http://war-uff.herokuapp.com";

	void Start(){
		instance = this;
		DontDestroyOnLoad(this.gameObject);
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
