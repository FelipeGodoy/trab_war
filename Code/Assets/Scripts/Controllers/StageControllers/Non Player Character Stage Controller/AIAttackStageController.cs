using UnityEngine;
using System.Collections;

public class AIAttackStageController : StageController {

	protected int numeroAtaquesTotal;
	protected int numeroAtaquesCorrente;
	protected int maiorQtdTropas;
	protected int menorQtdTropas;
	protected Territory territorioAtacante;
	protected Territory territorioAlvo;
	
	public override void OnStageStart(){
		numeroAtaquesTotal = 5;
		numeroAtaquesCorrente = 0;
		while(numeroAtaquesCorrente < numeroAtaquesTotal && this.Player.Territories.Count <= 3){
			maiorQtdTropas = 1;
			foreach(Territory territory in this.Player.Territories){
				if(territory.TroopsCount >= maiorQtdTropas){
					maiorQtdTropas = territory.TroopsCount;
					territorioAtacante = territory;

				}
			}
			if(maiorQtdTropas == 1){
				EndStage ();
			}
			menorQtdTropas = 200;
			foreach(Territory territory in territorioAtacante.neighbors){
				if(territory.TroopsCount <= menorQtdTropas && !this.Player.HaveTerritory(territory)){
					menorQtdTropas = territory.TroopsCount;
					territorioAlvo = territory;
				}
			}

			if(maiorQtdTropas > 1){
				ComputeShot(new AttackShot(this.Player,territorioAtacante,territorioAlvo,DiceResult));
				numeroAtaquesCorrente++;
			}

		}
		EndStage();
	
}
	private void DiceResult(bool conquested){
		Dice.Instance.ClearDices();
	}
}