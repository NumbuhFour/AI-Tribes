using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Find Tag by Distance")]
	public class FindTagDistance : FSMBehaviour {
	
		public string targetTag;
		public string targetParameter = "TargetFound";
		public float distance;
		
		private Animator anim;
		// Use this for initialization
		protected override void OnBegin () {
			anim = this.GetComponent<Animator>();
		}
		
		protected override void OnUpdate () {
			float minDist = Mathf.Infinity;
			GameObject find = null;
			GameObject[] matches = GameObject.FindGameObjectsWithTag(targetTag);
			Vector3 cen = this.transform.position;
			foreach(GameObject go in matches){
				if(!go.activeSelf) continue; //Destroyed
				
				Vector3 pos = go.transform.position;
				pos -= cen;
				float mags = pos.sqrMagnitude;
				if(mags < minDist){
					minDist = mags;
					find = go;
				}
			}
			
			if(find != null && minDist <= distance*distance){
				anim.SetBool(targetParameter,true);
				GetComponent<Species>().targetObject = find;
			}
		}
	}
}