using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class FollowPath : MonoBehaviour {

	public Path path;
	private int curInd = 0;
	private PathNode curNode;
	private Movement move;
	// Use this for initialization
	void Start () {
		move = this.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
