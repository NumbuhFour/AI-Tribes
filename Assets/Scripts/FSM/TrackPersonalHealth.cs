using UnityEngine;
using System.Collections;
using UnityEditor;

[AddComponentMenu("Scripts/FSM/Track Personal Health")]
public class TrackPersonalHealth : MonoBehaviour {
	
	private PropertyTracker props;
	private Animator anim;
	// Use this for initialization
	public void Start () {
		anim = this.GetComponent<Animator>();
		props = this.GetComponent<PropertyTracker>();
	}
	
	public void Update(){
		anim.SetFloat("Health", props.Health/props.MaxHealth);
	}
}
