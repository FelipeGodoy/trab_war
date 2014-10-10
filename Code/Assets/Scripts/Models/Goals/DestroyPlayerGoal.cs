using UnityEngine;
using System.Collections;

public class DestroyPlayerGoal : Goal {

	private Player playerToDestroy;

	public DestroyPlayerGoal(Player player){
		this.playerToDestroy = player;
	}

	public override bool Check(Game game, Player player){
		return this.playerToDestroy.TerritoriesCount == 0;
	}
}
