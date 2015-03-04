﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gatherer : Role {

	// Use this for initialization
	void Start () {
		base.Start();
		if (species != null){
			species.SeekFood = SeekFood;
			species.Search = Roam;
			species.CheckForFood = CheckForBush;
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
			if(species.IsWithinDistance(b.transform.position, species.sightDistance)){
				return b;
			}
		}
		return null;
	}

	//goes after target, returns false if target out of range
	private bool SeekFood(GameObject target){

		if(!species.IsWithinDistance(target.transform.position, species.sightDistance)){
			return false;
		}
		movement.Seek(target.transform);
		return true;
	}

	//roams randomly
	public Vector3 Roam(){
		Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
		return transform.position + targetDir;
	}




}
