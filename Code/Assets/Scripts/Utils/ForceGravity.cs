using UnityEngine;
using System.Collections;

public class ForceGravity : MonoBehaviour {

	public float mass = 10f;
	protected Rigidbody rig;

	void Start () {
		rig = GetComponentInParent<Rigidbody> ();
	}
	
	void FixedUpdate(){
		rig.AddForceAtPosition (Physics.gravity * mass, transform.position, ForceMode.Force);
	}
}
