using UnityEngine;
using System.Collections;

public class GathererAI : TribesmanAI {

	public enum States {Searching,	//Searching for bushes
						Seeking, 	//Seeking out a bush
						Gathering,	//At bush, gathering berries
						Returning,	//Returning to tribe to drop off food 
						Fleeing }	//Fleeing creature

	private States state = States.Searching;

	private GameObject target;
	private int taskTime = 0;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	
	// Update is called once per frame
	void Update () {
		switch(state){

		}
	}

	/// <summary>
	/// Seaches for a bush. Wanders in a direction for rand seconds until either 
	/// bush within range or random turn
	/// </summary>
	void SeachForBush(){
		taskTime += Time.deltaTime();

	}

}
