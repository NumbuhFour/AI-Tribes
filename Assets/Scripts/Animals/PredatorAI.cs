using UnityEngine;
using System.Collections;

public class PredatorAI : AnimalAI {

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
		}

		this.gameObject.SendMessage("SetDebugMessage", "State: " + state + "\nTarget: " + targetObject);
	}

	/// <summary>
	/// Seaches for a bush. Wanders in a direction for rand seconds until either 
	/// bush within range or random turn
	/// </summary>
	private GameObject SearchForPrey(){
		GameObject[] prey = GameObject.FindGameObjectsWithTag("Human");
		Vector3 pos = this.transform.position;
		foreach(GameObject p in prey){
			if(IsWithinDistance(p.transform.parent.position, sightDistance)){
				return p.transform.parent.gameObject;
			}
		}
		return null;
	}

	private void SeekPrey(){
		movement.Seek(targetObject.transform);
		if(!IsWithinDistance(target, sightDistance)){
			state = States.Searching;
		}/*else {
			target = targetObject.transform.position;
		}*/
		target = targetObject.transform.position;
	}

	private void Wander(){
		if(targetObject == null || (transform.position - target).sqrMagnitude < 10){ //Need to make a new wander target
			/*taskTime = Random.Range(3000,8000);
			Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
			target = this.transform.position + targetDir;
			hasTarget = true;*/
			if (targetObject != null && targetObject.GetComponent<PathNode> () != null)
				targetObject = targetObject.GetComponent<PathNode>().Last.gameObject;
			else
				targetObject = GameObject.FindWithTag ("Path").GetComponent<Path>().getNearestNode(transform.position).gameObject;
			target = targetObject.transform.position;
		} else{ //Wandering to a spot in a direction
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
		taskTime -= deltaTime;
		
	}
		
}
