using UnityEngine;
using System.Collections;

public class EmptyPanel : MonoBehaviour {
	public IaPanel iaPanel;
	public PlayerPanel playerPanel;
	// Use this for initialization

	public void setIA(){
		IaPanel p = (IaPanel)Instantiate (iaPanel);
		p.transform.position = transform.position;	//Deveria ser Rect Transform! To sem internet pra resolver isso.
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);
	}

	public void setPlayer(){
		PlayerPanel p = (PlayerPanel)Instantiate (playerPanel);
		p.transform.position = transform.position;	//Deveria ser Rect Transform! To sem internet pra resolver isso.
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);;
	}

}
