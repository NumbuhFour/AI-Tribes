﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gatherer : Role {

	public Human human;

	// Use this for initialization
	void Start () {
		base.Start();
		human = GetComponent<Human>();
		if (human != null){
			human.SeekFood = SeekFood;
			human.Search = Roam;
			human.CheckForFood = CheckForBush;
			human.Gather = Gather;
			human.EvaluateThreat = EvaluateThreat;
			human.initRole();
			human.foodCost = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//checks for all food sources, returns null if nothing found
	private GameObject CheckForBush(){
		List<GameObject> food = new List<GameObject>();
		foreach (string tag in species.FoodTags){
			GameObject[] source = GameObject.FindGameObjectsWithTag(tag);
			foreach (GameObject s in source)
				food.Add(s);
		}
		Vector3 pos = this.transform.position;
		foreach(GameObject b in food){
			if(species.IsInSight(b)){
				return b;
			}
		}
		return null;
	}

	//goes after target, returns false if target out of range
	private int SeekFood(GameObject target){

		if(!species.IsInSight(target.transform.position)){
			return 0;
		}
		movement.Seek(target.transform);
		target.GetComponent<Collider>().isTrigger = true;
		if (species.IsWithinReach(target))
			return 2;
		return 1;
	}

	/// <summary>
	/// Evaluates gameobject, 0 for nothing, 1 for flee, 2 for attack
	/// </summary>
	/// <returns>The threat.</returns>
	/// <param name="obj">Object.</param>
	public int EvaluateThreat(GameObject obj){
		Species spec = obj.GetComponent<Species>();
		if (spec != null && spec is Animal){
			if (spec.FoodTags.Contains("Human"))
				return 1;
		}
		return 0;
	}

	//roams randomly
	public Vector3 Roam(){
		Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
		return transform.position + targetDir;
	}

	//stays in place until time is up, returns to village
	protected int Gather(GameObject targetObject){
		if (!FoodTags.Contains(targetObject.tag))
			human.UpdateDecision();
		else{
			human.food += Time.deltaTime; //milliseconds
			if(human.food > human.foodLimit){
				targetObject.SendMessage("EatBerries");
				human.hasFood = true;
				human.UpdateDecision ();				
			}
		}
		return 0;
	}



}
