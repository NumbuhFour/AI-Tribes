using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SpreadPieces))]
public class EditorSpreadPieces : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		if(GUILayout.Button("Scatter Pieces")){
			((SpreadPieces)target).Scatter();
		}
		if(GUILayout.Button("Clear Pieces")){
			((SpreadPieces)target).Clear();
		}
	}
}
