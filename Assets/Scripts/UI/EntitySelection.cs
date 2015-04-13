using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EntitySelection : MonoBehaviour {
	
	private Vector3 rectStart;
	private Vector3 rectEnd;
	private Vector3 mouseStart;
	private bool mouseDown = false;
	public float distance = 80;

	public Transform uiRect;

	public List<GameObject> VisibleGameObjects { 
		get { 
			List<GameObject> rtn = new List<GameObject>();
			Vector3 pos = this.transform.position;
			pos.y = 0;
			Collider[] hits = Physics.OverlapSphere(this.transform.position, distance);
			foreach(Collider c in hits){
				rtn.Add(c.gameObject);
			}
			return rtn;
		} 
	}

	// Use this for initialization
	void Start () {
		uiRect.GetComponent<Image>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool mouse = Input.GetMouseButton(0);
		if(mouse && !mouseDown){
			GoMouseDown();
		}else if(!mouse && mouseDown){
			GoMouseUp();
		}

		if(mouseDown){
			DrawRect(mouseStart, Input.mousePosition);
			Vector3 mousePos = Input.mousePosition;
			rectEnd = Camera.main.ScreenToWorldPoint(mousePos);
			rectEnd.y = -30;
		}
	}

	void GoMouseDown(){
		mouseStart = Input.mousePosition;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = distance;
		rectStart = Camera.main.ScreenToWorldPoint(mousePos);
		rectStart.y = 30;
		mouseDown = true;
		DoRect(rectStart, rectEnd);
	}

	void GoMouseUp(){
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = distance;
		rectEnd = Camera.main.ScreenToWorldPoint(mousePos);
		rectEnd.y = -30;
		mouseDown = false;

		foreach(GameObject go in this.VisibleGameObjects){
			bool isHuman = false;
			for(int i = 0; i < go.transform.childCount && !isHuman; i++){
				if(go.transform.GetChild(i).tag == "Human") isHuman = true;
			}
			if(!isHuman) continue;

			Vector3 pos = go.transform.position;
			if(IsPointSelected(pos,rectStart,rectEnd)){
				Light l = go.AddComponent<Light>();
				l.intensity = 8;
				l.range = 25;
				Destroy(go);
			}
		}
		uiRect.GetComponent<Image>().enabled = false;
	}

	bool IsPointSelected(Vector3 position, Vector3 rectStart, Vector3 rectEnd){
		Vector2 min = new Vector2(Mathf.Min (rectStart.x,rectEnd.x), Mathf.Min (rectStart.z, rectEnd.z));
		Vector2 max = new Vector2(Mathf.Max (rectStart.x,rectEnd.x), Mathf.Max (rectStart.z, rectEnd.z));

		return 	position.x >= min.x && position.x < max.x &&
				position.z >= min.y && position.z < max.y;
		/*
		Rect r = new Rect(Mathf.Min (rectStart.x,rectEnd.x), Mathf.Min (rectStart.z, rectEnd.z),
		                  Mathf.Abs (rectStart.x - rectEnd.x), Mathf.Abs(rectStart.z - rectEnd.z));
		Vector3 check = position;
		check.y = 0;
		return r.Contains(check);*/
	}

	void DrawRect(Vector3 mouseStart, Vector3 mouseEnd){
		uiRect.GetComponent<Image>().enabled = true;
		((RectTransform)uiRect).anchoredPosition = mouseStart;
		((RectTransform)uiRect).localScale = mouseEnd - mouseStart;
	}

	void DoRect(Vector3 rectStart, Vector3 rectEnd){
		uiRect.GetComponent<Image>().enabled = false;

		Rect selectionBox = new Rect(Mathf.Min(rectStart.x, rectEnd.x), 
		                             Mathf.Min(rectStart.y, rectEnd.y),
		                             Mathf.Abs(rectStart.x - rectEnd.x),
		                             Mathf.Abs(rectStart.y - rectEnd.y));

		GameObject[] objs = GameObject.FindGameObjectsWithTag("Entity");

		foreach(GameObject o in objs){
			if(selectionBox.Contains(o.transform.position)){
				o.SetActive(false);
			}
		}

	}

	void OnDrawGizmos(){

		if(Application.isPlaying){
			Gizmos.color = new Color(0,1,0,0.8f);
			Gizmos.DrawCube (rectStart, Vector3.one);

			Gizmos.color = new Color(1,0,0,0.8f);
			Gizmos.DrawCube (rectEnd, Vector3.one);
			
			
			Gizmos.color = new Color(0,0,1,0.8f);
			Vector3 pos = this.transform.position;
			pos.y = 0;
			Gizmos.DrawWireSphere(pos, this.distance);
		}

	}
}
