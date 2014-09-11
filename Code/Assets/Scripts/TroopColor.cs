using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TroopColor : MonoBehaviour {

	private Color _color;
	public Color color;
	
	// Update is called once per frame
	void Update () {
		if(_color != color){
			SetMeshColors(color);
		}
	}

	private void SetMeshColors(Color c){
		_color = c;
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		Mesh mesh = meshFilter.sharedMesh;
		Color[] colors =  new Color[mesh.vertices.Length];
		for(int i =0; i < colors.Length; i++){
			colors[i] = c;
		}
		mesh.colors = colors;
		meshFilter.mesh = mesh;
	}
}
