using UnityEngine;
using System.Collections;

/// <summary>
/// Movement. Handles all movment of a character.
/// Seeking, fleeing, separation, alignment,
/// cohesion, and any others.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

	[Range(0,3)]
	public float speedMult = 1.0f;

	public float forwardSpeed;
	public float turnSpeed;
	public float sidestepSpeed; //Maybe?

	
	public AnimationCurve rayDistCurve = new AnimationCurve(new Keyframe(0,1), new Keyframe(90,0));
	public AnimationCurve rayWeightCurve = new AnimationCurve(new Keyframe(0,1), new Keyframe(90,0)); //How much each ray causes turning
	//Probably will need seeking distances (when to slow, when to stop, how slow to get)
	//public AnimationCurve seekSlowdown = new AnimationCurve(new Keyframe(0,1), new Keyframe(1,0)); //Slowdown(y) curve by distance(x)

	private Rigidbody rb;
	private NavMeshAgent agent;
	private Transform target;

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
		Seek(trans.position);
	}
	public void Seek(Vector3 pos){
		pos = new Vector3(pos.x, this.transform.position.y, pos.z);
		this.SwitchToSteering();
		float angle = GetAngleTo(pos);
		float direction = GetDirectionTo(pos);
		if(angle > 5f){
			float speed = Mathf.Min(angle/50f, 1);
			Turn (direction,speed);
		}
		
		if(angle < 90){
			float dist = (this.transform.position - pos).magnitude;
			if(dist > Mathf.Max (Mathf.Min (this.GetComponent<Rigidbody>().velocity.magnitude/2,20),2) ) {
				float speed = Mathf.Min (1-(angle/150f),1);
				GoForward (speed);
			}else{
				Stop ();
			}
		}

		bool frontHit;
		float vel = this.GetComponent<Rigidbody>().velocity.magnitude;
		float obstruct = ThrowRays(32, rayDistCurve, rayWeightCurve, vel *2, out frontHit);
		if(frontHit)
			Turn (1, turnSpeed/vel*1.6f); //TODO scale turn speed by velocity
		else Turn (Mathf.Sign(obstruct), Mathf.Abs (obstruct)*turnSpeed/vel*0.8f); //TODO scale turn speed by velocity

	}

	/// <summary>
	/// Switches to steering and flees target until 
	/// action changes
	/// </summary>
	/// <param name="trans">Transform to flee</param>
	public void Flee(Transform trans){
		Flee(trans.position);
	}
	public void Flee(Vector3 pos){
		pos = new Vector3(pos.x, this.transform.position.y, pos.z);
		this.SwitchToSteering();
		float angle = GetAngleTo(pos);
		float direction = GetDirectionTo(pos);
		if(Mathf.Abs(angle) < 170f){
			float speed = Mathf.Min(angle/170f, 1); //Slowing as we get closer to face target
			Turn (-direction,speed);
		}
		
		if(angle > 90){
			//float dist = (this.transform.position - pos).magnitude;
			//if(dist < 60) { Gotta keep running!
				float invAngle = 180-angle;
				float speed = Mathf.Min (1-(invAngle/150f),1);
				GoForward (speed);
			/*}else{
				Stop ();
			}*/
		}

		bool frontHit;
		float vel = this.GetComponent<Rigidbody>().velocity.magnitude;
		float obstruct = ThrowRays(32, rayDistCurve, rayWeightCurve, vel *2, out frontHit);
		if(frontHit)
			Turn (1, turnSpeed/vel*1.6f); //TODO scale turn speed by velocity
		else Turn (Mathf.Sign(obstruct), Mathf.Abs (obstruct)*turnSpeed/vel*0.8f); //TODO scale turn speed by velocity

	}

	/// <summary>
	/// Stops fleeing, seeking, or pathing to
	/// </summary>
	public void Stop(){
		this.SwitchToSteering();
		//this.rigidbody.AddForce(this.rigidbody.velocity.normalized*-1.5f); //Needs jitter prevention
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.98f;
	}


	public void GoForward(float mult=1.0f){
		this.SwitchToSteering();
		this.GetComponent<Rigidbody>().AddForce(this.transform.forward*this.forwardSpeed*mult*speedMult);
	}

	public void Turn(float direction, float mult=1.0f){
		this.SwitchToSteering();
		if (float.IsNaN(mult))
			mult = 1.0f;
		float t = Mathf.Sign (direction) * this.turnSpeed * mult * speedMult;
		if (!float.IsNaN(t))
			this.GetComponent<Rigidbody>().AddTorque (0, t, 0); //Needs jitter prevention
		else
			Debug.Log("Mathf.Sign (direction): " + Mathf.Sign(direction) + "this.turnSpeed: " + turnSpeed + "\nmult: " + mult + "\nspeedMult");
	}

	public void SwitchToSteering(){
		rb.isKinematic = false;
		agent.enabled = false;
	}

	public void SwitchToPathing(){
		agent.enabled = true;
		rb.isKinematic = true;
	}
	
	public void PathTo(Vector3 target){
		SwitchToPathing();
		this.agent.SetDestination(new Vector3(target.x, this.transform.position.y, target.z));
	}
	
	private float GetAngleTo(Vector3 destination){
		Vector3 target = new Vector3(destination.x,this.transform.position.y,destination.z) - this.transform.position;
		Vector3 forward = this.transform.forward;
		Debug.DrawRay(this.transform.position, target,Color.blue);
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
	private float ThrowRays(int count, AnimationCurve distCurve, AnimationCurve weightCurve, float maxDist, out bool frontHit){
		Vector3 pos = this.transform.position;
		Vector3 up = this.transform.up;
		frontHit = false;
		float rtn = 0;
		count /= 2;
		float leftObs = 0; //Prevents rays from adding up
		float rightObs = 0;
		for(int i = 0; i < count; i++){
			float angle = i*(90/count);
			Vector3 ray = this.transform.forward;
			ray = Quaternion.AngleAxis(angle,up)*ray;

			if(i ==0){ //Front ray
				frontHit = Physics.Raycast(pos,ray,Mathf.Max(0.001f,distCurve.Evaluate(angle)*maxDist));
				Debug.DrawRay(pos, ray*distCurve.Evaluate(angle)*maxDist,frontHit ? Color.red:Color.green);
			}else { //Mirror
				bool hit = Physics.Raycast(pos,ray,Mathf.Max(0.001f,distCurve.Evaluate(angle)*maxDist)); //CW
				Debug.DrawRay(pos, ray*distCurve.Evaluate(angle)*maxDist,hit ? Color.red:Color.green);
				if(hit) rightObs = Mathf.Max (weightCurve.Evaluate(angle), rightObs);

				ray = this.transform.forward; //CCW
				ray = Quaternion.AngleAxis(-angle,up)*ray;
				
				hit = Physics.Raycast(pos,ray,Mathf.Max(0.001f,distCurve.Evaluate(angle)*maxDist));
				Debug.DrawRay(pos, ray*distCurve.Evaluate(angle)*maxDist,hit ? Color.red:Color.green);
				if(hit) leftObs = Mathf.Max (weightCurve.Evaluate(angle), leftObs);
			}
		}
		rtn = leftObs-rightObs;
		return rtn;
	}
}
