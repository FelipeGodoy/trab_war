using UnityEngine;
using System.Collections;

public class DestroyPlayerGoal : Goal {

	private Player playerToDestroy;

	public DestroyPlayerGoal(Player player){
		this.playerToDestroy = player;
	}

	public override bool Check(GameController game, Player player){
		return this.playerToDestroy.TerritoriesCount == 0;
	}

	public override string Description{
		get{
			return "Destrua as tropas do jogador "+ this.playerToDestroy.name;
		}
	}
}
