using UnityEngine;
using System.Collections;

public abstract class StageController{
	public TurnController turnController;

	public StageController(){
		this.turnController = null;

	}

	public StageController(TurnController turnController){
		this.turnController = turnController;
		OnStageStart();
	}

	private Player _player;

	protected Player Player{
		get{
			if(_player == null){
				_player = GameController.Instance.CurrentPlayer;
			}
			return _player;
		}
	}

	protected virtual bool ComputeShot(Shot shot){
		return GameController.Instance.ComputeShot(shot);
	}

	public virtual void OnGUI(){}
	public virtual void Update(){}
	public virtual void OnStageStart(){}
	public virtual void OnStageEnd(){}
	public virtual void OnClickTerritory(Territory territory){}
	public virtual void OnPressTerritory(Territory territory){}
	public virtual void OnKeepPressedTerritory(Territory territory){}
	public virtual void OnStopPressTerritory(Territory territory){}
	public virtual void OnReleaseTerritory(Territory territory){}
	public virtual void OnDragTerritory(Territory source, Territory target){}
	public virtual void OnDragNDropTerritory(Territory source, Territory target){}

	public void EndStage(){
		OnStageEnd();
		turnController.NextStage();
	}

}
