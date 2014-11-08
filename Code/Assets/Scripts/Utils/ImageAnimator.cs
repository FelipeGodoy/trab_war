using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageAnimator : MonoBehaviour {
	public Sprite[] seq;
	public float delay;
	float i = 0;
	int j;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		i += Time.deltaTime;
		if (i > delay) {
			GetComponent<Image> ().sprite = seq [j];
			j = (j +1)%seq.Length;
			i = 0;
		}
	}
}
