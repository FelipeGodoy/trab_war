using UnityEngine;
using System.Collections;

public abstract class Shot{
	protected Player player;

	public abstract bool Do();
	public virtual void Callback(){}

}
