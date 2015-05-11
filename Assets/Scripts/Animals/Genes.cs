using UnityEngine;
using System.Collections;

public class Genes : MonoBehaviour {


	//Sight distance
	//Eating speed
	//Max health
	//forward speed
	//Turn speed
	public float sightDistance = 30;
	public float eatingSpeed = 5000;
	public int maxHealth = 100;
	public float forwardSpeed = 50;
	public float turnSpeed = 40;
	
	public float mutationRange = 0.1f;
	
	// Use this for initialization
	void Start () {
		Setup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Push properties to appropriate trackers
	private void Setup(){
		Species spec = this.GetComponent<Species>();
		spec.sightDistance = this.sightDistance;
		
		Movement move = this.GetComponent<Movement>();
		move.forwardSpeed = this.forwardSpeed;
		move.turnSpeed = this.turnSpeed;
		
		NavMeshAgent nma = this.GetComponent<NavMeshAgent>();
		nma.speed = this.forwardSpeed;
		nma.angularSpeed = this.turnSpeed;
		
		PropertyTracker prop = this.GetComponent<PropertyTracker>();
		prop.MaxHealth = this.maxHealth;
		prop.Health = this.maxHealth;
		
		prop["eatingSpeed"] = this.eatingSpeed;
	}
	
	public void MutateTo(Genes otherParent, Genes child){
		child.sightDistance = Random.Range(0,100) > 50 ? this.sightDistance : otherParent.sightDistance;
		child.eatingSpeed = Random.Range(0,100) > 50 ? this.eatingSpeed : otherParent.eatingSpeed;
		child.maxHealth = Random.Range(0,100) > 50 ? this.maxHealth : otherParent.maxHealth;
		child.forwardSpeed = Random.Range(0,100) > 50 ? this.forwardSpeed : otherParent.forwardSpeed;
		child.turnSpeed = Random.Range(0,100) > 50 ? this.turnSpeed : otherParent.turnSpeed;
		
		int mutate = Random.Range(0,5);
		switch(mutate){
		case 0: child.sightDistance = this.mutateVar(child.sightDistance); break;
		case 1: child.eatingSpeed = mutateVar(child.eatingSpeed); break;
		case 2: child.maxHealth = (int)mutateVar(child.maxHealth); break;
		case 3: child.forwardSpeed = mutateVar(child.forwardSpeed); break;
		case 4: child.turnSpeed = mutateVar(child.turnSpeed); break;
		}
		child.Setup();
	}
	
	private float mutateVar(float num){
		float percent = Random.Range(1-mutationRange, 1+mutationRange);
		return num * percent;
	}
}
