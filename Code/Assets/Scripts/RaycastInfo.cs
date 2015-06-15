using UnityEngine;
using System.Collections;

public class RaycastInfo : MonoBehaviour {
//	public Transform trans;
	GUIFacade gui{
		get{
			return GameController.Instance.gui;
		}
	}
	public LayerMask floorLayer;
	public float animationTime = 0.5f;
	public iTween.EaseType ease = iTween.EaseType.easeInBack;
	public Color animationColor = new Color(0.9f,0.9f,0.9f);
	public Color animationNeibColor = new Color(0.1f,0.1f,0.1f);
	public GameObject troopPrefab;
	public Vector3 troopOffset = new Vector3(0,0,-1);
	
	// Update is called once per frame
	void Awake(){
//		gui = GameObject.Find ("GUIFacade").GetComponent<GUIFacade> ();
	}

	void Update () {
		if(gui == null)return;
		RaycastHit raycast;
		if(Physics.Raycast(this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out raycast,Mathf.Infinity,floorLayer)){
//			trans.position = raycast.point;
			Vector2 point = new Vector2(raycast.point.x, raycast.point.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				TerritoryInfo territoryInfo = hitCollider.GetComponentInChildren<TerritoryInfo>();
				Territory territory = hitCollider.GetComponentInChildren<Territory>();
				if(territoryInfo != null){
					gui.info.setActive(true);
					gui.info.changeInfo(territoryInfo.name,""+ territoryInfo.GetComponentsInChildren<Troop>().Length, territoryInfo.color);
					SetColorTerritoryInfo(territoryInfo);
					SetColorNeighbors(territory);
					if(Input.GetMouseButtonDown(0)){
//						territory.AddTroop();
					}
					if(Input.GetMouseButtonDown(1)){
//						territory.RemoveTroop();
					}
				}
			}
			else{
				gui.info.setActive(false);
			}
		}
	}

	void SetColorTerritoryInfo(TerritoryInfo territoryInfo){
		iTween.ValueTo(territoryInfo.gameObject,iTween.Hash(iT.ValueTo.to, territoryInfo.color, 
		                                                    iT.ValueTo.from, this.animationColor, 
		                                                    iT.ValueTo.onupdatetarget, territoryInfo.gameObject,
		                                                    iT.ValueTo.onupdate, "SetAnimationColor",
		                                                    iT.ValueTo.time, this.animationTime,
		                                                    iT.ValueTo.easetype, ease));
	}

	void SetColorNeighbors(Territory territory){
		if(territory != null && territory.neighbors != null){
			foreach(Territory neigbor in territory.neighbors){
				TerritoryInfo territoryInfo = neigbor.GetComponent<TerritoryInfo>();
				iTween.ValueTo(territoryInfo.gameObject,iTween.Hash(iT.ValueTo.to, territoryInfo.color, 
				                                                    iT.ValueTo.from, this.animationNeibColor, 
				                                                    iT.ValueTo.onupdatetarget, territoryInfo.gameObject,
				                                                    iT.ValueTo.onupdate, "SetAnimationColor",
				                                                    iT.ValueTo.time, this.animationTime,
				                                                    iT.ValueTo.easetype, ease));
			}
		}
	}
}
