using UnityEngine;
using System.Collections;

public class Player : Tribesman {

	private Movement movement;
	private Vector3 target;
	private bool hasTarget = false;
	private bool isPathing = false;
	// Use this for initialization
	public override void Start () {
		base.Start();
		this.movement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		int horiz = (int)Input.GetAxisRaw("Horizontal");
		int vert = (int)Input.GetAxisRaw("Vertical");
		
		if(vert != 0 || horiz != 0){
			hasTarget = false;
			isPathing = false;
		}
		if(isPathing){}
		
		else if(hasTarget){
			movement.Seek (target);
		}else{
			this.movement.GoForward(vert);
			if(horiz != 0)
				this.movement.Turn(horiz);
		}
		
		if(Input.GetMouseButtonDown(0)){
			target = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			hasTarget = true;
			isPathing = false;
		}else if(Input.GetMouseButtonDown(1)){
			Vector3 go = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			movement.PathTo(go);
			hasTarget = false;
			isPathing = true;
		}
	}
}
