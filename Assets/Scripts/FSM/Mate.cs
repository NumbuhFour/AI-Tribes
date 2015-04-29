using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Mate")]
	public class Mate : FSMBehaviour {
		
		public string mateTimeProperty = "MateTimeDelay";
		
		protected override void OnEnd(){
			GameObject target = this.GetComponent<Species>().targetObject;
			if(target != null){

				this.GetComponent<Species>().targetObject = null;
				this.GetComponent<Animator>().SetInteger(mateTimeProperty,100);
			}
		}
	}
}
