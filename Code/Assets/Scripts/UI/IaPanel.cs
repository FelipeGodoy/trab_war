using UnityEngine;
using System.Collections;

public class IaPanel : MonoBehaviour {
	public EmptyPanel emptyPanel;

	public void setEmpty(){
		EmptyPanel p = (EmptyPanel)Instantiate (emptyPanel);
		p.transform.position = transform.position;	//Deveria ser Rect Transform! To sem internet pra resolver isso.
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);
	}
}
