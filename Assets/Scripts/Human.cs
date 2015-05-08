using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Human : Species {

	public DecisionTree decTree;

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

	// Use this for initialization
	public override void Start () {
		base.Start();
		if (decTree == null)
			decTree = new DecisionTree(Application.dataPath + "/" + "humanDT.txt");
		decTree.Predators = CheckForPredators;
		decTree.Return = CheckReturn;
		if (CheckForFood != null){
			decTree.Food = CheckForFood;
		}

	}

	public override void initRole(){
		if (decTree == null)
			decTree = new DecisionTree(Application.dataPath + "/" + "humanDT.txt");
		if (CheckForFood != null){
			decTree.Food = CheckForFood;
		}
	}

	public void UpdateDecision(){
		targetObject = null;
		string result = decTree.Run();
		switch (result){
			case "Seek": 
				targetObject = CheckForFood();
				state = States.Seeking;
				break;
			case "Flee":
				state = States.Fleeing;
				break;
			case "Search":
				state = States.Searching;
				break;
			case "Return":
				state = States.Returning;
				break;
		}
		taskTime = 0;
		//Debug.Log(gameObject.name + ": " + state);
	}

	//returns false if predators in the area
	public GameObject CheckForPredators(){

		GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
		GameObject[] predators = GameObject.FindGameObjectsWithTag("Predator");
		List<GameObject> threats = new List<GameObject>();
		foreach(GameObject a in animals){
			foreach(GameObject b in predators)
			if (b.transform.parent == a.transform.parent)
				threats.Add(a);
		}

		foreach(GameObject b in threats){
			if(IsInSight(b.transform.parent.gameObject)){
				return b.transform.parent.gameObject;
			}
		}
		return null;
	}

	public GameObject CheckReturn(){
		if (HasFood() > 0 || GetDistanceToVillage() > GameObject.FindWithTag("Village").GetComponent<Village>().range){
			return this.gameObject;
		}
		return null;
		
	}
	
	// Update is called once per frame
	public override void Update () {

		base.Update();
		switch(state){
			case States.Searching: {
				GameObject check = CheckForFood();
				if (check != null && IsInSight(check)){
					UpdateDecision();
				}
				else {
					target = Wander();	
					movement.Seek (target);
				}
				break;
			}
			case States.Seeking: {
				if (targetObject == null)
					targetObject = CheckForFood();
				int result = SeekFood(targetObject);
				if (result == 2){
					if (FoodTags.Contains(targetObject.tag))
						state = States.Gathering;
					else
						UpdateDecision();
				}
				else if (result == 0)
					UpdateDecision();
				else{
					if (FoodTags.Contains(targetObject.tag))
						targetObject.GetComponent<Collider>().isTrigger = true;
					movement.Seek(targetObject.transform);
				}
				
				break;
			}
			case States.Gathering: Gather(targetObject); break;
			case States.Returning: Return(); break;
		}
		
		this.gameObject.SendMessage("SetDebugMessage", "State: " + state);
	}

	protected float GetDistanceToVillage(){
		return (this.transform.position - GameObject.FindGameObjectWithTag("Village").transform.position).magnitude;
	}
	//moves back to village
	protected void Return(){
		if(!hasTarget){
			Vector3 targetPos = GameObject.FindGameObjectWithTag("Village").transform.position;
			targetPos.x += UnityEngine.Random.Range(-20,20);
			targetPos.z += UnityEngine.Random.Range(-20,20);
			target = targetPos;
			hasTarget = true;
		}
		
		movement.Seek(target);
		if(IsWithinReach(target)){
			hasFood = false;
			hasTarget = false;
			taskTime = 0;
			UpdateDecision();
		}
	}

	//follows path until target found
	public Vector3 Wander(){
		taskTime += (int)(Time.deltaTime * 1000);
		/*if(targetObject == null || (transform.position - targetObject.transform.position).sqrMagnitude < 15){ //Need to make a new wander target
			if (targetObject != null && targetObject.GetComponent<PathNode> () != null)
				targetObject = targetObject.GetComponent<PathNode>().Last.gameObject;
			else
				targetObject = GameObject.FindWithTag ("Path").GetComponent<Path>().getNearestNode(transform.position).gameObject;
			return targetObject.transform.position;
		}
		return targetObject.transform.position;*/
		if (target == null || (target - transform.position).sqrMagnitude < reachDistance * reachDistance){
			float dx = Mathf.Sin (transform.rotation.eulerAngles.y) * UnityEngine.Random.Range(5, 100);
			float dz = Mathf.Cos (transform.rotation.eulerAngles.y) * UnityEngine.Random.Range(5, 100);
			Vector3 wanderTarget = transform.position + new Vector3(dx, 0, dz);
			return wanderTarget;
		}
		return target;
		
	}

	public int HasFood(){
		float food = Convert.ToSingle(prop["food"]);
		float foodLimit = Convert.ToSingle(prop["foodLimit"]);
		return food > foodLimit ? 1 : 0;
	}
}
