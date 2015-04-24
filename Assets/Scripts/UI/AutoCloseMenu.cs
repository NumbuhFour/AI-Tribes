using UnityEngine;
using System.Collections;

public class AutoCloseMenu : MonoBehaviour {

	public float maxDist = 120f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector2 pos = ((RectTransform)this.transform).anchoredPosition;
		float dist = (pos - (Vector2)Input.mousePosition).magnitude;
		if(dist > maxDist){
			this.GetComponent<Animator>().SetBool("open",false);
		}
	}
}
