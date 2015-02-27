using UnityEngine;
using System.Collections;

public class AnimalAI : EntityAI {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void Gather(){
		taskTime += (int)(Time.deltaTime*1000); //milliseconds
		if(taskTime > 6000){
			state = States.Searching;
			hasTarget = false;
			taskTime = 0;
		}
	}
}
