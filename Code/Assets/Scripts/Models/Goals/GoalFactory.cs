using UnityEngine;
using System.Collections;

public class GoalFactory{

	const string AMERICA_DO_NORTE = "norte";
	const string AMERICA_DO_SUL = "sul";
	const string OCEANIA = "oceania";
	const string ASIA = "asia";
	const string AFRICA = "frica";
	const string EUROPA = "europa";

	public static Goal Create(int id){
		if(id >=0 && id <= 5){
			return new DestroyPlayerGoal(GameController.Instance.playersModels[id]);	
		}
		Map map = GameController.Instance.currentMap;
		switch(id){
		case 6:
			return new TerritoriesGoal(24);
			break;
		case 7:
			return new TerritoriesGoal(18);
			break;
		case 8:
			return new ContinentsGoal(map.ContinentsByNames(EUROPA, OCEANIA),1);
			break;
		case 9:
			return new ContinentsGoal(map.ContinentsByNames(ASIA, AMERICA_DO_SUL));
			break;
		case 10:
			return new ContinentsGoal(map.ContinentsByNames(EUROPA, AMERICA_DO_SUL),1);
			break;
		case 11:
			return new ContinentsGoal(map.ContinentsByNames(ASIA, AFRICA));
			break;
		case 12:
			return new ContinentsGoal(map.ContinentsByNames(AMERICA_DO_NORTE, AFRICA));
			break;
		case 13:
			return new ContinentsGoal(map.ContinentsByNames(AMERICA_DO_NORTE, OCEANIA));
			break;
		case 14:
			return new ContinentsGoal(map.ContinentsByNames(AMERICA_DO_SUL),2);
			break;
		case 15:
			return new ContinentsGoal(map.ContinentsByNames(OCEANIA),2);
			break;
		default:{
			Debug.LogError("Invalid ID to Goal Factory!");
			return null;
		}
		}
	}

	public static int GoalsCont{
		get{
			return 16;
		}
	}

}
