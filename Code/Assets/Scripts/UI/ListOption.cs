using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListOption : MonoBehaviour {
	public static int sala_id_selected = -1;
	public int sala_id;
	public Color selectedColor;
	Color normalColor;
	void Start(){
		normalColor = GetComponent<Image> ().color;
	}

	public void OnClick(){
		ListOption[] l = transform.parent.GetComponentsInChildren<ListOption> ();
		foreach (ListOption i in l) {
			if (i.sala_id == sala_id_selected)
				i.GetComponent<Image> ().color = normalColor;
		}
		GetComponent<Image> ().color = selectedColor;
		sala_id_selected = sala_id;
	}

}
