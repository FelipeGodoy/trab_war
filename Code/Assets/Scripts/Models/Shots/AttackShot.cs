using UnityEngine;
using System;

public class AttackShot : Shot{

	protected Territory sourceTerritory, destinationTerritory;
	protected int[] attackDices;
	protected int[] defenseDices;
		
	public AttackShot(Territory source, Territory destination, int[] attackDices, int[] defenseDices){
		this.player = source.CurrentPlayer;
		this.attackDices = attackDices;
		this.defenseDices = defenseDices;
		this.sourceTerritory = source;
		this.destinationTerritory = destination;
		Array.Sort<int>(this.attackDices);
		Array.Reverse(this.attackDices);
		Array.Sort<int>(this.defenseDices);
		Array.Reverse(this.defenseDices);
	}

	public override bool Do(){
		if(destinationTerritory.CurrentPlayer == player || 
		   !sourceTerritory.neighbors.Contains(destinationTerritory)){
			return false;
		}
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
		bool sourceRemoves = sourceTerritory.RemoveTroops(sourceTroopsDown);
		bool destRemoves = destinationTerritory.RemoveTroops(destTroopsDown);
		return true;
	}

}
