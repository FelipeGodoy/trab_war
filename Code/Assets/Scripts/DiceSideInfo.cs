﻿using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class DiceSideInfo : MonoBehaviour {

	public int diceNumber = -1;

	private readonly Vector3 defaultAngle = new Vector3(90f,0f,0f);

	void Update () {
		diceNumber = DiceSide(Quaternion.Inverse(Quaternion.Euler(Camera.main.transform.rotation.eulerAngles - defaultAngle)) * transform.rotation);
	}

	public int DiceSide(Quaternion angle){
		Vector3 eulerAngles = angle.eulerAngles;
		eulerAngles.x = Mathf.Round(eulerAngles.x / 90f) % 4f;
		eulerAngles.y = Mathf.Round(eulerAngles.y / 90f) % 4f;
		eulerAngles.z = Mathf.Round(eulerAngles.z / 90f) % 4f;
		if(eulerAngles.x == 1f)
			return 2;
		if(eulerAngles.x == 3f)
			return 5;
		if(eulerAngles.x ==0f){
			if(eulerAngles.z == 0f)
				return 1;
			if(eulerAngles.z == 1f)
				return 3;
			if(eulerAngles.z == 2f)
				return 6;
			else
				return 4;
		}
		if(eulerAngles.x == 2f){
			if(eulerAngles.z == 0f)
				return 6;
			if(eulerAngles.z == 1f)
				return 4;
			if(eulerAngles.z == 2f)
				return 1;
			else
				return 3;
		}
		return -1;
	}
}
