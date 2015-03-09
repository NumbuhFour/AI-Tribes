using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Human : Species {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {


		switch(state){
			case States.Searching: {
				if(GetDistanceToVillage() > 200){
					state = States.Returning;
					hasTarget = false;
					taskTime = 0;
				}	
				else {
					GameObject obj = CheckForFood();
					if(obj != null) {
						Debug.Log (obj);
						state = States.Seeking;
						target = obj.transform.position;
						targetObject = obj;
						taskTime = 0;
						hasTarget = true;
					}
					else
						target = Wander(); //in future, decide between human wander and component roam
					movement.Seek(target);
				}
				break;
			}
			case States.Seeking: {
				int result = SeekFood(targetObject);
				if (result == 0)
					state = States.Searching;
				else if (result == 2)
					state = States.Gathering;
				
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
			state = States.Returning;
			hasTarget = false;
			taskTime = 0;
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
			state = States.Searching;
			hasTarget = false;
			taskTime = 0;
		}
	}

	//follows path until target found
	public Vector3 Wander(){
		if(targetObject == null || (transform.position - target).sqrMagnitude < 10){ //Need to make a new wander target
			if (targetObject != null && targetObject.GetComponent<PathNode> () != null)
				targetObject = targetObject.GetComponent<PathNode>().Last.gameObject;
			else
				targetObject = GameObject.FindWithTag ("Path").GetComponent<Path>().getNearestNode(transform.position).gameObject;
			return targetObject.transform.position;
		}
		return targetObject.transform.position;/*else{ //Wandering to a spot in a direction
			movement.Seek(target);
			GameObject prey = SearchForPrey();
			if(prey) {
				state = States.Seeking;
				targetObject = prey;
				target = prey.transform.position;
				taskTime = 0;
				hasTarget = true;
				return;
			}
		}
		int deltaTime = (int)(Time.deltaTime*1000); //milliseconds
		taskTime -= deltaTime;*/
		
	}
}
