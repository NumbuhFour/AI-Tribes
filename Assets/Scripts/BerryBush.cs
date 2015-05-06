using UnityEngine;
using System.Collections;

public class BerryBush : MonoBehaviour {
	
	private float regrow = 0;
	private bool eaten = false;
	
	private Renderer berry;
	
	public void Start(){
		berry = this.transform.GetChild(0).GetComponent<Renderer>();
	}
	
	public void update(){
		if(eaten) {
			regrow -= Time.deltaTime;
		}
		
		if(regrow <= 0){
			eaten = false;
			berry.enabled = true;
			this.tag = "Bush";
		}
	}
	
	public void EatBerries(){
		GetComponent<Collider>().isTrigger = false;
		this.tag = "Untagged";
		eaten = true;
		berry.enabled = false;
		regrow = Random.Range(20,40);
	}
}
