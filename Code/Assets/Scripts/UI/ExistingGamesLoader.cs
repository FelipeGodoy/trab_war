using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExistingGamesLoader : MonoBehaviour {
	//public Request r;
	public LinkedList<ListOption> l;
	public ListOption option;
	float minY = 0.9077111f;
	float maxY = 0.9868443f;
	public GameObject pos;
	public Vector3 next;

	// Use this for initialization
	void Start () {
		l = new LinkedList<ListOption>();
		next = pos.transform.position;
		/*Request r = Request.Create(url);
		//Pega lista de jogos ativos com o servidor
		r.SetFields("a","1","b","2");
		r.Get(Callback);*/
	}
	
	// Update is called once per frame
	public void addOptions () {
		//Separa em grupos
		int tamanhoGrupo = 12;
		int qtd = l.Count;
		if (qtd >= 12) {
						return;
				}
		float diff = (maxY - minY);
		//for(int i = 0; i < tamanhoGrupo; i++){
			ListOption o = (ListOption)Instantiate (option);
			o.transform.position = pos.transform.position;
			next.y += (maxY - minY);
			o.transform.parent = this.transform;
			l.AddLast(o);
			RectTransform r = o.GetComponent<RectTransform>();
			RectTransform model = option.GetComponent<RectTransform>();
			print(r.pivot);
			r.sizeDelta = model.sizeDelta;
			
			Vector2 min = new Vector2 ();
			min = model.anchorMin;
			min.y = minY- qtd*diff;
			r.anchorMin = min;
			
			Vector2 max = new Vector2 ();
			max = model.anchorMax;
			max.y = maxY - qtd*diff;
			r.anchorMax = max;
	}
}
