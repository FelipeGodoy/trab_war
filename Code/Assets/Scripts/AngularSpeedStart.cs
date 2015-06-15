using UnityEngine;
using System.Collections;

public class AngularSpeedStart : MonoBehaviour {

	public Vector3 angularSpeedMin = new Vector3(-18,-18,-18);
	public Vector3 angularSpeedMax = new Vector3(18,18,18);
	public float maxAngularVelocity = 100f;

	void Start () {
		GetComponent<Rigidbody>().maxAngularVelocity = this.maxAngularVelocity;
		GetComponent<Rigidbody>().AddTorque(Random.Range(angularSpeedMin.x, angularSpeedMax.x),
		                    Random.Range(angularSpeedMin.y, angularSpeedMax.y),
		                    Random.Range(angularSpeedMin.z, angularSpeedMax.z),
		                    ForceMode.VelocityChange);
	}
}
