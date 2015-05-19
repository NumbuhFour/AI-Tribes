using UnityEngine;
using System.Collections;
using System;

public class PropertyBar : MonoBehaviour {

	PropertyTracker prop;

	//[System.Serializable]
	public string property;
	
	//[System.Serializable]
	public string maxProperty;

	// Use this for initialization
	void Start () {
		prop = GetComponentInParent<PropertyTracker>();
	}
	
	// Update is called once per frame
	void Update () {
		float value = Convert.ToSingle(prop[property]);
		float maxValue = Convert.ToSingle(prop[maxProperty]);

		if (!float.IsNaN(value) && !float.IsNaN(maxValue) && maxValue != 0){
			Vector3 scale = this.transform.localScale;
			scale.x = value/maxValue;
			this.transform.localScale = scale;
		}

	}
}
