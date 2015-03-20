using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Seek Target")]
	public class SeekTarget : FSMBehaviour {
	
		private Species species;
		private Movement move;
		private GameObject target;
		// Use this for initialization
		protected override void OnBegin () {
			species = this.GetComponent<Species>();
			move = this.GetComponent<Movement>();
			target = species.targetObject;
		}
		
		protected override void OnUpdate () {
			if(target) move.PathTo(target.transform.position);
		}
	}

}