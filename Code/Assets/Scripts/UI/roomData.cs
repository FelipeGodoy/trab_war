using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomData : MonoBehaviour {
	public Text roomNameText, userNameText;
	public string roomName{get{return roomNameText.text;}}
	public string username{get{return userNameText.text;}}
	// Use this for initialization
	
	// Update is called once per frame

	public void CreateRoom(){
		RequestController.Instance.playersInfos.Clear();
		if(roomName == null || username == null) return;
		Request r = Request.Create(RequestController.Instance.url+"/rooms.json");
		r.AddParam("game[name]",roomName);
		r.AddParam("game[n_territories]",""+42);
		r.AddParam("game[n_goals]",""+GoalFactory.GoalsCont);
		r.AddParam("game[infos]","0.1b");
		r.Post(OnCreateRoomResponse);
	}

	public void OnCreateRoomResponse(string s){
		JSONObject json = new JSONObject(s);
		int gameId = (int)json.GetField("id").n;
		int playerType = (int)Player.PlayerType.PLAYER_CHARACTER;
		RequestController.Instance.gameId = gameId;
		Request r = Request.Create(RequestController.Instance.url + "/rooms/connect.json");
		r.AddParam("player[name]",username);
		r.AddParam("player[type_id]",""+playerType);
		r.AddParam("game_id",""+gameId);
		r.Post(OnConnectRoomReponse);
	}

	public void OnConnectRoomReponse(string s){
		JSONObject json = new JSONObject(s);
		int playerId = (int)json.GetField("new_player").GetField("id").n;
		int colorId = (int)json.GetField("new_player").GetField("color").n;
		Player.PlayerType type = (Player.PlayerType)json.GetField("new_player").GetField("type_id").n;
		PlayerHold ph = new PlayerHold(username,playerId,colorId,type);
		RequestController.Instance.playersInfos.Add(ph);
		Application.LoadLevel("Lobby");
	}

}
