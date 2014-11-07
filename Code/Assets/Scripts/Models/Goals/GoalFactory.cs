using UnityEngine;
using System.Collections;

public class GoalFactory{

	public static Goal Create(int id){
		if(id >=0 && id <= 5){
			return new DestroyPlayerGoal(GameController.Instance.playersModels[id]);	
		}
		switch(id){
		case 6:
			return new TerritoriesGoal(24);
			break;
		case 7:
			return new TerritoriesGoal(18);
			break;
		default:{
			Debug.LogError("Invalid ID to Goal Factory!");
			return null;
		}
		}
	}

	public static int GoalsCont{
		get{
			return 8;
		}
	}

}
