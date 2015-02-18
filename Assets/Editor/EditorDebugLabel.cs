using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(DebugLabel))]
public class EditorDebugLabel : Editor {
	
	
	public void OnSceneGUI () {
		DebugLabel debug = (DebugLabel)this.target;
		GameObject go = debug.gameObject;
		Handles.color = Color.blue;
		Handles.Label(go.transform.position + new Vector3(0,0,2.3f), debug.GetMessage());
	}
}
