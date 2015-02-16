﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Movement. Handles all movment of a character.
/// Seeking, fleeing, separation, alignment,
/// cohesion, and any others.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	public Transform TestDestination;

	[Range(0,3)]
	public float speedMult = 1.0f;

	public float forwardSpeed;
	public float turnSpeed;
	public float sidestepSpeed; //Maybe?


	public AnimationCurve rayCurve = new AnimationCurve(new Keyframe(0,1), new Keyframe(90,0));
	//Probably will need seeking distances (when to slow, when to stop, how slow to get)
	//public AnimationCurve seekSlowdown = new AnimationCurve(new Keyframe(0,1), new Keyframe(1,0)); //Slowdown(y) curve by distance(x)

	private Rigidbody rb;
	private NavMeshAgent agent;
	private Transform target;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		agent = this.GetComponent<NavMeshAgent>();
		this.target = TestDestination;
	}
	
	// Update is called once per frame
	void Update () {
	
		Seek(target);
	}

	/// <summary>
	/// Switches to steering and seeks target until 
	/// action changes or target reached
	/// </summary>
	/// <param name="trans">Transform to seek</param>
	public void Seek(Transform trans){
		this.SwitchToSteering();
		float angle = GetAngleTo(trans.position);
		float direction = GetDirectionTo(trans.position);
		if(angle > 5f){
			float speed = Mathf.Min(angle/50f, 1);
			Turn (direction,speed);
		}
		
		if(angle < 90){
			float dist = (this.transform.position - trans.position).magnitude;
			if(dist > 10) {
				float speed = Mathf.Min (1-(angle/150f),1);
				GoForward (speed);
			}else{
				Stop ();
			}
		}

		bool frontHit;
		float obstruct = ThrowRays(10, rayCurve, this.rigidbody.velocity.magnitude*1.8f, out frontHit);
		Turn (-Mathf.Sign(obstruct), obstruct*turnSpeed/5); //TODO scale turn speed by velocity

	}

	/// <summary>
	/// Switches to steering and flees target until 
	/// action changes
	/// </summary>
	/// <param name="trans">Transform to flee</param>
	public void Flee(Transform trans){
		this.SwitchToSteering();
		float angle = GetAngleTo(trans.position);
		float direction = GetDirectionTo(trans.position);
		if(angle < 170f){
			float speed = Mathf.Min(angle/50f, 1);
			Turn (-direction,speed);
		}
		
		if(angle > 90){
			float dist = (this.transform.position - trans.position).magnitude;
			if(dist < 30) {
				float invAngle = 180-angle;
				float speed = Mathf.Min (1-(invAngle/150f),1);
				GoForward (speed);
			}else{
				Stop ();
			}
		}
	}

	/// <summary>
	/// Stops fleeing, seeking, or pathing to
	/// </summary>
	public void Stop(){
		this.SwitchToSteering();
		this.rigidbody.AddForce(this.rigidbody.velocity.normalized*-1); //Needs jitter prevention
	}


	protected void GoForward(float mult=1f){
		this.SwitchToSteering();
		this.rigidbody.AddForce(this.transform.forward*this.forwardSpeed*mult*speedMult);
	}

	protected void Turn(float direction, float mult=1f){
		this.SwitchToSteering();
		this.rigidbody.AddTorque(0,Mathf.Sign(direction)*this.turnSpeed*mult*speedMult,0); //Needs jitter prevention
	}

	public void SwitchToSteering(){
		rb.isKinematic = false;
		agent.enabled = false;
	}

	public void SwitchToPathing(){
		agent.enabled = true;
		rb.isKinematic = true;
	}
	
	private float GetAngleTo(Vector3 destination){
		Vector3 target = new Vector3(destination.x,this.transform.position.y,destination.z) - this.transform.position;
		Vector3 forward = this.transform.forward;
		Debug.DrawRay(this.transform.position, target*100,Color.blue);
		Debug.DrawRay(this.transform.position, forward*100, Color.yellow);
		return Vector3.Angle(forward,target);
	} 
	private float GetDirectionTo(Vector3 destination){
		Vector3 target = new Vector3(destination.x,this.transform.position.y,destination.z) - this.transform.position;
		Vector3 forward = this.transform.forward;
		return Mathf.Sign (Vector3.Cross(forward,target).y);
		
	} 

	/// <summary>
	/// Throws rays to test for collision. Returns float based on 
	/// which rays hit at what angle. CW = positive, CCW = negative.
	/// Further rays from forward vec equate to less return. 
	/// Average is returned.
	/// </summary>
	/// <returns>Weighted hit angle sorta</returns>
	/// <param name="count">Amount of rays to throw</param>
	/// <param name="curve">Ray distance curve per angle</param>
	/// <param name="maxDist">Farthest ray</param>
	/// <param name="frontHit">Returns true if ray straight ahead hits something</param>
	private float ThrowRays(int count, AnimationCurve curve, float maxDist, out bool frontHit){
		Vector3 pos = this.transform.position;
		Vector3 up = this.transform.up;
		frontHit = false;
		float rtn = 0;
		for(int i = 0; i < count; i++){
			float angle = i*(90/count);
			Vector3 ray = this.transform.forward;
			ray = Quaternion.AngleAxis(angle,up)*ray;

			if(i ==0){ //Front ray
				frontHit = Physics.Raycast(pos,ray,curve.Evaluate(angle)*maxDist);
				Debug.DrawRay(pos, ray*curve.Evaluate(angle)*maxDist,frontHit ? Color.red:Color.green);
			}else { //Mirror
				bool hit = Physics.Raycast(pos,ray,curve.Evaluate(angle)*maxDist); //CW
				Debug.DrawRay(pos, ray*curve.Evaluate(angle)*maxDist,hit ? Color.red:Color.green);
				if(hit) rtn += curve.Evaluate(angle);

				ray = this.transform.forward; //CCW
				ray = Quaternion.AngleAxis(-angle,up)*ray;
				hit = Physics.Raycast(pos,ray,curve.Evaluate(angle)*maxDist);
				Debug.DrawRay(pos, ray*curve.Evaluate(angle)*maxDist,hit ? Color.red:Color.green);
				if(hit) rtn -= curve.Evaluate(angle);
			}
		}
		return rtn;
	}
	
	public string DebugData(){
		return "Angle: " + GetAngleTo(this.target.position) + 
			"\nDirection" + GetDirectionTo(this.target.position);
	}
}
