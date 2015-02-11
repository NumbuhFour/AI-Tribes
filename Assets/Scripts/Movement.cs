using UnityEngine;
using System.Collections;

/// <summary>
/// Movement. Handles all movment of a character.
/// Seeking, fleeing, separation, alignment,
/// cohesion, and any others.
/// </summary>
public class Movement : MonoBehaviour {

	[Range(0,3)]
	public float speedMult = 1.0f;

	public float forwardSpeed;
	public float turnSpeed;
	public float sidestepSpeed; //Maybe?

	//Probably will need seeking distances (when to slow, when to stop, how slow to get)
	//public AnimationCurve seekSlowdown = new AnimationCurve(new Keyframe(0,1), new Keyframe(1,0)); //Slowdown(y) curve by distance(x)

	private Rigidbody rb;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Switches to steering and seeks target until 
	/// action changes or target reached
	/// </summary>
	/// <param name="trans">Transform to seek</param>
	public void Seek(Transform trans){

	}

	/// <summary>
	/// Switches to steering and flees target until 
	/// action changes
	/// </summary>
	/// <param name="trans">Transform to flee</param>
	public void Flee(Transform trans){

	}

	/// <summary>
	/// Stops fleeing, seeking, or pathing to
	/// </summary>
	public void Stop(){

	}


	protected void GoForward(){

	}

	protected void Turn(){

	}

	public void SwitchToSteering(){
		rb.isKinematic = false;
		agent.enabled = false;
	}

	public void SwitchToPathing(){
		agent.enabled = true;
		rb.isKinematic = true;
	}
}
