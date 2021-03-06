﻿using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour {

	public float distance = 1f;
	public float offset = -1.14f;
	public float magnitude = 6f;
	public float speed = 0.5f;
	public Vector3 accelerometer;
	public float avr = 0f;
	private float lastAvr = 0f;

	private bool up;

	public float velocity = 0f;

	
	// Update is called once per frame
	void Update () {
//		accelerometer = Input.acceleration;
////		avr = accelerometer.sqrMagnitude;
//		avr = Mathf.Round((accelerometer.sqrMagnitude - offset) * magnitude) / magnitude;
////		transform.Translate(0f,avr * speed, 0f);
//		velocity += (((lastAvr + avr) / 2f) * Time.deltaTime) - velocity * 0.1f;
//		transform.Translate(0f,velocity * Time.deltaTime, 0f);
//		lastAvr = avr;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(transform.position, new Vector3(1f,1f,distance));
	}
}
