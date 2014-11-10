using UnityEngine;
using System.Collections;

public class LoadingAnimation : MonoBehaviour {
	public GameObject loadingPref;
	GameObject loading;

	public void StartLoading(Transform pai){
		EndLoading ();
		loading = Instantiate(loadingPref) as GameObject;
		loading.transform.position = pai.position;
		loading.transform.parent = pai;
	}

	public void EndLoading(){
		Destroy (loading);
		loading = null;
	}

}
