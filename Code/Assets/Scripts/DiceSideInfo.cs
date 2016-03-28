using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class DiceSideInfo : MonoBehaviour {

	public delegate void Delegate();

	private const float MIN_SPEED_TO_CHANGE = 0.3f;
	private const float MIN_SPEED = 0.1f;
	private const float MIN_SPEED_TO_CHEATING = 17f;

	public int diceNumber = -1;
	public int forcedNumber = -1;
	public float verticalInpulse = 1f;
	public float impulse = 3f;
	[SerializeField]
	protected Vector3 angularVelocity;
	public GameObject cheatObject;
	[SerializeField]
	protected bool collided = false;
	protected int dirFlag = 0;
	protected float creationTime = 0f;

	public float magnitude;

	protected Rigidbody rig;

	public Vector3 relativeUp{
		get{
			return Physics.gravity.normalized;
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.GetComponentInParent<DiceSideInfo> () == null || Time.time - creationTime >= 1f) {
			collided = true;
		}
	}

	void Start(){
		rig = GetComponent<Rigidbody> ();
		creationTime = Time.time;
	}

	void Update () {
		diceNumber = DiceCount();
		magnitude = rig.angularVelocity.magnitude;
	}

	void FixedUpdate(){
		if(forcedNumber == Mathf.Clamp(forcedNumber,1,6)){
			CheckCheating ();
			CheckImpulse ();
		}
	}

	void CheckCheating(){
		angularVelocity = rig.angularVelocity;
		if (collided && cheatObject != null && angularVelocity.magnitude <= MIN_SPEED_TO_CHEATING) {
			cheatObject.transform.localEulerAngles = DiceCountToAnglesEuler (forcedNumber);
			cheatObject.SetActive (true);
		}
	}

	void CheckImpulse(){
		angularVelocity = rig.angularVelocity;
		if(angularVelocity.magnitude <= MIN_SPEED_TO_CHANGE && diceNumber != forcedNumber){
			dirFlag = Random.Range (0, 3);
			Vector3 f = -(Physics.gravity * verticalInpulse);
			rig.AddForce (f, ForceMode.Impulse);
			switch(dirFlag){
			case 0:{
					rig.AddTorque(new Vector3(impulse,0f,0f),ForceMode.Impulse);
					dirFlag = 1;
					break;
				}
			case 1:{
					rig.AddTorque(new Vector3(0f,impulse,0f),ForceMode.Impulse);
					dirFlag = 2;
					break;
				}
			case 2:{
				rig.AddTorque(new Vector3(-impulse,0f,0f),ForceMode.Impulse);
				dirFlag = 3;
				break;
			}
			case 3:{
				rig.AddTorque(new Vector3(0f,-impulse,0f),ForceMode.Impulse);
				dirFlag = 0;
				break;
			}
			}
		}
	}

	public int DiceCount(){
		if (Vector3.Dot (transform.forward, relativeUp) >= 0.643f)
			return 2;
		if (Vector3.Dot (-transform.forward, relativeUp) >= 0.643f)
			return 5;
		if (Vector3.Dot (transform.up, relativeUp) >= 0.643f)
			return 6;
		if (Vector3.Dot (-transform.up, relativeUp) >= 0.643f)
			return 1;
		if (Vector3.Dot (transform.right, relativeUp) >= 0.643f)
			return 4;
		if (Vector3.Dot (-transform.right, relativeUp) >= 0.643f)
			return 3;
		return -1;
	}

	public static Vector3 DiceCountToAnglesEuler(int diceCount){
		if (diceCount == 2)
			return Vector3.zero;
		if (diceCount == 5)
			return new Vector3(0f,180f,180f);
		if (diceCount == 6)
			return new Vector3(-90f,0f,0f);
		if (diceCount == 1)
			return new Vector3(90f,0f,0f);
		if (diceCount == 4)
			return new Vector3(0f,90f,0f);
		if (diceCount == 3)
			return new Vector3(0f,-90f,0f);
		return Vector3.zero;
	}

	public static Vector3 DiceCountToAxis(int diceCount){
		if (diceCount == 2)
			return Vector3.forward;
		if (diceCount == 5)
			return -Vector3.forward;
		if (diceCount == 6)
			return Vector3.up;
		if (diceCount == 1)
			return -Vector3.up;
		if (diceCount == 4)
			return Vector3.right;
		if (diceCount == 3)
			return -Vector3.right;
		return Vector3.zero;
	}
}
