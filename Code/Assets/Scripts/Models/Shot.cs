using UnityEngine;
using System.Collections;

public abstract class Shot{
	public delegate void ShotEnd(Shot shot);
	public ShotEnd OnShotEnd;
	public bool sendRequest = false;
	public enum Type{ALLOCK, ATTACK, MOVE, REMOVE, PASS_TURN};
	protected Player player;
	public Player Player{get{return player;}}

	public abstract bool Do();
	public abstract Type type{get;}

}
