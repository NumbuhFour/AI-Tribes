using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animal : Species {

	// Use this for initialization
	public override void Start () {
		base.Start();
		strength = 2;
	}
	
	// Update is called once per frame
	public override void Update () {
		/*switch(state){
			case States.Searching: {
				GameObject obj = CheckForFood();
				if(obj != null) {
					//Debug.Log (obj);
					state = States.Seeking;
					target = obj.transform.position;
					targetObject = obj;
					taskTime = 0;
					hasTarget = true;
				}
				else
					target = Wander(); 
				movement.Seek(target);
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
		}
		
		this.gameObject.SendMessage("SetDebugMessage", "State: " + state);*/
	}

	//gather food
	protected void Gather(){
		/*taskTime += (int)(Time.deltaTime*1000); //milliseconds
		if(taskTime > 6000){
			state = States.Searching;
			hasTarget = false;
			taskTime = 0;
		}*/
	}

	//wanders at random, will eventually wander territory
	public Vector3 Wander(){
		Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
		return transform.position + targetDir;
	}


}
