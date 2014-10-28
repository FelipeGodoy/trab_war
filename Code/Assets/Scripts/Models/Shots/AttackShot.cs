using UnityEngine;
using System;

public class AttackShot : Shot{
	public delegate void AttackShotCallback(bool conquested);

	protected Territory sourceTerritory, destinationTerritory;
	protected int[] attackDices;
	protected int[] defenseDices;
	protected AttackShotCallback callback;
		
	public AttackShot(Player player, Territory source, Territory destination, AttackShotCallback callback){
		this.player = player;
		this.sourceTerritory = source;
		this.destinationTerritory = destination;
		this.callback = callback;
	}

	public override bool Do(){
		if(destinationTerritory.CurrentPlayer == player || 
		   !sourceTerritory.neighbors.Contains(destinationTerritory) ||
		   sourceTerritory.CurrentPlayer != player ||
		   sourceTerritory.TroopsCount == 1){
			Debug.Log("nao pode atacar");
			return false; 
		}
		Dice.Instance.CreateDices(Vector3.Lerp(sourceTerritory.transform.position,destinationTerritory.transform.position,0.5f) + new Vector3(0,0,-10f),
		                          Mathf.Min(sourceTerritory.TroopsCount - 1,3),
		                          Mathf.Min(destinationTerritory.TroopsCount,3),
		                          DiceAnimationFinished);
		GameController.Instance.Pause();
		return true;
	}

	private void DiceAnimationFinished(int[] attackNumbers, int[] defenseNumbers){
		this.attackDices = attackNumbers;
		this.defenseDices = defenseNumbers;
		SetupDicesNumbers();
		int sourceTroopsDown =0;
		int destTroopsDown = 0;
		for(int i =0; i < Mathf.Min(this.attackDices.Length, this.defenseDices.Length);i++){
			if(this.attackDices[i] > this.defenseDices[i]){
				destTroopsDown++;
			}
			else{
				sourceTroopsDown++;
			}
		}
//		bool sourceRemoves = sourceTerritory.RemoveTroops(sourceTroopsDown);
		bool destRemoves = destinationTerritory.RemoveTroops(destTroopsDown);
		bool conquested = destinationTerritory.TroopsCount <= 0;
		if(conquested){
			destinationTerritory.CurrentPlayer = this.player;
			destinationTerritory.AddTroops(attackNumbers.Length - sourceTroopsDown);
			sourceTerritory.RemoveTroops(attackNumbers.Length);
		}
		else{
			sourceTerritory.RemoveTroops(sourceTroopsDown);
		}
		callback(conquested);
		GameController.Instance.Resume();
	}

	private void SetupDicesNumbers(){
		Array.Sort<int>(this.attackDices);
		Array.Reverse(this.attackDices);
		Array.Sort<int>(this.defenseDices);
		Array.Reverse(this.defenseDices);
	}

}
