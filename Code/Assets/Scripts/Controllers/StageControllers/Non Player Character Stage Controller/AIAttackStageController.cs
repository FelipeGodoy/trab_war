using UnityEngine;
using System.Collections;

public class AIAttackStageController : StageController {

	protected int numeroAtaquesTotal;
	protected int numeroAtaquesCorrente;
	protected int maiorQtdTropas;
	protected int menorQtdTropas;
	protected Territory territorioAtacante;
	protected Territory territorioAlvo;
	protected bool atacando;
	
	public override void OnStageStart(){
		numeroAtaquesTotal = 5;
		numeroAtaquesCorrente = 0;
		atacando = false;
		gui.setAtacar ();
		gui.left.setActive (false);
		gui.right.setActive (false);
	}

	public override void OnGUI(){
		//GUI.Label(new Rect(200,30,150,20),"Atacando");
	}

	public override void Update(){
		if(numeroAtaquesCorrente < numeroAtaquesTotal && this.Player.Territories.Count >= 3){
			maiorQtdTropas = 1;
			foreach(Territory territory in this.Player.Territories){
				if(territory.TroopsCount >= maiorQtdTropas && territory.HaveNeighborEnemy()){
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
			
			if(maiorQtdTropas > 1 && !atacando){
				atacando = true;
				ComputeShot(new AttackShot(this.Player,territorioAtacante,territorioAlvo,DiceResult));
				numeroAtaquesCorrente++;
			}
			
		}
		else{
			EndStage();
		}
	}

	private void DiceResult(bool conquested){
		atacando = false;
		gui.left.setActive (false);
		gui.right.setActive (false);
	}
}