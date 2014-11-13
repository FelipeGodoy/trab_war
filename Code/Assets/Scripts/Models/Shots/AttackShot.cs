using UnityEngine;
using System;

public class AttackShot : Shot{
	public delegate void AttackShotCallback(bool conquested);

	protected Territory sourceTerritory, destinationTerritory;
	protected int[] attackDices;
	protected int[] defenseDices;
	protected AttackShotCallback callback;

	public Territory SourceTerritory{get{return sourceTerritory;}}
	public Territory TargetTerritory{get{return destinationTerritory;}}
	public int[] AttackDices{get{return attackDices;}}
	public int[] DefenseDices{get{return defenseDices;}}

	public override Type type{
		get{
			return Type.ATTACK;
		}
	}
		
	public AttackShot(Player player, Territory source, Territory destination, AttackShotCallback callback){
		this.player = player;
		this.sourceTerritory = source;
		this.destinationTerritory = destination;
		this.attackDices = null;
		this.defenseDices = null;
		this.callback = callback;
	}

	public AttackShot(Player player, Territory source, Territory destination,int[] attackDices, int[] defenseDices, AttackShotCallback callback){
		this.player = player;
		this.sourceTerritory = source;
		this.destinationTerritory = destination;
		this.attackDices = attackDices;
		this.defenseDices = defenseDices;
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
//		gui = (GameObject.Find("GUIFacade")).GetComponent<GUIFacade>();
		gui.left.setActive (true);
		gui.left.setTexts (player.name, sourceTerritory.name,""+ sourceTerritory.TroopsCount);
		gui.left.changeColor (player.displayColor);

		gui.right.setActive (true);
		gui.right.setTexts (destinationTerritory.CurrentPlayer.name, destinationTerritory.name, "" + destinationTerritory.TroopsCount);
		gui.right.changeColor (destinationTerritory.CurrentPlayer.displayColor);
		if(this.attackDices!= null && this.defenseDices != null){
			Dice.Instance.CreateDices(Vector3.Lerp(sourceTerritory.transform.position,destinationTerritory.transform.position,0.5f) + new Vector3(0,0,-10f),
			                          this.attackDices,
			                          this.defenseDices,
			                          DiceAnimationFinished);
			GameController.Instance.Pause();
		}
		else{
			Dice.Instance.CreateDices(Vector3.Lerp(sourceTerritory.transform.position,destinationTerritory.transform.position,0.5f) + new Vector3(0,0,-10f),
			                          Mathf.Min(sourceTerritory.TroopsCount - 1,3),
			                          Mathf.Min(destinationTerritory.TroopsCount,3),
			                          DiceAnimationFinished);
			GameController.Instance.Pause();
		}
	
		return true;
	}

	private void DiceAnimationFinished(int[] attackNumbers, int[] defenseNumbers){
		if(this.attackDices == null || this.defenseDices == null){
			this.attackDices = attackNumbers;
			this.defenseDices = defenseNumbers;
		}
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
		if(callback != null) callback(conquested);
		Dice.Instance.ClearDices();
		GameController.Instance.Resume();
		GameController.Instance.OnShotEnd(this);
	}

	private void SetupDicesNumbers(){
		Array.Sort<int>(this.attackDices);
		Array.Reverse(this.attackDices);
		Array.Sort<int>(this.defenseDices);
		Array.Reverse(this.defenseDices);
	}

}
