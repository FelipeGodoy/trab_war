using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour {
	public Panel	 emptyPanel;
	public Panel playerPanel;
	public Panel iaPanel;

	public void setName(string s){
		Text t = GetComponentInChildren<Text> ();
		if(t != null)
			t.text = s;
	}

	public void setEmpty(){
		Panel p = (Panel)Instantiate (emptyPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);
	}
	
	public void setPlayer(string s){
		Panel p = (Panel)Instantiate (playerPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		p.setName (s);
		Destroy (this.gameObject);;
	}

	public void setIA(string s){
		Panel p = (Panel)Instantiate (iaPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		p.setName (s);
		Destroy (this.gameObject);
	}
}
