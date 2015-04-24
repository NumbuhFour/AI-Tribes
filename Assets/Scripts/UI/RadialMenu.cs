using UnityEngine;
using System.Collections;

public class RadialMenu : MonoBehaviour {

	public EntitySelection entSel;

	// Use this for initialization
	void Start () {
		close ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Cancel whatever they are doing and go idle
	//If they are following the player or cancelled, go to work
	public void OnCancel(){
		//foreach(GameObject go in entSel.Selection) { /*Perform cancel*/ }
		close ();
	}
	//Tell them to go home immediately
	public void OnHome(){
		close ();
	}
	//Tell them to follow the player
	public void OnFollow(){
		close ();
	}

	public void close(){
		this.GetComponent<Animator>().SetBool("open",false);
	}
}
