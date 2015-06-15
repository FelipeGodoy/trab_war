using UnityEngine;
using System.Collections;

public class TroopInTerritory : MonoBehaviour {

	public Troop troop;
	public Territory territory;

	private Rigidbody parentRigidbody;
	private Vector3 lastTroopPosition;
	private Vector3 lastPosition;

	void Start(){
		territory = troop.CurrentTerritory;
		parentRigidbody = troop.GetComponent<Rigidbody>();
		lastTroopPosition = parentRigidbody.position;
		lastPosition = transform.position;
	}

	void Update(){
		if(this.territory != null && lastPosition != transform.position){
			Vector2 point = new Vector2(transform.position.x, transform.position.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				Territory otherTerritory = hitCollider.GetComponentInChildren<Territory>();
				if(this.territory == otherTerritory){
					lastTroopPosition = parentRigidbody.position;
					lastPosition = transform.position;
				}
				else{
					parentRigidbody.position = lastTroopPosition;
					parentRigidbody.velocity = Vector3.zero;
					parentRigidbody.angularVelocity = Vector3.zero;
				}
			}
			else{
				parentRigidbody.position = lastTroopPosition;
				parentRigidbody.velocity = Vector3.zero;
				parentRigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

}
