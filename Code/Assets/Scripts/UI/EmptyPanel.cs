using UnityEngine;
using System.Collections;

public class EmptyPanel : MonoBehaviour, IPanel {
	public IaPanel iaPanel;
	public PlayerPanel playerPanel;
	// Use this for initialization

	public void setIA(){
		IaPanel p = (IaPanel)Instantiate (iaPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);
	}

	public void setPlayer(){
		PlayerPanel p = (PlayerPanel)Instantiate (playerPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);;
	}

}
