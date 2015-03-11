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
		//case States.Searching: Wander(); break;
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


		
}
