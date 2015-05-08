using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
	[SerializeField]
	private States _state;
	public States state {
		get { return _state; }
		set { _state = value; }
	}

	public List<string> FoodTags;
	public PropertyTracker prop;

	protected Movement movement;

	public float foodLimit;
	public int foodCost;
	public float strength;
	public float size;
	public float attackSpeed;
	public float health;
	public float attackTimer;
	public bool hasFood = false;
	public float food = 0;

	public delegate bool Test();
	public delegate void PerformAction();
	public delegate int Action(GameObject target);
	public Action SeekFood;
	public Action Gather;

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
		target = transform.position;
		prop = GetComponent<PropertyTracker>();
		prop["food"] = 0;
		prop["foodLimit"] = foodLimit;
		prop["strength"] = strength;
		prop["size"] = size;
		prop["attackSpeed"] = attackSpeed;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		if (prop.Health <= 0)
			Die ();
	}

	public virtual void initRole(){
	}

	public void Attack(Species other){
		if (attackTimer <= 0){
			other.prop.Health -= (int)Convert.ToSingle(prop["strength"]);
			attackTimer = attackSpeed;
		}
	}

	public virtual void Die(){
		GetComponent<Species>().enabled = false;
		GetComponent<Role>().enabled = false;
		GetComponent<Movement>().enabled = false;
		foreach(Transform obj in transform)
			GameObject.Destroy(obj.gameObject);
		tag = this is Human ? "HumanMeat" : "AnimalMeat";
	}

	public void AddFoodTag(string tag){
		FoodTags.Add(tag);
		//Debug.Log (FoodTags);
	}

	public bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}

	public bool IsInSight(GameObject obj){
		return (this.transform.position - obj.transform.position).sqrMagnitude <= sightDistance*sightDistance;
	}

	public bool IsInSight(Vector3 pos){
		return (this.transform.position - pos).sqrMagnitude <= sightDistance*sightDistance;
	}

	public bool IsWithinReach(GameObject obj){
		return IsWithinReach(obj.transform.position);
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
