using UnityEngine;
using System.Collections;

public class TribesmanAI : EntityAI {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	
	// Update is called once per frame
	void Update () {

	}

	protected float GetDistanceToVillage(){
		return (this.transform.position - GameObject.FindGameObjectWithTag("Village").transform.position).magnitude;
	}

	protected void Gather(){
		taskTime += (int)(Time.deltaTime*1000); //milliseconds
		if(taskTime > 6000){
			state = States.Returning;
			hasTarget = false;
			taskTime = 0;
		}
	}

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
}
