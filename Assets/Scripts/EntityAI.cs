using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class EntityAI : MonoBehaviour {

	protected Movement movement;
	private Choice currentChoice;
	
	public Choice CurrentChoice { get { return currentChoice; } }
	protected Choice SetChoice<T>() where T : Choice{
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
		this.movement = GetComponent<Movement>();
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
