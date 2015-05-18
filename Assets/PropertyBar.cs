using UnityEngine;
using System.Collections;
using System;

public class PropertyBar : MonoBehaviour {

	PropertyTracker prop;
	GUITexture texture;

	//[System.Serializable]
	public string property;
	
	//[System.Serializable]
	public string maxProperty;

	// Use this for initialization
	void Start () {
		texture = GetComponent<GUITexture>();
		prop = GetComponentInParent<PropertyTracker>();
	}
	
	// Update is called once per frame
	void Update () {
		float value = Convert.ToSingle(prop[property]);
		float maxValue = Convert.ToSingle(prop[maxProperty]);

		if (!float.IsNaN(value) && !float.IsNaN(maxValue) && maxValue != 0){
			Vector3 textureScale = texture.transform.localScale;
			textureScale.x = value / maxValue * 10;
		
			texture.transform.localScale = textureScale;
			texture.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
		}

	}
}
