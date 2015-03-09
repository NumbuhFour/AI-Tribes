using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class Species : MonoBehaviour {
	
	//will become decision tree
	public enum States {
		Searching,	//Searching for stuff
		Seeking, 	//Seeking object
		Gathering,	//At object, gathering food
		Returning,	//Returning to home area
		Fleeing,	//Fleeing creature 
		Killing		//Fighting something
	}	
	public States state;

	public List<string> FoodTags;

	protected Movement movement;

	public delegate int Action(GameObject target);
	public Action SeekFood;

	public delegate Vector3 MoveAction();
	public MoveAction Search;

	public delegate GameObject TargetAction();
	public TargetAction CheckForFood;

	public float sightDistance;
	public float reachDistance;
	
	protected Vector3 target;
	public GameObject targetObject;
	protected bool hasTarget = false;
	protected int taskTime = 0;

	// Use this for initialization
	public virtual void Start () {
		movement = GetComponentInParent<Movement>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public void AddFoodTag(string tag){
		FoodTags.Add(tag);
		Debug.Log (FoodTags);
	}

	public bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}

	public bool IsInSight(Vector3 pos){
		return (this.transform.position - pos).sqrMagnitude <= sightDistance*sightDistance;
	}

	public bool IsWithinReach(Vector3 pos){
		return (this.transform.position - pos).sqrMagnitude <= reachDistance*reachDistance;
	}
	
	void OnDrawGizmos() {
		string icon = "uglyassicon.png";
		switch(this.state){
		case States.Searching: icon = "blue_icon.png";
			break;
		case States.Seeking: icon = "green_icon.png";
			break;
		case States.Gathering: icon = "turquise_icon.png";
			break;
		case States.Returning: icon = "magenta_icon.png";
			break;
		case States.Fleeing: icon = "yellow_icon.png";
			break;
		case States.Killing: icon = "red_icon.png";
			break;
		}
		Gizmos.DrawIcon(transform.position + Vector3.up*2, icon, false);
	}
}
