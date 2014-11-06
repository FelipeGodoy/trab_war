﻿using UnityEngine;
using System.Collections;

public abstract class Shot{
	public delegate void ShotEnd(Shot shot);
	public ShotEnd OnShotEnd;
	public bool sendRequest = true;
	public enum Type{ALLOCK, ATTACK, MOVE, REMOVE, END_STAGE, END_TURN};
	protected Player player;
	public Player Player{get{return player;}}

	public abstract bool Do();
	public abstract Type type{get;}

}
