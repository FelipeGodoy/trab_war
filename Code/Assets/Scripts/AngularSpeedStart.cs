using UnityEngine;
using System.Collections;

public class AngularSpeedStart : MonoBehaviour {

	public Vector3 angularSpeed = new Vector3(-40,20,40);
	void Start () {
		rigidbody.maxAngularVelocity = 100f;
		rigidbody.AddTorque(angularSpeed,ForceMode.VelocityChange);
	}
}
