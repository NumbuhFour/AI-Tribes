using UnityEngine;
using System.Collections;

public class EntityAI : MonoBehaviour {

	protected NavMeshAgent agent;
	private Choice currentChoice;
	
	public Choice CurrentChoice { get { return currentChoice; } }
	protected void SetChoice<T>() where T extends Choice{
		if(currentChoice) Destroy(currentChoice);
		currentChoice = GetComponent<T>()
	}
	protected void DestroyChoice() {
		if(currentChoice) Destroy(currentChoice);
		currentChoice = null;
	}

	// Use this for initialization
	public virtual void Start () {
		this.agent = GetComponent<NavMeshAgent>()
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
