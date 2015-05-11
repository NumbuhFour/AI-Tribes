using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Die")]
	public class Die : FSMBehaviour {
		protected override void OnBegin(){
			Destroy(gameObject);
		}
	}
}
