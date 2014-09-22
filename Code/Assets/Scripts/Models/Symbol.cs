using UnityEngine;
using System.Collections;

public class Symbol{

	public enum TypeSymbol{Square, Circle, Triangle};

	public Symbol(TypeSymbol type){
		this.typeSymbol = type;
	}

	internal TypeSymbol typeSymbol;

	public override bool Equals(System.Object obj){ 
		if(obj == null){
			return false;
		}
		Symbol other = obj as Symbol;
		if((System.Object)other == null){
			return false;
		}
		return this.typeSymbol == other.typeSymbol;
	}

	public static Symbol GetRandom(){
		int randomInt = Random.Range(0,2);
		return new Symbol((TypeSymbol)randomInt);
	}

}
