﻿using UnityEngine;
using System.Collections;

public class MouseListener : MonoBehaviour {

	public Camera cam;
	public LayerMask floorLayer;
	public float clickPixelsOffset = 3f;

	protected bool mousePressedOnLastFrame;
	protected bool mousePressed;
	protected bool mouseButtonDown;
	protected bool mouseButtonRelease;
	protected Vector3 mouseDownPosition;
	protected Vector3 mousePosition;
	protected Vector3 lastMousePosition;
	protected Territory sourceTerritory;
	protected Territory targetTerritory;

	void Start () {
		if(cam == null) cam = Camera.main;
		sourceTerritory = null;
		mousePressedOnLastFrame = false;
		mousePressed = false;
		Input.simulateMouseWithTouches = true;
	}

	void Update () {
		mousePosition = Input.mousePosition;
		mouseButtonDown = Input.GetMouseButtonDown(0);
		mousePressed = Input.GetMouseButton(0);
		mouseButtonRelease = Input.GetMouseButtonUp(0);
		if(mousePressed){
			Territory territory = RaycastTerritory(mousePosition);
			if(territory != targetTerritory){
				GameController.Instance.OnStopPressTerritory(targetTerritory);
				if(!mouseButtonDown && targetTerritory != null){
					GameController.Instance.OnDragTerritory(sourceTerritory,territory);
				}
			}
			if(!mouseButtonDown){
				GameController.Instance.OnKeepPressedTerritory(territory);
			}
			targetTerritory = territory;
		}
		if(mouseButtonDown){
			mouseDownPosition = mousePosition;
			sourceTerritory = targetTerritory;
			if(targetTerritory != null) GameController.Instance.OnPressTerritory(targetTerritory);

		}
		if(mouseButtonRelease){
			if(targetTerritory != null) GameController.Instance.OnReleaseTerritory(targetTerritory);
			if(targetTerritory != null) GameController.Instance.OnStopPressTerritory(targetTerritory);
			if(sourceTerritory != null && sourceTerritory == targetTerritory && Vector3.Distance(mousePosition, mouseDownPosition) <= clickPixelsOffset){
				GameController.Instance.OnClickTerritory(targetTerritory);
			}
			if(sourceTerritory != targetTerritory && sourceTerritory != null && targetTerritory != null){
				GameController.Instance.OnDragNDropTerritory(sourceTerritory,targetTerritory);
			}
			sourceTerritory = null;
			targetTerritory = null;
		}
	}

	void LateUpdate(){
		lastMousePosition = mousePosition;
	}

	private Territory RaycastTerritory(Vector3 screenPosition){
		RaycastHit raycastHit;
		if(Physics.Raycast(cam.ScreenPointToRay(screenPosition), out raycastHit,Mathf.Infinity,floorLayer)){
			Vector2 point = new Vector2(raycastHit.point.x, raycastHit.point.y);
			Collider2D hitCollider = Physics2D.OverlapPoint(point);
			if(hitCollider){
				return hitCollider.GetComponentInChildren<Territory>();
			}
		}
		return null;
	}
}
