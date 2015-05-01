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
			human.initRole();
			human.Gather = Gather;
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
		movement.Seek(target.transform);
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

	//stays in place until time is up, returns to village
	protected int Gather(GameObject targetObject){
		if (!FoodTags.Contains(targetObject.tag))
			human.UpdateDecision();
		else{
			if (targetObject.GetComponent<Animal>().health > 0){
				human.Fight(targetObject.GetComponent<Animal>());
			}
			else{
				human.food += Time.deltaTime; //milliseconds
				if(human.food > human.foodLimit){
					targetObject.SendMessage("Eat");
					human.hasFood = true;
					human.UpdateDecision ();
					//state = States.Returning;
					
				}
			}
		}
		return 0;
	}

}
