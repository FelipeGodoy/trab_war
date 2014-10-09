using UnityEngine;
using System.Collections;

public class TestRequest : MonoBehaviour {

	public string url;

	void Start () {
		Request r = Request.Create(url);
		r.Get(Callback);
	}

	public static void Callback(string s){
		print(s);
	}
}
