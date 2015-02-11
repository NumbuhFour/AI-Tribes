using UnityEngine;
using System.Collections;

public class EntityAI : MonoBehaviour {

	protected NavMeshAgent agent;
	private Choice currentChoice;
	
	public Choice CurrentChoice { get { return currentChoice; } }
	protected T SetChoice<T>() where T : Choice{
		if(currentChoice) Destroy(currentChoice);
		currentChoice = GetComponent<T>();
		return currentChoice;
	}
	protected void DestroyChoice() {
		if(currentChoice) Destroy(currentChoice);
		currentChoice = null;
	}

	// Use this for initialization
	public virtual void Start () {
		this.agent = GetComponent<NavMeshAgent>();
	}

	public virtual void Update() {
		if(CurrentChoice.IsDone){
			DestroyChoice ();
			OnChoiceEnd ();
		}
	}

	public virtual void OnChoiceEnd(){

	}
}
