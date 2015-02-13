using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Movement))]
public class MovementDebug : Editor {
	
	
	public void OnSceneGUI(){
		if (!Application.isPlaying) return;
		Movement player = (Movement)this.target;
		Handles.color = Color.blue;
		Handles.Label(player.transform.position + Vector3.right + Vector3.up*1.3f, player.DebugData());
		//Handles.Label (player.transform.position + Vector3.right + Vector3.up*1.3f, player.JumpData());
		//Handles.drawL
		
		/*if (Application.isPlaying) { //Camera follow
			Vector3 position = SceneView.lastActiveSceneView.pivot;
			position = new Vector3 (player.transform.position.x, player.transform.position.y, position.z);
			SceneView.lastActiveSceneView.pivot = position;
			SceneView.lastActiveSceneView.Repaint();
		}*/
	}
}
