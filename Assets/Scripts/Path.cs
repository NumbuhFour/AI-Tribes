using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	private PathNode[] nodes;

	public PathNode this [uint i] {
		get { return nodes[i]; }
	}

	public int IndexOf(PathNode node){
		for(int i = 0; i < nodes.Length; i++) if(nodes[i]==node) return i;
		return -1;
	}

	public PathNode getNearestNode(Vector3 position){
		int n = 0;
		float d = float.MaxValue;
		for (int i = 0; i < nodes.Length; i++) {
			float d1 = Mathf.Abs((nodes[i].transform.position  - position).sqrMagnitude);
			if (d1 < d) {
				d = d1;
				n = i;
			}
		}
		return nodes[n];
	}
	// Use this for initialization
	void Start () {
		nodes = new PathNode[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			PathNode child = transform.GetChild(i).GetComponent<PathNode>();
			child.FindNextAndLast();
			nodes[i] = child;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
