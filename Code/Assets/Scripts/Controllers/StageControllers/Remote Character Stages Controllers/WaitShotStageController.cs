using UnityEngine;
using System.Collections.Generic;

public class WaitShotStageController : StageController {

	protected int gameId;
	protected string shotsUrl;
	protected bool waitingResponse;
	protected const float tickToRequest = 1f;
	private float tickElapsed;
	private List<Shot> ShotsBuffer{
		get{
			return RequestController.Instance.shotsReceived;
		}
	}

//	public override void OnGUI(){
//		GUI.Label(new Rect(50,50,50,50),""+RequestController.Instance.shotCount);
//	}

	public override void OnStageStart(){
		this.waitingResponse = false;
		this.gameId = RequestController.Instance.gameId;
		this.shotsUrl = RequestController.Instance.url + "/games/shots.json";
		this.tickElapsed = 0f;
	}

	public override void ForcedUpdate (){
		if(tickElapsed >= tickToRequest && !waitingResponse && RequestController.Instance.shotsToSend.Count == 0){
			GetRemoteShots();
			tickElapsed = tickElapsed % tickToRequest;
		}
		tickElapsed += Time.deltaTime;
	}

	public override void Update(){
		if(ShotsBuffer.Count > 0){
			Shot shot = ShotsBuffer[0];
			ShotsBuffer.RemoveAt(0);
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

	protected void OnGetShotsResponse(WWW www){
		JSONObject json = new JSONObject(www.text);
		if(www.error == null && json != null){
			int n = 0;
			json.list.Sort(delegate(JSONObject x, JSONObject y) {
				if(!x.HasField("index_in_game"))return -1;
				if(!y.HasField("index_in_game"))return 1;
				return x.GetField("index_in_game").n.CompareTo(y.GetField("index_in_game").n);
			});
			foreach(JSONObject shotJson in json.list){
				Shot shot = ShotDecoder.FromJSON(shotJson.GetField("content"));
				if(shot != null && (RequestController.Instance.shotCount + n +1) == (int)shotJson.GetField("index_in_game").n){
					ShotsBuffer.Add(shot);
					n++;
				}
			}
			RequestController.Instance.shotCount += n;
		}
		this.waitingResponse = false;
	}

	public override void OnStageEnd ()
	{
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

}
