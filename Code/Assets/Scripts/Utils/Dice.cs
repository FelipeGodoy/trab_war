using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	private const float MAGNITUDE_VELOCITY_MIN = 0.01f;

	public GameObject diceDefensePrefab;
	public GameObject diceAttackPrefab;

	public delegate void DiceCallback(int[] attackDices, int[] defenseDices);

	private static Dice instance;

	private DiceSideInfo[] attackDices;
	private DiceSideInfo[] defenseDices;
	private DiceCallback callback;

	public static Dice Instance{
		get{
			if(instance == null){
				GameObject g = new GameObject("DiceController");
				instance = g.AddComponent<Dice>();
			}
			return instance;
		}
	}

	void Awake(){
		instance = this;
		this.enabled = false;
	}

	public void ClearDices(){
		DiceSideInfo[] dices = gameObject.GetComponentsInChildren<DiceSideInfo>();
		foreach(DiceSideInfo dice in dices){
			Destroy(dice.gameObject);
		}
	}

	public void CreateDices(Vector3 position, int attackCount, int defenseCount, DiceCallback callback){
		this.attackDices = new DiceSideInfo[attackCount];
		this.defenseDices = new DiceSideInfo[defenseCount];
		for(int i = 0; i < attackCount;i++){
			this.attackDices[i] = InstanciateDice(position,diceAttackPrefab);
		}
		for(int i = 0; i < defenseCount;i++){
			this.defenseDices[i] = InstanciateDice(position,diceDefensePrefab);
		}
		this.callback = callback;
		this.enabled = true;
	}

	private DiceSideInfo InstanciateDice(Vector3 position, GameObject prefab){
		GameObject g = Instantiate(prefab) as GameObject;
		g.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f,360),Random.Range(0f,360),Random.Range(0f,360f)));
		g.rigidbody.position = position;
		DiceSideInfo dice = g.GetComponent<DiceSideInfo>();
		g.transform.parent = transform;
		return dice; 
	}

	void Update(){
		foreach(DiceSideInfo dice in this.attackDices){
			if(dice.rigidbody.angularVelocity.magnitude > MAGNITUDE_VELOCITY_MIN || dice.rigidbody.velocity.magnitude > MAGNITUDE_VELOCITY_MIN) return;
		}
		foreach(DiceSideInfo dice in this.defenseDices){
			if(dice.rigidbody.angularVelocity.magnitude > MAGNITUDE_VELOCITY_MIN || dice.rigidbody.velocity.magnitude > MAGNITUDE_VELOCITY_MIN) return;
		}
		int[] attackNumbers = new int[this.attackDices.Length];
		int[] defenseNumber = new int[this.defenseDices.Length]; 
		for(int i =0; i < attackNumbers.Length; i++){
			DiceSideInfo dice = this.attackDices[i];
			attackNumbers[i] = dice.diceNumber;
		}
		for(int i =0; i < defenseNumber.Length; i++){
			DiceSideInfo dice = this.defenseDices[i];
			defenseNumber[i] = dice.diceNumber;
		}
		callback(attackNumbers,defenseNumber);
		this.enabled = false;
	}

}
