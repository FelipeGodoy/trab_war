﻿using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	private const float MAGNITUDE_VELOCITY_MIN = 0.1f;

	public GameObject diceDefensePrefab;
	public GameObject diceAttackPrefab;

	public delegate void DiceCallback(int[] attackDices, int[] defenseDices);

	private static Dice instance;

	private DiceSideInfo[] attackDices;
	private DiceSideInfo[] defenseDices;
	private DiceCallback callback;
	private float creationTime = 0f;
	private float maxElapsedTime = 10f;

	public static Dice Instance{
		get{
			if(instance == null){
				GameObject g = new GameObject("DiceController");
				instance = g.AddComponent<Dice>();
			}
			return instance;
		}
	}

	public bool CheckElapsedTime(){
		return Time.time - creationTime >= maxElapsedTime;
	}

	void ResetElapsedDiceTime(){
		creationTime = Time.time;
	}

	void Start(){
		ResetElapsedDiceTime ();
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

	public void CreateDices(Vector3 position, int[] attackNumbers, int[] defenseNumbers, DiceCallback callback){
		for(int i = 0; i < attackNumbers.Length; i++){
			attackNumbers[i] = Mathf.Clamp(attackNumbers[i],1,6);
		}
		for(int i =0; i < defenseNumbers.Length;i++){
			defenseNumbers[i] = Mathf.Clamp(defenseNumbers[i],1,6);
		}
		this.attackDices = new DiceSideInfo[attackNumbers.Length];
		this.defenseDices = new DiceSideInfo[defenseNumbers.Length];
		for(int i = 0; i < attackNumbers.Length;i++){
			this.attackDices[i] = InstanciateDice(position,diceAttackPrefab);
			this.attackDices[i].forcedNumber = attackNumbers[i];
		}
		for(int i = 0; i < defenseNumbers.Length;i++){
			this.defenseDices[i] = InstanciateDice(position,diceDefensePrefab);
			this.defenseDices[i].forcedNumber = defenseNumbers[i];
		}
		this.callback = callback;
		this.enabled = true;
		ResetElapsedDiceTime ();
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
		ResetElapsedDiceTime ();
	}

	private DiceSideInfo InstanciateDice(Vector3 position, GameObject prefab){
		GameObject g = Instantiate(prefab) as GameObject;
		g.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0f,360),Random.Range(0f,360),Random.Range(0f,360f)));
		g.GetComponent<Rigidbody>().position = position;
		DiceSideInfo dice = g.GetComponent<DiceSideInfo>();
		g.transform.parent = transform;
		return dice; 
	}

	void Update(){
		foreach(DiceSideInfo dice in this.attackDices){
			if((dice.GetComponent<Rigidbody>().angularVelocity.magnitude > MAGNITUDE_VELOCITY_MIN ||
			   dice.GetComponent<Rigidbody>().velocity.magnitude > MAGNITUDE_VELOCITY_MIN ||
				(Mathf.Clamp(dice.forcedNumber,1,6) == dice.forcedNumber && dice.forcedNumber != dice.diceNumber)) && !CheckElapsedTime()) return;
		}
		foreach(DiceSideInfo dice in this.defenseDices){
			if((dice.GetComponent<Rigidbody>().angularVelocity.magnitude > MAGNITUDE_VELOCITY_MIN ||
			   dice.GetComponent<Rigidbody>().velocity.magnitude > MAGNITUDE_VELOCITY_MIN ||
				(Mathf.Clamp(dice.forcedNumber,1,6) == dice.forcedNumber && dice.forcedNumber != dice.diceNumber)) && !CheckElapsedTime()) return;
		}
		int[] attackNumbers = new int[this.attackDices.Length];
		int[] defenseNumber = new int[this.defenseDices.Length]; 
		for(int i =0; i < attackNumbers.Length; i++){
			DiceSideInfo dice = this.attackDices[i];
			attackNumbers[i] = dice.diceNumber;
			if (Mathf.Clamp (dice.forcedNumber, 1, 6) == dice.forcedNumber) {
				attackNumbers [i] = dice.forcedNumber;
			}
		}
		for(int i =0; i < defenseNumber.Length; i++){
			DiceSideInfo dice = this.defenseDices[i];
			defenseNumber[i] = dice.diceNumber;
			if (Mathf.Clamp (dice.forcedNumber, 1, 6) == dice.forcedNumber) {
				defenseNumber [i] = dice.forcedNumber;
			}
		}
		callback(attackNumbers,defenseNumber);
		this.enabled = false;
	}

}
