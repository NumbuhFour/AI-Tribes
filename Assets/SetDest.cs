using UnityEngine;
using System.Collections;

public class SetDest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
