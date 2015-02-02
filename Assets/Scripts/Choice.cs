using UnityEngine;
using System.Collections;

public class Choice : MonoBehaviour {

	protected NavMeshAgent agent;

	protected bool isDone;
	public bool IsDone { get { return isDone; } }
	
	// Use this for initialization
	public virtual void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
