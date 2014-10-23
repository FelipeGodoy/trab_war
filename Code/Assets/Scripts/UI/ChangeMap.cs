using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeMap : MonoBehaviour {
	public Sprite pic;
	public Image aim;


	public void changeMap(){
		print (aim.sprite.name);
		aim.sprite = pic;
		print (aim.sprite.name);
	}
}
