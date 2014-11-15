using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int freeTroops;
	public Territory[] territories;
	public Continent[] continents;

	public Continent[] ContinentsByNames(params string[] names){
		List<string> namesList = new List<string>();
		foreach(string name in names){namesList.Add(name.ToLower());}
		List<Continent> contList = new List<Continent>();
		foreach(Continent c in this.continents){
			if(namesList.Exists(s => c.name.ToLower().Contains(s)))contList.Add(c);
		}
		return contList.ToArray();
	}

}
