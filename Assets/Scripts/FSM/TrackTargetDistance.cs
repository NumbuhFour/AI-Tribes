using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Track Target Distance")]
	public class TrackTargetDistance : FSMBehaviour {
		
		public string targetParameter = "TargetDistance";
		
		private Species species;
		private Animator anim;
		
		protected override void OnBegin(){
			if(species == null) species = GetComponent<Species>();
			if(anim == null) anim = GetComponent<Animator>();
		}
		
		protected override void OnUpdate(){
			if(species.targetObject != null){
				float dist = (species.targetObject.transform.position - this.transform.position).magnitude;
				anim.SetFloat(targetParameter, dist);
				Debug.Log("CasdasfaUNT");
			}else{
				Debug.Log("CUNT");
			}
		}
		
		protected override void OnEnd(){
			Debug.Log("You're a biiiitch");
		}
	}
}
