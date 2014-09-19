using UnityEngine;
using System.Collections;

public class RaycastInfo : MonoBehaviour {
//	public Transform trans;
	public float animationTime = 0.5f;
	public iTween.EaseType ease = iTween.EaseType.easeInBack;
	public Color animationColor = new Color(0.9f,0.9f,0.9f);
	public GameObject troopPrefab;
	public Vector3 troopOffset = new Vector3(0,0,-1);
	
	// Update is called once per frame
	void Update () {
		RaycastHit raycast;

		if(Physics.Raycast(this.camera.ScreenPointToRay(Input.mousePosition),out raycast)){
//			trans.position = raycast.point;
			Vector2 point = new Vector2(raycast.point.x, raycast.point.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				TerritoryInfo territory = hitCollider.GetComponentInChildren<TerritoryInfo>();
				if(territory != null){
					iTween.ValueTo(territory.gameObject,iTween.Hash(iT.ValueTo.to, territory.color, 
					                                                iT.ValueTo.from, this.animationColor, 
					                                                iT.ValueTo.onupdatetarget, territory.gameObject,
					                                                iT.ValueTo.onupdate, "SetAnimationColor",
					               									iT.ValueTo.time, this.animationTime,
					               									iT.ValueTo.easetype, ease));
					if(Input.GetMouseButtonDown(0)){
						GameObject g = (GameObject)Instantiate(troopPrefab);
						g.transform.position = territory.transform.position;
						g.transform.Translate(troopOffset);
						AngularSpeedStart a = g.AddComponent<AngularSpeedStart>();
						a.angularSpeed.x = Random.Range(-18f,18f);
						a.angularSpeed.y = Random.Range(-18f,18f);
						a.angularSpeed.z = Random.Range(-18f,18f);
					}
				}
			}
		}
	}
}
