using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hunter : Role {

	public Human human;

	// Use this for initialization
	public override void Start () {
		base.Start();
		human = GetComponent<Human>();
		if (human != null){
			human.SeekFood = SeekPrey;
			human.Search = Roam;
			human.CheckForFood = CheckForPrey;
			human.Gather = Gather;
			human.EvaluateThreat = EvaluateThreat;
			human.foodCost = 3;
			human.initRole();
		}
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

	//seeks target, returns false if out of range
	public int SeekPrey(GameObject target){
		
		if(!species.IsInSight(target)){
			return 0;
		}
		movement.PathTo(target.transform.position);
		if (species.IsWithinReach(target))
			return 2;
		return 1;
	}

	//roams at random
	public Vector3 Roam(){
		Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
		return transform.position + targetDir;
	}


	//checks all prey tags, returns null if nothing in range
	public GameObject CheckForPrey(){
		List<GameObject> prey = new List<GameObject>();
		foreach (string tag in species.FoodTags){
			GameObject[] source = GameObject.FindGameObjectsWithTag(tag);
			foreach (GameObject s in source)
				prey.Add(s.transform.parent.gameObject);
		}
		Vector3 pos = this.transform.position;
		foreach(GameObject p in prey){
			if(species.IsInSight(p.transform.position)){
				return p;
			}
		}
		return null;
	}

	/// <summary>
	/// Evaluates gameobject, 0 for nothing, 1 for flee, 2 for attack
	/// </summary>
	/// <returns>The threat.</returns>
	/// <param name="obj">Object.</param>
	public int EvaluateThreat(GameObject obj){
		Species spec = obj.GetComponent<Species>();
		if (spec != null && spec is Animal){
			return 2;
		}
		return 0;
	}


	//stays in place until time is up, returns to village
	protected int Gather(GameObject targetObject){
		if (!human.IsWithinReach(targetObject)){
			human.targetObject = null;
			human.UpdateDecision();
			return 0;
		}
		if (targetObject.tag == "AnimalMeat"){
			prop["food"] = (float)prop["food"] + Mathf.Max((int)targetObject.GetComponent<PropertyTracker>()["size"], (float)prop["foodLimit"] - (float)prop["food"]); //milliseconds
			human.hasFood = true;
			human.targetObject = null;
			human.UpdateDecision ();
			return 0;
		}
		bool attacked = false;
		movement.PathTo(targetObject.transform.position);
		foreach (Transform t in targetObject.transform) {
			if (FoodTags.Contains(t.tag)){
				human.Attack(targetObject.GetComponent<Animal>());
				attacked = true;
				break;
			}
		}
		if (!attacked)
			human.UpdateDecision();
	

		return 0;
	}

}
