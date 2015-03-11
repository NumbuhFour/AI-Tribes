using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpreadPieces : MonoBehaviour {

	public GameObject prefab;
	public Transform center;
	
	public float minRange = 50f;
	public float maxRange = 500f;
	public float frequency = 0.2f;

	public void Scatter(){
		Clear ();
		float surface = 2 * this.maxRange * 3.14f;
		int count = (int)(surface * frequency);
		
		for(; count > 0; count --){
			MakePiece();
		}
	}

	private void MakePiece(){
		GameObject make = (GameObject)PrefabUtility.InstantiatePrefab (prefab);
		make.transform.SetParent(this.transform);
		Vector3 pos = Quaternion.AngleAxis(Random.Range(0,360), Vector3.up) * Vector3.forward;
		pos *= Random.Range(minRange, maxRange);
		pos += center.position;
		
		make.transform.position = pos;
	}

	public void Clear(){
		int count = this.transform.childCount;
		for(; count > 0; count --){
			DestroyImmediate(this.transform.GetChild(0).gameObject);
		}
	}
}
