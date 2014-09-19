using UnityEngine;
using System.Collections;

public class TerritoryInfo : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	private Color _color;
	public Color color{
		get{
			return _color;
		}
		set{
			_color = value;
			spriteRenderer.color = value;
		}
	}

	private Color _animationColor;
	public Color animationColor{
		get{
			return _animationColor;
		}
		set{
			_animationColor = value;
			spriteRenderer.color = value;
		}
	}

	public void SetAnimationColor(Color c){
		animationColor = c;
	}

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		_color = spriteRenderer.color;
		_animationColor = color;
	}
}
