using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(Path))]
public class EditorPath : Editor {
	
	
	public void OnSceneGUI () {
		Transform debug = ((Path)this.target).transform;
		for(int i = 0; i < debug.childCount; i++){
			PathNode child = debug.GetChild(i).GetComponent<PathNode>();
			child.FindNextAndLast();
			Vector3 diff = child.Next.transform.position - child.transform.position;
			Debug.DrawRay(child.transform.position, diff, Color.yellow,0f, false);

			Handles.color = Color.blue;
			Handles.Label(child.transform.position + new Vector3(0,0,2.3f), "["+i+"]");
		}
	}
}
