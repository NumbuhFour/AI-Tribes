using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		string result = decTree.Run();
		switch (result){
			case "Seek": 
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

		GameObject[] predators = GameObject.FindGameObjectsWithTag("Animal");
		Vector3 pos = this.transform.position;
		foreach(GameObject b in predators){
			if(IsInSight(b.transform.parent.gameObject)){
				return b.transform.parent.gameObject;
			}
		}
		return null;
	}

	public GameObject CheckReturn(){
		if (GetDistanceToVillage() > 200){
			return this.gameObject;
		}
		if (hasFood)
			return this.gameObject;
		return null;
		
	}
	
	// Update is called once per frame
	public override void Update () {


		switch(state){
			case States.Searching: {
				if (taskTime > 1000)
					UpdateDecision();
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
				if (result == 2)
					state = States.Gathering;
				else if (result == 0)
					UpdateDecision();
				else
					movement.Seek(targetObject.transform);
				
				break;
			}
			case States.Gathering: Gather(); break;
			case States.Returning: Return(); break;
		}
		
		this.gameObject.SendMessage("SetDebugMessage", "State: " + state);
	}

	protected float GetDistanceToVillage(){
		return (this.transform.position - GameObject.FindGameObjectWithTag("Village").transform.position).magnitude;
	}

	//stays in place until time is up, returns to village
	protected void Gather(){
		taskTime += (int)(Time.deltaTime*1000); //milliseconds
		if(taskTime > 6000){
			hasFood = true;
			hasTarget = false;
			taskTime = 0;
			UpdateDecision ();
			//state = States.Returning;

		}
	}


	//moves back to village
	protected void Return(){
		if(!hasTarget){
			Vector3 targetPos = GameObject.FindGameObjectWithTag("Village").transform.position;
			targetPos.x += Random.Range(-20,20);
			targetPos.z += Random.Range(-20,20);
			target = targetPos;
			hasTarget = true;
		}
		
		movement.Seek(target);
		if(IsWithinDistance(target, 10f)){
			hasFood = false;
			//state = States.Searching;
			hasTarget = false;
			taskTime = 0;
			UpdateDecision();
		}
	}

	//follows path until target found
	public Vector3 Wander(){
		taskTime += (int)(Time.deltaTime * 1000);
		if(targetObject == null || (transform.position - target).sqrMagnitude < 10){ //Need to make a new wander target
			if (targetObject != null && targetObject.GetComponent<PathNode> () != null)
				targetObject = targetObject.GetComponent<PathNode>().Last.gameObject;
			else
				targetObject = GameObject.FindWithTag ("Path").GetComponent<Path>().getNearestNode(transform.position).gameObject;
			return targetObject.transform.position;
		}
		return targetObject.transform.position;
		
	}
}
