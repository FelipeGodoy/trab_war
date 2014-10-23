using UnityEngine;
using System.Collections;

public class TroopInTerritory : MonoBehaviour {

	public Troop troop;
	public Territory territory;

	private Rigidbody parentRigidbody;

	private Vector3 lastPosition;

	void Start(){
		territory = troop.CurrentTerritory;
		parentRigidbody = troop.rigidbody;
		lastPosition = parentRigidbody.position;
	}

	void Update(){
		if(this.territory != null && lastPosition != parentRigidbody.position){
			Vector2 point = new Vector2(parentRigidbody.position.x, parentRigidbody.position.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				Territory otherTerritory = hitCollider.GetComponentInChildren<Territory>();
				if(this.territory == otherTerritory){
					lastPosition = parentRigidbody.position;
				}
				else{
					parentRigidbody.position = lastPosition;
					parentRigidbody.velocity = Vector3.zero;
					parentRigidbody.angularVelocity = Vector3.zero;
				}
			}
			else{
				parentRigidbody.position = lastPosition;
				parentRigidbody.velocity = Vector3.zero;
				parentRigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

}
