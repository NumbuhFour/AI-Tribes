using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Role : MonoBehaviour {

	public List<string> FoodTags;

	public Species species;

	protected Movement movement;

	// Use this for initialization
	public virtual void Start () {
		movement = GetComponentInParent<Movement>();
		species = GetComponentInParent<Species>();
		if (species != null){
			if (FoodTags != null){
				foreach (string tag in FoodTags)
					species.AddFoodTag(tag);
			}
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	protected bool IsWithinDistance(Vector3 pos, float dist){
		return (this.transform.position - pos).sqrMagnitude <= dist*dist;
	}
}
