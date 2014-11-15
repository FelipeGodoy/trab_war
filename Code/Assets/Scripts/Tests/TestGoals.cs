using UnityEngine;
using System.Collections;

public class TestGoals : MonoBehaviour {

	void Start () {
		for(int i =0; i < GoalFactory.GoalsCont;i++){	
			Goal goal = GoalFactory.Create(i);
			Debug.Log(goal.Description);
		}
	}
}
