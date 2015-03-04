﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hunter : Role {

	// Use this for initialization
	public override void Start () {
		base.Start();
		if (species != null){
			species.SeekFood = SeekPrey;
			species.Search = Roam;
			species.CheckForFood = CheckForPrey;
		}
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

	//seeks target, returns false if out of range
	public bool SeekPrey(GameObject target){
		
		if(!species.IsWithinDistance(target.transform.position, species.sightDistance)){
			return false;
		}
		movement.Seek(target.transform);
		return true;
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
			if(species.IsWithinDistance(p.transform.position, species.sightDistance)){
				return p;
			}
		}
		return null;
	}

}
