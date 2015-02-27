using UnityEngine;
using System.Collections;

public class HunterAI : TribesmanAI {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case States.Searching: Wander(); break;
		case States.Seeking: SeekPrey(); break;
		case States.Gathering: Gather(); break;
		case States.Returning: Return(); break;
		}
		
		this.gameObject.SendMessage("SetDebugMessage", "State: " + state + "\nTarget: " + targetObject);
	}

	/// <summary>
	/// Seaches for a bush. Wanders in a direction for rand seconds until either 
	/// bush within range or random turn
	/// </summary>
	private GameObject SearchForPrey(){
		GameObject[] prey = GameObject.FindGameObjectsWithTag("Animal");
		Vector3 pos = this.transform.position;
		foreach(GameObject p in prey){
			if(IsWithinDistance(p.transform.parent.position, sightDistance)){
				return p.transform.parent.gameObject;
			}
		}
		return null;
	}

	private void SeekPrey(){

		if(!IsWithinDistance(target, sightDistance)){
			state = States.Searching;
			targetObject = null;
		}
		movement.Seek(target);
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
			GameObject prey = SearchForPrey();
			if(prey != null) {
				Debug.Log (prey);
				state = States.Seeking;
				target = prey.transform.position;
				targetObject = prey;
				taskTime = 0;
				hasTarget = true;
				return;
			}
		}
		int deltaTime = (int)(Time.deltaTime*1000); //milliseconds
		taskTime -= deltaTime;
		
	}
	
	private bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}
}
