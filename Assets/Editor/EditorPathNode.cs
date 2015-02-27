using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(PathNode))]
public class EditorPathNode : Editor {
	
	
	public void OnSceneGUI () {
		PathNode debug = (PathNode)this.target;
		debug.FindNextAndLast();
		Vector3 diff = debug.Next.transform.position - debug.transform.position;
		Debug.DrawRay(debug.transform.position, diff, Color.yellow, 0f, false);

		Vector3 diffLast = debug.transform.position - debug.Last.transform.position;
		Debug.DrawRay(debug.Last.transform.position, diffLast, Color.blue, 0f, false);
	}
}
