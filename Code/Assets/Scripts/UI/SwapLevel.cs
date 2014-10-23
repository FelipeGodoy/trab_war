using UnityEngine;
using System.Collections;

public class SwapLevel : MonoBehaviour {
	// Use this for initialization
	public void changeLevel(int levelIndex){
		Application.LoadLevel (levelIndex);
	}
}
