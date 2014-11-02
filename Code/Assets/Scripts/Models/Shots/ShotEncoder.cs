using UnityEngine;
using System.Collections;

public class ShotEncoder{

	public const string SHOT_TYPE = "shotType";
	public const string PLAYER_INDEX = "playerIndex";
	public const string TARGET_INDEX = "targetTerritoryIndex";
	public const string SOURCE_INDEX = "sourceTerritoryIndex";
	public const string TROOPS_COUNT = "troopsCount";
	public const string ATTACK_DICES = "attackDices";
	public const string DEFENSE_DICES = "defenseDices";

	public static JSONObject ToJSON(Shot shot){
		JSONObject json = new JSONObject();
		switch(shot.type){
		case(Shot.Type.ALLOCK): {
			AllockTroopShot allockShot = (AllockTroopShot) shot;
			json.AddField(TARGET_INDEX,allockShot.Territory.index);
			json.AddField(TROOPS_COUNT, allockShot.TroopsCount);
			break;
		}
		case(Shot.Type.ATTACK):{
			AttackShot attackShot = (AttackShot) shot;
			json.AddField(SOURCE_INDEX,attackShot.SourceTerritory.index);
			json.AddField(TARGET_INDEX,attackShot.TargetTerritory.index);
			JSONObject arrAttack = ArrayToJSON(attackShot.AttackDices);
			JSONObject arrDefense = ArrayToJSON(attackShot.DefenseDices);
			json.AddField(ATTACK_DICES,arrAttack);
			json.AddField(DEFENSE_DICES,arrDefense);
			break;
		}
		case(Shot.Type.MOVE):{
			MoveShot moveShot = (MoveShot) shot;
			json.AddField(SOURCE_INDEX,moveShot.SourceTerritory.index);
			json.AddField(TARGET_INDEX,moveShot.TargetTerritory.index);
			json.AddField(TROOPS_COUNT,moveShot.TroopsCount);
			break;
		}
		case(Shot.Type.REMOVE):{
			RemoveTroopShot removeShot = (RemoveTroopShot) shot;
			json.AddField(TARGET_INDEX, removeShot.Territory.index);
			json.AddField(TROOPS_COUNT, removeShot.TroopsCount);
			break;
		}
		default: return null;
		}
		json.AddField(SHOT_TYPE, (int)shot.type);
		json.AddField(PLAYER_INDEX, shot.Player.index);
		return json;
	}

	public static JSONObject ArrayToJSON(int[] array){
		JSONObject json = new JSONObject(JSONObject.Type.ARRAY);
		foreach(int i in array){json.Add(i);}
		return json;
	}
}
