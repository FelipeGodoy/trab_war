using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIFacade : MonoBehaviour {
	public StatesHandler states;
	public PlayerInfo left;
	public PlayerInfo right;
	public InfoHandler info;
	public Button passar;

	public void setAlocar(int qtd){
		states.setAlocar (qtd);
	}
	public void setAtacar(){
		states.setAtacar ();
	}
	public void setMover(){
		states.setMover ();
	}

	public void setInfo(string territoryName, string qtd, Color c){
		info.setActive (true);
		info.changeInfo (territoryName, qtd, c);
	}

	public void hideInfo(){
		info.setActive (false);	
	}

	public void setRightPlayer(string Name, string Territory, string Qtd){
		right.setTexts (Name, Territory, Qtd);
	}

	public void setLeftPlayer(string Name, string Territory, string Qtd){
		left.setTexts (Name, Territory, Qtd);
	}

	public void setLeftPlayerActive(bool active){
		left.setActive (active);
	}

	public void showPassar(){
		//if (GameObject.FindObjectOfType<GameController>().CurrentPlayer.type == Player.PlayerType.PLAYER_CHARACTER) {
						Behaviour[] b = passar.GetComponentsInChildren<Behaviour> ();
						foreach (Behaviour h in b) {
								h.enabled = true;
						}
		/*		} else {
						hidePassar ();
				}*/
	}

	public void hidePassar(){
		Behaviour[] b = passar.GetComponentsInChildren<Behaviour>();
		foreach(Behaviour h in b){
			h.enabled = false;
		}
	}
}
