using UnityEngine;
using System.Collections;

public class PlayerPanel : MonoBehaviour, IPanel {
	public EmptyPanel emptyPanel;

	public void setEmpty(){
		EmptyPanel p = (EmptyPanel)Instantiate (emptyPanel);
		p.transform.position = transform.position;
		RectTransform antigo, novo;
		novo = p.GetComponent<RectTransform> ();
		antigo = GetComponent<RectTransform> ();
		novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
		p.transform.parent = this.transform.parent.transform;
		Destroy (this.gameObject);
	}
}
