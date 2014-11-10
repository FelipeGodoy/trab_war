using UnityEngine;
using System.Collections.Generic;

public class WaitShotStageController : StageController {

	protected int gameId;
	protected string shotsUrl;
	protected bool waitingResponse;
	protected const float tickToRequest = 1f;
	private float tickElapsed;
	private List<Shot> shotsBuffer;

	public override void OnStageStart(){
		this.waitingResponse = false;
		this.gameId = RequestController.Instance.gameId;
		this.shotsUrl = RequestController.Instance.url + "/games/shots.json";
		this.shotsBuffer = new List<Shot>();
		this.tickElapsed = 0f;
	}

	public override void ForcedUpdate (){
		if(tickElapsed >= tickToRequest && !waitingResponse && !RequestController.Instance.SendingRequest){
			GetRemoteShots();
			tickElapsed = tickElapsed % tickToRequest;
		}
		tickElapsed += Time.deltaTime;
	}

	public override void Update(){
		if(shotsBuffer.Count > 0){
			Shot shot = shotsBuffer[0];
			shotsBuffer.RemoveAt(0);
			shot.sendRequest = false;
			ComputeShot(shot);
		}
	}

	protected void GetRemoteShots(){
		waitingResponse = true;
		Request r = Request.Create(this.shotsUrl);
		r.AddParam("game_id",""+this.gameId);
		r.AddParam("shot_id",""+RequestController.Instance.shotCount);
		r.Get(OnGetShotsResponse);
	}

	protected void OnGetShotsResponse(string s){
		JSONObject json = new JSONObject(s);
		foreach(JSONObject shotJson in json.list){
			Shot shot = ShotDecoder.FromJSON(shotJson.GetField("content"));
			shotsBuffer.Add(shot);
		}
		RequestController.Instance.shotCount += json.list.Count;
		this.waitingResponse = false;
	}

}
