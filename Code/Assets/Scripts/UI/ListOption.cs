using UnityEngine;
using System.Collections;

public class ListOption : MonoBehaviour {
	public static int sala_id_selected = -1;
	public int sala_id;


	public void OnClick(){
		sala_id_selected = sala_id;
	}

}
