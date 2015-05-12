using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour {

	public float elapsedTime;

	// Use this for initialization
	void Start () {
		elapsedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > 10){
			Object.Destroy(this.gameObject);
		}
	}
}
