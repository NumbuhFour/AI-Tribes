using UnityEngine;
using System.Collections;

public class PreventRotation : MonoBehaviour {

	private Quaternion rotation;
	// Use this for initialization
	void Start () {
		rotation = this.transform.rotation;
		Vector3 rot = rotation.eulerAngles;
		rot.y = 0;
		rotation.eulerAngles = rot;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.rotation = this.rotation;
	}
}
