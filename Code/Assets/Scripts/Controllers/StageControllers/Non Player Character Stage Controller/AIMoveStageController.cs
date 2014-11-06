using UnityEngine;
using System.Collections.Generic;

public class AIMoveStageController : StageController {


	private List<Territory> checados;
	//protected Dictionary<Territory,int> territorioNovasTropas;
	
	public override void OnStageStart(){
		Territory territorio1;
		Territory territorio2;
		int numeroMovimentosTotal = 5;
		int numeroMovimentosCorrente = 0;
		int menorQtdTropas;
		int maiorQtdTropas;
		checados = new List<Territory>();
		//territorioNovasTropas = new Dictionary<Territory, int>();
		while (numeroMovimentosCorrente < numeroMovimentosTotal && PodeMover(checados)) {
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
				ComputeShot(new MoveShot(this.Player,territorio1,territorio2,1));
				numeroMovimentosCorrente++;
				checados.Add(territorio1);
				checados.Add(territorio2);
			}
			else{
				checados.Add(territorio1);
			}		
		}
	
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
	
}
