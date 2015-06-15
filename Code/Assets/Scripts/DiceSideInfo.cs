using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class DiceSideInfo : MonoBehaviour {

	public delegate void Delegate();

	private const float MIN_SPEED_TO_CHANGE = 0.3f;
	private const float MIN_SPEED = 0.1f;

	public int diceNumber = -1;
	public int forcedNumber = -1;
	public float impulse = 10f;

	protected Vector3 angularVelocity;
	protected Vector3 lastAngularVelocity = new Vector3(0f,0f,0f);
	protected int dirFlag = 0;

	private readonly Vector3 defaultAngle = new Vector3(90f,0f,0f);

	void Update () {
		diceNumber = DiceSideByPerspective(transform.rotation);
	}

	void FixedUpdate(){
		if(forcedNumber == Mathf.Clamp(forcedNumber,1,6)){
			angularVelocity = GetComponent<Rigidbody>().angularVelocity;
			if(angularVelocity.magnitude <= MIN_SPEED_TO_CHANGE && diceNumber != forcedNumber){
				switch(dirFlag){
				case 0:{
					GetComponent<Rigidbody>().AddTorque(new Vector3(impulse,0f,0f),ForceMode.Impulse);
					dirFlag = 1;
					break;
				}
				case 1:{
					GetComponent<Rigidbody>().AddTorque(new Vector3(0f,impulse,0f),ForceMode.Impulse);
					dirFlag = 0;
					break;
				}
				}
			}
		}
	}

	public int DiceSideByPerspective(Quaternion angle){
		return DiceSide(Quaternion.Inverse(Quaternion.Euler(Camera.main.transform.rotation.eulerAngles - defaultAngle)) * angle);
	}

	public int DiceSide(Quaternion angle){
		Vector3 eulerAngles = angle.eulerAngles;
		eulerAngles.x = Mathf.Round(eulerAngles.x / 90f) % 4f;
		eulerAngles.y = Mathf.Round(eulerAngles.y / 90f) % 4f;
		eulerAngles.z = Mathf.Round(eulerAngles.z / 90f) % 4f;
		if(eulerAngles.x == 1f)
			return 2;
		if(eulerAngles.x == 3f)
			return 5;
		if(eulerAngles.x ==0f){
			if(eulerAngles.z == 0f)
				return 1;
			if(eulerAngles.z == 1f)
				return 3;
			if(eulerAngles.z == 2f)
				return 6;
			else
				return 4;
		}
		if(eulerAngles.x == 2f){
			if(eulerAngles.z == 0f)
				return 6;
			if(eulerAngles.z == 1f)
				return 4;
			if(eulerAngles.z == 2f)
				return 1;
			else
				return 3;
		}
		return -1;
	}
}
