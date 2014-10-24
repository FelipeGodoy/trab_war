using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

	public delegate void DiceCallback(int[] attackDices, int[] defenseDices);

	private static Dice instance;

	private DiceSideInfo[] attackDices;
	private DiceSideInfo[] defenseDices;

	public static Dice Instance{
		get{
			if(instance == null){
				GameObject g = new GameObject("DiceController");
				instance = g.AddComponent<Dice>();
			}
			return instance;
		}
	}

	public void CreateDices(Vector3 position, int attackDices, int defenseDices, DiceCallback callback){
		
	}

}
