using UnityEngine;
using System.Collections;

public abstract class Shot{
	public delegate void ShotEnd(Shot shot);
	public ShotEnd OnShotEnd;
	public bool _sendRequest = true;
	public bool sendRequest{
		get{
#if LOCAL_MODE
			return false;
#else
			return _sendRequest;
#endif
		}
		set{
			_sendRequest = value;
		}
	}
	public enum Type{ALLOCK, ATTACK, MOVE, REMOVE, PASS_TURN};
	protected Player player;
	public Player Player{get{return player;}}
	public GUIFacade gui;
	public abstract bool Do();
	public abstract Type type{get;}

}
