using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {
	
	private PathNode last;
	private PathNode next;
	
	public PathNode Next { get { return next; } }
	public PathNode Last { get { return last; } }

	public Vector3 Position{
		get { return this.transform.position; }
	}

	// Use this for initialization
	void Start () {
	
	}

	public void FindNextAndLast(){
		Transform parent = this.transform.parent;
		int max = parent.childCount;
		int siblingIndex = this.transform.GetSiblingIndex();
		next = parent.GetChild((siblingIndex + 1 >= max) ? 0:(siblingIndex+1)).GetComponent<PathNode>();
		last = parent.GetChild((siblingIndex - 1 < 0) ? (max-1):(siblingIndex-1)).GetComponent<PathNode>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
