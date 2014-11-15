using UnityEngine;
using System.Collections;

public class TestRequest : MonoBehaviour {

	public string url;

	void Start () {
		Request r = Request.Create(url);
		r.SetFields("a","1","b","2");
		r.Get(Callback);
	}

	public static void Callback(WWW www){
		print(www.text);
	}
}
