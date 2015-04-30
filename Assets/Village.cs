using UnityEngine;
using System.Collections;

public class Village : MonoBehaviour {

	public float food;
	public float childFoodCost;
	public float breedingTime;
	public float elapsedTime;
	public GameObject hunter;
	public GameObject gatherer;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > breedingTime && food > childFoodCost){
			float num = Random.Range(0.0f, 1.0f); 
			GameObject obj = (GameObject)Instantiate(num < 0.5f ? gatherer : hunter, transform.position, Quaternion.identity);
			food -= childFoodCost;
			elapsedTime = 0;
			breedingTime += 10;
			//run bayes evaluation to set variables in new human
		}
	}

	public void OnTriggerEnter(Collider collider){
		Human human = collider.gameObject.GetComponent<Human>();
		if (human != null){
			food += human.food;
			human.food = 0;
			human.UpdateDecision();
		}
	}
}
