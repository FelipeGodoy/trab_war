using UnityEngine;
using System.Collections;

public abstract class Goal{

	public abstract bool Check(GameController game, Player player);

	public abstract string Description{get;}

}
