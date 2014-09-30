using UnityEngine;
using System.Collections;

public class TroopInTerritory : MonoBehaviour {

	public Troop troop;
	public Territory territory;

	private Vector3 lastPosition;

	void Start(){
		lastPosition = rigidbody.position;
		troop = GetComponent<Troop>();
	}

	void Update(){
		if(this.territory != null && lastPosition != rigidbody.position){
			Vector2 point = new Vector2(rigidbody.position.x, rigidbody.position.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				Territory otherTerritory = hitCollider.GetComponentInChildren<Territory>();
				if(this.territory == otherTerritory){
					lastPosition = rigidbody.position;
				}
				else{
					rigidbody.position = lastPosition;
					rigidbody.velocity = Vector3.zero;
					rigidbody.angularVelocity = Vector3.zero;
				}
			}
			else{
				rigidbody.position = lastPosition;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

}
