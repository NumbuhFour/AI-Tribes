﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class EntityAI : MonoBehaviour {

	protected Movement movement;
	protected Choice currentChoice;

	public float sightDistance;

	protected Vector3 target;
	public GameObject targetObject;
	protected bool hasTarget = false;
	protected int taskTime = 0;

	public enum States {
		Searching,	//Searching for stuff
		Seeking, 	//Seeking object
		Gathering,	//At object, gathering food
		Returning,	//Returning to home area
		Fleeing,	//Fleeing creature 
		Killing		//Fighting something
	}	

	protected States state;
	
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
		state = States.Searching;
	}

	public virtual void Update() {
		if(CurrentChoice.IsDone){
			DestroyChoice ();
			OnChoiceEnd ();
		}
	}

	public virtual void OnChoiceEnd(){
		
	}

	protected bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}

	void OnDrawGizmos() {
		string file = "uglyassicon.png";
		switch(this.state){
		case States.Searching:
			file = "yellow_icon.png";
			break;
		case States.Seeking:
			file = "turquise_icon.png";
			break;
		case States.Gathering:
			file = "green_icon.png";
			break;
		case States.Returning:
			file = "magenta_icon.png";
			break;
		case States.Fleeing:
			file = "white_icon.png";
			break;
		case States.Killing:
			file = "red_icon.png";
			break;
		}
		Gizmos.DrawIcon(transform.position + Vector3.up*5, file, false);
	}
}
