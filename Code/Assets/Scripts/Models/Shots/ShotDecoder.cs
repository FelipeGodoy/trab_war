using UnityEngine;
using System.Collections;

public class ShotDecoder{

	public static Shot FromJSON(JSONObject json){
		if(json == null || !json.HasField(ShotEncoder.SHOT_TYPE))return null;
		Shot.Type shotType = (Shot.Type)((int)json.GetField(ShotEncoder.SHOT_TYPE).n);
		Player player = GameController.Instance.playersModels[(int)json.GetField(ShotEncoder.PLAYER_INDEX).n];
		switch(shotType){
		case(Shot.Type.ALLOCK): {
			Territory target = JSONToTerritory(json.GetField(ShotEncoder.TARGET_INDEX));
			int troopsCount = (int)json.GetField(ShotEncoder.TROOPS_COUNT).n;
			return new AllockTroopShot(player,target,troopsCount);
		}
		case(Shot.Type.ATTACK):{
			int[] attackDices = JSONToIntArray(json.GetField(ShotEncoder.ATTACK_DICES));
			int[] defenseDices = JSONToIntArray(json.GetField(ShotEncoder.DEFENSE_DICES));
			Territory target = JSONToTerritory(json.GetField(ShotEncoder.TARGET_INDEX));
			Territory source = JSONToTerritory(json.GetField(ShotEncoder.SOURCE_INDEX));
			return new AttackShot(player,source, target, attackDices, defenseDices, null);
			break;
		}
		case(Shot.Type.MOVE):{
			Territory target = JSONToTerritory(json.GetField(ShotEncoder.TARGET_INDEX));
			Territory source = JSONToTerritory(json.GetField(ShotEncoder.SOURCE_INDEX));
			int troopsCount = (int)json.GetField(ShotEncoder.TROOPS_COUNT).n;
			return new MoveShot(player,source,target,troopsCount);
		}
		case(Shot.Type.REMOVE):{
			Territory target = JSONToTerritory(json.GetField(ShotEncoder.TARGET_INDEX));
			int troopsCount = (int)json.GetField(ShotEncoder.TROOPS_COUNT).n;
			return new RemoveTroopShot(player,target, troopsCount);
		}
		case(Shot.Type.PASS_TURN):{
			return new PassTurnShot(player);
		}
		default: return null;
		}
	}

	public static int[] JSONToIntArray(JSONObject json){
		int[] array = new int[json.list.Count];
		for(int i =0; i < json.list.Count; i++){
			array[i] = (int)json.list[i].n;
		}
		return array;
	}

	public static Territory JSONToTerritory(JSONObject json){
		return GameController.Instance.currentMap.territories[(int)json.n];
	}

}
