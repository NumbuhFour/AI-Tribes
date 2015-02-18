using UnityEngine;
using System.Collections;

public class GathererAI : TribesmanAI {


	public float sightDistance = 20f;

	public enum States {Searching,	//Searching for bushes
						Seeking, 	//Seeking out a bush
						Gathering,	//At bush, gathering berries
						Returning,	//Returning to tribe to drop off food 
						Fleeing }	//Fleeing creature

	private States state = States.Searching;
	
	private Vector3 target;
	private bool hasTarget = false;
	private int taskTime = 0;
	
	


	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case States.Searching: Wander(); break;
		case States.Seeking: SeekBush(); break;
		case States.Gathering: Gathering(); break;
		case States.Returning: Returning(); break;
		}
		
		this.gameObject.SendMessage("SetDebugMessage", "State: " + state);
	}

	/// <summary>
	/// Seaches for a bush. Wanders in a direction for rand seconds until either 
	/// bush within range or random turn
	/// </summary>
	private GameObject SearchForBush(){
		GameObject[] bushes = GameObject.FindGameObjectsWithTag("Bush");
		Vector3 pos = this.transform.position;
		foreach(GameObject b in bushes){
			if(IsWithinDistance(b.transform.position, sightDistance)){
				return b;
			}
		}
		return null;
	}
	
	private void SeekBush(){
		movement.Seek(target);
		if(IsWithinDistance(target, 10f)){
			state = States.Gathering;
			taskTime = 0;
			hasTarget = false;
		}
	}
	
	private void Gathering(){
		taskTime += (int)(Time.deltaTime*1000); //milliseconds
		if(taskTime > 6000){
			state = States.Returning;
			hasTarget = false;
			taskTime = 0;
		}
	}
	
	private void Returning(){
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
	
	private void Wander(){
		if(taskTime <= 0){ //Need to make a new wander target
			if(GetDistanceToVillage() > 200){
				state = States.Returning;
				hasTarget = false;
				taskTime = 0;
			}else {
				taskTime = Random.Range(3000,8000);
				Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
				target = this.transform.position + targetDir;
				hasTarget = true;
			}
		} else{ //Wandering to a spot in a direction
			movement.Seek(target);
			GameObject bush = SearchForBush();
			if(bush) {
				state = States.Seeking;
				target = bush.transform.position;
				taskTime = 0;
				hasTarget = true;
				return;
			}
		}
		int deltaTime = (int)(Time.deltaTime*1000); //milliseconds
		taskTime -= deltaTime;
		
	}
	
	private float GetDistanceToVillage(){
		return (this.transform.position - GameObject.FindGameObjectWithTag("Village").transform.position).magnitude;
	}
	
	private bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}

}
