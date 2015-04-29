using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Flee Target")]
	public class FleeTarget : FSMBehaviour {

		private Species species;
		private Movement move;
		// Use this for initialization
		protected override void OnBegin () {
			species = this.GetComponent<Species>();
			move = this.GetComponent<Movement>();
		}
		
		protected override void OnUpdate () {
			move.Flee(species.targetObject.transform);
		}
	}
	
}