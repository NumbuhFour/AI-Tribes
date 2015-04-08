using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntitySelection : MonoBehaviour {
	
	private Vector3 rectStart;
	private Vector3 rectEnd;
	private Vector3 mouseStart;
	private bool mouseDown = false;
	public float distance = 80;

	public Transform uiRect;

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
			mousePos.z = distance;
			rectEnd = Camera.main.ScreenToWorldPoint(mousePos);
		}
	}

	void GoMouseDown(){
		mouseStart = Input.mousePosition;
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = distance;
		rectStart = Camera.main.ScreenToWorldPoint(mousePos);
		mouseDown = true;
		DoRect(rectStart, rectEnd);
	}

	void GoMouseUp(){
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = distance;
		rectEnd = Camera.main.ScreenToWorldPoint(mousePos);
		mouseDown = false;
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
		}
	}
}
