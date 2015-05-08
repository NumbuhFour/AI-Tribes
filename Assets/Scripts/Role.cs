using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Role : MonoBehaviour {

	public List<string> FoodTags;

	public Species species;
	public PropertyTracker prop;
	protected Movement movement;

	// Use this for initialization
	public virtual void Start () {
		movement = GetComponentInParent<Movement>();
		species = GetComponentInParent<Species>();
		prop = GetComponent<PropertyTracker>();
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

	public bool CheckForFood(){
		List<GameObject> food = new List<GameObject>();
		foreach (string tag in species.FoodTags){
			GameObject[] source = GameObject.FindGameObjectsWithTag(tag);
			foreach (GameObject s in source)
				food.Add(s);
		}
		Vector3 pos = this.transform.position;
		foreach(GameObject b in food){
			if(species.IsWithinDistance(b.transform.position, species.sightDistance)){
				return true;
			}
		}
		return false;
	}
}
