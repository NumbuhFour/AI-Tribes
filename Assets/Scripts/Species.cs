using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class Species : MonoBehaviour {

	public List<string> FoodTags;

	protected Movement movement;

	public delegate bool Action(GameObject target);
	public Action SeekFood;

	public delegate Vector3 MoveAction();
	public MoveAction Search;

	public delegate GameObject TargetAction();
	public TargetAction CheckForFood;

	public float sightDistance;
	
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
	
	void OnDrawGizmos() {
		Debug.Log("RAWRAWRA");
		Gizmos.DrawIcon(transform.position + Vector3.up*10, "uglyassicon.png", true);
	}
}
