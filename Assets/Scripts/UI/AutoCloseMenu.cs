using UnityEngine;
using System.Collections;

public class AutoCloseMenu : MonoBehaviour {

	float maxDist = 120f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = ((RectTransform)this.transform).anchoredPosition;
		float dist = (pos - (Vector2)Input.mousePosition).magnitude;
		if(dist > maxDist){
			//Close
		}
	}
}
