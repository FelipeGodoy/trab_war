using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AIMoveStageController : StageController {


	private List<Territory> checados;
	protected Dictionary<Territory,int> territoriosTropas;
	protected Territory territorio1;
	protected Territory territorio2;
	protected int numeroMovimentosTotal;
	protected int numeroMovimentosCorrente;
	protected int menorQtdTropas;
	protected int maiorQtdTropas;


	public override void OnGUI(){
		GUI.Label(new Rect(200,30,150,20),"Movendo");
	}
	
	public override void OnStageStart(){

		territorio1 = null;
		territorio2 = null;
		numeroMovimentosTotal = 5;
		numeroMovimentosCorrente = 0;
		checados = new List<Territory> ();

		gui.left.setActive (false);
		gui.right.setActive (false);
		}

		public override void Update(){
			if (numeroMovimentosCorrente < numeroMovimentosTotal && PodeMover(checados)) {
				maiorQtdTropas = 1;
				foreach(Territory territory in this.Player.Territories){
					if(checados.Contains(territory)){}
					
					else if(territory.TroopsCount >= maiorQtdTropas){
						maiorQtdTropas = territory.TroopsCount;
						territorio1 = territory;
					}
				}
				menorQtdTropas = 200;
				foreach(Territory territory in territorio1.neighbors){
					if(this.Player.HaveTerritory(territory) && territory.TroopsCount <= menorQtdTropas){
						menorQtdTropas = territory.TroopsCount;
						territorio2 = territory;
					}
				}
				if(menorQtdTropas < 200){
					gui.left.setActive (true);
					gui.left.setTexts (territorio1.CurrentPlayer.name, territorio1.gameObject.name, "" + territorio1.TroopsCount);
					gui.left.changeColor (territorio1.CurrentPlayer.troopMaterial.color);

					gui.right.setActive (true);
					gui.right.setTexts (territorio2.CurrentPlayer.name, territorio2.gameObject.name, "" + territorio2.TroopsCount);
					gui.right.changeColor (territorio2.CurrentPlayer.troopMaterial.color);

					ComputeShot(new MoveShot(this.Player,territorio1,territorio2,1));
					numeroMovimentosCorrente++;
					checados.Add(territorio1);
					checados.Add(territorio2);
					
				}
				else{
					checados.Add(territorio1);
				}		
			}
			EndStage ();
			
		}

	public bool PodeMover(List<Territory> checados){
		bool resp = false;
		foreach(Territory territory in this.Player.Territories){
			if(checados.Contains(territory)){}
			else{
				foreach(Territory territory2 in territory.neighbors){
					if(!this.Player.HaveTerritory(territory2)){}
					else{
						if(checados.Contains(territory2)){}
						else{
							if(territory.TroopsCount == 1 && territory2.TroopsCount == 1){}
							else{
								resp = true;
							}
						}
					}
					
				}
				
			}
		}
		return resp;
	}

	public override void OnStageEnd ()
	{
		gui.left.setActive (false);
		gui.right.setActive (false);
	}
	
}
