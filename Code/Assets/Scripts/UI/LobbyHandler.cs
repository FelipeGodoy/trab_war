using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyHandler : MonoBehaviour {
	public string url;
	public GameObject[] slots;
	public Request r;
	public IPanel p;
	public GameObject EmptyPrefab, LocalPlayerPrefab, RemotePlayerPrefab, IAPrefab;

	private bool sendingRequest;

	void Start () {
		sendingRequest = false;
		/*Request r = Request.Create(url);
		//Enviar mensagem com nome da sala e senha
		//Enviar dados dos players controlados por este
		r.SetFields("a","1","b","2");
		r.Get(Callback);*/
	}

	void onChange(){
		//Enviar dados dos players controlados por este para o servidor
		string sendingData = "";
		GameObject g;
		for (int i = 0; i < 6; i++) {
			g = slots[i];
			if(g.tag == "LocalPlayer"){
				//fgsfds
			}
			else if( g.tag == "IA"){
				//fgsfds
			}
			//posiçao i
		}
		r.SetFields ("lobbyId", sendingData);
		//r.Post (update);
	}
	void Update(){
		if(!sendingRequest){
			Request r = Request.Create(RequestController.Instance.url+"/games/"+RequestController.Instance.gameId+".json");
			r.Get(OnGameRequestResponse);
			sendingRequest = true;
		}
	}

	public void OnGameRequestResponse(string s){
		sendingRequest = false;
		JSONObject json = new JSONObject(s);
	}

	void updateView(string data){
		GameObject g;
		for (int i = 0; i < 6; i++) {
			g = slots[i].transform.GetChild(0).gameObject;
			g = g.gameObject;
			string tag = "";
			string nome = "";
			if(g.tag == tag){
				if(g.tag == "RemotePlayer"){
					Text t = g.GetComponentInChildren<Text>();
					t.text = nome;
				}
				else if( g.tag == "IA"){
					InputField name = g.GetComponentInChildren<InputField>();
					name.value = nome;
					//fgsfds
				}
			}
			else{
				Object o = null;
				if(tag == "Empty"){
					o = Instantiate (EmptyPrefab);
				}
				else if( tag == "RemotePlayer"){
					o = Instantiate (RemotePlayerPrefab);
				}
				else if( tag == "IA"){
					o = Instantiate (IAPrefab);
				}
				else if( tag == "LocalPlayer"){
					o = Instantiate (LocalPlayerPrefab);
				}
				else{
					print("ERROR: " + tag + ", " + nome);
				}
				swapPanels ((GameObject)o,g);
			}
		}
	}
	
	public static void Callback(string s){
		//print(s);
		//Atualizar slots de acordo com mensagem
	}

	public void swapPanels(GameObject novoObj, GameObject oldObj){
			novoObj.transform.position = oldObj.transform.position;
			RectTransform antigo, novo;
			novo = novoObj.GetComponent<RectTransform> ();
			antigo = oldObj.GetComponent<RectTransform> ();
			novo.sizeDelta = new Vector2(antigo.rect.width,antigo.rect.height);
			novoObj.transform.parent = oldObj.transform.parent.transform;
			Destroy (oldObj);
	}
}