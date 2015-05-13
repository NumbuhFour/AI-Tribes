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
		foreach(GameObject obj in entSel.Selected) 
		{
			if (obj.GetComponent<Human>() != null)
				obj.GetComponent<Human>().UpdateDecision();
		}
		close ();
	}
	//Tell them to go home immediately
	public void OnHome(){
		foreach (GameObject obj in entSel.Selected){
			if (obj.GetComponent<Human>() != null)
				obj.GetComponent<Human>().state = Human.States.Returning;
		}
		close ();
	}
	//Tell them to follow the player
	public void OnFollow(){
		foreach (GameObject obj in entSel.Selected){
			if (obj.GetComponent<Human>() != null){
				obj.GetComponent<Human>().targetObject = GameObject.FindWithTag("Player");
				obj.GetComponent<Human>().state = Human.States.Seeking;
			}
		}
		close ();
	}

	public void close(){
		this.GetComponent<Animator>().SetBool("open",false);
	}
}
