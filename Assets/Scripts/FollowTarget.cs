using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPos = target.position;
		newPos.y = this.transform.position.y;
		this.transform.position = Vector3.Lerp(this.transform.position, newPos,0.5f);
	}
}
